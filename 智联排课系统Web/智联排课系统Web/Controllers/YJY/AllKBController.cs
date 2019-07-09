using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class AllKBController : Controller
    {
        //
        // GET: /AllKB/

        public BLL.KeChengPaiKeZongBiaoBLL bLL = new BLL.KeChengPaiKeZongBiaoBLL();

        public BLL.YuanGongBiaoBLL yuan = new BLL.YuanGongBiaoBLL();

        public ActionResult Index()
        {
            var list = yuan.TeacherBZR();
            ViewBag.List = list;
            return View();
        }

        public ActionResult List(int? pagenum, int? pagesize, string ClassName, DateTime? RiQi, int? JiaoYuanBianHao)
        {
            int ID = JiaoYuanBianHao ?? -1;
            DateTime time = RiQi ?? new DateTime(0001, 1, 1);
            int Pageindex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var list = bLL.List(Pageindex, PageSize, out int Total, ClassName, time, ID);
            var json = new
            {
                errorNo = "0",
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = list
                    }
                }
            };
            return Json(json, JsonRequestBehavior.AllowGet);
        }

    }
}
