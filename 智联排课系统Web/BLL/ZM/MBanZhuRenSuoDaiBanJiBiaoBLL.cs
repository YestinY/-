using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ZM;
using Models;
using Models.ZM;
namespace BLL.ZM
{
    public class MBanZhuRenSuoDaiBanJiBiaoBLL
    {
        MBanZhuRenSuoDaiBanJiBiaoDAL md = new MBanZhuRenSuoDaiBanJiBiaoDAL();
        MBanJiBiaoBLL bjb = new MBanJiBiaoBLL();
        #region 查询所有班主任
        public List<YuanGongBiao> banzhuren()
        {
            return md.banzhuren();
        }
        #endregion

        #region 1新增班主任所带班级：类
        public int bzrsdbjb(string banjiMing, int bzrID, string BeiZhu)
        {
            int id = bjb.GetOneID(banjiMing);
            return md.bzrsdbjb(id, bzrID, BeiZhu);
        }
        #endregion

        #region 查询一条
        public BanJiBanZhuRen GetBjBzrOne(int BjID)
        {
            return md.GetBjBzrOne(BjID);
        } 
        #endregion

        #region 班级更换班主任
        public int UpdateBZR(int ID, int bjID, int bzrID, string BeiZhu)
        {
            int n = md.UpdateDate(ID);
            if (n > 0)
            {
                int j = md.bzrsdbjb(bjID, bzrID, BeiZhu);
                if (j > 0)
                {
                    return j;
                }
            }
            return 0;
        } 
        #endregion

    }
}
