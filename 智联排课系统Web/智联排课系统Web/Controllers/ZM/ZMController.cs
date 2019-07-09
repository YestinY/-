using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.ZM;
using Models;
using Models.ZM;
using NPOI.HSSF.UserModel;

namespace 智联排课系统Web.Controllers
{
    public class ZMController : Controller
    {
        //
        // GET: /ZM/
        MJiaoXueJieDuanBiaoBLL jxjd = new MJiaoXueJieDuanBiaoBLL();
        MXueShengBiaoBLL xsb = new MXueShengBiaoBLL();
        MBanJiBiaoBLL bjb = new MBanJiBiaoBLL();
        MBanZhuRenSuoDaiBanJiBiaoBLL bbb = new MBanZhuRenSuoDaiBanJiBiaoBLL();


        /// <summary>
        /// 学生信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ZMIndex()
        {
            List<JiaoXueJieDuanBiao> gradeList = jxjd.GetAll();
            return View(gradeList);
        }

        #region 学生显示
        public ActionResult ZMGetStudentAjax(string xm, int? sex, int? XSGrade, string XSDZ,int? zt,
                  int? pageNum, int? pageSize)
        {
            // 条件
            int xb = sex ?? -1;//为空为-1
            int nj = XSGrade ?? -1;//年级没有值使用-1

            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数
            int ZhuangTai = zt ?? -1;

            int tot = 0;//页码总数
            /////////先按数据获取数据///////////////////////////////////////////

            List<Student> studentList = xsb.GetStudentALL(ZhuangTai,xm, nj, XSDZ, null, xb, pindex, size, out tot);//查询学员的功能
            var show = studentList.ToList().Select(p => new { p.HomePhone, p.XID, p.Phone, p.BanJiMing, p.Age, p.Address, p.JieDuanMing, p.StudentName, Sex = p.Sex == true ? "男" : "女" });

            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//总页码
                        list = show //数据集合
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 增加 [第一步:]
        //增加页面
        //增加学生时，先判断最新的班级是否满员
        //如果满员，提示，请先增加班级
        //不满员，则新增学生信息
        public ActionResult ZMAdd()
        {
            //BjRenShu n = bjb.GetRenShu();
            //if (n != null)
            //{
            //    if (n.banjirenshu < 30)
            //    {
            //        //已有班级，增加学生
            //        string bj = n.BanJiMing;
            //        ViewBag.Name = bj;
            //        return View();
            //    }
            //}
            //return Json(new { errorNo = "1", errorInfo = "请先新增班级" }, JsonRequestBehavior.AllowGet);
            List<BanJiBiao> bjList = bjb.bjList();
            return View(bjList);
        }
        ////增加操作
        //增加学生时，做判断,判断学生添加所在班级是否满员
        //添加学生时防止同时添加多条信息
        public ActionResult ZMAddAction(Student st)
        {
            XueShengBiao xs = new XueShengBiao();
            xs.StudentName = st.StudentName;
            xs.Age = st.Age;
            xs.Address = st.Address;
            xs.Phone = st.Phone;
            xs.HomePhone = st.HomePhone;
            if (st.Sex != true)
            {
                st.Sex = false;
            }
            xs.Sex = st.Sex;
            xs.MiMa = "123456";
            xs.StudentClassID = Convert.ToInt16(st.BanJiMing);

            int n = xsb.Add(xs);

            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }

        }
        #endregion

        public ActionResult StuAdd()
        {
            //BjRenShu n = bjb.GetRenShu();
            //if (n != null && n.BanJiMing != null)
            //{
            //    if (n.banjirenshu < 30)
            //    {
            //        //已有班级，增加学生
            //        string bj = n.BanJiMing;
            //        ViewBag.Name = bj;
            //        return View();
            //    }
            //}
            //return Json(new { errorNo = "1", errorInfo = "请先新增班级" }, JsonRequestBehavior.AllowGet);
            List<BanJiBiao> bjList = bjb.bjList();
            return View(bjList);
        }

