using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public partial class PaiKeListBLL : BLLBase<Models.PaikeLinshiBiao>
    {
        public DAL.PaiKeListDAL dAL = new PaiKeListDAL();
        public override void SetDAL()
        {
            this.dal = dAL;
        }
    }
}
