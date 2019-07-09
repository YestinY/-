using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
   public class XueShengBiaoDAL
    {
       ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 登录(学生登录)
        public XueShengBiao Login(string phone, string pwd)
        {
            try
            {
                if (pwd=="")
                {
                    var a = context.XueShengBiao.Where(p => p.Phone == phone).First();
                    return a;
                }
                else
                {
                    var a = context.XueShengBiao.Where(p => p.Phone == phone && p.MiMa == pwd).First();
                    return a;
                }
               
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        #region 修改学生信息
        public int UpdateXueSheng(XueShengBiao  stu)
        {
            context.Set<XueShengBiao>().Attach(stu);
            context.Entry<XueShengBiao>(stu).State = EntityState.Modified;
            context.SaveChanges();
            return 1;
        }
        #endregion
        
    }
}
