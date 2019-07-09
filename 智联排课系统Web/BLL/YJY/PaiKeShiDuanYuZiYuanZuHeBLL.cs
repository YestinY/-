using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class PaiKeShiDuanYuZiYuanZuHeBLL : BLLBase<PaiKeShiDuanYuZiYuanZuHe>
    {
        public List<Models.PaiKeShiDuanYuZiYuanZuHe> List(int PageIndex, int PageSize, out int Total)
        {
            return dAL.List(PageIndex, PageSize, out Total);
        }
    }

}
