using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ZM
{
    public class Student
    {
        public int XID { get; set; }
        public string StudentName { get; set; }
        public Nullable<int> StudentClassID { get; set; }
        public Nullable<bool> Sex { get; set; }
        public Nullable<int> Age { get; set; }
        public string MiMa { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string HomePhone { get; set; }

        public string YBanJiMing { get; set; }
        public string BanJiMing { get; set; }
        public int JieDuanID { get; set; }

        public int BjID { get; set; }

        public string JieDuanMing { get; set; }

        public virtual JiaoXueJieDuanBiao Grade { get; set; }
    }
}
