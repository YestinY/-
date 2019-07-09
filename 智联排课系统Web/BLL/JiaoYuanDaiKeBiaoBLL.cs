using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class JiaoYuanDaiKeBiaoBLL : BLLBase<JiaoYuanDaiKeBiao>
    {
        JiaoYuanDaiKeBiaoDAL biaoBLL = new JiaoYuanDaiKeBiaoDAL();
        public override void SetDAL()
        {
            this.dal = biaoBLL;
        }
    }
}
