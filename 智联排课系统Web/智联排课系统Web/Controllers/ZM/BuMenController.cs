using BLL.ZM;
using Models;
using Models.ZM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using 智联排课系统Web.Models;

namespace 智联排课系统Web.Controllers
{
    public class BuMenController : Controller
    {
        //
        // GET: /BuMen/
        public BLL.CHB.BuMenBiaoBLL A = new BLL.CHB.BuMenBiaoBLL();
        public BLL.CHB.YuanGongBiaoBLL B = new BLL.CHB.YuanGongBiaoBLL();
        public BLL.ZM.BuMenBiaoBLL bm = new BLL.ZM.BuMenBiaoBLL();
        public ActionResult Index()
        {
            return View();
        }

        //树控件加载数据方法
        public ActionResult CreateTreeData()
        {
            //1.查询数据库所有数据
            List<BuMenBiao> deplist = A.GetAll();
            //2.转成fslayui所需要格式
            List<智联排课系统Web.Models.TreeNode> treenodeList = new List<智联排课系统Web.Models.TreeNode>();
            foreach (var dep in deplist)
            {
                Models.TreeNode trnode = new Models.TreeNode
                {
                    id = dep.ID + "",//节点编号
                    pId = dep.DuiYingFuJiID + "",//父节点编号
                    isParent = false,//是不是父节点
                    open = false,//展开状态
                    name = dep.BuMenMingCheng//部门名
                };
                //加入到集合中
                treenodeList.Add(trnode);
            }
            //3.作出响应,构建响应格式
            var jsondata = new
            {
                errorNo = 0,
                results = new
                {
                    data = treenodeList
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        #region 表格查询，通过部门查询员工
        //表格查询，通过部门查询员工
        public ActionResult GetEmpData(string menuId, int? pageNum, int? pageSize)
        {
            //1.menuId框架定义的传递参数，值为部门的id即  D1_2 正常程序需要
            //查询出该部门及其下级部门的员工
            //实现步骤：查询出所有员工，根据部分筛选
            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数
            //List<YGB> empTabList = B.GetAll();//所有员工
            List<YGB> empTabList1 = bm.YGShow();
            List<YGB> empTabList = bm.YGShow();
            if (menuId != null && menuId != "" && menuId != "0") //按部门查询员工2
            {
                empTabList = empTabList1.Where(p => p.DuiYingFuJiID == Convert.ToInt32(menuId)).ToList();
                if (empTabList.Count() == 0)
                {
                    empTabList = empTabList1.Where(p => p.BuMenID == Convert.ToInt32(menuId)).ToList();//以什么开头
                }
            }
            ///////使用分页///////////////
            int tot = 0;//页码总数
            tot = empTabList.Count();//数据条数即可
            int n = (pindex - 1) * size;
            var showlist = empTabList.Skip(n).Take(size).ToList();
            ////////////////////////
            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//
                        list = showlist.Select(q =>
                        {
                            return new { q.ID, q.Name, q.Phone, q.ZhiWeiMing, q.BuMenMingCheng, q.BuMenID,q.YuanGongZhuangTai };
                        }) //数据集合
                    }
                }

            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        ///// <summary>
        ///// 通过部门名得到全路径
        ///// </summary>
        ///// <param name="bmname"></param>
        ///// <returns></returns>
        //public string GetBMFullName(string bmname)
        //{
        //    //利用部门查询部分的路径
        //    string fullnameString = depBLL.GetOne(bmname).DepPath;
        //    //利用中间分隔符隔开
        //    string[] depArrs = fullnameString.Split(new char[] { '-' });
        //    string tempString = "";
        //    foreach (string strname in depArrs)
        //    {
        //        if (strname != "0")
        //        {
        //            tempString += depBLL.GetOne(strname).DepName + ".";
        //        }
        //    }
        //    return tempString;
        //}
        #endregion

        public ActionResult Add(int menuId)
        {
            List<ZhiWeiBiao> zwList = bm.zwToList(menuId);
            return View(zwList);
        }


        //增加部门
        public ActionResult AddBM()
        {
            List<YuanGongBiao> YGList = bm.YGToList();
            return View(YGList);
        }

        #region 下拉框使用级联实现：显示一级部门
        public ActionResult YiBuMen()
        {
            List<BuMenBiao> jxlist = bm.BMToList();
            var showlist = jxlist.Select(p => new { name = p.BuMenMingCheng, id = p.ID }).ToList();
            var rJson = new
            {
                errorNo = "0",
                results = new { data = showlist }
            };
            return Json(rJson, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region 根据一级部门查询二级部门
        public ActionResult ErBuMen(int id)
        {
            //var stulist = bm.zwToList(id).Select(q => new { name = q.ZhiWeiMing, id = q.ID }).ToList();
            var stulist = bm.BMToList().Where(p => p.DuiYingFuJiID != p.ID && p.DuiYingFuJiID == id).Select(q => new { name = q.BuMenMingCheng, id = q.ID }).ToList();
            var rJson = new
            {
                errorNo = "0",
                results = new { data = stulist }
            };
            return Json(rJson, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 增加部门
        public ActionResult AddAction(string name, string BuMenMingCheng, string BuMenMingCheng1, int Phone)
        {
            string DuiYingFuJiID = BuMenMingCheng;
            if (BuMenMingCheng1 != "")
            {
                DuiYingFuJiID = BuMenMingCheng1;
            }
            BuMenBiao bumen = new BuMenBiao
            {
                BuMenMingCheng = name,
                BuMenFuZheRen = null,
                ChengLiTime = DateTime.Now,
                DuiYingFuJiID = (DuiYingFuJiID == "" ? 0 : Convert.ToInt32(DuiYingFuJiID)),
                Phone = Phone,
                ShiFouQiYong = true
            };
            int i = bm.BMToList().Where(p => p.BuMenMingCheng == BuMenMingCheng).ToList().Count();
            if (i == 0)
            {
                int n = bm.AddBM(bumen);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "该部门已存在" }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region 修改部门
        //修改部门
        public ActionResult UpdateBM(string menuId, string ID)
        {
            if (menuId == null)
            {
                menuId = ID;
            }
            var list = bm.BMToList().Where(p => p.ID == Convert.ToInt32(menuId)).First();
            ViewBag.id = list.ID;
            ViewBag.zt = list.ShiFouQiYong;
            ViewBag.Name = list.BuMenMingCheng;
            ViewBag.Phone = list.Phone;
            return View();
        }

        public ActionResult UpdateAction(int ID, string BuMenMingCheng, string Phone)
        {
            int n = bm.UpdateBM(ID, BuMenMingCheng, Phone);
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

        #region 删除部门
        public ActionResult delBM(int menuId)
        {
            List<YGB> ygList = bm.YGShow().Where(p => p.BuMenID == Convert.ToInt32(menuId)).ToList();
            List<BuMenBiao> bmList = bm.BMToList().Where(p => p.DuiYingFuJiID == Convert.ToInt32(menuId)).ToList();
            if (ygList.Count() == 0 && bmList.Count() < 1)
            {
                //int n = bm.delBM(menuId);
                int n = bm.JinYong(menuId,false);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        //增加职位
        #region 增加职位
        public ActionResult AddZhiWei(string menuId, string BuMenMingCheng)
        {
            if (menuId == null)
            {
                menuId = "1";
            }
            BLL.ZM.DiGuiBLL d = new BLL.ZM.DiGuiBLL();
            List<DG> dg = d.AddChildDept(Convert.ToInt32(menuId));
            return View(dg);
        }
        public ActionResult AddZW(ZhiWeiBiao t)
        {
            t.ShiFouQiYong = true;
            int n = bm.AddZW(t);
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

        #region 显示职位
        public ActionResult ZhiWei(string menuId)
        {
            ViewBag.id = menuId;
            return View();
        }

        public ActionResult AZhiWei(string menuId, int? pageNum, int? pageSize)
        {
            List<ZhiWeiBiao> zwList = bm.zwList(Convert.ToInt32(menuId));
            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数
            ///////使用分页///////////////
            int tot = 0;//页码总数
            tot = zwList.Count();//数据条数即可
            int n = (pindex - 1) * size;
            var showlist = zwList.Skip(n).Take(size).ToList();
            ////////////////////////
            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//总页码
                        list = showlist
                        //.Select(q =>
                        //{
                        //    return new { q.ID, q.ZhiWeiMing, BuMenMingCheng = bm.BMToList().Where(p => p.ID == q.BuMenID).First(), q.ShiFouQiYong };
                        //}) //数据集合
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 修改职位
        public ActionResult UpdateZW(int ID)
        {
            ZhiWeiBiao zw = bm.zwShow(ID).First();
            ViewBag.Name = zw.ZhiWeiMing;
            ViewBag.BeiZhu = zw.BeiZhu;
            ViewBag.id = zw.ID;
            return View();
        }
        public ActionResult UpdateZhiWei(int ID, string ZhiWeiMing, string BeiZhu)
        {
            int n = bm.UpdateZW(ID, ZhiWeiMing, BeiZhu);
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

        #region 删除职位
        public ActionResult DelZW(int ID)
        {
            int num = bm.YGNum(ID);
            if (num == 0)
            {
                int n = bm.delZW(ID);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else { return Json(new { errorNo = "1", errorInfo = "不能进行" }, JsonRequestBehavior.AllowGet); }
        }
        #endregion

        private BMBLL BB = new BMBLL();
        //部门信息
        public ActionResult BuMenIndex()
        {
            return View();
        }
        //部门显示
        public ActionResult GetBM(string name, int? zt, int? pageNum, int? pageSize)
        {
            int Type = zt ?? -1;
            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;
            //页码总数
            List<BuMenBiao> deplist = A.GetAll();
            List<BuMenBiao> studentList = BB.GetBM(name, Type, pindex, size, out int tot);
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
        //增加负责人
        public ActionResult AddPeo(int ID)
        {
            var list = bm.BMToList().Where(p => p.ID == Convert.ToInt32(ID)).First();
            ViewBag.Name = list.BuMenMingCheng;
            ViewBag.FZR = "<--不限-->";
            ViewBag.FZRid = "-1";
            if (list.BuMenFuZheRen != null)
            {
                var ygL = BB.YGToList().Where(p => p.ID == Convert.ToInt32(list.BuMenFuZheRen)).First();
                ViewBag.FZR = ygL.Name;
                ViewBag.FZRid = ygL.ID;
            }
            List<YGB> yg = BB.ygName(ID);
            return View(yg);
        }

        public ActionResult UpdateFuZeRen(int ID, string BuMenMingCheng, string BuMenFuZheRen)
        {
            int n = BB.AddFZR(ID, BuMenFuZheRen);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }


        #region 删除部门
        public ActionResult delBuMen(int ID)
        {
            List<YGB> ygList = bm.YGShow().Where(p => p.BuMenID == Convert.ToInt32(ID)).ToList();
            List<BuMenBiao> bmList = bm.BMToList().Where(p => p.DuiYingFuJiID == Convert.ToInt32(ID)).ToList();
            if (ygList.Count() == 0 && bmList.Count() < 1)
            {
                //int n = bm.delBM(ID);
                int n = bm.JinYong(ID, false);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult qiyong(int ID)
        {
            int n = bm.JinYong(ID, true);
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


    }
}