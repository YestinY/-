using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
namespace 智联排课系统Web.Controllers.YJY
{
    public class BeciPaikeKeYongZhiYuanController : Controller
    {
        public BLL.PaiKeJiHuaBLL bLL = new BLL.PaiKeJiHuaBLL();

        public BLL.PaiKeShiDuanYuZiYuanZuHeBLL paiKe = new BLL.PaiKeShiDuanYuZiYuanZuHeBLL();

        public BLL.BenCiPaiBanKeYongZiYuanBLL ben = new BLL.BenCiPaiBanKeYongZiYuanBLL();
        //
        // GET: /BeciPaikeKeYongZhiYuan/

        #region 本次排课可用资源View
        //设置本次排课可用资源
        public ActionResult Index()
        {
            Models.All all = new Models.All();
            var list = bLL.GetAllData().Where(p=>p.ShiFouWanCheng==false).ToList();
            all.PaiKeJih = list;
            return View(all);
        }
        #endregion

        #region 添加排课时段与资源组合Action
        public ActionResult AddAction(int ID)
        {
            bool i = ben.ADDZhiYuan(ID);
            if (i)
            {
                return Json(new { errorNo = "0", errorInfo = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                GetALL(1, 10);
                return Json(new { errorNo = "1", errorInfo = "操作失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 排课时段与资源组合数据
        public ActionResult GetALL(int? pagenum, int? pagesize)
        {
            int PageSize = pagesize ?? 10;
            int PageIndex = pagenum ?? 1;
            int Total;
            var List = paiKe.List(PageIndex, PageSize, out Total);
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

    }
}
