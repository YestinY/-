using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public partial class PaiKeFangAnDAL : DALBase<Models.PaiKeFangAnBiao>
    {
        public int Delete()
        {
            try
            {
                string sql = "truncate table PaiKeFangAnBiao";
                ExcuteCommand(sql);
                Save();
                return 1;
            }
            catch (Exception ex)
            {
                return 0;
                throw ex;
            }
        }

        public List<Models.PaiKeFangAnBiao> paiKeJis(int PageIndex, int PageSize, out int Total)
        {
            Total = DBContext.PaiKeFangAnBiao.Count();
            var list = DBContext.PaiKeFangAnBiao.OrderByDescending(p => p.ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
            return list;
        }

        //public void Delete1()
        //{
        //    var a = DBContext.PaiKeFangAnBiao.Where(p => p.ID == 1).First();
        //    DBContext.PaiKeFangAnBiao.Remove(a);
        //}
    }
}
