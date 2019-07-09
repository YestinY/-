using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
namespace DAL
{
    public partial class PaiKeBanJiMoRenSheZhiDAL : DALBase<PaiKeBanJiMoRenSheZhi>
    {

        public List<Models.PaiKeBanJiMoRenSheZhi> moRenSheZhisList(string StudentName, int PageIndex, int PageSize,out int Total)
        {
            List<SqlParameter> sqls = new List<SqlParameter>();
            StringBuilder Str = new StringBuilder();
            Str.Append("select * from PaiKeBanJiMoRenSheZhi where 1=1");
            if (!String.IsNullOrEmpty(StudentName))
            {
                Str.Append(" and BanJiMing like @BanJiMing");
                SqlParameter sql2 = new SqlParameter("@BanJiMing", StudentName);
                sqls.Add(sql2);
            }
            var List = this.GetDataBySQL<Models.PaiKeBanJiMoRenSheZhi>(Str.ToString(), sqls.ToArray()).ToList();
            Total=List.Count();
            return List.OrderByDescending(p => p.BanJiId).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
