using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.ZM
{
    public class MJiaoXueJieDuanBiaoDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();
       
        #region 查询所有的
        public List<JiaoXueJieDuanBiao> GetAll()
        {
            return context.Set<JiaoXueJieDuanBiao>().ToList();
        }
        #endregion

        public List<YuanGongBiao> ToList()
        {
            List<YuanGongBiao> list = new List<YuanGongBiao>();
            return list;
        }
    }
}
