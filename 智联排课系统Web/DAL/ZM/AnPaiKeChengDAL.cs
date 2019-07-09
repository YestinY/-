using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ZM;

namespace DAL.ZM
{
    public class AnPaiKeChengDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        //#region 查询教员
        //public List<YuanGongBiao> Teacher(int BanJiID)
        //{
        //    string sql = "select BanJiID, AnPaiJiaoYuan From BanJiKaiSheKeChengJiHuaBiao where KeChengMing in (select KeChengMing from BanJiKaiSheKeChengJiHuaBiao where AnPaiJiaoYuan  in (select ID From YuanGongBiao ) group by KeChengMing ) group by BanJiID,AnPaiJiaoYuan";
        //    List<AnPaiKeCheng> apList = context.Database.SqlQuery<AnPaiKeCheng>(sql).ToList();
        //    List<YuanGongBiao> ygList = new List<YuanGongBiao>();
        //    string sql1 = "";
        //    foreach (AnPaiKeCheng a in apList)
        //    {
        //        sql1 = "select * from YuanGongBiao where ID= " + a.AnPaiJiaoYuan;
        //        YuanGongBiao yg = context.Database.SqlQuery<YuanGongBiao>(sql1).First();
        //        for (int i = 0; i < ygList.Count; i++)
        //        {
        //            if (yg != ygList[i])
        //            {
        //                ygList.Add(yg);
        //            }
        //        }
        //    }
        //    return ygList;
        //}
        //#endregion

        #region 查询课程
        public List<JiaoXueKeCheng> KeCheng(int BanJiID)
        {
            string sql = " select * from JiaoXueKeCheng where ID not in (select KeChengMing from BanJiKaiSheKeChengJiHuaBiao where BanJiID = " + BanJiID + " and AnPaiJiaoYuan is not null) and SuoShuJiaoXueJiHua in ( select CaiYongJiaoXueJiHua From BanJiKaiSheKeChengJiHuaBiao where BanJiID =" + BanJiID + " )";
            return context.Database.SqlQuery<JiaoXueKeCheng>(sql).ToList();
        }
        #endregion

        #region 查询班主任
        public List<YGB> ygList()
        {
            string sql = "select * From BuMenBiao A,ZhiWeiBiao B,YuanGongBiao C where C.ZhiWeiID = B.ID and B.BuMenID = A.ID and A.BuMenMingCheng = '班主任'";
            return context.Database.SqlQuery<YGB>(sql).ToList();
        }
        #endregion
    }
}
