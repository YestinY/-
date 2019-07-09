using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class PaiKeJiHuaDAL : DALBase<PaiKeJiHua>
    {
        //public int AddPaiKeJihua(Models.PaiKeJiHua paiKeJiHua)
        //{

        //}

        public List<Models.PaiKeJiHua> paiKeJiHuaList(string JiHuaMingChen, string KaiShiShiJian, string JieShuShiJian, int PageIndex, int PageSize, out int Total)
        {

            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " select * from PaiKeJiHua where 1=1 ";
            if (!string.IsNullOrEmpty(JiHuaMingChen))
            {
                sql += "  and JiHuaMingChen like @JiHuaMingChen  ";
                SqlParameter p1 = new SqlParameter("@JiHuaMingChen", "%" + JiHuaMingChen + "%");
                spls.Add(p1);
            }
            if (KaiShiShiJian != null && KaiShiShiJian != "")
            {
                sql += " and KaiShiShiJian>=@KaiShiShiJian ";
                SqlParameter p4 = new SqlParameter("@KaiShiShiJian", KaiShiShiJian);
                spls.Add(p4);
            }
            if (JieShuShiJian != null && JieShuShiJian != "")
            {
                sql += " and JieShuShiJian<=@JieShuShiJian ";
                SqlParameter p4 = new SqlParameter("@JieShuShiJian", JieShuShiJian);
                spls.Add(p4);
            }
            List<PaiKeJiHua> stuList = DBContext.Database.SqlQuery<PaiKeJiHua>
                (sql, spls.ToArray()).ToList();//通过sql语句查询所有符合条件数据
                                               ///////////算总页码部分//////////////////////////
                                               //算条数
                                               //算总页数
            Total = stuList.Count();
            ////////分页返回数据//////////////////
            int n = (PageIndex - 1) * PageSize;
            return stuList.OrderByDescending(x => x.ID).Skip(n).Take(PageSize).ToList();
        }
        public int DeleteManyByIdS(string ids)
        {
            string sql = "delete from PaiKeJiHua where ID in (" + ids + ")";
            //直接执行sql语句
            return DBContext.Database.ExecuteSqlCommand(sql);
        }
        public int SelectGetWeiZhi(string name)
        {
            return DBContext.PaiKeJiHua.Where(p => p.JiHuaMingChen == name).Count();
        }
        public int delete(string JiHuaMingChen)
        {
            var list = DBContext.BenCiPaiKeShiDuanBiao.Where(p => p.JiHuaMingChen == JiHuaMingChen);
            foreach (var item in list)
            {
                DBContext.BenCiPaiKeShiDuanBiao.Remove(item);
            }
            var list2 = DBContext.PaiKeJiHua.Single(p => p.JiHuaMingChen == JiHuaMingChen);
            DBContext.PaiKeJiHua.Remove(list2);
            return DBContext.SaveChanges();
        }

        //获取排课结束时间到期的数据
        public PaiKeJiHua one()
        {
            try
            {
                var one = DBContext.PaiKeJiHua.Where(p => p.JieShuShiJian <= DateTime.Now && p.ShiFouWanCheng == false).First();
                return one;
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
