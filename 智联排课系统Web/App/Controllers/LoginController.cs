using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Models;

namespace App.Controllers
{
    public class LoginController : ApiController
    {
        public BLL.YuanGongBiaoBLL yuanGong = new BLL.YuanGongBiaoBLL();

        public BLL.XueShengBiaoBLL XueSheng = new BLL.XueShengBiaoBLL();


        #region 登录
        [HttpGet]
        public IHttpActionResult LoginAction(/*[FromBody] Models.YuanGongBiao yuanGongBiao,*/string Phone,string MiMA)
        {
            YuanGongBiao yuanGongBiao2 = new YuanGongBiao();
            yuanGongBiao2.Phone = Phone;
            yuanGongBiao2.MiMa = MiMA;
            var Teacher = yuanGong.YuanGongBiao(yuanGongBiao2);
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string Name = "";
            if (Teacher != null)
            {
                Name = "登录成功";
                context.Session.Add("Teacher", Teacher);
                return Ok(Name);
            }
            else
            {
                XueShengBiao xueShengBiao = new XueShengBiao
                {
                    MiMa = yuanGongBiao2.MiMa,
                    Phone = yuanGongBiao2.Phone
                };
                var Student = XueSheng.XueSheng(xueShengBiao);
                if (Student != null)
                {
                    Name = "登录成功";
                    context.Session.Add("Student", Student);
                }
                else
                {
                    Name = "登录失败";
                }
            }
            return Ok(Name);
        }
        #endregion
    }
}
