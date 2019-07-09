using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class PaiKeShiDuanYuZiYuanZuHeDAL : DALBase<PaiKeShiDuanYuZiYuanZuHe>
    {

        public List<Models.PaiKeShiDuanYuZiYuanZuHe> List(int PageIndex, int PageSize, out int Total)
        {
            Total = DBContext.PaiKeShiDuanYuZiYuanZuHe.Count();

            return DBContext.PaiKeShiDuanYuZiYuanZuHe.OrderByDescending(p => p.ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

        //删除PaiKeShiDuanYuZiYuanZuHe数据
        public int Delete()
        {
            string sql = "truncate table PaiKeShiDuanYuZiYuanZuHe";
            this.ExcuteCommand(sql);
            return 1;
        }

        
    }
}
