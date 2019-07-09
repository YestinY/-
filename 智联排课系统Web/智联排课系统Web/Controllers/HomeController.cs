using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace 智联排课系统Web.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public BLL.YuanGongBiaoBLL YuanGong = new BLL.YuanGongBiaoBLL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult Login()
        {
            HttpCookie phone = Request.Cookies["Phone"];
            HttpCookie MiMa = Request.Cookies["MiMa"];
            if (phone != null && MiMa != null)
            {
                YuanGongBiao yuanGong = new YuanGongBiao() { Phone = phone.Value, MiMa = MiMa.Value };
                var Teacher = YuanGong.YuanGongBiao(yuanGong);
                Session.Add("Admin", Teacher);
                return Redirect("/Home/Index");
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginAction(string Phone, string MiMa, string online)
        {
            YuanGongBiao yuanGong = new YuanGongBiao() { Phone = Phone, MiMa = MiMa };
            var Teacher = YuanGong.YuanGongBiao(yuanGong);
            if (Teacher != null)
            {
                Session.Add("Admin", Teacher);
                if (online == "on")
                {
                    HttpCookie Phone2 = new HttpCookie("Phone", Teacher.Phone);
                    HttpCookie MiMa2 = new HttpCookie("MiMa", Teacher.MiMa);
                    Phone2.Expires = System.DateTime.MaxValue;
                    MiMa2.Expires = System.DateTime.Now.AddHours(1);
                    Response.Cookies.Add(Phone2);
                    Response.Cookies.Add(MiMa2);
                }
                return Redirect("/Home/Index");
            }
            else
            {
                return Redirect("/Home/Login");
            }
        }

    }
}
