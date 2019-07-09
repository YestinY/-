using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class PaiKeListBLL : BLLBase<Models.PaikeLinshiBiao>
    {
        public List<Models.BenCiPaiBanKeYongZiYuan> List(DateTime date)
        {
            return dAL.List(date);
        }

        public List<Models.BenCiPaiBanKeYongZiYuan> List(DateTime date, string SD)
        {
            return dAL.List(date, SD);
        }

        public void DeleteTable()
        {
            dAL.DeleteTable();
        }

    }
}
