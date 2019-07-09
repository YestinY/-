using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.ZM;
using Models.ZM;

namespace BLL.ZM
{
    public class AnPaiKeChengBLL
    {
        AnPaiKeChengDAL AP = new AnPaiKeChengDAL();
        #region 查询教员
        //public List<YuanGongBiao> Teacher(int BanJiID)
        //{
        //    return AP.Teacher(BanJiID);
        //}
        #endregion

        #region 查询课程
        public List<JiaoXueKeCheng> KeCheng(int BanJiID)
        {
            return AP.KeCheng(BanJiID);
        }
        #endregion

        #region 查询班主任
        public List<YGB> ygList(){
            return AP.ygList();
        }
        #endregion
    }
}
