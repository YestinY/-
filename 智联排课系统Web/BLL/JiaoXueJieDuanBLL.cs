using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public partial class JiaoXueJieDuanBLL : BLLBase<Models.JiaoXueJieDuanBiao>
    {
        DAL.JiaoXueJieDuanBiaoDAL dAL = new JiaoXueJieDuanBiaoDAL();

        public override void SetDAL()
        {
            this.dal = dAL;
        }
    }
}
