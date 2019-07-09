using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
    public class YiCipaikedekebiaoxinxiBLL
    {
        YiCipaikedekebiaoxinxiDAL dal = new YiCipaikedekebiaoxinxiDAL();

        #region 查询所有
        public List<YiCiPaiKeDeKeBiaoXinXiBiao> GetAll()
        {
            return dal.GetAll();
        }
        #endregion

        #region 分页
        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, string Name, DateTime date, out int Total)
        {
            return dal.List(PageIndex, PageSize, Name, date, out Total);
        }
        #endregion
    }
}
