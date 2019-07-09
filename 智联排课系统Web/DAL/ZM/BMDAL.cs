using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data.SqlClient;
using Models.ZM;

namespace DAL.ZM
{
    public class BMDAL
    {

        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();
        //部门查询
        public List<BuMenBiao> GetBM(string name, int zt, int p, int size, out int totPage)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " select * from BuMenBiao where 1=1 ";
            if (!string.IsNullOrEmpty(name))
            {
                sql += "  and BuMenMingCheng like @ygname  ";
                SqlParameter p1 = new SqlParameter("@ygname", "%" + name + "%");
                spls.Add(p1);
            }
            if (zt != -1) //-1不限，0,女,1男
            {
                sql += " and ShiFouQiYong=@YuanGongZhuangTai ";
                SqlParameter p4 = new SqlParameter("@YuanGongZhuangTai", zt);
                spls.Add(p4);
            }
            //////////////////////////////
            List<BuMenBiao> stuList = context.Database.SqlQuery<BuMenBiao>
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

        #region 获取部门人员
        public List<YGB> ygName(int ID)
        {
            string sql = "select X.ID,X.Name,X.RuGangShiJian From BuMenBiao A,ZhiWeiBiao B ,YuanGongBiao X where A.ID = B.BuMenID and X.ZhiWeiID = B.ID and A.ID =" + ID;
            return context.Database.SqlQuery<YGB>(sql).ToList();
        }
        #endregion

        #region 增加部门负责人
        public int AddFZR(int ID, string fuzeren)
        {
            string sql = "update BuMenBiao set BuMenFuZheRen = " + fuzeren + " where ID = " + ID;
            return context.Database.ExecuteSqlCommand(sql);
        }
        #endregion


        #region 查询员工所有
        public List<YuanGongBiao> YGToList()
        {
            return context.YuanGongBiao.ToList();
        }
        #endregion
    }
}
