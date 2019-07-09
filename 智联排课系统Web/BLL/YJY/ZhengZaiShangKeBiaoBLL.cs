using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class ZhengZaiShangKeBiaoBLL : BLLBase<ZhengZaiShangKeBiao>
    {
        public bool Add()
        {
            return dAL.Add();
        }

        public bool TruncateZhangZaiShangKeBiao()
        {
            return dAL.TruncateZhengzaishangkebiao();
        }


        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, out int Total, string Name, DateTime date, int Teacher)
        {
            return dAL.List(PageIndex, PageSize, out Total, Name, date, Teacher);
        }

        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, string Name, DateTime date, out int Total)
        {
            return dAL.List(PageIndex, PageSize, Name, date, out Total);
        }
    }
}
