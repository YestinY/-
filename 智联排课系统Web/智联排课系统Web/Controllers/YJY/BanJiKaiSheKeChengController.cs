using Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class BanJiKaiSheKeChengController : Controller
    {
        //
        // GET: /BanJiKaiSheKeCheng/
        public BLL.BanZhuRenSuoDaiBanJiBiaoBLL BanZhuRen = new BLL.BanZhuRenSuoDaiBanJiBiaoBLL();

        public BLL.BanJiBiaoBLL ban = new BLL.BanJiBiaoBLL();

        public BLL.YuanGongBiaoBLL yuanGong = new BLL.YuanGongBiaoBLL();

        public BLL.BanJiKaiSheKeChengJiHuaBiaoBLL ban2 = new BLL.BanJiKaiSheKeChengJiHuaBiaoBLL();

        public BLL.JiaoXueKeChengBLL jiaoXue = new BLL.JiaoXueKeChengBLL();

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 分配教学计划界面
        /// </summary>
        /// <param name="banjiID"></param>
        /// <returns></returns>
        public ActionResult AddPage(int ID)
        {
            var Student = ban.GetOneData(ID);
            ViewBag.ID = Student.ID;
            ViewBag.Name = Student.BanJiMing;
            return View();
        }


        public ActionResult FenPeiBZR(int ID)
        {
            return Json(JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 弹出班主任选择界面
        /// </summary>
        /// <returns></returns>
        public ActionResult Tan()
        {
            Models.All all = new Models.All
            {
                List2 = yuanGong.GetAllData().Where(p => p.ZhiWeiID == 2).ToList()
            };
            return View(all);
        }

        /// <summary>
        /// 显示分配员工界面
        /// </summary>
        /// <param name="banjiID"></param>
        /// <returns></returns>
        public ActionResult ShowPage(int ID)
        {
            var Bna = ban.GetOneData(ID);
            ViewBag.ClassName = Bna.BanJiMing;
            ViewBag.ClassID = Bna.ID;
            var Class = BanZhuRen.GetAllData().Where(p => p.BanJiBianHao == ID).ToList();
            ViewBag.BZRName = "";
            //ViewBag.BZR = yuanGong.GetAllData().Where(p => p.ZhiWeiID == 2).ToList();
            if (Class.Count != 0)
            {
                int ID2 = Class.Last().BanZhuRenBianHao.Value;
                var BZRone = yuanGong.GetOneData(ID2);
                ViewBag.BZRName = BZRone.JiaoYuanMingChen;
            }
            var Ban = ban2.GetAllData().Where(p => p.BanJiID == Bna.ID).Count();
            if (Ban == 0)
            {
                return Json(new { errorNo = "1", errorInfo = "当前此班为分配教学计划" }, JsonRequestBehavior.AllowGet);
            }
            var Ban2 = ban2.GetAllData().Where(p => p.BanJiID == Bna.ID).First();
            BLL.ZM.AnPaiKeChengBLL AP = new BLL.ZM.AnPaiKeChengBLL();
            //var TeacherList = AP.Teacher(banjiID);
            var TeacherList = yuanGong.GetAllData().Where(p => p.ZhiWeiID == 1).ToList();
            //var BZRlist = AP.ygList();
            //var Course = AP.KeCheng(banjiID);
            var Course = jiaoXue.GetAllData().Where(p => p.SuoShuJiaoXueJiHua == Ban2.CaiYongJiaoXueJiHua).ToList();
            var TeacherList2 = yuanGong.GetAllData().Where(p => p.ZhiWeiID == 2).ToList();
            Models.All all = new Models.All
            {
                List2 = TeacherList2,
                List = TeacherList,
                Course = Course
            };
            return View(all);
        }

        /// <summary>
        /// 显示班级教学进度
        /// </summary>
        /// <param name="banjiID"></param>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult ShowAction(int ID, int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var List = ban2.QueryID(ID, PageIndex, PageSize, out int Total);
            var jsondata = new
            {
                errorNo = "0",
                errorInfo = "查找成功",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = List
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 分配教员
        /// </summary>
        /// <param name="KCID"></param>
        /// <param name="Teacher"></param>
        /// <param name="bjid"></param>
        /// <returns></returns>
        public ActionResult FenPeiTeacher(int KCID, int Teacher, int bjid)
        {
            try
            {
                var KcList = ban2.GetAllData().Where(p => p.BanJiID == bjid && p.KeChengMing == KCID && p.ShiFouYiWanCheng == false).ToList();
                var a = jiaoXue.GetAllData().Where(p => p.ID == KCID).First();
                var Te = yuanGong.GetAllData().Where(p => p.ID == Teacher).First();
                if (a.KeChengMing.Contains("职业素质训练 第一学期"))
                {
                    if (Te.ZhiWeiID != 2)
                    {
                        return Json(new { errorNo = "1", errorInfo = "分配失败,此为班主任课程" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    foreach (var item in KcList)
                    {
                        if (item.AnPaiJiaoYuan != null && item.AnPaiJiaoYuan != 0)
                        {
                            item.BeiZhu = item.BeiZhu != null ? ("," + Teacher) : item.AnPaiJiaoYuan.ToString();
                            item.AnPaiJiaoYuan = Teacher;
                        }
                        else
                        {
                            item.AnPaiJiaoYuan = Teacher;
                        }
                        ban2.Modify(item);
                    }
                }
                return Json(new { errorNo = "0", errorInfo = "分配成功" }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new { errorNo = "1", errorInfo = ex }, JsonRequestBehavior.AllowGet);
            }
        }
        /// <summary>
        /// 分配教学计划
        /// </summary>
        /// <param name="banJiKaiShe"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public ActionResult FenPaiPlanAction(BanJiKaiSheKeChengJiHuaBiao banJiKaiShe, int Plan)
        {

            int i = ban2.FenPei(banJiKaiShe, Plan);
            if (i > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "分配成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "分配失败" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult BZRFPGL(int bjid, int bzrid)
        {
            bool f = BanZhuRen.BzrFpBJ(bjid, bzrid);
            if (f)
            {
                return Json(new { errorNo = "0", errorInfo = "分配成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "分配错误!" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
