using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ZM;
using System.Data.SqlClient;


namespace DAL.ZM
{
    public class MBanZhuRenSuoDaiBanJiBiaoDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 查询所有班主任
        public List<YuanGongBiao> banzhuren()
        {
            return context.Set<YuanGongBiao>().Where(p => p.ZhiWeiID == 1).ToList();
        }
        #endregion

        #region 2新增班主任所带班级
        public int AddBanZhuRen(BanZhuRenSuoDaiBanJiBiao b)
        {
            context.BanZhuRenSuoDaiBanJiBiao.Add(b);
            return context.SaveChanges();
        }
        #endregion

        #region 1新增班主任所带班级：类
        public int bzrsdbjb(int bjID, int bzrID, string BeiZhu)
        {
            BanZhuRenSuoDaiBanJiBiao b = new BanZhuRenSuoDaiBanJiBiao();
            b.BanJiBianHao = bjID;
            b.KaiShiDaiBanShiJian = DateTime.Now;
            b.BanZhuRenBianHao = bzrID;
            b.BeiZhu = BeiZhu;
            return AddBanZhuRen(b);
        }
        #endregion

        #region 查询一条
        public BanJiBanZhuRen GetBjBzrOne(int BjID)
        {
            BanZhuRenSuoDaiBanJiBiao xsb = context.Set<BanZhuRenSuoDaiBanJiBiao>().Find(BjID);
            YuanGongBiao yg = context.Set<YuanGongBiao>().Find(xsb.BanZhuRenBianHao);
            BanJiBiao bjb = context.Set<BanJiBiao>().Find(xsb.BanJiBianHao);
            BanJiBanZhuRen bz = new BanJiBanZhuRen();
            bz.BanZhuRenMing = yg.Name;
            bz.BanJiBeiZhu = bjb.BeiZhu;
            bz.BanJiBianHao = bjb.ID;
            bz.BanJiMing = bjb.BanJiMing;
            bz.BanZhuRenBianHao = Convert.ToInt32(xsb.BanZhuRenBianHao);
            bz.BanZhuRenDaiID = xsb.ID;
            bz.BeiZhu = xsb.BeiZhu;
            return bz;
        }
        #endregion

        #region 修改原班主任结束时间
        public int UpdateDate(int ID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " update BanZhuRenSuoDaiBanJiBiao set JieShuShiJian= GETDATE() where BanJiBianHao = @ID";
            SqlParameter p1 = new SqlParameter("@ID", ID);
            spls.Add(p1);
            return context.Database.ExecuteSqlCommand(sql, spls.ToArray());
        } 
        #endregion

  }
}
