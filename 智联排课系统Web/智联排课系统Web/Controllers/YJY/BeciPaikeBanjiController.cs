using Models;
using System.Linq;
using System.Web.Mvc;

namespace 智联排课系统Web.Controllers.YJY
{
    public class BeciPaikeBanjiController : Controller
    {
        public BLL.PaiKeJiHuaBLL bLL = new BLL.PaiKeJiHuaBLL();

        public BLL.BenCiPaiKeBanJiJiCiShuBLL ben = new BLL.BenCiPaiKeBanJiJiCiShuBLL();

        //
        // GET: /BeciPaikeBanji/

        #region 本次排课班级View
        public ActionResult Index()
        {
            Models.All all = new Models.All();
            var list = bLL.GetAllData().Where(p => p.ShiFouWanCheng == false).ToList();
            all.PaiKeJih = list;
            return View(all);
        }
        #endregion

        #region 本次排课班级数据
        public ActionResult List(int? pagenum, int? pagesize)
        {
            int PageIndex = pagenum ?? 1;
            int PageSize = pagesize ?? 10;
            var list = ben.List(PageIndex, PageSize, out int Total);
            var Jsondata = new
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
            return Json(Jsondata, JsonRequestBehavior.AllowGet);
        }
        #endregion

        //public ActionResult AddAction(int ID)
        //{
        //    PaiKeJiHua paiKeJi = bLL.GetOneData(ID);
        //    bool i = ben.Add(paiKeJi);
        //}
    }
}
