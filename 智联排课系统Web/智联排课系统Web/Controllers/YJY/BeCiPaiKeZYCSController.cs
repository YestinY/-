using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class BeCiPaiKeZYCSController : Controller
    {
        //
        // GET: /BeCiPaiKeZYCS/
        public BLL.BeCiPaiKeMorenKeCiBiaoBLL bLL = new BLL.BeCiPaiKeMorenKeCiBiaoBLL();
        public static object a = new object();

        #region 本次排课可用资源组合View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 设置资源可排班级数
        public ActionResult Add(string ID, int ClassCount)
        {
            int i = bLL.Add(ClassCount, ID);
            if (i > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "设置成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "设置失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 显示本次排课可用资源组合数据 + Lock
        public ActionResult List(int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagenum ?? 10;
            int total = bLL.GetAllData().Count();
            var List = bLL.GetAllData().Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            var JSondata = new
            {
                errorNo = "0",
                errorInfo = "配置成功",
                results = new
                {
                    data = new
                    {
                        list = List,
                        total = total
                    }
                }
            };
            //lock (a)
            //{
             
            //}
            return Json(JSondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
