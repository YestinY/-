using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class PaiKeFangAnBLL : BLLBase<Models.PaiKeFangAnBiao>
    {
        private DAL.PaiKeFangAnDAL dAL = new DAL.PaiKeFangAnDAL();
        public override void SetDAL()
        {
            dal = dAL;
        }
    }
}
