using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
namespace DAL.CHB
{
    public class BuMenBiaoDAL
    {
        ZhiLianPaiKeXiTongDBEntities zl = new ZhiLianPaiKeXiTongDBEntities();

        #region 增加
        public int Add(BuMenBiao st)
        {
            zl.Set<BuMenBiao>().Add(st);
            return zl.SaveChanges();
        }
        #endregion

        #region 删除[以对象删除]
        public int Delete(BuMenBiao st)
        {
            //传入对象到上下文进行管理
            zl.Set<BuMenBiao>().Attach(st);
            //在上下文中移除
            zl.Set<BuMenBiao>().Remove(st);
            return zl.SaveChanges();
        }
        #endregion

        #region 修改
        public int Update(BuMenBiao t)
        {
            zl.Set<BuMenBiao>().Attach(t);
            //引用程序集system.data.entity ,引入命名空间System.Data
            zl.Entry<BuMenBiao>(t).State = EntityState.Modified;
            return zl.SaveChanges();
        }
        #endregion

        #region 查询所有的
        public List<BuMenBiao> GetAll()
        {

            return zl.Set<BuMenBiao>().ToList();
        }
        #endregion

        #region 查询一条
        public BuMenBiao GetOne(object id)
        {
            return zl.Set<BuMenBiao>().Find(id);
        }
        #endregion

        #region 以主键删除
        public int DeleteByPkey(object id)
        {
            var aobj = GetOne(id);
            return Delete(aobj);//调用删除对象方法
        }
        #endregion
    }
}
