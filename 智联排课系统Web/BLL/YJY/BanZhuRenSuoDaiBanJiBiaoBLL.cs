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
    public partial class BanZhuRenSuoDaiBanJiBiaoBLL : BLLBase<BanZhuRenSuoDaiBanJiBiao>
    {
        private BanZhuRenSuoDaiBanJiBiaoDAL bzrsdbjbDAL = new BanZhuRenSuoDaiBanJiBiaoDAL();//班主任所带班级班级访问层对象
        private BanJiBiaoDAL bjbDAL = new BanJiBiaoDAL();//班级表数据访问层
        private BanJiKaiSheKeChengJiHuaBiaoDAL bjkskcjhb = new BanJiKaiSheKeChengJiHuaBiaoDAL();//班级开设课程计划表
        private JiaoXueKeChengDAL jxkcDAL = new JiaoXueKeChengDAL();//教学课程表访问层对象
        private YuanGongBiaoDAL YuanGongBiao = new YuanGongBiaoDAL();

        #region 班主任分配班级
        /// <summary>
        ///  业务过程：1）根据班级查询班主任带班表 2）如果存在，说明该表已有班主任
        ///  3)修改班主任结束时间为当前时间  4)该表增加一条新数据  5)修改班级课程分配表中，Cot课程中没有()
        ///  开展的为新班主任，已开展的课程班主任信息不要改
        /// </summary>
        /// <param name="bjId">班级Id</param>
        /// <param name="bjzId">班主任ID</param>
        /// <returns></returns>
        public bool BzrFpBJ(int bjId, int bjzId)
        {
            bool f = false;
            TransactionScope trans = null;
            using (trans = new TransactionScope())
            {
                try
                {
                    YuanGongBiao bzrObj = YuanGongBiao.GetOneData(bjzId);
                    BanJiBiao bjObj = bjbDAL.GetOneData(bjId);//班级对象
                                                              //////////1 步骤1//////////////
                    var aa = bzrsdbjbDAL.GetAllData().Where(p => p.BanJiBianHao == bjId);
                    if (aa.Count() > 0) //班级已有班主任
                    {
                        var LastObj = aa.Last();//最后一条
                        LastObj.JieShuShiJian = DateTime.Now;//结束时间为当前时间
                        LastObj.BeiZhu = "中间换班主任";
                        bzrsdbjbDAL.Modify(LastObj);//修改下写回
                        bzrsdbjbDAL.Save();
                    }
                    int an = aa.Count();
                    ///////////////////////
                    BanZhuRenSuoDaiBanJiBiao bzrsdbjb = new BanZhuRenSuoDaiBanJiBiao
                    {
                        BanJiBianHao = bjId,
                        KaiShiDaiBanShiJian = DateTime.Now,
                        BanZhuRenBianHao = bjzId,
                        BeiZhu = an > 0 ? "班级换班主任" : "新开班安排班主任"
                    };
                    bzrsdbjbDAL.Add(bzrsdbjb);//调用增加
                    bzrsdbjbDAL.Save();
                    ////////////////////////////////////

                    var Class = bjkskcjhb.GetAllData().Where(p => p.BanJiID == bjId).First();
                    var KC = jxkcDAL.GetAllData().Where(p => p.ID == Class.KeChengMing.Value).First();

                    var KCc = bjkskcjhb.GetAllData().Where(p => p.KeChengMing.Value == KC.ID && p.BanJiID == bjId).ToList();
                    //循环修改
                    foreach (var gobj in KCc)
                    {
                        if (gobj.BeiZhu!="")
                        {
                            gobj.BeiZhu = "由班主任" + gobj.AnPaiJiaoYuan + "替换为" + bjzId + "";
                        }
                        gobj.AnPaiJiaoYuan = bjzId;
                        gobj.AnPaiShiJian = DateTime.Now;
                        bjkskcjhb.Modify(gobj);
                        bjkskcjhb.Save();
                    };
                    trans.Complete();//提交事务
                    f = true;
                }
                catch (Exception ex)
                {
                    trans.Dispose();//回溯事务
                }
            }
            return f;
        }
        #endregion
    }
}
