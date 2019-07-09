using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;

namespace BLL
{
   public class XueShengBiaoBLL
    {
       XueShengBiaoDAL dal = new XueShengBiaoDAL();

       #region 登录
       public XueShengBiao Login(string phone, string pwd)
       {
           return dal.Login(phone, pwd);
       }
       #endregion

       #region 修改学生信息
       public int UpdateXueSheng(XueShengBiao stu)
       {  
           return dal.UpdateXueSheng(stu);
       }
       #endregion
    }
}
