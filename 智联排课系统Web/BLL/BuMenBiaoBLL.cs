using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BuMenBiaoBLL : BLLBase<BuMenBiao>
    {
        BuMenBiaoDAL buMen = new BuMenBiaoDAL();
        public override void SetDAL()
        {
            this.dal = buMen;
        }
    }
}
