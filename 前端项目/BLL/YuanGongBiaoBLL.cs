using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
   public class YuanGongBiaoBLL
    {
       YuanGongBiaoDAL dal = new YuanGongBiaoDAL();

       #region 登录
       public YuanGongBiao Login(string phone, string pwd)
       {
           return dal.Login(phone, pwd);
       }
       #endregion

       #region 修改教员信息
       public int UpdateYuanGong(YuanGongBiao yg)
       {
           return dal.UpdateYuanGong(yg);
       }
       #endregion

    }
}
