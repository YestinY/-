using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public partial class BeCiPaiKeMorenKeCiBiaoBLL : BLLBase<Models.BeCiPaiKeMorenKeCiBioa>
    {
        public int Add(int Count, string ID)
        {
            return be.Add(Count, ID);
        }
    }
}
