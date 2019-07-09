using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class YiCiPaiKeDeKeBiaoXinXiBiaoBLL : BLLBase<YiCiPaiKeDeKeBiaoXinXiBiao>
    {


        public List<Models.YiCiPaiKeDeKeBiaoXinXiBiao> List(int PageIndex, int PageSize, out int Total, string Name, DateTime date, int Teacher)
        {
            return dAL.List(PageIndex, PageSize, out Total, Name, date, Teacher);
        }

        public List<Models.YiCiPaiKeDeKeBiaoXinXiBiao> List(int PageIndex, int PageSize, string Name, DateTime date,out int Total)
        {
            return dAL.List(PageIndex, PageSize, Name, date,out Total);
        }

        public List<Models.Hi> banJiKaiSheKeChengJiHuaBiaos()
        {
            return dAL.banJiKaiSheKeChengJiHuaBiaos();
        }

        public List<Hi> List(int ID)
        {
            return dAL.JioaYunaShouDaiBanJi(ID);
        }

        public void DeleteTable()
        {
            dAL.DeleteTable();
        }

         
    }
}
