using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class BenCiPaiKeBanJiJiCiShuBLL : BLLBase<BenCiPaiKeBanJiJiCiShu>
    {
       
        public List<Models.BenCiPaiKeBanJiJiCiShu> List(int PageIndex, int PageSize, out int Total)
        {
            return shuDAL.benCiPaiKeBanJiJiCis(PageIndex, PageSize, out Total);
        }

        public bool Add(PaiKeJiHua paiKeJi)
        {
            return shuDAL.Add(paiKeJi);
        }


    }
}
