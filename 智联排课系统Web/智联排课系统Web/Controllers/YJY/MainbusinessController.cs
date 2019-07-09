using Models;
using Submail.AppConfig;
using Submail.Lib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class MainbusinessController : Controller
    {
        //
        // GET: /Mainbusiness/

        #region 实例化
        //一次排课的课表信息
        public BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL bLL = new BLL.YiCiPaiKeDeKeBiaoXinXiBiaoBLL();

        //排课计划
        public BLL.PaiKeJiHuaBLL jiHuaBLL = new BLL.PaiKeJiHuaBLL();

        //班级
        public BLL.BanJiBiaoBLL BanJiBiaoBLL = new BLL.BanJiBiaoBLL();

        //本次排课时段
        public BLL.BenCiPaiKeShiDuanBiaoBLL ShiDuanBiaoBLL = new BLL.BenCiPaiKeShiDuanBiaoBLL();

        //本次排班可用资源
        public BLL.BenCiPaiBanKeYongZiYuanBLL keYongZiYuanBLL = new BLL.BenCiPaiBanKeYongZiYuanBLL();

        //本次排课班级及次数
        public BLL.BenCiPaiKeBanJiJiCiShuBLL PaiKeBanJiJiCiShuBLL = new BLL.BenCiPaiKeBanJiJiCiShuBLL();

        //本次排课课此表
        public BLL.BeCiPaiKeMorenKeCiBiaoBLL KeCi = new BLL.BeCiPaiKeMorenKeCiBiaoBLL();

        //排课时段与资源组合
        public BLL.PaiKeShiDuanYuZiYuanZuHeBLL PaiKeShiDuanYuZiYuan = new BLL.PaiKeShiDuanYuZiYuanZuHeBLL();

        //班级开设课程对象
        public BLL.BanJiKaiSheKeChengJiHuaBiaoBLL KeChengJiHuaBiaoBLL = new BLL.BanJiKaiSheKeChengJiHuaBiaoBLL();

        //排课方案
        public BLL.PaiKeFangAnBLL PaiKeFangAn = new BLL.PaiKeFangAnBLL();

        //排课List
        public BLL.PaiKeListBLL PaiKeBLL = new BLL.PaiKeListBLL();

        //员工BLL
        public BLL.YuanGongBiaoBLL yuan = new BLL.YuanGongBiaoBLL();

        //教学课程
        public BLL.JiaoXueKeChengBLL JiaoXueKeChengBLL = new BLL.JiaoXueKeChengBLL();

        //资源管理
        public BLL.ZiYuanGuanLiBLL ZiYuanGuanLiBLL = new BLL.ZiYuanGuanLiBLL();

        //教学阶段
        public BLL.JiaoXueJieDuanBLL JiaoXueJieDuanBLL = new BLL.JiaoXueJieDuanBLL();

        //教员代课表
        public BLL.JiaoYuanDaiKeBiaoBLL DaiKeBiaoBLL = new BLL.JiaoYuanDaiKeBiaoBLL();

        public BLL.ZhengZaiShangKeBiaoBLL ShangKeBiaoBLL = new BLL.ZhengZaiShangKeBiaoBLL();
        #endregion

        #region 课表View
        //课表
        public ActionResult Index()
        {
            var list = yuan.TeacherBZR();
            ViewBag.List = list;
            return View();
        }
        #endregion

        #region 班级排课次数View
        //班级排课次数
        public ActionResult SetUp()
        {
            return View();
        }
        #endregion

        #region 排课方案
        //排课方案
        public ActionResult Main()
        {
            if (Session["Count"] != null)
            {

            }
            else
            {
                Play();
            }
            return View();
        }
        #endregion

        #region 一次排课课表信息
        //一次排课课表信息
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
        #endregion

        #region 排课方案
        public ActionResult Play()
        {
            //本次班级排课班级及次数集合
            List<BenCiPaiKeBanJiJiCiShu> Class = PaiKeBanJiJiCiShuBLL.GetAllData();
            //本次排课默认课次表
            List<BeCiPaiKeMorenKeCiBioa> KeCiList = KeCi.GetAllData();

            //资源数量
            var ZYuanCount = keYongZiYuanBLL.GetAllData().Count();

            //算法区start！
            int pindex = 0;//符合的方案数
            //获取排课班级数-可用资源
            var ClassCount = Class.Count();
            //如果表里存在数据。进行删除
            PaiKeFangAn.DeleteTable();
            //例:30班级-30资源、可3*6=18、2*6=12,
            for (int i1 = 0; i1 <= ZYuanCount; i1++) //排0次的教室数
            {
                for (int i2 = 0; i2 <= ZYuanCount - i1; i2++)
                {
                    for (int i3 = 0; i3 <= ZYuanCount - i1 - i2; i3++)
                    {
                        for (int i4 = 0; i4 <= ZYuanCount - i1 - i2 - i3; i4++)
                        {
                            int jscount = i1 + i2 + i3 + i4;
                            //排班数
                            int pbcount = i1 * 0 + i2 * 1 + i3 * 2 + i4 * 3;
                            if (pbcount >= ClassCount && jscount == ZYuanCount)
                            {
                                PaiKeFangAnBiao pai = new PaiKeFangAnBiao
                                {
                                    ScenarioName = "安排1次排课资源的班级数" + i2 + ",安排2次排课资源的班级数" + i3 + ",安排3次排课资源的班级数" + i4 + "",
                                    one = i2,
                                    two = i3,
                                    three = i4
                                };
                                PaiKeFangAn.Add(pai);
                            }
                        }
                    }
                }
            }
            Session.Add("Count", "1");
            return Json(new { errorNo = "0", erronInfo = "生成计划成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 全部方案
        public ActionResult GetAll(int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var lista = PaiKeFangAn.List(PageIndex, PageSize, out int Total);
            var Jsona = new
            {
                errorNo = "0",
                errorInfo = "查找成功,地址深圳",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = lista
                    }
                }
            };
            return Json(Jsona, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 推荐方案 根据教员所带的班级来指定 
        //推荐方案 根据教员所带的班级来指定 
        public ActionResult GetAll2(int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var lista = PaiKeFangAn.List(PageIndex, PageSize, out int Total);
            var Jsona = new
            {
                errorNo = "0",
                errorInfo = "查找成功,地址深圳",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = lista
                    }
                }
            };
            return Json(Jsona, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 排课
        public ActionResult PaiKe(int ID)
        {
            //根据ID获取教务选择的排课方案
            var one = PaiKeFangAn.GetAllData().Where(p => p.ID == ID).First();

            //排课时段与资源集合
            List<PaiKeShiDuanYuZiYuanZuHe> shiDuanBiaos = PaiKeShiDuanYuZiYuan.GetAllData();

            //本次班级排课班级及次数集合
            List<BenCiPaiKeBanJiJiCiShu> Class = PaiKeBanJiJiCiShuBLL.GetAllData();

            //本次排课默认课次表
            List<BeCiPaiKeMorenKeCiBioa> KeCiList = KeCi.GetAllData();

            var ZYuan = keYongZiYuanBLL.GetAllData();

            var ZYuanCount = keYongZiYuanBLL.GetAllData().Count();
            //可排三个班的教室
            var three = KeCiList.Where(p => p.ClassCount == 3).ToList();

            //可排两个班的教室
            var two = KeCiList.Where(p => p.ClassCount == 2).ToList();

            //可排一个班的教室
            var one2 = KeCiList.Where(p => p.ClassCount == 1).ToList();

            List<Hi> o = bLL.banJiKaiSheKeChengJiHuaBiaos();

            //获取排课资源的次数

            //删除零时表数据
            PaiKeBLL.DeleteTable();
            bLL.DeleteTable();

            //形态
            Dictionary<string, List<List<string>>> dicFA = new Dictionary<string, List<List<string>>>
            {
                //3个班排课方式只有一种
                {
                    "3-1",
                    new List<List<string>>() {
                                            new List<string>{ "1-上","3-上","4-上","6-上" },//第1个班
                                             new List<string>{ "1-下","2-下","4-下","5-下" },//第2个班
                                               new List<string>{ "2-上","3-下","5-上","6-下" }//第3个班
                                        }
                },
                //2个班排课方式列出几种
                {
                    "2-1",
                    new List<List<string>>() {
                                             new List<string>{ "1-上","3-上","4-上","6-上" },//第1个班
                                             new List<string>{ "1-下","3-下","4-下","6-下" }//第2个班
                                            
                                        }
                },

                {
                    "2-2",
                    new List<List<string>>() {
                                             new List<string>{ "1-上","3-上","4-上","6-上" },//第1个班
                                             new List<string>{ "1-下","2-下","4-下","5-下" }//第2个班
                                            
                                        }
                },
                {
                    "2-3",
                    new List<List<string>>() {
                                             new List<string>{ "2-上","3-上","5-上","6-上" },//第1个班
                                             new List<string>{ "2-下","3-下","5-下","6-下" }//第2个班
                                            
                                        }
                },
                //1个班排课方式列出几种
                {
                    "1-1",
                    new List<List<string>>() {
                                             new List<string>{ "1-上","3-上","4-上","6-上" }//第1个班        
                                        }
                },
                {
                    "1-2",
                    new List<List<string>>() {
                                             new List<string>{ "1-下","3-下","4-下","6-下" }//第1个班        
                                        }
                },

                {
                    "1-3",
                    new List<List<string>>() {
                                             new List<string>{ "1-上","2-上","4-上","5-上" }//第1个班        
                                        }
                },

                {
                    "1-4",
                    new List<List<string>>() {
                                             new List<string>{ "1-下","2-下","4-下","5-下" }//第1个班        
                                        }
                },

                {
                    "1-5",
                    new List<List<string>>() {
                                             new List<string>{ "2-下","3-下","5-下","6-下" }//第1个班        
                                        }
                },
                {
                    "1-6",
                    new List<List<string>>() {
                                             new List<string>{ "2-上","3-上","5-上","6-上" }//第1个班        
                    }
                }
            };

            //上课课表集合
            List<JSPJJY> jsspjjyList = new List<JSPJJY>();
            int n3_1 = 0;
            int n2_1 = 0;
            int n1_1 = 0;
            string str = "";
            foreach (var item in o.OrderBy(p => p.AnPaiJiaoYuan))
            {
                if (str != item.AnPaiJiaoYuan + "")
                {
                    //根据安排教员查找出所带班级
                    var tempbjlist = bLL.List(item.AnPaiJiaoYuan);
                    int bjcount = tempbjlist.Count();//
                    //==========================//
                    if (bjcount == 3)
                    {
                        var aaa = three[n3_1++];//
                        for (int in1 = 0; in1 < tempbjlist.Count(); in1++)
                        {
                            JSPJJY jSPJJY = new JSPJJY
                            {
                                JS = aaa.ZiYuanMingChen,
                                JY = item.AnPaiJiaoYuan + "",
                                PBS = 3 + "",
                                XD = "3-1",
                                XDIndex = in1 + 1,
                                BJ = tempbjlist[in1].BanJiMing
                            };
                            jsspjjyList.Add(jSPJJY);
                        }
                    }
                    //=========================//
                    if (bjcount == 2)
                    {
                        var aaa = two[n2_1++];//
                        for (int in1 = 0; in1 < tempbjlist.Count(); in1++)
                        {
                            JSPJJY jSPJJY = new JSPJJY
                            {
                                JS = aaa.ZiYuanMingChen,
                                JY = item.AnPaiJiaoYuan + "",
                                PBS = 2 + "",
                                XD = "2-1",
                                XDIndex = in1 + 1,
                                BJ = tempbjlist[in1].BanJiMing
                            };
                            jsspjjyList.Add(jSPJJY);
                        }
                    }
                    //==================//
                    if (bjcount == 1)
                    {
                        var aaa = one2[n1_1++];//

                        //
                        for (int in1 = 0; in1 < tempbjlist.Count(); in1++)
                        {
                            JSPJJY jSPJJY = new JSPJJY
                            {
                                JS = aaa.ZiYuanMingChen,
                                JY = item.AnPaiJiaoYuan + "",
                                PBS = 1 + "",
                                XD = "1-1",
                                XDIndex = in1 + 1,
                                BJ = tempbjlist[in1].BanJiMing
                            };
                            //
                            jsspjjyList.Add(jSPJJY);

                        }
                    }
                    str = item.AnPaiJiaoYuan + "";//
                }
            }
            ////////////////////////
            //开始排课时间
            var Jih = jiHuaBLL.GetAllData().Where(p => p.ShiFouWanCheng == false).OrderByDescending(p => p.ID).First();

            DateTime StartTime = (DateTime)Jih.KaiShiShiJian;

            DateTime EndTime = (DateTime)Jih.JieShuShiJian;

            TimeSpan time = EndTime - StartTime;
            //课表集合
            List<KB> kBs = new List<KB>();

            //安排上课教室
            foreach (var item in jsspjjyList)
            {
                var obj = item;
                List<string> SD = dicFA[item.XD][item.XDIndex - 1];
                int i = 0;
                foreach (var item2 in SD)
                {
                    string[] s = item2.Split(new char[] { '-' });

                    int aday = Convert.ToInt32(s[0]);//开始时间+天数,为排课日期
                    string strsd = s[1];//时段
                    i++;
                    if (StartTime.AddDays(aday - 1) <= EndTime)
                    {
                        var ClassKC = KeChengJiHuaBiaoBLL.GetAllData().Where(p => p.BanJiMing == item.BJ && p.ShiFouYiWanCheng == false).Take(4).ToList();
                        KB kB = new KB()
                        {
                            BJ = item.BJ,
                            RQ = StartTime.AddDays(aday - 1),
                            SD = strsd,
                            JS = item.JS,
                            KC = ClassKC[i - 1].ZhangJieMingChen,
                            JY = item.JY
                        };
                        PaikeLinshiBiao paikeLinshiBiao = new PaikeLinshiBiao
                        {
                            BJ = kB.BJ,
                            RQ = kB.RQ,
                            SD = kB.SD,
                            JS = kB.JS,
                            KC = kB.KC,
                            JY = kB.JY
                        };
                        PaiKeBLL.Add(paikeLinshiBiao);
                        kBs.Add(kB);
                    }
                    //设置自习课:实习方式，查询出没有进行排课的教室，设置为自习，并且不能出现时间冲突
                }
            }

            //#region 无用
            ////已安排上课的班级
            //var ClassList = PaiKeBLL.GetAllData();

            ////本次排课的班级
            //var ClassList2 = Class.ToList().Distinct();

            //foreach (var item in ClassList2)
            //{

            //    for (int i = 0; i < time.Days; i++)
            //    {
            //        //查询当天的上午是否排课
            //        var Count = ClassList.Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i) && p.SD == "上").Count();
            //        var WeiAnPaiList = PaiKeBLL.List(StartTime.AddDays(i));
            //        if (Count == 0)
            //        {
            //            var a = PaiKeBLL.List(StartTime.AddDays(i), "上").First(); ;
            //            PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
            //            {
            //                JS = a.KeYongZiYuanMingChen,
            //                BJ = item.BanJiMingChen,
            //                KC = "自习",
            //                JY = "未安排",
            //                RQ = StartTime.AddDays(i),
            //                SD = "上"
            //            };
            //            PaiKeBLL.Add(paikeLinshi);
            //        }

            //        //查询当天的下午是否排课
            //        var XW = ClassList.Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i) && p.SD == "下").Count();
            //        if (XW == 0)
            //        {
            //            var a = PaiKeBLL.List(StartTime.AddDays(i), "下").First(); ;
            //            PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
            //            {
            //                JS = a.KeYongZiYuanMingChen,
            //                BJ = item.BanJiMingChen,
            //                KC = "自习",
            //                JY = "未安排",
            //                RQ = StartTime.AddDays(i),
            //                SD = "下"
            //            };
            //            PaiKeBLL.Add(paikeLinshi);
            //        }
            //        //查询当天是否排课
            //        var SD = PaiKeBLL.GetAllData().Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i)).Count();
            //        if (SD == 0)
            //        {
            //            var a = PaiKeBLL.List(StartTime.AddDays(i)).First();
            //            for (int Hi = 0; Hi < 2; Hi++)
            //            {
            //                string str2 = "";
            //                if (i == 1)
            //                {
            //                    str2 = "上";
            //                }
            //                else
            //                {
            //                    str2 = "下";
            //                }
            //                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
            //                {
            //                    JS = a.KeYongZiYuanMingChen,
            //                    BJ = item.BanJiMingChen,
            //                    KC = "自习",
            //                    JY = "0",
            //                    RQ = StartTime.AddDays(i),
            //                    SD = str2
            //                };
            //                PaiKeBLL.Add(paikeLinshi);
            //            }
            //        }
            //    }
            //}
            //#endregion

            //排课集合
            var List = PaiKeBLL.GetAllData().OrderBy(p => p.RQ).ToList();
            //添加到一次课表
            foreach (var item in List)
            {
                string TeacherName = "";
                int TeacherID = 0;
                YuanGongBiao Teacher;
                JiaoXueKeCheng KC;
                string KCName;
                int KCID;
                string ZJName = "";
                int ZJID;
                Teacher = yuan.GetAllData().Where(p => p.ID == Convert.ToInt32(item.JY)).First();
                TeacherName = Teacher.JiaoYuanMingChen;
                TeacherID = Teacher.ID;
                var ZJ = KeChengJiHuaBiaoBLL.GetAllData().Where(p => p.BanJiMing == item.BJ && p.ZhangJieMingChen == item.KC).First();
                KC = JiaoXueKeChengBLL.GetAllData().Where(p => p.ID == ZJ.KeChengMing).First();
                KCName = KC.KeChengMing;
                KCID = KC.ID;
                ZJName = ZJ.ZhangJieMingChen;
                ZJID = (int)ZJ.ZhangJieBianHao;
                //if (item.JY == "未安排")
                //{
                //    TeacherName = "未安排";
                //}
                //else
                //{

                //}
                //if (item.KC == "自习")
                //{
                //    KCName = "自习";
                //    KCID = 0;
                //    ZJName = "无";
                //    ZJID = 0;
                //}
                //else
                //{

                //}
                //获取课程名

                var ZY = ZiYuanGuanLiBLL.GetAllData().Where(p => p.ZiYuanMing == item.JS).First();
                string SJMing = "";
                if (item.SD == "上")
                {
                    SJMing = "8:00-12:20";
                }
                else
                {
                    SJMing = "13:30 - 17:30";
                }
                YiCiPaiKeDeKeBiaoXinXiBiao yiCiPaiKeDeKeBiaoXinXi = new YiCiPaiKeDeKeBiaoXinXiBiao
                {
                    CanJiaRenYuan = TeacherName,
                    BeiZhu = Jih.JiHuaMingChen,
                    RiQi = item.RQ,
                    ShiJianDuan = item.SD,
                    ShiJianMing = SJMing,
                    ZhuangTai = "待上课",
                    JiaoYuanBianHao = TeacherID.ToString(),
                    JiaoYuanMingChen = TeacherName,
                    ShiShiShiJian = item.RQ.ToString(),
                    ZhangJieBianHao = ZJID.ToString(),
                    ZhangJieMingChen = ZJName,
                    KeChengBianHao = KCID.ToString(),
                    KeChengMingChen = KCName,
                    ZiYuanBianHao = ZY.ID.ToString(),
                    ZiYuanMingChen = item.JS,
                    ClassName = item.BJ,
                };
                bLL.Add(yiCiPaiKeDeKeBiaoXinXi);
            }

            return Json(new { errorInfo = "设置上课课表完成", errorNo = "0" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 自习课
        public ActionResult ZiXiAction()
        {
            var Jih = jiHuaBLL.GetAllData().Where(p => p.ShiFouWanCheng == false).OrderByDescending(p => p.ID).First();

            DateTime StartTime = (DateTime)Jih.KaiShiShiJian;

            DateTime EndTime = (DateTime)Jih.JieShuShiJian;

            TimeSpan time = EndTime - StartTime;
            //本次班级排课班级及次数集合
            List<BenCiPaiKeBanJiJiCiShu> Class = PaiKeBanJiJiCiShuBLL.GetAllData();
            //已安排上课的班级
            var ClassList = PaiKeBLL.GetAllData();

            //本次排课的班级
            var ClassList2 = Class.ToList().Distinct();

            var ClassList3 = ClassList2.ToList().OrderBy(i => Guid.NewGuid()).ToList();
            try
            {
                foreach (var item in ClassList2)
                {
                    for (int i = 0; i < time.Days; i++)
                    {
                        //查询当天的上午是否排课
                        var Count = ClassList.Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i) && p.SD == "上").Count();
                        var WeiAnPaiList = PaiKeBLL.List(StartTime.AddDays(i));
                        if (Count == 0)
                        {
                            if (PaiKeBLL.List(StartTime.AddDays(i), "上").Count() == 0)
                            {
                                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
                                {
                                    JS = "未安排",
                                    BJ = item.BanJiMingChen,
                                    KC = "放假",
                                    JY = "未安排",
                                    RQ = StartTime.AddDays(i),
                                    SD = "上"
                                };
                                PaiKeBLL.Add(paikeLinshi);
                            }
                            else
                            {
                                var a = PaiKeBLL.List(StartTime.AddDays(i), "上").First(); ;
                                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
                                {
                                    JS = a.KeYongZiYuanMingChen,
                                    BJ = item.BanJiMingChen,
                                    KC = "自习",
                                    JY = "未安排",
                                    RQ = StartTime.AddDays(i),
                                    SD = "上"
                                };
                                PaiKeBLL.Add(paikeLinshi);
                            }
                        }
                        //查询当天的下午是否排课
                        var XW = ClassList.Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i) && p.SD == "下").Count();
                        if (XW == 0)
                        {
                            if (PaiKeBLL.List(StartTime.AddDays(i), "下").Count() == 0)
                            {
                                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
                                {
                                    JS = "未安排",
                                    BJ = item.BanJiMingChen,
                                    KC = "自习",
                                    JY = "未安排",
                                    RQ = StartTime.AddDays(i),
                                    SD = "下"
                                };
                                PaiKeBLL.Add(paikeLinshi);
                            }
                            else
                            {
                                var a = PaiKeBLL.List(StartTime.AddDays(i), "下").First();
                                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
                                {
                                    JS = a.KeYongZiYuanMingChen,
                                    BJ = item.BanJiMingChen,
                                    KC = "自习",
                                    JY = "未安排",
                                    RQ = StartTime.AddDays(i),
                                    SD = "下"
                                };
                                PaiKeBLL.Add(paikeLinshi);
                            }
                        }
                        //查询当天是否排课
                        var SD = PaiKeBLL.GetAllData().Where(p => p.BJ == item.BanJiMingChen && p.RQ == StartTime.AddDays(i)).Count();
                        if (SD == 0)
                        {
                            var a = PaiKeBLL.List(StartTime.AddDays(i)).First();
                            for (int Hi = 0; Hi < 2; Hi++)
                            {
                                string str2 = "";
                                if (i == 1)
                                {
                                    str2 = "上";
                                }
                                else
                                {
                                    str2 = "下";
                                }
                                PaikeLinshiBiao paikeLinshi = new PaikeLinshiBiao()
                                {
                                    JS = a.KeYongZiYuanMingChen,
                                    BJ = item.BanJiMingChen,
                                    KC = "自习",
                                    JY = "0",
                                    RQ = StartTime.AddDays(i),
                                    SD = str2
                                };
                                PaiKeBLL.Add(paikeLinshi);
                            }
                        }
                    }
                }
                ShangKeBiaoBLL.TruncateZhangZaiShangKeBiao();
                //排课集合
                var List = PaiKeBLL.GetAllData().OrderBy(p => p.RQ).Where(p => p.KC == "自习").ToList();
                //添加到一次课表
                foreach (var item in List)
                {
                    string TeacherName = "";
                    int TeacherID = 0;
                    YuanGongBiao Teacher;
                    JiaoXueKeCheng KC;
                    string KCName;
                    int KCID;
                    string ZJName = "";
                    int ZJID;
                    if (item.JY == "未安排")
                    {
                        TeacherName = "未安排";
                    }
                    else
                    {
                        Teacher = yuan.GetAllData().Where(p => p.ID == Convert.ToInt32(item.JY)).First();
                        TeacherName = Teacher.JiaoYuanMingChen;
                        TeacherID = Teacher.ID;
                    }
                    if (item.KC == "自习")
                    {
                        KCName = "自习";
                        KCID = 0;
                        ZJName = "无";
                        ZJID = 0;
                    }
                    else
                    {
                        var ZJ = KeChengJiHuaBiaoBLL.GetAllData().Where(p => p.BanJiMing == item.BJ && p.ZhangJieMingChen == item.KC).First();
                        KC = JiaoXueKeChengBLL.GetAllData().Where(p => p.ID == ZJ.KeChengMing).First();
                        KCName = KC.KeChengMing;
                        KCID = KC.ID;
                        ZJName = ZJ.ZhangJieMingChen;
                        ZJID = (int)ZJ.ZhangJieBianHao;
                    }
                    //获取课程名
                    var ZY = ZiYuanGuanLiBLL.GetAllData().Where(p => p.ZiYuanMing == item.JS).First();
                    string SJMing = "";
                    if (item.SD == "上")
                    {
                        SJMing = "8:00-12:20";
                    }
                    else
                    {
                        SJMing = "13:30 - 17:30";
                    }
                    YiCiPaiKeDeKeBiaoXinXiBiao yiCiPaiKeDeKeBiaoXinXi = new YiCiPaiKeDeKeBiaoXinXiBiao
                    {
                        CanJiaRenYuan = TeacherName,
                        BeiZhu = Jih.JiHuaMingChen,
                        RiQi = item.RQ,
                        ShiJianDuan = item.SD,
                        ShiJianMing = SJMing,
                        ZhuangTai = "待上课",
                        JiaoYuanBianHao = TeacherID.ToString(),
                        JiaoYuanMingChen = TeacherName,
                        ShiShiShiJian = item.RQ.ToString(),
                        ZhangJieBianHao = ZJID.ToString(),
                        ZhangJieMingChen = ZJName,
                        KeChengBianHao = KCID.ToString(),
                        KeChengMingChen = KCName,
                        ZiYuanBianHao = ZY.ID.ToString(),
                        ZiYuanMingChen = item.JS,
                        ClassName = item.BJ,
                    };
                    bLL.Add(yiCiPaiKeDeKeBiaoXinXi);
                }
            }
            catch (Exception ex)
            {
                return Json(new { errorInfo = "没有可安排排课的资源" + ex + "", errorNo = "0", }, JsonRequestBehavior.AllowGet);
            }
            ShangKeBiaoBLL.Add();
            return Json(new { errorInfo = "设置本次自习课完成", errorNo = "0", }, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region  导出课表
        public ActionResult ExceFill()
        {
            //创建一个ExCel文件的对象
            NPOI.HSSF.UserModel.HSSFWorkbook book = new NPOI.HSSF.UserModel.HSSFWorkbook();
            //获取集合的数据
            List<ZhengZaiShangKeBiao> LIst = ShangKeBiaoBLL.GetAllData().OrderBy(p => p.RiQi).ToList();
            //添加1个Sheet
            NPOI.HSSF.UserModel.HSSFSheet sheet = (NPOI.HSSF.UserModel.HSSFSheet)book.CreateSheet("Sheet1");
            //给sheet添加第一行的头部标题；获取工作表的第一行
            NPOI.HSSF.UserModel.HSSFRow row1 = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(0);
            row1.CreateCell(0).SetCellValue("班级名称");
            row1.CreateCell(1).SetCellValue("教室");
            row1.CreateCell(2).SetCellValue("时间");
            row1.CreateCell(3).SetCellValue("时间段");
            row1.CreateCell(4).SetCellValue("阶段");
            row1.CreateCell(5).SetCellValue("教员名称");
            row1.CreateCell(6).SetCellValue("课程");
            //将数据逐步写入到Sheet各个行
            for (int i = 0; i < LIst.Count; i++)
            {
                //根据班级名查询出班级阶段
                //int c;
                //if (LIst[i].JiaoYuanMingChen == "未安排")
                //{
                //    c = 0;
                //}
                //else
                //{
                //    c = Convert.ToInt32(LIst[i].JiaoYuanBianHao);
                //}
                //var a = yuan.GetAllData().Where(p => p.ID == c).ToList();
                var ClassJD = BanJiBiaoBLL.GetAllData().Where(p => p.BanJiMing == LIst[i].ClassName).First();
                //var JD = JiaoXueJieDuanBLL.GetAllData().Where(p => p.ID == ClassJD.JieDuanID).First();
                //继续构造excel的数据行
                NPOI.HSSF.UserModel.HSSFRow rowtemp = (NPOI.HSSF.UserModel.HSSFRow)sheet.CreateRow(i + 1);
                var Class = KeChengJiHuaBiaoBLL.GetAllData().Where(p => p.BanJiMing == LIst[i].ClassName).First();
                //var JD = JiaoXueJieDuanBLL.GetAllData().Where(p => p.ID == Class.KaiSheJiaoXueJieDuan).First();
                rowtemp.CreateCell(0).SetCellValue(LIst[i].ClassName.ToString());
                rowtemp.CreateCell(1).SetCellValue(LIst[i].ZiYuanMingChen.ToString());
                rowtemp.CreateCell(2).SetCellValue(Convert.ToDateTime(LIst[i].RiQi).ToString("yyyy-MM-dd"));
                rowtemp.CreateCell(3).SetCellValue(LIst[i].ShiJianDuan.ToString());
                rowtemp.CreateCell(4).SetCellValue(1);
                rowtemp.CreateCell(5).SetCellValue(LIst[i].JiaoYuanMingChen);
                rowtemp.CreateCell(6).SetCellValue(LIst[i].KeChengMingChen.ToString());
                //继续添加列数据
            }
            //写入到客户端
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            //Excel写入到内存中
            book.Write(ms);
            //从0位置读到结束
            ms.Seek(0, System.IO.SeekOrigin.Begin);
            Message();
            //读取内存流中的二进制
            byte[] bytes = ms.ToArray();
            ms.Close();
            ms.Dispose();
            OutputCilent(bytes);
            return Json(new { errorNo = "0", errorInfo = "查询成功" }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 生成exce文件
        public void OutputCilent(byte[] bytes)
        {
            HttpResponse response = System.Web.HttpContext.Current.Response;
            response.Buffer = true;
            response.Clear();
            response.ClearHeaders();
            response.ClearContent();
            response.ContentType = "application/vnd.ms-excel";
            response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.xls", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")));
            response.Charset = "GB2312";
            response.ContentEncoding = Encoding.GetEncoding("GB2312");
            response.BinaryWrite(bytes);
            response.Flush();
            response.Close();

        }
        #endregion

        #region 课表完成发送message
        public ActionResult Message()
        {
            var TeacherList = yuan.PhoneList();

            string returnMessage = string.Empty;
            foreach (var item in TeacherList)
            {
                IAppConfig appConfig = new MessageConfig("26920", "d22f4cccf0441fcafcab541dd7d6e646", SignType.md5);
                MessageSend messageSend = new MessageSend(appConfig);
                messageSend.AddTo(item.Phone);
                messageSend.AddContent("【智联科技有限公司】尊敬的" + item.Name + "(先生),本周的课表已出,请去官网or手机端进行查看。生话愉快。");
                messageSend.AddTag("xxxxx");
                messageSend.Send(out returnMessage);
            }
            return Json(new { errorNo = "0", errorInfo = returnMessage }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 课表调课之后发送message
        public ActionResult Message2()
        {
            var TeacherList = yuan.PhoneList();

            string returnMessage = string.Empty;
            foreach (var item in TeacherList)
            {
                IAppConfig appConfig = new MessageConfig("26920", "d22f4cccf0441fcafcab541dd7d6e646", SignType.md5);
                MessageSend messageSend = new MessageSend(appConfig);
                messageSend.AddTo(item.Phone);
                messageSend.AddContent("【智联科技有限公司】尊敬的" + item.Name + "(先生),本周的课表已修改,请去官网,140.143.54.130,or手机端,140.143.54.130:100进行查看。生话愉快。");
                messageSend.AddTag("xxxxx");
                messageSend.Send(out returnMessage);
            }
            return Json(new { errorNo = "0", errorInfo = returnMessage }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 改为上课
        //改为上课
        public ActionResult EditKeBiao(int ID)
        {
            ZhengZaiShangKeBiao one = ShangKeBiaoBLL.GetAllData().Where(p => p.ID == ID).First();
            var TeacherName = bLL.GetAllData().Where(p => p.ClassName == one.ClassName && p.KeChengMingChen != "自习").First();
            var KC = bLL.GetAllData().OrderByDescending(p => p.ZhangJieBianHao).Where(P => P.ClassName == one.ClassName && P.KeChengMingChen != "自习").First();
            //查询本班教员本时段是否已排课
            var count = bLL.GetAllData().Where(p => p.JiaoYuanMingChen == TeacherName.JiaoYuanMingChen && p.RiQi == one.RiQi && p.ShiJianDuan == one.ShiJianDuan).Count();
            if (count != 0)
            {
                return Json(new { errorNo = "1", errorInfo = "本班教员今天已排满课,不能进行更改" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int ZJID = Convert.ToInt32(KC.ZhangJieBianHao);
                var a = KeChengJiHuaBiaoBLL.NextKC(ZJID + 1, one.ClassName);
                var Teacher = yuan.GetAllData().Where(p => p.ID == a.AnPaiJiaoYuan).First();
                var KCName = JiaoXueKeChengBLL.GetAllData().Where(p => p.ID == a.KeChengMing).First();
                one.KeChengBianHao = a.KeChengMing.Value.ToString();
                one.KeChengMingChen = KCName.KeChengMing;
                one.ZhangJieMingChen = a.ZhangJieMingChen;
                one.ZhangJieBianHao = a.ZhangJieBianHao.Value.ToString();
                one.JiaoYuanBianHao = a.AnPaiJiaoYuan.Value.ToString();
                one.JiaoYuanMingChen = Teacher.JiaoYuanMingChen;
                ShangKeBiaoBLL.Modify(one);
                //Message2();
                return Json(new { errorNo = "0", errorInfo = "安排上课成功" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 班级排课形式
        //班级排课形式
        public class JSPJJY
        {
            public string JS { get; set; }

            public string JY { get; set; }

            public string PBS { get; set; }

            public string XD { get; set; }

            public string BJ { get; set; }

            public int XDIndex { get; set; }
        }
        #endregion

        #region 课表
        //课表
        public class KB
        {
            //日期
            public DateTime RQ { get; set; }
            //时段
            public string SD { get; set; }
            //教室
            public string JS { get; set; }
            //班级
            public string BJ { get; set; }
            //教员
            public string JY { get; set; }
            //课程
            public string KC { get; set; }
        }
        #endregion

        #region 教员代课View
        public ActionResult TeacherDaiKe(int ID)
        {

            ZhengZaiShangKeBiao one = ShangKeBiaoBLL.GetAllData().Where(p => p.ID == ID).First();
            //根据时间查询本时段未上课的教员
            var TeacherName = bLL.GetAllData().Where(p => p.ClassName == one.ClassName && p.KeChengMingChen != "自习").First();
            var Teacher = yuan.GetAllData().Where(p => p.JiaoYuanMingChen == TeacherName.JiaoYuanMingChen).First();
            List<YuanGongBiao> yuans = yuan.DaiKeTeacher(one.RiQi.Value, Teacher.ShanChangKeCheng).OrderByDescending(p => p.ID).ToList();
            string Msg = "";
            if (yuans.Count == 0 || yuans == null)
            {
                ViewBag.One = one;
                ViewBag.TeacherList = yuans;
                Msg = "无可安排代课的教员";
            }
            else
            {
                ViewBag.One = one;
                ViewBag.TeacherList = yuans;
                Msg = "可安排";
            }
            ViewBag.Msg = Msg;
            return View();
        }
        #endregion

        #region 教员代课Action
        public ActionResult UpdateTeacher(ZhengZaiShangKeBiao yiCiPaiKeDeKeBiaoXinXi)
        {
            var One = ShangKeBiaoBLL.GetAllData().Where(p => p.ID == yiCiPaiKeDeKeBiaoXinXi.ID).First();
            var Teacher = yuan.GetAllData().Where(p => p.ID == Convert.ToInt32(yiCiPaiKeDeKeBiaoXinXi.JiaoYuanBianHao)).First();
            One.JiaoYuanBianHao = yiCiPaiKeDeKeBiaoXinXi.JiaoYuanBianHao;
            One.BeiZhu = "因教员" + One.JiaoYuanMingChen + "有事,安排" + Teacher.JiaoYuanMingChen + "来代课";
            One.JiaoYuanMingChen = Teacher.JiaoYuanMingChen;
            One.CanJiaRenYuan = Teacher.JiaoYuanMingChen;
            JiaoYuanDaiKeBiao jiaoYuanDaiKe = new JiaoYuanDaiKeBiao
            {
                JiaoYuanMingChen = yiCiPaiKeDeKeBiaoXinXi.JiaoYuanMingChen,
                KeChengBianHao = One.KeChengBianHao,
                BeiZhu = One.BeiZhu,
                CanJiaRenYuan = One.CanJiaRenYuan,
                JiaoYuanBianHao = yiCiPaiKeDeKeBiaoXinXi.JiaoYuanBianHao,
                KeChengMingChen = One.KeChengMingChen,
                RiQi = One.RiQi,
                ShiJianDuan = One.ShiJianDuan,
                ShiJianMing = One.ShiJianMing,
                ShiShiShiJian = One.ShiShiShiJian,
                ZhangJieBianHao = One.ZhangJieBianHao,
                ZhangJieMingChen = One.ZhangJieMingChen,
                ZhuangTai = One.ZhuangTai,
                ClassName=One.ClassName,
                ZiYuanBianHao = One.ZiYuanBianHao,
                ZiYuanMingChen = One.ZiYuanMingChen
            };
            DaiKeBiaoBLL.Add(jiaoYuanDaiKe);
            Message2();
            return Json(new { errorNo = "0", errorInfo = "安排教员代课完成" }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }


}
