using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Models;
namespace DAL
{
    public partial class JiaoXueJiHuaBiaoDAL : DALBase<JiaoXueJiHuaBiao>
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();
        #region 教学计划
        /// <summary>
        /// 教学计划
        /// </summary>
        /// <param name="JiHuaBianHaoJiBanBen">计划名称</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">大小</param>
        /// <param name="Total">总条数</param>
        /// <returns></returns>
        public List<JiaoXueJiHuaBiao> Pageing(string JiHuaBianHaoJiBanBen, string KaiShiShiYongShiJian, string ZhongZhiShiYongShiJian, string ShenHeRen, int ShenHeShiFouTongGuo, int ShiFouQiYong, int PageIndex, int PageSize, out int Total)
        {
            //DateTime KaiShiShiYongShiJian, DateTime ZhongZhiShiYongShiJian, string ShenHeRen
            //string ZhongZhiShiYongShiJian, string ShenHeRen, int shenhe, int qiyong, 
            List<SqlParameter> par = new List<SqlParameter>();
            StringBuilder sql = new StringBuilder();
            sql.Append("select * from  JiaoXueJiHuaBiao where 1=1");
            if (!string.IsNullOrEmpty(JiHuaBianHaoJiBanBen))
            {
                sql.Append(" and JiHuaBianHaoJiBanBen like @JiHuaBianHaoJiBanBen");
                SqlParameter par1 = new SqlParameter("@JiHuaBianHaoJiBanBen", "%" + JiHuaBianHaoJiBanBen + "%");
                par.Add(par1);
            }
            if (!string.IsNullOrEmpty(KaiShiShiYongShiJian))
            {
                sql.Append(" and KaiShiShiYongShiJian >= @KaiShiShiYongShiJian");
                SqlParameter par2 = new SqlParameter("@KaiShiShiYongShiJian", KaiShiShiYongShiJian);
                par.Add(par2);
            }
            if (!string.IsNullOrEmpty(ZhongZhiShiYongShiJian))
            {
                sql.Append(" and ZhongZhiShiYongShiJian <= @ZhongZhiShiYongShiJian");
                SqlParameter par3 = new SqlParameter("@ZhongZhiShiYongShiJian", ZhongZhiShiYongShiJian);
                par.Add(par3);
            }
            if (!string.IsNullOrEmpty(ShenHeRen))
            {
                sql.Append(" and ShenHeRen like @ShenHeRen");
                SqlParameter par4 = new SqlParameter("@ShenHeRen", "%" + ShenHeRen + "%");
                par.Add(par4);
            }
            if (ShenHeShiFouTongGuo != -1)
            {
                sql.Append(" and ShenHeShiFouTongGuo=@ShenHeShiFouTongGuo");
                SqlParameter par4 = new SqlParameter("@ShenHeShiFouTongGuo", ShenHeShiFouTongGuo);
                par.Add(par4);
            }
            if (ShiFouQiYong != -1)
            {
                sql.Append(" and ShiFouQiYong=@ShiFouQiYong");
                SqlParameter par5 = new SqlParameter("@ShiFouQiYong", ShiFouQiYong);
                par.Add(par5);
            }
            var list = DBContext.Database.SqlQuery<Models.JiaoXueJiHuaBiao>(sql.ToString(), par.ToArray()).ToList();
            Total = list.Count();
            return list.OrderByDescending(p => p.ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        #endregion

        #region 计划详细
        /// <summary>
        /// 计划详细
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="PageIndex"></param>
        /// <param name="PageSize"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public List<Models.StageCourse> stageCoursesList(int ID, int PageIndex, int PageSize, out int Total)
        {
            string sql = @"select Jiao.ID as PlayID,Jiao.JiHuaBianHaoJiBanBen as PlayName,Ke.KeChengMing as Course,JiaoXueJiHuaDeKaiZhanShunXuHao, Ke.SuoShuJiaoXueJieDuan as StageID,Z.JianYiKeShi as 'Hour',Z.ZhangJieMingChen as section,Z.JianYiShouKeZiYuan as resource,ZhangJieShunXuHao,Z.ZhangJieBianHao as sectionID,Ke.ID as CourseID from JiaoXueJiHuaBiao Jiao inner join JiaoXueKeCheng Ke on Jiao.ID = Ke.SuoShuJiaoXueJiHua inner join KeChengShouKeZhangJie Z on Ke.ID = Z.SuoShuKeChengBianHao";
            var List = this.GetDataBySQL<Models.StageCourse>(sql);
            var NewList = List.Where(p => p.PlayID == ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            Total = List.Where(p => p.PlayID == ID).Count();
            return NewList.ToList();
        }
        #endregion


        #region 多表删除
        public int Deleteduobiao(int id)
        {
            var a = (from p in context.JiaoXueJiHuaBiao where p.ID == id select p).First();
            var b = (from p in context.JiaoXueKeCheng where p.SuoShuJiaoXueJiHua == id select p).ToList();
            foreach (var item in b)
            {
                var count = DBContext.KeChengShouKeZhangJie.Where(p => p.SuoShuKeChengMing == item.KeChengMing).ToList();
                foreach (var item2 in count)
                {
                    var c = (from p in context.KeChengShouKeZhangJie where p.SuoShuKeChengMing == item.KeChengMing select p).First();
                    context.KeChengShouKeZhangJie.Remove(c);
                    context.SaveChanges();
                }
                var One = (from p in context.JiaoXueKeCheng where p.KeChengMing == item.KeChengMing select p).First();
                context.JiaoXueKeCheng.Remove(One);
            }
           
            context.JiaoXueJiHuaBiao.Remove(a);
            return context.SaveChanges();
        }
        #endregion

        #region 删除时判断
        //班级开设课程计划表
        public int Selectjiaoxuejihua(int ID)
        {
            return context.BanJiKaiSheKeChengJiHuaBiao.Where(p => p.CaiYongJiaoXueJiHua == ID).Count();
        }
        #endregion
    }
}
