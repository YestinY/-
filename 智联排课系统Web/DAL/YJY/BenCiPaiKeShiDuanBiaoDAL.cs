using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class BenCiPaiKeShiDuanBiaoDAL : DALBase<BenCiPaiKeShiDuanBiao>
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
            var List = (from p in DBContext.BenCiPaiKeShiDuanBiao join k in DBContext.PaiKeJiHua on p.JiHuaBianHao equals k.ID where k.ShiFouWanCheng == false select p).ToList();
            Total = List.Count();
            return List.OrderBy(p => p.ID).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }
        #endregion

        //删除以前的记录
        public int DeleteShiDuan()
        {
            string sql = "truncate table BenCiPaiKeShiDuanBiao";
            this.ExcuteCommand(sql);
            return 1;
        }
    }
}
