using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class JiaoXueKeChengBLL : BLLBase<JiaoXueKeCheng>
    {
        public List<Models.JiaoXueKeCheng> keChengs(int ID)
        {
            return jiaoXue.keChengs(ID);
        }
    }
}
