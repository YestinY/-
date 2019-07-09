using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Data;

namespace DAL
{
    public class YiCipaikedekebiaoxinxiDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 查询所有
        public List<YiCiPaiKeDeKeBiaoXinXiBiao> GetAll() 
        {
            return context.Set<YiCiPaiKeDeKeBiaoXinXiBiao>().ToList();
        }
        #endregion

        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, string Name, DateTime date, out int Total)
        {
            var List = context.ZhengZaiShangKeBiao.ToList();
            DateTime date2 = new DateTime(0001, 1, 1);
            if (!string.IsNullOrEmpty(Name))
            {
                //string[] vs = Name.Split(',');
                List = List.Where(p => p.ClassName.Contains(Name)).ToList();
            }
            if (date != date2)
            {
                List = List.Where(p => p.RiQi == date).ToList();
            }
            //Total = List.Count() % PageSize == 0 ? PageSize % List.Count() : PageSize % List.Count() + 1;
            Total = List.Count();
            return List.OrderBy(p => p.ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}
