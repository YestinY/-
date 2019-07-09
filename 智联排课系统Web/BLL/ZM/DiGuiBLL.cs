using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ZM;
using Models;
using Models.ZM;

namespace BLL.ZM
{
    public class DiGuiBLL
    {
        public List<DG> AddChildDept(int parentId)
        {
            DiGuiDAL d = new DiGuiDAL();
            List<DG> dg = d.Add(parentId);
            dg = d.AddChildDept(parentId);
            return dg;
        }
    }
}
