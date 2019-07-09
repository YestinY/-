using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BanZhuRenSuoDaiBanJiBiaoBLL : BLLBase<BanZhuRenSuoDaiBanJiBiao>
    {
        BanZhuRenSuoDaiBanJiBiaoDAL ban = new BanZhuRenSuoDaiBanJiBiaoDAL();
        public override void SetDAL()
        {
            this.dal = ban;
        }
    }
}
