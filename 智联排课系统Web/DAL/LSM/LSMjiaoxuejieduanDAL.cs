using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using System.Data;

namespace DAL.LSM
{
   public class LSMjiaoxuejieduanDAL
    {
       ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 查询所有
        public List<JiaoXueJieDuanBiao> GetAll()
        {
            return context.Set<JiaoXueJieDuanBiao>().ToList();
        }
        #endregion
       
        #region 根据多个条件查询，分页显示
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="shenhe"></param>
        /// <param name="qiyong"></param>
        /// <param name="p">当前第几页</param>
        /// <param name="size">每页条数</param>
        /// <param name="totpage"></param>
        /// <returns></returns>
        public List<JiaoXueJieDuanBiao> GetjieduanByLike(int id, string name, int shenhe, int qiyong, int p, int size, out int totpage)
        {
            List<SqlParameter> sqls = new List<SqlParameter>();
            string sql = "select * from JiaoXueJieDuanBiao where 1=1";
            if (id != -1)
            {
                sql += " and ID=@ID";
                SqlParameter p1 = new SqlParameter("@ID", id);
                sqls.Add(p1);
            }
            if (!string.IsNullOrEmpty(name))
            {
                sql += " and JieDuanMing like @JieDuanMing";
                SqlParameter p2 = new SqlParameter("@JieDuanMing", "%" + name + "%");
                sqls.Add(p2);
            }
            if (shenhe != -1)
            {
                sql += " and ShenHeShiFouTongGuo=@ShenHeShiFouTongGuo";
                SqlParameter p3 = new SqlParameter("@ShenHeShiFouTongGuo", shenhe);
                sqls.Add(p3);
            }
            if (qiyong != -1)
            {
                sql += " and ShiFouQiYong=@ShiFouQiYong";
                SqlParameter p4 = new SqlParameter("@ShiFouQiYong", qiyong);
                sqls.Add(p4);
            }
            List<JiaoXueJieDuanBiao> list = context.Database.SqlQuery<JiaoXueJieDuanBiao>(sql, sqls.ToArray()).ToList();
            totpage = list.Count();
            int n = (p - 1) * size;
            return list.OrderBy(x => x.ID).Skip(n).Take(size).ToList();
        }
        #endregion

        #region 新增时查询阶段名是否重复
        public JiaoXueJieDuanBiao SelectGetName(string name,int ID)
        {
            try
            {
                return context.JiaoXueJieDuanBiao.Where(p => p.JieDuanMing == name &&p.ID!=ID).First();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }

        public JiaoXueJieDuanBiao SelectGetName(string name)
        {
            try
            {
                return context.JiaoXueJieDuanBiao.Where(p => p.JieDuanMing == name).First();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }
        }
        #endregion

        #region 新增
        public int Addjiaoxuejieduan(JiaoXueJieDuanBiao jxjd) 
        {
            context.Set<JiaoXueJieDuanBiao>().Add(jxjd);
            return context.SaveChanges();
        }
        #endregion

        #region 主键查询一条
        public JiaoXueJieDuanBiao GetOne(object id)
        {
            return context.Set<JiaoXueJieDuanBiao>().Find(id);
        }
        #endregion

        #region 修改
        public int Updatejiaoxuejieduan(JiaoXueJieDuanBiao jxjd) 
        {
            context.Set<JiaoXueJieDuanBiao>().Attach(jxjd);
            context.Entry<JiaoXueJieDuanBiao>(jxjd).State = EntityState.Modified;
            return context.SaveChanges();
        }
        #endregion

        #region 删除[以对象删除]
        public int Deletejiaoxuejieduan(JiaoXueJieDuanBiao jxjd)
        {
            context.Set<JiaoXueJieDuanBiao>().Attach(jxjd);
            context.Set<JiaoXueJieDuanBiao>().Remove(jxjd);
            return context.SaveChanges();
        }
        #endregion

        #region 批量删除
        public int DeleteManyIds(string ids)
        {
            string sql = "delete from JiaoXueJieDuanBiao where ID in (" + ids + ")";
            return context.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region 删除时判断
       //教学课程表
        public int SelectKC(int ID)
        {
            return context.JiaoXueKeCheng.Where(p => p.SuoShuJiaoXueJieDuan == ID).Count();
        }
       //班级开设课程计划表
        public int SelectJH(int ID)
        {
            return context.BanJiKaiSheKeChengJiHuaBiao.Where(p => p.KaiSheJiaoXueJieDuan == ID).Count();
        }
        #endregion

        
    }
}
