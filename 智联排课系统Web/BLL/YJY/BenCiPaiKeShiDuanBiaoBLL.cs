using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BenCiPaiKeShiDuanBiaoBLL : BLLBase<BenCiPaiKeShiDuanBiao>
    {

        #region 显示未完成的排课时段
        /// <summary>
        /// 显示未完成的排课时段
        /// </summary>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="Total">总条数</param>
        /// <returns></returns>
        public List<Models.BenCiPaiKeShiDuanBiao> List(int PageIndex, int PageSize, out int Total)
        {
            return dAL.List(PageIndex, PageSize, out Total);
        }
        #endregion


        
    }
}
