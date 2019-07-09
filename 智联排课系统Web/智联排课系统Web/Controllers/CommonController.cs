using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers
{
    public class CommonController : Controller
    {
        //
        // GET: /Common/
        public BLL.BanJiBiaoBLL bLL = new BLL.BanJiBiaoBLL();

        public BLL.JiaoXueJieDuanBLL JieDuanBLL = new BLL.JiaoXueJieDuanBLL();

        public BLL.JiaoXueJiHuaBiaoBLL JiHuaBiaoBLL = new BLL.JiaoXueJiHuaBiaoBLL();

        public ActionResult Index()
        {
            return View();
        }

        #region 阶段字典
        /// <summary>
        /// 阶段字典
        /// </summary>
        /// <returns></returns>
        public ActionResult Plan()
        {
            var List = JiHuaBiaoBLL.GetAllData().Select(p => new
            {
                ID = p.ID,
                Name = p.JiHuaBianHaoJiBanBen
            });
            var Jsondata = new
            {
                errorNo = "0",
                errorInfo = "获取成功",
                results = new
                {
                    data = List
                }
            };
            return Json(Jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 班级字典
        /// <summary>
        /// 班级字典
        /// </summary>
        /// <returns></returns>
        public ActionResult Studentlist()
        {

            var list = bLL.GetAllData().Where(p => p.JiaoXuePlan == false && p.BanJiZhuangTai==1).Select(p => new
            {
                BanJiId = p.ID,
                Name = p.BanJiMing
            });
            var jsondata = new
            {
                errorNo = "0",
                errorInfo = "查询成功",
                results = new
                {
                    data = list
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 教学阶段字典
        /// <summary>
        /// 教学阶段字典
        /// </summary>
        /// <returns></returns>
        /// 
        public ActionResult JiaoXueJieDuan()
        {
            BLL.JiaoXueJieDuanBLL jiaoXueJieDuan = new BLL.JiaoXueJieDuanBLL();
            var List = jiaoXueJieDuan.GetAllData();
            var NewList = List.Select(P => new
            {
                ID = P.ID,
                JieDuanMing = P.JieDuanMing
            });
            var jsondata = new
            {
                errorNo = "0",
                errorInfo = "查询成功",
                results = new
                {
                    data = NewList
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 文件
        /// <summary>
        /// 文件
        /// </summary>
        /// <returns></returns>
        public ActionResult upload()
        {
            string filename = "";
            if (Request.Files.Count != 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFileBase hb = Request.Files[i];//获取请求过来的第一个文件
                    if (hb.ContentLength != 0)
                    {
                        filename = Guid.NewGuid().ToString() + "" + System.IO.Path.GetExtension(hb.FileName);
                        hb.SaveAs(Server.MapPath("~/upload/" + filename));
                        return Json(new { errorNo = "0", errorInfo = "上传成功", results = new { data = new { filePath = filename } } },
                            JsonRequestBehavior.AllowGet);
                    }
                }
            }
            return Json(new
            {
                errorNo = "1",
                errorInfo = "上传错误",
                results = new
                {
                    data = new
                    {
                        filePath = ""
                    }
                }
            }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}
