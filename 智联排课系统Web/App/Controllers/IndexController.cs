using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace App.Controllers
{
    public class IndexController : Controller
    {
        // GET: Index
        public BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL bLL = new BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL();

        //班级开设课程
        public BLL.BanJiKaiSheKeChengJiHuaBiaoBLL ban = new BLL.BanJiKaiSheKeChengJiHuaBiaoBLL();

        public BLL.ZhengZaiShangKeBiaoBLL ShangKeBiaoBLL = new BLL.ZhengZaiShangKeBiaoBLL();

        public BLL.YuanGongBiaoBLL YuanGongBiaoBLL = new BLL.YuanGongBiaoBLL();

        //班级课程
        public BLL.JiaoXueKeChengBLL chengBLL = new BLL.JiaoXueKeChengBLL();

        //首页
        public ActionResult Index()
        {
            //if (Session["Teacher"] == null)
            //{
            //    return Redirect("/Index/login");
            //}
            //else if (Session["Student"] == null)
            //{
            //    return Redirect("/Index/login");
            //}
            return View();
        }

        //登陆页
        public ActionResult login()
        {
            return View();
        }

        //课表
        public ActionResult SchoolTimeTable()
        {
            return View();
        }

        /// <summary>
        /// 显式课表
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="page"></param>
        /// <param name="limit"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public ActionResult ShowTable(string Name, int? page, int? limit, DateTime? time)
        {
            int PageIndex = page ?? 1;
            int PageSize = limit ?? 10;
            DateTime datetime = time ?? new DateTime(0001, 1, 1);
            List<ZhengZaiShangKeBiao> List = ShangKeBiaoBLL.List(PageIndex, PageSize, Name, datetime, out int Total);
            var Json2 = new
            {
                code = 0,
                msg = "成功",
                count = Total,
                data = List
            };
            return Json(Json2, JsonRequestBehavior.AllowGet);
        }

        // 班级课程
        public ActionResult ClassKC()
        {
            var Studnet = Session["Student"] as Models.XueShengBiao;
            ViewBag.List = chengBLL.keChengs(Studnet.StudentClassID.Value);
            return View();
        }

        public ActionResult TeacherPage()
        {
            //教员所带班级
            var Teacher = Session["Teacher"] as YuanGongBiao;
            ViewBag.Class = ban.TeacherClass(Teacher.ID);
            return View();
        }

        //学生所在班级的课程
        public ActionResult ClassKCShow(int? page, int? limit, int? KC)
        {

            //var Studnet = Session["Student"] as Models.XueShengBiao;
            int PageIndex = page ?? 1;
            int PageSize = limit ?? 10;
            int KCID = KC ?? -1;
            var list = ban.banJiKaiSheKes(/*Studnet.StudentClassID.Value,*/12, PageIndex, PageSize, KCID, out int Total);
            return Json(new { code = 0, msg = "查询成功", count = Total, data = list }, JsonRequestBehavior.AllowGet);
        }

        //教员所在班级的课程
        public ActionResult TeacherKCShow(int? page, int? limit, int? ID)
        {
            var Teacher = Session["Teacher"] as YuanGongBiao;
            int PageIndex = page ?? 1;
            int PageSize = limit ?? 10;
            int ClasaID = ID ?? -1;
            var list = ban.TeacherClass(Teacher.ID, PageIndex, PageSize, ClasaID, out int Total);
            return Json(new { code = 0, msg = "查询成功", count = Total, data = list }, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult LoginAction(string Phone, string MiMA)
        //{
        //    YuanGongBiao yuanGongBiao2 = new YuanGongBiao();
        //    yuanGongBiao2.Phone = Phone;
        //    yuanGongBiao2.MiMa = MiMA;
        //    var Teacher = yuanGong.YuanGongBiao(yuanGongBiao2);
        //    System.Web.HttpContext context = System.Web.HttpContext.Current;
        //    string Name = "";
        //    if (Teacher != null)
        //    {
        //        Name = "登录成功";
        //        context.Session.Add("Teacher", Teacher);
        //        return Ok(Name);
        //    }
        //    else
        //    {
        //        XueShengBiao xueShengBiao = new XueShengBiao
        //        {
        //            MiMa = yuanGongBiao2.MiMa,
        //            Phone = yuanGongBiao2.Phone
        //        };
        //        var Student = XueSheng.XueSheng(xueShengBiao);
        //        if (Student != null)
        //        {
        //            Name = "登录成功";
        //            context.Session.Add("Student", Student);
        //        }
        //        else
        //        {
        //            Name = "登录失败";
        //        }
        //    }
        //    return Json(Name, JsonRequestBehavior.AllowGet);
        //}



    }
}