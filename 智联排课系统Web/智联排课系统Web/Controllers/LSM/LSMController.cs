using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BLL.LSM;

namespace 智联排课系统Web.Controllers
{
    public class LSMController : Controller
    {
        //
        // GET: /LSM/
        LSMjiaoxuejieduanBLL bll = new LSMjiaoxuejieduanBLL();

        public ActionResult Index()
        {
            return View();
        }

        #region 异步查询教学阶段[条件查询]
        public ActionResult Getjiaoxuejieduan(int? id, string name, int? shenhe, int? qiyong, int? pagenum, int? pagesize)
        {
            int ID = id ?? -1;  //为空为-1
            int ShenHe = shenhe ?? -1;  //没有审核使用-1
            int QiYong = qiyong ?? -1;  //没有启用使用-1
            int pindex = pagenum ?? 1;  //默认显示第一页
            int size = pagesize ?? 10;  //每页条数
            int tot = 0;  //页码总数

            List<JiaoXueJieDuanBiao> lists = bll.GetjieduanByLike(ID, name, ShenHe, QiYong, pindex, size, out tot);
            var jsondata = new
            {
                errorNo = "0", //错误编码
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = tot,//总页码
                        list = lists //数据集合
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 新增教学阶段
        public ActionResult 新增教学阶段()
        {
            return View();
        }

        //ajax提交处理的action
        public ActionResult AddAction(JiaoXueJieDuanBiao jxjd)
        {
            if (jxjd.ShenHeShiFouTongGuo == null)
            {
                jxjd.ShenHeShiFouTongGuo = false;
            }
            if (jxjd.ShiFouQiYong == null)
            {
                jxjd.ShiFouQiYong = false;
            }
            var jdm = bll.SelectGetName(jxjd.JieDuanMing);
            if (jdm == null)
            {
                int n = bll.Addjiaoxuejieduan(jxjd);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "教学阶段新增成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "教学阶段新增失败，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "此阶段名已经存在，请重新输入" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 修改教学阶段
        public ActionResult 修改教学阶段()
        {
            return View();
        }

        public static bool? A;
        //修改时用来读取数据的action方法，显示用来读取数据方法，异步返回数据
        public ActionResult UpdateGetData(int ID)
        {
            //获取到一个对象
            var dx = bll.GetOne(ID);
            //转换为json处理主外键导航属性
            var showdx = new { dx.ID, dx.JieDuanMing, dx.ShenHeShiFouTongGuo, dx.ShiFouQiYong };

            A = showdx.ShenHeShiFouTongGuo;
            //一个对象的封装格式
            var showjsondata = new
            {
                errorNo = "0",
                errorInfo = "教学阶段信息修改成功",
                results = new { data = showdx }
            };
            return Json(showjsondata, JsonRequestBehavior.AllowGet);
        }

        //修改时提交
        public ActionResult UpdateAction(JiaoXueJieDuanBiao jxjd)
        {
            if (jxjd.ShenHeShiFouTongGuo == null)
            {
                jxjd.ShenHeShiFouTongGuo = false;
            }
            if (jxjd.ShiFouQiYong == null)
            {
                jxjd.ShiFouQiYong = false;
            }

            var name = bll.SelectGetName(jxjd.JieDuanMing, jxjd.ID);
            if (A == false)
            {
                if (name == null)
                {
                    int n = bll.Updatejiaoxuejieduan(jxjd);
                    if (n > 0)
                    {
                        return Json(new { errorNo = "0", errorInfo = "教学阶段基本信息修改成功" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        return Json(new { errorNo = "1", errorInfo = "教学阶段基本信息修改失败，请检查重试" }, JsonRequestBehavior.AllowGet);
                    }
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "此阶段名已经存在，请重新输入" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "审核已通过，不能修改" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 删除教学阶段
        public ActionResult 删除教学阶段(int ID)
        {
            int a = bll.SelectJH(ID);
            int b = bll.SelectKC(ID);
            if (a == 0 && b == 0)
            {
                int n = bll.Delete(ID);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "教学阶段信息删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "教学阶段信息删除成功，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "此阶段名正在使用中，不能删除" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region 批量删除教学阶段
        public ActionResult 批量删除教学阶段(string ID)
        {
            int n = bll.DeleteManyIds(ID);
            if (n > 0)
            {
                return Json(new { errorNo = "0", errorInfo = "删除多条执行成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "删除执行错误，请检查重试" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

    }
}
