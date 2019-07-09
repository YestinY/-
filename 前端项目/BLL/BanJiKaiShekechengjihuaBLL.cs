using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
   public class BanJiKaiShekechengjihuaBLL
    {
       BanJiKaiShekechengjihuaDAL dal = new BanJiKaiShekechengjihuaDAL();

       #region 查询所有
       public List<BanJiKaiSheKeChengJiHuaBiao> GetAll()
       {
           return dal.GetAll();
       }
       #endregion

       #region 分页
       public List<BanJiKaiSheKeChengJiHuaBiao> GetPage(int p, int size, out int totpage,int ClassID)
       {
           return dal.GetPage(p, size, out totpage,ClassID);
       }
        #endregion

        #region 分页
        public List<BanJiKaiSheKeChengJiHuaBiao> GetPage(int p, int size, out int totpage,string TeacherID)
        {
            return dal.GetPage(p, size, out totpage, TeacherID);
        }
        #endregion
    }
}
