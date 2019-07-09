using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class ZiYuanGuanLiDAL : DALBase<ZiYuanGuanLi>
    {

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
        public List<ZiYuanGuanLi> GetStudentByManyIf2_fxlayui(string ZiYuanMing, string ZiYuanLeiXing,
              string ZiYuanWeiZhi, int p, int size, out int totPage)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " select * from ZiYuanGuanLi where 1=1 ";
            if (!string.IsNullOrEmpty(ZiYuanMing))
            {
                sql += "  and ZiYuanMing like @ZiYuanMing  ";
                SqlParameter p1 = new SqlParameter("@ZiYuanMing", "%" + ZiYuanMing + "%");
                spls.Add(p1);
            }
            if (!string.IsNullOrEmpty(ZiYuanLeiXing) && ZiYuanLeiXing != "")
            {
                sql += " and ZiYuanLeiXing=@ZiYuanLeiXing ";
                SqlParameter p2 = new SqlParameter("@ZiYuanLeiXing", ZiYuanLeiXing);
                spls.Add(p2);
            }
            if (!string.IsNullOrEmpty(ZiYuanWeiZhi) && ZiYuanWeiZhi != "")
            {
                sql += " and ZiYuanWeiZhi=@ZiYuanWeiZhi ";
                SqlParameter p4 = new SqlParameter("@ZiYuanWeiZhi", ZiYuanWeiZhi);
                spls.Add(p4);
            }
            //////////////////////////////
            List<ZiYuanGuanLi> stuList = DBContext.Database.SqlQuery<ZiYuanGuanLi>
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
            string sql = "delete from ZiYuanGuanLi where ID in (" + ids + ")";
            //直接执行sql语句
            return DBContext.Database.ExecuteSqlCommand(sql);
        }

        #endregion
        public ZiYuanGuanLi SelectGetName(string name)
        {
            try
            {
                return DBContext.ZiYuanGuanLi.Where(P => P.ZiYuanMing == name).First();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }
        public ZiYuanGuanLi SelectGetName(string name, int ID)
        {
            try
            {
                return DBContext.ZiYuanGuanLi.Where(P => P.ZiYuanMing == name && P.ID != ID).First();
            }
            catch (Exception ex)
            {
                return null;
                throw ex;
            }

        }
        public int SelectGetWeiZhi(int ID)
        {
            return DBContext.ZiYuanGuanLi.Where(p => p.ID == ID).Count();
        }
        public int SelectGetSk(string ID)
        {
            int id =Convert.ToInt32(ID);
            return DBContext.PaiKeShiDuanYuZiYuanZuHe.Where(p => p.KeYongZiYuanBianHao.Value == id).Count();
        }
    }
}
