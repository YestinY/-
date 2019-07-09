using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class JiaoYuanDuiYingJiHuaZhangJieBLL : BLLBase<JiaoYuanDuiYingJiHuaZhangJie>
    {
        JiaoYuanDuiYingJiHuaZhangJieDAL jiao = new JiaoYuanDuiYingJiHuaZhangJieDAL();
        public override void SetDAL()
        {
            this.dal = jiao;
        }
    }
}
