using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class JiaoXueKeChengDAL : DALBase<JiaoXueKeCheng>
    {
        //班级教学课程
        public List<Models.JiaoXueKeCheng> keChengs(int ID)
        {
            string sql = "select * from [dbo].[JiaoXueKeCheng] where [ID] in (select [KeChengMing] from  [dbo].[BanJiKaiSheKeChengJiHuaBiao] where [BanJiID]=@ID)";
            SqlParameter sqr = new SqlParameter("@ID", ID);
            return GetDataBySQL<Models.JiaoXueKeCheng>(sql, sqr).ToList();
        }
    }
}
