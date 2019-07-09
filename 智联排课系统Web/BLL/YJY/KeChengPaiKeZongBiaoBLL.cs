using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class KeChengPaiKeZongBiaoBLL : BLLBase<KeChengPaiKeZongBiao>
    {
        public bool Add()
        {
            return dAL.Add();
        }

        public List<Models.KeChengPaiKeZongBiao> List(int PageIndex, int PageSize, out int Total, string Name, DateTime date, int Teacher)
        {
            return dAL.List(PageIndex, PageSize, out Total, Name, date, Teacher);
        }
    }
}
