using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL;
using Models;
using BLL.ZM;
using BLL.CHB;


namespace 智联排课系统Web.Controllers
{
    public class YuanGongController : Controller
    {
        //
        // GET: /YuanGong/

       

        BLL.ZM.BuMenBiaoBLL bm = new BLL.ZM.BuMenBiaoBLL();

        BLL.CHB.YuanGongBiaoBLL b = new BLL.CHB.YuanGongBiaoBLL();

        BLL.YuanGongBiaoBLL YuanGongBiaoBLL = new BLL.YuanGongBiaoBLL();

        public ActionResult Index()
        {
            List<ZhiWeiBiao> zw = bm.zw();
            return View(zw);
        }

        public ActionResult GetYG(string name, int? ZhiWeiMing, string Addr, int? zt, int? pageNum, int? pageSize)
        {
            //条件
            int xb = zt ?? -1;//为空为-1s
            int nj = ZhiWeiMing ?? -1;//年级没有值使用-1

            int pindex = pageNum ?? 1;//默认显示第一页
            int size = pageSize ?? 10;//每页条数

            int tot = 0;//页码总数
            ///////////先按数据获取数据///////////////

            List<YuanGongBiao> studentList = b.GetStudentByManyIf2_fxlayui(name, nj, Addr, xb, pindex, size, out tot);
            //查询学员的功能
            ///按框架结构要求响应json格式数据////////////////
            ///有数据返回建在data属性中///////////////////
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

        #region 增加 [第一步:]
        public ActionResult Add()
        {
            //List<YuanGongBiao> gradeList = b.GetAllData();
            return View();
        }
        /// <summary>
        /// ajax提交处理的action
        /// </summary>
        /// <returns></returns>
        public ActionResult AddAction(YuanGongBiao st, string ZhiWeiMing)
        {
            st.JiaoYuanMingChen = st.Name;
            if (ZhiWeiMing != null)
            {
                if (st.ZhiWeiID == null)
                {
                    st.ZhiWeiID = Convert.ToInt32(ZhiWeiMing);
                }
            }
            else
            {
                if (st.ZhiWeiID == null)
                {
                    return Json(new { errorNo = "1", errorInfo = "该部门还没有职位" }, JsonRequestBehavior.AllowGet);
                }
            }
            st.JiaoYuanMingChen = st.Name;
            st.MiMa = "123456";
            st.RuGangShiJian = DateTime.Now.ToString("yyyy-MM-dd");
            st.YuanGongZhuangTai = 1;

            int n = YuanGongBiaoBLL.Add(st);
            //--------------------处理成fslayui格式要求数据响应，----不要响应数据的构建方式------------------------------//
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

        #region 修改操作  代表有数据从列表项传到新窗口的显示(了解传参方式)
        //实现过程：1）先通过层打开一个内嵌的页面 ，可以同步实现生成部分动态内容
        //2)也可能通过异步获取数据，3）提交修改，为异步接收数据
        public ActionResult Edit()
        {
            List<ZhiWeiBiao> zwList = bm.zw();
            return View(zwList);
        }
        /// <summary>
        /// 修改时用来读取数据的Action方法,显示用来读取数据方法，异步返回数据
        /// </summary>
        /// <returns></returns>
        public ActionResult EditGetData(int ID)
        {
            //获取到一个对象
            var yp = YuanGongBiaoBLL.GetOneData(ID);
            //转化为json处理主处键导航属性[不要导航属性]
            var showyp = new { yp.ID, yp.Name, yp.YuanGongZhuangTai, yp.ZhiWeiID, yp.Address, yp.Phone, yp.ShanChangKeCheng,yp.BeiZhu };
            ////////////////一个对象的封装格式////////////////////////////
            var showJsondata = new
            {
                errorNo = "0",
                errorInfo = "执行成功",
                results = new { data = showyp }
            };
            return Json(showJsondata, JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// 修改时，提交
        /// </summary>
        /// <param name="st"></param>
        /// <returns></returns>
        public ActionResult EditAction(YuanGongBiao st,string YuanGongZhuangTai)
        {
            //调用修改操作
            if (YuanGongZhuangTai == "true")
            {
                st.YuanGongZhuangTai = 1;
            }
            else if (YuanGongZhuangTai == "false")
            {
                st.YuanGongZhuangTai = 2;
            }
            int n = b.Update(st);

            /////////////////////////////////
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

        #region 行后按钮操作  自动调用ajax功能
        public ActionResult Delete(int ID)
        {
            //int n = b.DeleteByPkey(ID);
            int j = bm.JiaoXue(Convert.ToString(ID));
            if (j == 0)
            {
                int n =bm.LiZhi(ID,2);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "该员工正在教学" }, JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult RenZhi(int ID)
        {
            //int n = b.DeleteByPkey(ID);
           
                int n = bm.LiZhi(ID,1);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "执行删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
          
        }
        #endregion

        #region 批量删除，代表批量操作的实现方法ajax
        public ActionResult DeleteManyId(string ID)
        {

            int d = b.DeleteByPkey(ID);
            if (d > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "删除多条执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "删除执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion

        #region 下拉框使用级联实现：显示一级部门
        public ActionResult YiBuMen1()
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
        public ActionResult ErZhiWEi(int id)
        {
            var stulist = bm.zwToList(id).Select(q => new { name = q.ZhiWeiMing, id = q.ID }).ToList();
            var rJson = new
            {
                errorNo = "0",
                results = new { data = stulist }
            };
            return Json(rJson, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