        #region  Add学员信息
        /// <summary>
        /// Add学员信息
        /// </summary>
        /// <param name="BanJiMing">班级名</param>
        /// <param name="filePath">文件</param>
        /// <returns></returns>
        public ActionResult StuAddAction(string BanJiMing, string filePath)
        {
            int BanJiID = Convert.ToInt16(BanJiMing);
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    string path = AppDomain.CurrentDomain.BaseDirectory + @"/upload";
                    //保存文件的真实文件名，和全路径;
                    string savepath = Server.MapPath("~/upload/" + filePath);//上传后的文件名
                    //判断是否存在此路径
                    if (!Directory.Exists(path))
                    {
                        //创建文件夹
                        Directory.CreateDirectory(path);
                    }
                    //创建一个WorkBook对象;
                    HSSFWorkbook hSSFWorkbook;
                    using (FileStream filel = new FileStream(savepath, FileMode.Open, FileAccess.Read))
                    {
                        hSSFWorkbook = new HSSFWorkbook(filel);
                    }
                    //获取工作簿的第一个工作表
                    var sheep = hSSFWorkbook.GetSheetAt(0);
                    //获取所有的行
                    System.Collections.IEnumerator rows = sheep.GetRowEnumerator();
                    //获取标题行
                    var headerRow = sheep.GetRow(0);
                    int cellCount = headerRow.LastCellNum;

                    List<XueShengBiao> stu = new List<XueShengBiao>();

                    for (int i = (sheep.FirstRowNum + 1); i < sheep.LastRowNum; i++)
                    {
                        var row = sheep.GetRow(i);
                        XueShengBiao stu1 = new XueShengBiao
                        {
                            StudentName = (row.GetCell(1).ToString()),
                            StudentClassID = BanJiID,
                            Sex = Convert.ToInt32(row.GetCell(2).ToString()) == 0 ? false : true,
                            Age = Convert.ToInt32(row.GetCell(3).ToString()),
                            MiMa = row.GetCell(4).ToString(),
                            Address = row.GetCell(5).ToString(),
                            Phone = row.GetCell(6).ToString(),
                            HomePhone = row.GetCell(7).ToString(),
                            ZhuangTai = 1
                        };
                        //把对象加入到集合
                        stu.Add(stu1);
                    }
                    bool error = xsb.StuAdd(stu);
                    if (error)
                    {
                       return Json(new { errorNo = "0", errorInfo = "导入学员信息成功" });
                    }
                    else
                    {
                        return Json(new { errorNo = "1", errorInfo = "导入学员信息失败" });
                    }
                }
                catch (Exception ex)
                {
                    return Json(new { errorNo = "1", errorInfo = ex.Message });
                    throw ex;
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "无上传文件,操作错误" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 退学
        public ActionResult TuiStu(int XID)
        {
            int n = xsb.TuiStu(XID);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行退学成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改操作
        //修改页面
        public ActionResult ZMEdit()
        {
            return View();
        }
        ////查询数据
        public ActionResult ZMEditGetData(int XID)
        {
            //获取到一个对象
            var yp = xsb.GetOneInfo(XID);
            //转化为json处理主处键导航属性[不要导航属性]
            var showyp = new { yp.XID, yp.StudentName, yp.Sex, yp.Age, yp.Address, yp.Phone, yp.HomePhone, yp.StudentClassID, yp.BanJiMing };
            var showJsondata = new
            {
                errorNo = "0",
                errorInfo = "执行成功",
                results = new { data = showyp }
            };
            return Json(showJsondata, JsonRequestBehavior.AllowGet);

        }
        // 修改时，提交
        public ActionResult ZMEditAction(Student st)
        {
            XueShengBiao xs = new XueShengBiao();
            xs.StudentName = st.StudentName;
            xs.Age = st.Age;
            xs.Address = st.Address;
            xs.Phone = st.Phone;
            xs.HomePhone = st.HomePhone;
            xs.ID = st.XID;
            if (st.Sex != true)
            {
                st.Sex = false;
            }
            xs.Sex = st.Sex;

            int n = xsb.UpdateXS(xs);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 增加班级
        public ActionResult AddBanZhuRen()
        {
            //先判断是否有班级没满人，满人是成立，还有空余时，提示有班级
            //BjRenShu n = bjb.GetRenShu();
            //if (n == null && n.banjirenshu >= 30 || n.BanJiMing == null)
            //{
                string strdate = DateTime.Now.ToString("yyyy");
                string BanJiMing = bjb.GetNewClassHao(strdate);
                ViewBag.Name = BanJiMing;
                return View();
            //}
            //return Json(new { errorNo = "1", errorInfo = "还有空余班级" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddBanZhuRenAction(BanJiBanZhuRen bb)
        {
            List<BanJiBiao> bj = bjb.GetPD(bb.BanJiMing);
            if (bj.Count() == 0)
            {
                int n = bjb.biao(bb.BanJiMing, bb.BanJiBeiZhu, 0, bjb.jd(1).ID);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行失败" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "班级名重复，执行失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 留级
        public ActionResult LiuJi(int XID)
        {
            Student stu = xsb.GetOneInfo(XID);
            string Name = stu.StudentName;
            ViewBag.Name = Name;
            ViewBag.XID = stu.XID;
            int id = bjb.jd(1).ID;
            if (stu.JieDuanID == id)
            {
                return Json(new { errorNo = "1", errorInfo = "不可进行此操作" }, JsonRequestBehavior.AllowGet);
            }
            int bjID = Convert.ToInt32(stu.StudentClassID);
            int jdID = stu.JieDuanID;

            List<BanJiBiao> bjList = bjb.GetBJ(bjb.njID(jdID), bjID);
            ViewBag.BanJiMingID = bjb.JieDuan(bjID).ID;
            ViewBag.BanJiMing = bjb.JieDuan(bjID).BanJiMing;
            return View(bjList);
        }
        #endregion

        #region 换班
        public ActionResult HuanBan(int XID)
        {
            Student stu = xsb.GetOneInfo(XID);
            string Name = stu.StudentName;
            ViewBag.Name = Name;
            ViewBag.XID = stu.XID;
            int bjID = Convert.ToInt32(stu.StudentClassID);
            int jdID = stu.JieDuanID;
            List<BanJiBiao> bjList = bjb.GetBJ(jdID, bjID);
            ViewBag.BanJiMingID = bjb.JieDuan(bjID).ID;
            ViewBag.BanJiMing = bjb.JieDuan(bjID).BanJiMing;
            return View(bjList);
        }

        public ActionResult HuanAction(Student bj,string YuanBanJiID)
        {
            int banjiMing = Convert.ToInt32(bj.BanJiMing);
            string name = bj.StudentName;
            int xid = bj.XID;
            int n = xsb.HuanBan(name, banjiMing, xid, YuanBanJiID);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行换班成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        /// <summary>
        /// 班级信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BJIndex()
        {

            List<JiaoXueJieDuanBiao> gradeList = jxjd.GetAll();

            return View(gradeList);
        }

        #region 班级信息显示
        public ActionResult GetGrade(string BanJiMing, int? JieDuanMing, int? BanJiZhuangTai, int? pageNum, int? pageSize)
        {
            // 条件
            int jdID = JieDuanMing ?? -1;
            int bjztID = BanJiZhuangTai ?? -1;
            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数

            int tot = 0;//页码总数
            /////////先按数据获取数据/////////////////////

            List<BanJiBiao> GradeList = bjb.GetGradeALL(BanJiMing, jdID, bjztID, pindex, size, out tot);//查询班级的功能

            var show = GradeList.ToList().Select(p => new { p.ID, p.BanJiMing,  p.KaiBanShiJian, p.BanJiRenShu, p.JieDuanID,p.BanJiZhuangTai,p.JiaoXuePlan });

            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//总页码
                        list = show //数据集合
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 根据班级查询学生
        public ActionResult BjStu(int ID)
        {
            ViewBag.BjID = ID;
            return View();
        }

        public ActionResult BjStuShow(string BjID, int? pageNum, int? pageSize)
        {
            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数

            int tot = 0;//页码总数
            /////////先按数据获取数据///////////////////////////////////////////

            List<Student> studentList = xsb.GetStudentALL(1,null, -1, null, BjID, -1, pindex, size, out tot);//查询学员的功能
            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//总页码
                        list = studentList //数据集合
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改操作
        public ActionResult BjEdit(int ID)
        {
            BanJiBiao nj = bjb.JieDuan(ID);
            ViewBag.Name = nj.BanJiMing;
            ViewBag.ID = nj.ID;
            ViewBag.BeiZhu = nj.BeiZhu;
            return View();
        }

        // 修改时，提交
        public ActionResult BjEditAction(BanJiBanZhuRen st)
        {
            int n = bjb.UpdateBanJi(st.BanJiBianHao, st.BanJiBeiZhu);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行修改成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 升阶段
        public ActionResult SoY(int ID)
        {
            int BanJiID = ID;
            BanJiBiao nj = bjb.JieDuan(BanJiID);
            int id = bjb.jd(-1).ID;
            if (nj.JieDuanID < id && nj.JieDuanID > 0)
            {
                string BanJiMing = null;
                string strdate = DateTime.Now.ToString("yyyy");
                BanJiMing = bjb.GetGradeID(strdate,bjb.njID1(Convert.ToInt32(nj.JieDuanID)), BanJiID);
                ViewBag.Name = BanJiMing;
                ViewBag.ID = BanJiID;
                return View();
            }
            int n = bjb.BiYe(BanJiID);
            return Json(new { errorNo = "0", errorInfo = "毕业成功" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShengJieDuanAction(BanJiBanZhuRen bb)
        {
            BanJiBiao bj1 = bjb.JieDuan(bb.BanJiBianHao);
            int jieduanID = Convert.ToInt32(bj1.JieDuanID) + 1;
            int RenShu = Convert.ToInt32(bj1.BanJiRenShu);
            //新增升阶段后班级
            int n = bjb.biao(bb.BanJiMing, bb.BanJiBeiZhu, RenShu, jieduanID);
            //修改学生的班级ID
            bjb.UpdateStu(bb.BanJiBianHao);
            //修改原班级状态
            bjb.UpdateZT(bb.BanJiBianHao);
            //新增学生与班级关联表
            bjb.AddUP();
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行失败" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
