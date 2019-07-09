using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class BenCiPaiBanKeYongZiYuanBLL : BLLBase<BenCiPaiBanKeYongZiYuan>
    {
        private BenCiPaiBanKeYongZiYuanDAL ben = new BenCiPaiBanKeYongZiYuanDAL();
        private PaiKeShiDuanYuZiYuanZuHeDAL paiKe = new PaiKeShiDuanYuZiYuanZuHeDAL();
        private BenCiPaiKeShiDuanBiaoDAL dAL = new BenCiPaiKeShiDuanBiaoDAL();
        public PaiKeShiDuanYuZiYuanZuHeDAL PaiKeShiDuanYuZiYuanZuHeDAL = new PaiKeShiDuanYuZiYuanZuHeDAL();

        public bool ADDZhiYuan(int ID)
        {

            ben.DeleteBenCiPaiBanKeYongZiYuan();
            ben.Add(ID);
            PaiKeShiDuanYuZiYuanZuHeDAL.Delete();
            var list = dAL.GetAllData().Where(p => p.JiHuaBianHao == ID).ToList();
            var List2 = ben.GetAllData().Where(p => p.PaiKeJiHuaBianHao == ID).ToList();
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
            return true;
        }
    }
}
