using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class JiaoYuanDaiKeBiaoBLL : BLLBase<JiaoYuanDaiKeBiao>
    {
        public List<Models.JiaoYuanDaiKeBiao> List(int PageIndex, int PageSize, out int Total, string Name, DateTime date, int Teacher)
        {
            return biaoBLL.List(PageIndex, PageSize, out Total, Name, date, Teacher);
        }
    }
}
