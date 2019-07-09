using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class BeCiPaiKeMorenKeCiBiaoDAL : DALBase<Models.BeCiPaiKeMorenKeCiBioa>
    {
        public int Add(int Count, string ID)
        {
            StringBuilder @string = new StringBuilder();
            //@string.Append("TRUNCATE TABLE  BeCiPaiKeMorenKeCiBioa;");
            @string.Append("insert into BeCiPaiKeMorenKeCiBioa(ZiYuanBianHao,ZiYuanMingChen,ClassCount)");
            @string.Append("select ID,ZiYuanMing,'" + Count + "' from ZiYuanGuanLi where ID in(" + ID + ")");
            return ExcuteCommand(@string.ToString());
        }
    }
}
