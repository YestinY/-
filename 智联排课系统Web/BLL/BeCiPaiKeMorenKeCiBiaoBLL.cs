using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class BeCiPaiKeMorenKeCiBiaoBLL : BLLBase<Models.BeCiPaiKeMorenKeCiBioa>
    {
        public DAL.BeCiPaiKeMorenKeCiBiaoDAL be = new DAL.BeCiPaiKeMorenKeCiBiaoDAL();
        public override void SetDAL()
        {
            dal = be;
        }
    }
}
