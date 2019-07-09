using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class KeShiBiaoBLL : BLLBase<KeShiBiao>
    {
        KeShiBiaoDAL dAL = new KeShiBiaoDAL();
        public override void SetDAL()
        {
            this.dal = dAL;
        }
    }
}
