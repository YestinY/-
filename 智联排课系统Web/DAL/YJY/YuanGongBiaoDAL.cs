using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class YuanGongBiaoDAL : DALBase<YuanGongBiao>
    {
        //教员登录
        public Models.YuanGongBiao login(Models.YuanGongBiao yuanGongBiao)
        {
            try
            {
                var json = DBContext.YuanGongBiao.Where(p => p.MiMa == yuanGongBiao.MiMa && p.Phone == yuanGongBiao.Phone).First();
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //电话号码不为空的教员
        public List<Models.YuanGongBiao> PhoneList()
        {
            string sql = "select * from [dbo].[YuanGongBiao] where Phone!=''";
            return GetDataBySQL<Models.YuanGongBiao>(sql).ToList();
        }

        //本时段未安排课的教员并且擅长课程对口的教员
        public List<Models.YuanGongBiao> DaiKeTeacher(DateTime time, string KC)
        {
            try
            {
                string sql = "select * from [dbo].[YuanGongBiao] where [ID] not   in (select [JiaoYuanBianHao] from [dbo].[YiCiPaiKeDeKeBiaoXinXiBiao] where [RiQi] = @Time and [KeChengMingChen] != '自习' ) and [ZhiWeiID] = 1 and [ShanChangKeCheng] like @KC";
                SqlParameter[] sqls = new SqlParameter[] {
                new SqlParameter("@Time", time),
                new SqlParameter("@KC","%"+KC+"%")
            };
                return DBContext.Database.SqlQuery<Models.YuanGongBiao>(sql, sqls.ToArray()).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }

        //教员和班主任
        public List<YuanGongBiao> yuanGongBiaos()
        {
            string sql = "select * from [dbo].[YuanGongBiao] where [ZhiWeiID] in(1,2)";
            return DBContext.Database.SqlQuery<Models.YuanGongBiao>(sql).ToList();
        }
    }
}
