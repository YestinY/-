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
    public partial class PaiKeJiHuaBLL : BLLBase<PaiKeJiHua>
    {
        private BenCiPaiKeShiDuanBiaoDAL ben = new BenCiPaiKeShiDuanBiaoDAL();

        private BenCiPaiBanKeYongZiYuanDAL ben2 = new BenCiPaiBanKeYongZiYuanDAL();

        private PaiKeShiDuanYuZiYuanZuHeDAL paiKe = new PaiKeShiDuanYuZiYuanZuHeDAL();

        private BenCiPaiKeShiDuanBiaoDAL dAL2 = new BenCiPaiKeShiDuanBiaoDAL();

        private BenCiPaiKeBanJiJiCiShuDAL shuDAL = new BenCiPaiKeBanJiJiCiShuDAL();

        private PaiKeJiHuaDAL PaiKeJi = new PaiKeJiHuaDAL();

        public PaiKeShiDuanYuZiYuanZuHeDAL PaiKeShiDuanYuZiYuanZuHeDAL = new PaiKeShiDuanYuZiYuanZuHeDAL();

        public List<Models.PaiKeJiHua> paiKeJiHuaList(string JiHuaMingChen, string KaiShiShiJian, string JieShuShiJian, int PageIndex, int PageSize, out int Total)
        {
            return dAL.paiKeJiHuaList(JiHuaMingChen, KaiShiShiJian, JieShuShiJian, PageIndex, PageSize, out Total);
        }

        public PaiKeJiHua one()
        {
            return dAL.one();
        }

        #region 添加排课计划
        /// <summary>
        /// 添加排课计划
        /// </summary>
        /// <param name="paiKeJi">排课计划对象</param>
        /// <returns></returns>
        public bool AddPaiKeJiHua(Models.PaiKeJiHua paiKeJi)
        {
            bool error = false;

            ////PaiKeJiHua one= DAL.GetOneData(paiKeJi.JiHuaMingChen); ;
            ////if (one!=null)
            ////{
            ////    dal.Delete(one);
            ////}
            try
            {
                ben.DeleteShiDuan();
                string name = paiKeJi.KaiShiShiJian.Value.ToString("yyyyMMdd") + "-" + paiKeJi.JieShuShiJian.Value.ToString("yyyyMMdd");
                paiKeJi.JiHuaMingChen = name;
                int a = dAL.SelectGetWeiZhi(paiKeJi.JiHuaMingChen);
                if (a == 0)
                {
                    dAL.Add(paiKeJi);
                    dAL.Save();
                    TransactionScope scope = null;
                    DateTime Start = (DateTime)paiKeJi.KaiShiShiJian;
                    DateTime End = (DateTime)paiKeJi.JieShuShiJian;
                    TimeSpan time = End - Start;//获取数差
                    string[] Hour = { "8:00 - 12:20", "13:30 - 17:30", "18：30 - 20：00" };
                    string[] Shidaun = { "上午", "下午", "晚上" };
                    for (int i = 0; i <= time.Days; i++)
                    {
                        DateTime dateTime = Start.AddDays(i);
                        Models.BenCiPaiKeShiDuanBiao benCiPaiKe = new BenCiPaiKeShiDuanBiao
                        {
                            JiHuaBianHao = paiKeJi.ID,
                            JiHuaMingChen = paiKeJi.JiHuaMingChen,
                            ShiJian = dateTime,
                            BeiZhu = ""
                        };
                        for (int j = 0; j < Hour.Length; j++)
                        {
                            benCiPaiKe.ShiDuan = Shidaun[j];
                            benCiPaiKe.DuiYingShiJian = Hour[j];
                            if (Shidaun[j] == "晚上")
                            {
                                benCiPaiKe.ShiFouPaiKe = false;
                            }
                            else
                            {
                                benCiPaiKe.ShiFouPaiKe = true;
                            }
                            ben.Add(benCiPaiKe);
                            dAL.Save();
                        }
                    }
                    //设置本次排课可用资源与资源与时段组合
                    var ac = dAL.GetAllData().Where(p => p.JiHuaMingChen == paiKeJi.JiHuaMingChen).First();
                    ben2.DeleteBenCiPaiBanKeYongZiYuan();
                    ben2.Add(ac.ID);
                    PaiKeShiDuanYuZiYuanZuHeDAL.Delete();
                    var list = dAL2.GetAllData().Where(p => p.JiHuaBianHao == ac.ID).ToList();
                    var List2 = ben2.GetAllData().Where(p => p.PaiKeJiHuaBianHao == ac.ID).ToList();
                    foreach (var item in List2)
                    {
                        foreach (var item2 in list)
                        {
                            PaiKeShiDuanYuZiYuanZuHe paiKeShi = new PaiKeShiDuanYuZiYuanZuHe
                            {
                                KeYongZiYuanBianHao = item.KeYongZiYuanBianHao,
                                ShiJianDuan = item2.ShiDuan,
                                ShiJianMing = item2.DuiYingShiJian,
                                ZiYuanMing = item.KeYongZiYuanMingChen,
                                ZhouJi_ShiJian = item2.ShiJian,
                                ShiFouPaiKe = item2.ShiFouPaiKe
                            };
                            paiKe.Add(paiKeShi);
                            paiKe.Save();
                        }
                    }
                    PaiKeJiHua one = PaiKeJi.GetAllData().Where(p => p.JiHuaMingChen == name).First();
                    shuDAL.Add(paiKeJi);
                    error = true;
                    scope.Complete();
                    scope.Dispose();
                    return error;
                }
                else
                {
                    return error;
                }
            }
            catch (Exception ex)
            {
                return error;
                throw ex;
            }
        }
        #endregion
        public int DeleteManyByIdS(string ids)
        {
            string sql = "delete from PaiKeJiHua where ID in (" + ids + ")";
            //直接执行sql语句
            return dAL.DeleteManyByIdS(ids);
        }
        public int SelectGetWeiZhi(string name)
        {
            return dAL.SelectGetWeiZhi(name);
        }
        public int delete(string JiHuaMingChen)
        {
            return dAL.delete(JiHuaMingChen);
        }
    }
}
