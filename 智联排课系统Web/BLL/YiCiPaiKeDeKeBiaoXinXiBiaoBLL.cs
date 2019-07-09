using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class YiCiPaiKeDeKeBiaoXinXiBiaoBLL : BLLBase<YiCiPaiKeDeKeBiaoXinXiBiao>
    {
        YiCiPaiKeDeKeBiaoXinXiBiaoDAL dAL = new YiCiPaiKeDeKeBiaoXinXiBiaoDAL();
        public override void SetDAL()
        {
            this.dal = dAL;
        }
    }
}
