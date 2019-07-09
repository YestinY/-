using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DAL
{
    public class DALBase<T> where T : class, new()
    {
        private ZhiLianPaiKeXiTongDBEntities dbcontext = DBContextFactory.CreateDbContext();
        //获取上下文
        public ZhiLianPaiKeXiTongDBEntities DBContext
        {
            get { return dbcontext; }
        }

        //增加
        public void Add(T t)
        {
            dbcontext.Set<T>().Add(t);
        }

        //修改
        public void Modify(T t)
        {
            dbcontext.Set<T>().Attach(t);
            dbcontext.Entry<T>(t).State = EntityState.Modified;
        }
        //删除
        public void Delete(T t)
        {
            dbcontext.Set<T>().Attach(t);
            dbcontext.Set<T>().Remove(t);
        }
        //单对象查询[]
        public T GetOneData(object id)
        {
            return dbcontext.Set<T>().Find(id);
        }
        //查询所有数据
        public virtual List<T> GetAllData()
        {
            return dbcontext.Set<T>().ToList();
        }

        public int Save()
        {
            return dbcontext.SaveChanges();
        }
        /////////////直接使用sql语句///////////////////////////
        public int ExcuteCommand(string sql, params SqlParameter[] pars)
        {
            return dbcontext.Database.ExecuteSqlCommand(sql, pars);
        }
        public List<K> GetDataBySQL<K>(string sql, params SqlParameter[] pars)
        {
            return dbcontext.Database.SqlQuery<K>(sql, pars).ToList();
        }
    }
}
