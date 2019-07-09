using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL;
namespace 智联排课系统Web.Controllers.LZJ
{
    public class LzjZiYuanGuanLiController : Controller
    {
        //
        // GET: /LzjZiYuanGuanLi/
        ZiYuanGuanLiBLL bll = new ZiYuanGuanLiBLL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetStudentAjax(string ZiYuanMing, string ZiYuanLeiXing, string ZiYuanWeiZhi, int? pageNum, int? pageSize)
        {
            //条件


            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数

            int tot = 0;//页码总数
            ///////////先按数据获取数据///////////////////////////////////////////

            List<ZiYuanGuanLi> studentList = bll.GetStudentByManyIf2_fxlayui(ZiYuanMing, ZiYuanLeiXing, ZiYuanWeiZhi, pindex, size, out tot);
            ///////////////按框架结构要求响应json格式数据///有数据返回建在data属性中///////////////////
            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {

                    data = new
                    {

                        total = tot,//总页码
                        list = studentList //数据集合
                    }
                }

            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add()
        {
            return View();
        }

        public ActionResult AddAction(ZiYuanGuanLi zy)
        {

            var zym = bll.SelectGetName(zy.ZiYuanMing);
            if (zym == null)
            {
                int n = bll.Add(zy);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "资源名重复,请重新输入" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Update()
        {
            return View();
        }
        public static bool? A;
        public ActionResult UpdateGetOne(int ID)
        {
            var yp = bll.GetOneData(ID);
            var showyp = new { yp.ZiYuanMing, yp.ZiYuanLeiXing, yp.ZiYuanWeiZhi, yp.ZiYuanRongNaRenShu, yp.ShiFouYunHuDuoBanTongPai };
            var showJsondata = new
            {
                errorNo = "0",
                errorInfo = "执行成功",
                results = new { data = showyp }
            };
            return Json(showJsondata, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdataAction(ZiYuanGuanLi zy)
        {
            int n = bll.Modify(zy);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Delete(ZiYuanGuanLi LI)
        {
            int a = bll.SelectGetSk(Convert.ToString(LI.ID));

            if (a == 0)
            {
                int n = bll.Delete(LI);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "当前教室正在上课不能执行此操作" }, JsonRequestBehavior.AllowGet);
            }

        }
        #region 批量删除，代表批量操作的实现方法ajax
        public ActionResult DeleteManyId(string ID)
        {

            int d = bll.DeleteManyByIdS(ID);
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
