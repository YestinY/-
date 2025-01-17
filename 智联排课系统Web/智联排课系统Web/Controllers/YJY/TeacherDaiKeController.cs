﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class TeacherDaiKeController : Controller
    {
        //
        // GET: /TeacherDaiKe/
        public BLL.JiaoYuanDaiKeBiaoBLL JiaoYuanDaiKeBiaoBLL = new BLL.JiaoYuanDaiKeBiaoBLL();

        public BLL.YuanGongBiaoBLL Yuan = new BLL.YuanGongBiaoBLL();

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult TeacherHome()
        {
            var list = Yuan.TeacherBZR();
            ViewBag.List = list;
            return View();
        }

        public ActionResult List(int? pagenum, int? pagesize, string ClassName, DateTime? RiQi, int? JiaoYuanBianHao)
        {
            int ID = JiaoYuanBianHao ?? -1;
            DateTime time = RiQi ?? new DateTime(0001, 1, 1);
            int Pageindex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var list = JiaoYuanDaiKeBiaoBLL.List(Pageindex, PageSize, out int Total, ClassName, time, ID);
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
