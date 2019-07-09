using Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class YuanGongBiaoDAL
    {
        private ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 登录(教员登录)
        public YuanGongBiao Login(string phone, string pwd)
        {
            try
            {
                if (pwd=="")
                {
                    var Teacher = context.YuanGongBiao.Where(p => p.Phone == phone).First();
                    return Teacher;
                }
                else
                {
                    var a = context.YuanGongBiao.Where(p => p.Phone == phone && p.MiMa == pwd).First();
                    return a;
                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
        #endregion

        #region 修改教员信息
        public int UpdateYuanGong(YuanGongBiao yg)
        {
            context.Set<YuanGongBiao>().Attach(yg);
            context.Entry<YuanGongBiao>(yg).State = EntityState.Modified;
            context.SaveChanges();
            return 1;
        }
        #endregion
    }
}
