using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class BanJiKaiSheKeChengJiHuaBiaoBLL : BLLBase<BanJiKaiSheKeChengJiHuaBiao>
    {
        BanJiKaiSheKeChengJiHuaBiaoDAL ban = new BanJiKaiSheKeChengJiHuaBiaoDAL();
        public override void SetDAL()
        {
            this.dal = ban;
        }
    }
}
