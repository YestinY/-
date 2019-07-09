using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.CHB
{
    public class BuMenBiaoBLL
    {
        public DAL.CHB.BuMenBiaoDAL Bll = new DAL.CHB.BuMenBiaoDAL();

        public int Add(BuMenBiao st)
        {
            return Bll.Add(st);
        }

        #region 删除[以对象删除]
        public int Delete(BuMenBiao st)
        {
            return Bll.Delete(st);
        }
        #endregion

        #region 修改
        public int Update(BuMenBiao t)
        {
            return Bll.Update(t);
        }
        #endregion

        #region 查询所有的
        public List<BuMenBiao> GetAll()
        {
            return Bll.GetAll();
        }
        #endregion

        #region 查询一条
        public BuMenBiao GetOne(object id)
        {
            return Bll.GetOne(id);
        }
        #endregion

        #region 以主键删除
        public int DeleteByPkey(object id)
        {
            return Bll.DeleteByPkey(id);
        }
        #endregion
    }
}
