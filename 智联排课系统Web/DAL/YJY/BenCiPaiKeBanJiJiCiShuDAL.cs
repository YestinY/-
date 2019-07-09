using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{

    public partial class BenCiPaiKeBanJiJiCiShuDAL : DALBase<BenCiPaiKeBanJiJiCiShu>
    {
        public List<Models.BenCiPaiKeBanJiJiCiShu> benCiPaiKeBanJiJiCis(int PageIndex, int PageSize, out int Total)
        {
            var obj = DBContext.BenCiPaiKeBanJiJiCiShu.ToList();
            Total = DBContext.BenCiPaiKeBanJiJiCiShu.Count();
            return obj.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        public bool Add(Models.PaiKeJiHua paiKeJi)
        {
            try
            {
                int i = 4;
                StringBuilder @string = new StringBuilder();
                //@string.Append(" delete from  BenCiPaiKeBanJiJiCiShu where ZhuangTai=4;");
                @string.Append(" truncate table BenCiPaiKeBanJiJiCiShu;");
                @string.Append(" insert into BenCiPaiKeBanJiJiCiShu (DuiYingJiHuaMing,KaiShiShiJian,JieShuShiJian,BanJiId,BanJiMingChen,AnPaiLiLunKeCiShu,AnPaiShangJiCiShu,AnPaiJiTaKeCiShu,ZhuangTai)");
                @string.Append(" select '" + paiKeJi.JiHuaMingChen + "','" + paiKeJi.KaiShiShiJian + "','" + paiKeJi.JieShuShiJian + "',BanJiId,BanJiMingChen,AnPaiLiLunKeCiShu,AnPaiZiXiKeCiShu,AnPaiJiTaKeCiShu,'" + i + "' from PaiKeBanJiMoRenSheZhi ");
                ExcuteCommand(@string.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

