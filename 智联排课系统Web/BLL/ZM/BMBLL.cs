using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.ZM;
using Models.ZM;
namespace BLL.ZM
{
    public class BMBLL
    {
        BMDAL bm = new BMDAL();

        //查询全部
        public List<BuMenBiao> GetBM(string name, int zt, int p, int size, out int totPage)
        {
            return bm.GetBM(name, zt, p, size, out totPage);
        }

        #region 获取部门人员
        public List<YGB> ygName(int ID)
        {
            return bm.ygName(ID);
        }
        #endregion

        #region 增加部门负责人
        public int AddFZR(int ID, string fuzeren)
        {
            return bm.AddFZR(ID, fuzeren);
        }
        #endregion


        #region 查询员工所有
        public List<YuanGongBiao> YGToList()
        {
            return bm.YGToList();
        }
        #endregion
    }
}
