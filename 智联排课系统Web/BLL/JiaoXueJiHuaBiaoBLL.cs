using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class JiaoXueJiHuaBiaoBLL : BLLBase<JiaoXueJiHuaBiao>
    {
        JiaoXueJiHuaBiaoDAL JiaoXueJiHuaBiaoDAL = new JiaoXueJiHuaBiaoDAL();
        public override void SetDAL()
        {
            this.dal = JiaoXueJiHuaBiaoDAL;
        }
    }
}
