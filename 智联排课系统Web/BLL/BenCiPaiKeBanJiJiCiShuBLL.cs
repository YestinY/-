using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BenCiPaiKeBanJiJiCiShuBLL : BLLBase<BenCiPaiKeBanJiJiCiShu>
    {
        BenCiPaiKeBanJiJiCiShuDAL shuDAL = new BenCiPaiKeBanJiJiCiShuDAL();
        public override void SetDAL()
        {
            this.dal = shuDAL;
        }
    }
}
