using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;
namespace DAL.CHB
{
    public class YuanGongBiaoDAL:DALBase<Models.YuanGongBiao>
    {

        #region 分页查询所有数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">当前页码</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByPindex(int p, int size)
        {
            int n = (p - 1) * size;
            return DBContext.YuanGongBiao.
                OrderByDescending(k => k.ID)
                .Skip(n).Take(size).ToList();
        }
        #endregion

        #region 分页查询所有数据,可提供总页码
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">当前页码</param>
        /// <param name="size">每页条数</param>
        /// <param name="totPage">页码总数</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByPindex2(int p,
            int size, out int totPage) //out可以理解为输出参数
        {
            //获取条件数据
            List<YuanGongBiao> stuList = DBContext.YuanGongBiao.ToList();//所有数据
                                                                  //算条数

            //算总页数
            totPage = stuList.Count();

            int n = (p - 1) * size;
            return stuList.Skip(n).Take(size).ToList();
        }
        #endregion

        #region 根据名字关键字及年级ID多条件查询学生（两个可以组合）
        public List<YuanGongBiao> GetStudentByManyIf(string key, int zwid,
            int p, int size, out int totPage)
        {
            List<YuanGongBiao> stuList = DBContext.YuanGongBiao.ToList();//所有数据
            //////////////根据条件筛选数据[分部查询]///////////////////
            if (!string.IsNullOrEmpty(key))
            {
                stuList = stuList.
                    Where(q => q.Name.Contains(key)).ToList();//查询满足数据
            }
            if (zwid != -1)//-1为不限
            {
                stuList = stuList.
                   Where(q => q.ZhiWeiID == zwid).ToList();//查询满足数据
            }
            ///////////算总页码部分//////////////////////////
            //算条数
            int count = stuList.Count();
            //算总页数
            totPage = count % size == 0 ?
                count / size : count / size + 1;
            ////////分页返回数据//////////////////
            int n = (p - 1) * size;
            return stuList.Skip(n).Take(size).ToList();
        }
        #endregion


