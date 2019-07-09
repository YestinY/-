using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class XueShengBiaoBLL : BLLBase<XueShengBiao>
    {
        public Models.XueShengBiao XueSheng(Models.XueShengBiao xueSheng)
        {
            return dAL.login(xueSheng);
        }
    }
}
