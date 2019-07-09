using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class ShiDuanController : Controller
    {
        //
        // GET: /ShiDuan/
        public BLL.BenCiPaiKeShiDuanBiaoBLL bLL = new BLL.BenCiPaiKeShiDuanBiaoBLL();

        #region 显示排课时段View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 显示排课时段数据
        public ActionResult Show(int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            int Total;
            List<BenCiPaiKeShiDuanBiao> List = bLL.List(PageIndex, PageSize, out Total);
            var Jsondata = new
            {
                errorNo = "0",
                errorInfo = "搜索成功",
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
