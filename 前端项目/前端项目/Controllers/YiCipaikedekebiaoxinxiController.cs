using BLL;
using Models;
using Submail.AppConfig;
using Submail.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 前端项目.Controllers
{
    public class YiCipaikedekebiaoxinxiController : Controller
    {
        //
        // GET: /YiCipaikedekebiaoxinxi/
        private YiCipaikedekebiaoxinxiBLL bll = new YiCipaikedekebiaoxinxiBLL();
        private BanJiKaiShekechengjihuaBLL bll2 = new BanJiKaiShekechengjihuaBLL();
        private YuanGongBiaoBLL bll3 = new YuanGongBiaoBLL();
        private XueShengBiaoBLL bll4 = new XueShengBiaoBLL();

        //public  chengBLL = new BLL.JiaoXueKeChengBLL();

        public ZhiLianPaiKeXiTongDBEntities entities = new ZhiLianPaiKeXiTongDBEntities();

        #region 废用（没用了）
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 智联排课信息主页
        public ActionResult zhuye()
        {
            //var Studnet = Session["Student"] as Models.XueShengBiao;
            //ViewBag.List = chengBLL.keChengs(Studnet.StudentClassID.Value);
            return View();
        }
        #endregion

        #region 查询所有分页(一次排课的课表信息)
        public ActionResult GetAllYiCipaike(string Name, int? page, int? limit, DateTime? time)
        {
            int PageIndex = page ?? 1;
            int PageSize = limit ?? 10;
            DateTime datetime = time ?? new DateTime(0001, 1, 1);
            List<ZhengZaiShangKeBiao> lists = bll.List(PageIndex, PageSize, Name, datetime, out int Total);
            var viewlist = lists.Select(p => new
            {
                p.ID,
                RiQi = p.RiQi.Value.ToString("yyyy-MM-dd"),
                p.ShiJianDuan,
                p.ShiJianMing,
                p.ClassName,
                p.KeChengMingChen,
                p.ZhangJieMingChen,
                p.ZiYuanMingChen,
                p.JiaoYuanMingChen,
                p.ShiShiShiJian,
                p.CanJiaRenYuan,
                p.BeiZhu,
                p.ZhuangTai
            });
            var jsondata = new
            {
                code = 0,
                msg = "成功",
                count = Total,
                data = viewlist
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 查询所有分页（班级开设课程计划）
        public ActionResult GetAllBanJikaishekecheng(int? page, int? limit)
        {

            int PageIndex = page ?? 1;
            int PageSize = limit ?? 20;
            int COUNT = bll2.GetAll().Count();
            int Count;
            List<BanJiKaiSheKeChengJiHuaBiao> list2;
            if (Session["Teacher"] != null)
            {
                YuanGongBiao yuanGong = Session["Teacher"] as YuanGongBiao;
                list2 = bll2.GetPage(PageIndex, PageSize, out Count, yuanGong.ID.ToString());
            }
            else
            {
                XueShengBiao xueSheng = Session["Student"] as XueShengBiao;
                list2 = bll2.GetPage(PageIndex, PageSize, out Count, xueSheng.StudentClassID.Value);

            }
            var jsondata = new
            {
                code = 0,
                msg = "成功",
                count = Count,
                data = list2
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 登录
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LoginAction(string phone, string pwd, int peo)
        {
            //  0：教员   1：学生
            YuanGongBiao user1 = null;
            XueShengBiao user2 = null;
            int i = 0;
            if (peo == 0)
            {
                user1 = bll3.Login(phone, pwd);
                Session.Add("Teacher", user1);
            }
            else if (peo == 1)
            {
                user2 = bll4.Login(phone, pwd);
                Session.Add("Student", user2);
            }
            if (user1 != null || user2 != null)
            {
                i = 1;
            }
            return Json(new { Count=1},JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改员工信息
        public void UpdateAction(YuanGongBiao yg)
        {
            yg.Name = yg.JiaoYuanMingChen;
            int n = bll3.UpdateYuanGong(yg);
            if (n > 0)
            {
                Session["Teacher"] = yg;
                Response.Write("<script>alert('员工信息修改成功');location.href='/YiCipaikedekebiaoxinxi/zhuye'</script>");
            }
            else
            {
                Response.Write("<script>alert('员工信息修改失败，请重试');location.href='/YiCipaikedekebiaoxinxi/zhuye'</script>");
            }
        }
        #endregion

        #region 修改学生信息
        public void UpdateActionStu(XueShengBiao stu)
        {
            int n = bll4.UpdateXueSheng(stu);
            if (n > 0)
            {
                Session["Student"] = stu;
                Response.Write("<script>alert('学生信息修改成功');location.href='/YiCipaikedekebiaoxinxi/zhuye'</script>");
            }
            else
            {
                Response.Write("<script>alert('学生信息修改失败，请重试');location.href='/YiCipaikedekebiaoxinxi/zhuye'</script>");
            }
        }
        #endregion

        //课表完成发送的action
        public ActionResult SendMessage(string PhoneNumber)
        {
            IAppConfig appConfig = new MessageConfig("26920", "d22f4cccf0441fcafcab541dd7d6e646", SignType.md5);
            MessageSend messageSend = new MessageSend(appConfig);
            messageSend.AddTo(PhoneNumber);
            string temp = "";
            Random random = new Random();
            for (int i = 0; i < 6; i++)
            {
                temp += random.Next(1, 9).ToString();
            }
            DateTime date = DateTime.Now;
            messageSend.AddContent("【智联科技有限公司】你的验证码为" + temp + ",请在一分钟内进行输入。");
            messageSend.AddTag("xxxxx");
            string returnMessage = string.Empty;
            messageSend.Send(out returnMessage);
            return Json(new { Count = 1, Message = returnMessage, Number = temp }, JsonRequestBehavior.AllowGet);
        }
    }
}
