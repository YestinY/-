using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class PaiKeBanJiMoRenSheZhiBLL : BLLBase<PaiKeBanJiMoRenSheZhi>
    {

        public List<Models.PaiKeBanJiMoRenSheZhi> moRenSheZhisList(string StudentName, int PageIndex, int PageSize, out int Total)
        {
            return dAL.moRenSheZhisList(StudentName, PageIndex, PageSize, out Total);
        }
    }
}
