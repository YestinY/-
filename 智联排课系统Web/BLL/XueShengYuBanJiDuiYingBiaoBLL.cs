using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Models;
namespace BLL
{
    public partial class XueShengYuBanJiDuiYingBiaoBLL : BLLBase<XueShengYuBanJiDuiYingBiao>
    {
        XueShengYuBanJiDuiYingBiaoDAL dAL = new XueShengYuBanJiDuiYingBiaoDAL();
        public override void SetDAL()
        {
            this.dal = dAL;
        }
    }
}
