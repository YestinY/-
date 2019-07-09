using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class PaiKeListDAL
    {
        //当天未安排课的教室
        public List<Models.BenCiPaiBanKeYongZiYuan> List(DateTime date)
        {
            string sql = "select * from [dbo].[BenCiPaiBanKeYongZiYuan] where KeYongZiYuanBianHao not in (select ID from [dbo].[ZiYuanGuanLi] where ZiYuanMing in (select JS from PaikeLinshiBiao where RQ=@Time))";
            SqlParameter sqr = new SqlParameter("@Time", date);
            return DBContext.Database.SqlQuery<Models.BenCiPaiBanKeYongZiYuan>(sql, sqr).ToList();
        }

        //当天的某一个时段未安排的教室
        public List<Models.BenCiPaiBanKeYongZiYuan> List(DateTime date, string SD)
        {
            string sql = "select * from[dbo].[BenCiPaiBanKeYongZiYuan] where KeYongZiYuanBianHao not in (select ID from[dbo].[ZiYuanGuanLi] where ZiYuanMing  in (select JS from PaikeLinshiBiao where RQ=@Time And SD =@SD))";
            SqlParameter[] sqr = {
                new SqlParameter("@Time", date),
                new SqlParameter("@SD", SD)
            };
            return DBContext.Database.SqlQuery<Models.BenCiPaiBanKeYongZiYuan>(sql, sqr).ToList();
        }

        //删除零时表数据
        public void DeleteTable()
        {
            string sql = "truncate table PaikeLinshiBiao";
            ExcuteCommand(sql);
        }

    }
}
