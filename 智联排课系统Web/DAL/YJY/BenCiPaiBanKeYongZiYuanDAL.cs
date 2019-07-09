using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class BenCiPaiBanKeYongZiYuanDAL : DALBase<BenCiPaiBanKeYongZiYuan>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public int Add(int ID)
        {
            try
            {
                string Sql = "insert into BenCiPaiBanKeYongZiYuan(KeYongZiYuanBianHao,KeYongZiYuanMingChen,PaiKeJiHuaBianHao)select ID,ZiYuanMing,'" + ID + "'from ZiYuanGuanLi";
                ExcuteCommand(Sql);
                return 1;
            }
            catch (Exception ex)
            {

                return 0;
            }
        }


        //清除之前的记录
        public int DeleteBenCiPaiBanKeYongZiYuan()
        {
            string sql = "truncate table BenCiPaiBanKeYongZiYuan";
            ExcuteCommand(sql);
            return 1;
        }
    }
}
