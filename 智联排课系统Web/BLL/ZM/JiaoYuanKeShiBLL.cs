using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.ZM;

namespace BLL.ZM
{
   public class JiaoYuanKeShiBLL
    {
        JiaoYuanKeShiDAL j = new JiaoYuanKeShiDAL();

        public int GetKeShi()
        {
            int i = j.del();
            j.GetKeShi();
            return 1;
        }

        public List<KeShiBiao> GetAll()
        {
            return j.GetAll();
        }
    }
}
