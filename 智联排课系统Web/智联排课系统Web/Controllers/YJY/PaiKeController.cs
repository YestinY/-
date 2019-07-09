using Models;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class PaiKeController : Controller
    {
      
        // GET: /PaiKe/
        private BLL.PaiKeJiHuaBLL bLL = new BLL.PaiKeJiHuaBLL();

        #region 排课计划View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 显式排课计划
        /// <summary>
        /// 显式排课计划
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowPage(string JiHuaMingChen, string KaiShiShiJian, string JieShuShiJian, int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 20;
            var List = bLL.paiKeJiHuaList(JiHuaMingChen, KaiShiShiJian, JieShuShiJian, PageIndex, PageSize, out int Total);
            var Jsondata = new
            {
                errorNo = "0",
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = List
                    }
                }
            };
            return Json(Jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加排课计划View
        public ActionResult AddPage()
        {
            return View();
        }
        #endregion

        #region 添加排课计划Action
        public ActionResult AddAction(PaiKeJiHua paiKeJi)
        {
            int a = bLL.SelectGetWeiZhi(paiKeJi.JiHuaMingChen);
            
            bool error = bLL.AddPaiKeJiHua(paiKeJi);
           
            if (a == 0)
            {
                if (error)
                {
                    return Json(new { errorNo = "0", errorInfo = "操作排课计划成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "操作排课计划失败" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "操作排课计划失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改排课计划View
        public ActionResult Update()
        {
            return View();
        }
        #endregion

        #region 显示修改排课计划数据Action
        public ActionResult UpdateGetOne(int ID)
        {
            var yp = bLL.GetOneData(ID);
            var showyp = new { yp.JiHuaMingChen, ks = yp.KaiShiShiJian.Value.ToString("yyyy-MM-dd"), js = yp.JieShuShiJian.Value.ToString("yyyy-MM-dd"), yp.ShiFouCaiYong, yp.ShiFouWanCheng };
            var showJsondata = new
            {
                errorNo = "0",
                errorInfo = "执行成功",
                results = new { data = showyp }
            };
            return Json(showJsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改Action
        public ActionResult UpdataAction(PaiKeJiHua zy)
        {
            int n = bLL.Modify(zy);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region Delete排课计划Action
        public ActionResult Delete(string JiHuaMingChen)
        {

            int n = bLL.delete(JiHuaMingChen);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行删除成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 多条数据删除
        public ActionResult DeleteManyId(string ID)
        {

            int d = bLL.DeleteManyByIdS(ID);
            if (d > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "删除多条执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "删除执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}
