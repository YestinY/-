using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class PaiKeFangAnBLL : BLLBase<Models.PaiKeFangAnBiao>
    {
        //删除所有方案
        public int DeleteTable()
        {
            return dAL.Delete();
        }

        public List<Models.PaiKeFangAnBiao> List(int PageIndex, int PageSize, out int Total)
        {
            return dAL.paiKeJis(PageIndex, PageSize, out Total);
        }

        //public void Delete1()
        //{
        //    dAL.Delete1();
        //}m
    }
}
