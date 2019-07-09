using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class BanJiMoRenSheziController : Controller
    {
        //
        // GET: /BanJiMoRenShezi/
        public BLL.PaiKeBanJiMoRenSheZhiBLL bLL = new BLL.PaiKeBanJiMoRenSheZhiBLL();

        public BLL.BanJiBiaoBLL Ban = new BLL.BanJiBiaoBLL();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddPage()
        {
            return View();
        }

        /// <summary>
        /// 显示All所有班级排课默认设置
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="pagenum"></param>
        /// <param name="pagesize"></param>
        /// <returns></returns>
        public ActionResult ShowList(string Name, int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 20;
            var list = bLL.moRenSheZhisList(Name, PageIndex, PageSize, out int Total);
            var jsondata = new
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
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddAction(PaiKeBanJiMoRenSheZhi moRenSheZhi, int StudentList)
        {

            var Ban2 = Ban.GetOneData(StudentList);
            if (moRenSheZhi.AnPaiJiTaKeCiShu == null)
            {
                moRenSheZhi.AnPaiJiTaKeCiShu = 0;
            }
            moRenSheZhi.AnPaiJiTaKeCiShu = 0;
            moRenSheZhi.BanJiId = StudentList;
            moRenSheZhi.BanJiMingChen = Ban2.BanJiMing;
            moRenSheZhi.BeiZhu = "NO Not";
            int i = bLL.Add(moRenSheZhi);
            if (i > 0)
            {

                return Json(new { errorNo = "0", errorInfo = "操作成功" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "操作失败" }, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
