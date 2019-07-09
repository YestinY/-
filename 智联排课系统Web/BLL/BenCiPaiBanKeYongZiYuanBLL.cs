using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BenCiPaiBanKeYongZiYuanBLL : BLLBase<BenCiPaiBanKeYongZiYuan>
    {
        BenCiPaiBanKeYongZiYuanDAL benCiPai = new BenCiPaiBanKeYongZiYuanDAL();
        public override void SetDAL()
        {
            this.dal = benCiPai;
        }
    }
}
