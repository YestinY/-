using BLL;
using Models;
using Newtonsoft.Json;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.Model;
using NPOI.XSSF.Streaming;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace 智联排课系统Web.Controllers.YJY
{
    public class JiaoXueJiHuaController : Controller
    {
        //
        // GET: /JiaoXueJiHua/
        public BLL.JiaoXueJiHuaBiaoBLL jiaoXueJiHuaBiaoBLL = new BLL.JiaoXueJiHuaBiaoBLL();

        #region 教学计划View
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 显示所有计划
        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="JiHuaBianHaoJiBanBen">计划名称</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public ActionResult PageIng(string JiHuaBianHaoJiBanBen, string KaiShiShiYongShiJian, string ZhongZhiShiYongShiJian, string ShenHeRen, int ShenHeShiFouTongGuo, int ShiFouQiYong, int? pagenum, int? pagesize)
        {

            int PageIndex2 = pagenum ?? 1;
            int PageSize2 = pagesize ?? 10;
            var JiaoXueJiHua = jiaoXueJiHuaBiaoBLL.Pageing(JiHuaBianHaoJiBanBen, KaiShiShiYongShiJian, ZhongZhiShiYongShiJian, ShenHeRen, ShenHeShiFouTongGuo, ShiFouQiYong, PageIndex2, PageSize2, out int Total);
            JsonConvert.SerializeObject(JiaoXueJiHua, Formatting.Indented, new Newtonsoft.Json.Converters.IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" });
            var jsondata = new
            {
                errorNo = "0",
                errorInfo = "查询成功",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = JiaoXueJiHua
                    }
                }
            };
            return Json(jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 添加教学计划View
        /// <summary>
        ///添加教学计划
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowPage()
        {
            return View();
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

        #region  Add教学计划
        /// <summary>
        /// Add教学计划
        /// </summary>
        /// <param name="jiHuaBiao">计划表</param>
        /// <param name="JiaoXueJieDuan">阶段ID</param>
        /// <param name="filePath">文件</param>
        /// <returns></returns>
        public ActionResult AddAction(JiaoXueJiHuaBiao jiHuaBiao, int JiaoXueJieDuan, string filePath)
        {

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

                    List<Excele> exceles = new List<Excele>();

                    for (int i = (sheep.FirstRowNum + 2); i < sheep.LastRowNum; i++)
                    {
                        var row = sheep.GetRow(i);
                        Excele excele2 = new Excele
                        {
                            //第一行下标为1
                            KCM = (row.GetCell(0).ToString()),
                            //第二列下标为1
                            KCID = row.GetCell(1).ToString(),

                            ZJID = row.GetCell(2).ToString(),

                            ZJM = row.GetCell(3).ToString(),

                            Count = Convert.ToInt32(row.GetCell(4).ToString()),

                            KCXH = i - 1
                        };
                        //把对象加入到集合
                        exceles.Add(excele2);
                    }
                    jiHuaBiao.JiHuaBianHaoJiBanBen = jiHuaBiao.JiHuaBianHaoJiBanBen + "_" + JiaoXueJieDuan;
                    jiHuaBiao.ShenHeRen = jiHuaBiao.ShenHeRen + "_" + filePath;
                    BLL.JiaoXueJiHuaBiaoBLL jiaoXueJi = new BLL.JiaoXueJiHuaBiaoBLL();
                    bool error = jiaoXueJi.AddJiaoXueJihua(exceles, jiHuaBiao, JiaoXueJieDuan);
                    if (error)
                    {
                        return Json(new { errorNo = "0", errorInfo = "导入教学计划成功" });
                    }
                    else
                    {
                        return Json(new { errorNo = "1", errorInfo = "导入教学计划失败" });
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

        #region 计划课程View
        public ActionResult ShowPageCourse()
        {
            return View();
        }
        #endregion

        #region 计划课程集合
        public ActionResult ShowCourse(int ID, int? pagenum, int? pagesize)
        {
            BLL.JiaoXueJiHuaBiaoBLL jiaoXueJiHuaBiaoBLL = new BLL.JiaoXueJiHuaBiaoBLL();
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            List<StageCourse> lists = jiaoXueJiHuaBiaoBLL.StageCoursesList(ID, PageIndex, PageSize, out int Total);
            var JsonDdata = new
            {
                errorNo = "0",
                errorInfo = "查询数据成功",
                results = new
                {
                    data = new
                    {
                        total = Total,
                        list = lists,
                    }
                }
            };
            return Json(JsonDdata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Down教学计划
        //下载教学计划
        public ActionResult DownFile()
        {
            string root = Server.MapPath("~/File/");
            string fileName = "课程开展计划模板.xls";
            string filePath = Path.Combine(root, fileName);
            string s = MimeMapping.GetMimeMapping(fileName);
            return File(filePath, s + DateTime.Now, Path.GetFileName(filePath));
        }
        #endregion

        #region Down学生案例Exce
        //下载学生案例Exce
        public ActionResult DownFileStudent()
        {
            string root = Server.MapPath("~/File/");
            string fileName = "学生档案.xls";
            string filePath = Path.Combine(root, fileName);
            string s = MimeMapping.GetMimeMapping(fileName);
            return File(filePath, s + DateTime.Now, Path.GetFileName(filePath));
        }
        #endregion

        #region 多表删除
        public ActionResult Deleteduobiao(int ID)
        {
            int a = jiaoXueJiHuaBiaoBLL.Selectjiaoxuejihua(ID);
            if (a == 0)
            {

                int n = jiaoXueJiHuaBiaoBLL.Deleteduobiao(ID);
                if (n > 0)
                {
                    return Json(new { errorNo = "0", errorInfo = "教学计划删除成功" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(new { errorNo = "1", errorInfo = "教学计划删除失败，请检查重试" }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { errorNo = "1", errorInfo = "有班级在使用此教学计划" }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
    }
}
