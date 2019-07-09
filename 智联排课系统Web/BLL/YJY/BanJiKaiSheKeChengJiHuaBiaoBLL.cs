using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BLL
{
    public partial class BanJiKaiSheKeChengJiHuaBiaoBLL : BLLBase<BanJiKaiSheKeChengJiHuaBiao>
    {
        private BanJiKaiSheKeChengJiHuaBiaoDAL ban2 = new BanJiKaiSheKeChengJiHuaBiaoDAL();
        private JiaoXueJiHuaBiaoDAL jiaoXue = new JiaoXueJiHuaBiaoDAL();
        private BanJiBiaoBLL bLL = new BanJiBiaoBLL();

        /// <summary>
        /// 分配计划
        /// </summary>
        /// <param name="banJiKaiSheKe"></param>
        /// <param name="Plan"></param>
        /// <returns></returns>
        public int FenPei(Models.BanJiKaiSheKeChengJiHuaBiao banJiKaiSheKe, int Plan)
        {
            try
            {
                var PlanOne = jiaoXue.GetOneData(Plan);
                var List = ban2.List(Plan);
                var One = bLL.GetOneData(banJiKaiSheKe.BanJiID);
                One.JiaoXuePlan = true;
                bLL.Modify(One);
                ban.Save();
                foreach (var item in List)
                {
                    BanJiKaiSheKeChengJiHuaBiao He = new BanJiKaiSheKeChengJiHuaBiao
                    {
                        BanJiMing = banJiKaiSheKe.BanJiMing,
                        BanJiID = banJiKaiSheKe.BanJiID,
                        CaiYongJiaoXueJiHua = Plan,
                        KeChengMing = item.SuoShuKeChengBianHao,
                        ZhangJieBianHao = item.ZhangJieBianHao,
                        ZhangJieMingChen = item.ZhangJieMingChen,
                        KaiSheJiaoXueJieDuan = PlanOne.ID,
                        AnPaiShiJian = DateTime.Now,
                        ShouKeDeMoShi = "理论课",
                        KeChengShunXuHao = item.ZhangJieShunXuHao,
                        JianYiKeShi = Convert.ToInt32(item.JianYiKeShi).ToString(),
                        ShiFouHeBing = false,
                        ShiFouYiQuXiao = false
                    };
                    He.ShiFouHeBing = false;
                    He.ShiFouYiWanCheng = false;
                    ban.Add(He);
                }
                ban.Save();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        /// <summary>
        /// 显示班级教学计划
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        ///
        public List<Models.BanJiKaiSheKeChengJiHuaBiao> QueryID(int ID, int PageIndex, int PageSize, out int Total)
        {
            return ban2.QueryID(ID, PageIndex, PageSize, out Total);
        }

        //手机端 学生
        public List<Models.ClassKC> banJiKaiSheKes(int ID, int Pageindex, int PageSize, int KCID, out int Total)
        {
            return ban2.banJiKaiSheKes(ID, Pageindex, PageSize, KCID, out Total);
        }

        //手机端 教员
        public List<Models.ClassKC> TeacherClass(int ID, int Pageindex, int PageSize, int ClassID, out int Total)
        {
            return ban2.TeacherKC(ID, Pageindex, PageSize, ClassID, out Total);
        }

        //教员所带班级
        public List<Models.Class> TeacherClass(int ID)
        {
            return ban2.TeacherClass(ID);
        }

        //下一个课程
        public BanJiKaiSheKeChengJiHuaBiao NextKC(int ID,string ClassName)
        {
            return ban2.NextKC(ID,ClassName);
        }
        ////教员课时 
        //public int TeacherKSCount()
        //{

        //}

    }

}
