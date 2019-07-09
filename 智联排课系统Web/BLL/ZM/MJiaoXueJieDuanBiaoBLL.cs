using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.ZM;

namespace BLL.ZM
{
    public class MJiaoXueJieDuanBiaoBLL
    {
        MJiaoXueJieDuanBiaoDAL mbll = new MJiaoXueJieDuanBiaoDAL();

        #region 查询所有的
        public List<JiaoXueJieDuanBiao> GetAll()
        {
            return mbll.GetAll();
        }
        #endregion
    }
}