        #region 多条件查询学生[H-ui-admin框架查询数据 多条件]不需分页
        ////////////多条件查询学生[H-ui-admin框架查询数据 多条件]//////
        /// <summary>
        /// 多条件查询数据
        /// </summary>
        /// <param name="namekey">名字关键字</param>
        /// <param name="grade">年纪编号-1为查询所有</param>
        /// <param name="dzkey">住址关键字</param>
        /// <param name="sex">性别，传"-1",不限男女，0,女,1(男）</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByManyIf(string namekey, int zw,
            string dzkey, int zt)
        {
            //参数的集合
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " select * from YuanGongBiao where 1=1 ";
            if (!string.IsNullOrEmpty(namekey))
            {
                sql += "  and Name like @ygname  ";
                SqlParameter p1 = new SqlParameter("@ygname", "%" + namekey + "%");
                spls.Add(p1);
            }
            if (zw != -1)
            {
                sql += " and ZhiWeiID=@ZhiWeiID ";
                SqlParameter p2 = new SqlParameter("@ZhiWeiID", zw);
                spls.Add(p2);
            }
            if (!string.IsNullOrEmpty(dzkey))
            {
                sql += " and Address like @Address ";
                SqlParameter p3 = new SqlParameter("@Address", "%" + dzkey + "%");
                spls.Add(p3);
            }
            if (zt != -1) //-1不限，0,女,1男
            {
                sql += " and YuanGongZhuangTai=@YuanGongZhuangTai ";
                SqlParameter p4 = new SqlParameter("@XSSex", zt);
                spls.Add(p4);
            }
            //////////////////////////////
            return DBContext.Database.SqlQuery<YuanGongBiao>
                (sql, spls.ToArray()).ToList();
            ///////////////////////////////

        }

        #endregion

        #region 学生id的查询
        public YuanGongBiao GetStudentByXsId(int id)
        {
            return DBContext.YuanGongBiao.Find(id);
        }
        #endregion

        #region 类似1,2,3 格式的多条删除，批量删除，批量修改可以类比操作
        /// <summary>
        /// 多条数据删除
        /// </summary>
        /// <param name="ids">传过来的数据必须是1,2,3</param>
        /// <returns></returns>
        public int DeleteManyStudent(string ids)
        {
            string sql = "delete from YuanGongBiao where ID in (" + ids + ")";
            return DBContext.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        ////////////////////////////////////////
        #region 根据多个条件查询符合条件数据，并分页显示 本方法给fxlayui查询显示用
        /// <summary>
        /// 按照多个条件查询出分页数据
        /// </summary>
        /// <param name="namekey"></param>
        /// <param name="grade"></param>
        /// <param name="dzkey"></param>
        /// <param name="sex"></param>
        /// <param name="p">当前第几页</param>
        /// <param name="size">每页条数</param>
        /// <param name="totPage">总页码</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByManyIf2_fxlayui(string namekey, int zw,
            string dzkey, int zt, int p, int size, out int totPage)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " select * from YuanGongBiao where 1=1 ";
            if (!string.IsNullOrEmpty(namekey))
            {
                sql += "  and Name like @ygname  ";
                SqlParameter p1 = new SqlParameter("@ygname", "%" + namekey + "%");
                spls.Add(p1);
            }
            if (zw != -1)
            {
                sql += " and ZhiWeiID=@ZhiWeiID ";
                SqlParameter p2 = new SqlParameter("@ZhiWeiID", zw);
                spls.Add(p2);
            }
            if (!string.IsNullOrEmpty(dzkey))
            {
                sql += " and Address like @Address ";
                SqlParameter p3 = new SqlParameter("@Address", "%" + dzkey + "%");
                spls.Add(p3);
            }
            if (zt != -1) //-1不限，0,女,1男
            {
                sql += " and YuanGongZhuangTai=@YuanGongZhuangTai ";
                SqlParameter p4 = new SqlParameter("@YuanGongZhuangTai", zt);
                spls.Add(p4);
            }
            //////////////////////////////
            List<YuanGongBiao> stuList = DBContext.Database.SqlQuery<YuanGongBiao>
                (sql, spls.ToArray()).ToList();//通过sql语句查询所有符合条件数据
            ///////////算总页码部分//////////////////////////
            //算条数
            int count = stuList.Count();
            //算总页数
            totPage = count;
            ////////分页返回数据//////////////////
            int n = (p - 1) * size;
            return stuList.OrderByDescending(x => x.ID).Skip(n).Take(size).ToList();
        }
        #endregion

        #region 批量删除，多条数据写法

        /// <summary>
        /// 多条数据删除方法
        /// </summary>
        /// <param name="ids">ids格式1,2,3,4</param>
        /// <returns>删除成功的行数</returns>
        public int DeleteManyByIdS(string ids)
        {
            string sql = "delete from YuanGongBiao where ID in (" + ids + ")";
            //直接执行sql语句
            return DBContext.Database.ExecuteSqlCommand(sql);
        }

        public int DeleteManyByIdS(int ids)
        {
            string sql = "delete from YuanGongBiao where ID=@ID";
            SqlParameter sqr = new SqlParameter("@ID", ids);
            //直接执行sql语句
            return DBContext.Database.ExecuteSqlCommand(sql,sqr);
        }

        #endregion

        public int Update(YuanGongBiao st)
        {
            st.JiaoYuanMingChen = st.Name;
            string sql = null;
            if (st.YuanGongZhuangTai != null)
            {
                 sql = "update YuanGongBiao set Name = '" + st.Name + "',Phone = '" + st.Phone + "',Address = '" + st.Address + "',YuanGongZhuangTai = " + st.YuanGongZhuangTai + ",BeiZhu = '" + st.BeiZhu + "',ShanChangKeCheng = '" + st.ShanChangKeCheng + "',ZhiWeiID = " + st.ZhiWeiID + ",JiaoYuanMingChen = '" + st.JiaoYuanMingChen + "' where ID = " + st.ID;
            }
            if (st.YuanGongZhuangTai == null)
            {
                 sql = "update YuanGongBiao set Name = '" + st.Name + "',Phone = '" + st.Phone + "',Address = '" + st.Address + ",BeiZhu = '" + st.BeiZhu + "',ShanChangKeCheng = '" + st.ShanChangKeCheng + "',ZhiWeiID = " + st.ZhiWeiID + ",JiaoYuanMingChen = '" + st.JiaoYuanMingChen + "' where ID = " + st.ID;
            }
            return DBContext.Database.ExecuteSqlCommand(sql);
        }

    }
}
