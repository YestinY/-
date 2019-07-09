using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Runtime.Remoting.Messaging;
namespace DAL
{
    public class DBContextFactory
    {
        //httpcontent对象
        public static ZhiLianPaiKeXiTongDBEntities CreateDbContext()
        {
            ZhiLianPaiKeXiTongDBEntities dbcontex = null;
            dbcontex = CallContext.GetData("db") as ZhiLianPaiKeXiTongDBEntities;
            if (dbcontex == null)
            {
                dbcontex = new ZhiLianPaiKeXiTongDBEntities();
                CallContext.SetData("db", dbcontex);
            }
            return dbcontex;
        }
    }
}
