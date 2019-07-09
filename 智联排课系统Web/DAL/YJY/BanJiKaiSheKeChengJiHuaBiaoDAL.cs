using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class BanJiKaiSheKeChengJiHuaBiaoDAL : DALBase<BanJiKaiSheKeChengJiHuaBiao>
    {


        ///<summary>
        /// 获取计划ID的课程
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public List<Models.KeChengShouKeZhangJie> List(int ID)
        {
            string sql = "select * from KeChengShouKeZhangJie where SuoShuKeChengBianHao in(select ID from JiaoXueKeCheng where SuoShuJiaoXueJiHua =@ID)";
            SqlParameter sqls = new SqlParameter("@ID", ID);
            var List = DBContext.Database.SqlQuery<Models.KeChengShouKeZhangJie>(sql, sqls).ToList();
            return List;
        }

        /// <summary>
        /// 显示教学计划
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <returns></returns>
        public List<Models.BanJiKaiSheKeChengJiHuaBiao> QueryID(int ID, int PageIndex, int PageSize, out int Total)
        {
            Total = DBContext.BanJiKaiSheKeChengJiHuaBiao.Count();
            string sql = "select * from BanJiKaiSheKeChengJiHuaBiao where BanJiID=@ID";
            SqlParameter sqr = new SqlParameter("@ID", ID);
            var List = DBContext.Database.SqlQuery<Models.BanJiKaiSheKeChengJiHuaBiao>(sql, sqr).ToList();
            Total = List.Count();
            return List.OrderByDescending(p => p.KeChengShunXuHao).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        /// <summary>
        /// 手机端课程
        /// </summary>
        /// <param name="ID">班级ID</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="KC">课程名</param>
        /// <param name="Total">总条数</param>
        /// <returns></returns>
        public List<Models.ClassKC> banJiKaiSheKes(int ID, int PageIndex, int PageSize, int KCID, out int Total)
        {
            var list = (from p in DBContext.BanJiKaiSheKeChengJiHuaBiao
                        join J in DBContext.JiaoXueJieDuanBiao on p.KaiSheJiaoXueJieDuan equals J.ID
                        join Play in DBContext.JiaoXueJiHuaBiao on p.CaiYongJiaoXueJiHua equals Play.ID
                        join KC in DBContext.JiaoXueKeCheng on p.KeChengMing.Value equals KC.ID
                        select new Models.ClassKC
                        {
                            ClassName = p.BanJiMing,
                            CaiYongJiaoXueJiHua = Play.JiHuaBianHaoJiBanBen,
                            KaiSheJiaoXueJieDuan = J.JieDuanMing,
                            KCID = KC.ID,
                            KeChengMing = KC.KeChengMing,
                            ShiFouYiWanCheng = p.ShiFouYiWanCheng.Value,
                            ZhangJieMingChen = p.ZhangJieMingChen,
                            ClassID = p.BanJiID.Value,
                            AnPaiJiaoYuanID = p.AnPaiJiaoYuan ?? 0
                        }
                       ).Where(p => p.ClassID == ID).ToList();
            if (KCID != -1)
            {
                list = list.Where(p => p.KCID == KCID).ToList();
            }
            Total = list.Count();
            return list.OrderByDescending(p => p.KeChengMing).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        /// <summary>
        ///教员带班课程
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="KCID"></param>
        /// <param name="ClassName"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<Models.ClassKC> TeacherKC(int ID, int PageIndex, int PageSize, int ClassID, out int Total)
        {
            var list = (from p in DBContext.BanJiKaiSheKeChengJiHuaBiao
                        join J in DBContext.JiaoXueJieDuanBiao on p.KaiSheJiaoXueJieDuan equals J.ID
                        join Play in DBContext.JiaoXueJiHuaBiao on p.CaiYongJiaoXueJiHua equals Play.ID
                        join KC in DBContext.JiaoXueKeCheng on p.KeChengMing.Value equals KC.ID
                        select new Models.ClassKC
                        {
                            ClassName = p.BanJiMing,
                            CaiYongJiaoXueJiHua = Play.JiHuaBianHaoJiBanBen,
                            KaiSheJiaoXueJieDuan = J.JieDuanMing,
                            KCID = KC.ID,
                            KeChengMing = KC.KeChengMing,
                            ShiFouYiWanCheng = p.ShiFouYiWanCheng.Value,
                            ZhangJieMingChen = p.ZhangJieMingChen,
                            ClassID = p.BanJiID.Value,
                            AnPaiJiaoYuanID = p.AnPaiJiaoYuan ?? 0
                        }
                       ).Where(p => p.AnPaiJiaoYuanID == ID).ToList();
            if (ClassID != -1)
            {
                list = list.Where(p => p.ClassID == ClassID).ToList();
            }
            Total = list.Count();
            return list.OrderByDescending(p => p.KeChengMing).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public List<Models.Class> TeacherClass(int ID)
        {
            string sql = "select distinct BanJiID,BanJiMing from  BanJiKaiSheKeChengJiHuaBiao where AnPaiJiaoYuan=@AnPaiJiaoYuan";
            SqlParameter sqr = new SqlParameter("@AnPaiJiaoYuan", ID);
            return GetDataBySQL<Models.Class>(sql, sqr).ToList();
        }

        //下一个课程
        public BanJiKaiSheKeChengJiHuaBiao NextKC(int ID, string Class)
        {
            string sql = "  SELECT TOP 1 * FROM[dbo].[BanJiKaiSheKeChengJiHuaBiao] WHERE[ZhangJieBianHao]<=@ID and[BanJiMing]=@Class ORDER BY[ZhangJieBianHao] DESC";
            SqlParameter[] sqr = new SqlParameter[] {
                new SqlParameter("@ID", ID),
                new SqlParameter("@Class", Class) };
            return GetDataBySQL<Models.BanJiKaiSheKeChengJiHuaBiao>(sql, sqr).ToList()[0];
        }


        ////教员课时
        //public int TeacherAdd()
        //{

        //}

    }
}
