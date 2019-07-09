using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ZM
{
    public class Grade
    {
        //班级id. 班级名称，开始时间，现有人数，现属阶段，
        //学生姓名，班主任，

        public int banjiID { get; set; }
        public string BanJiMing { get; set; }
        public DateTime KaiBanShiJian { get; set; }
        public int BanJiRenShu { get; set; }
        public int JieDuanID { get; set; }
        public string JieDuanMing { get; set; }
        public string StudentName { get; set; }
        public string Name { get; set; }
        public int BanZhuRenID { get; set; }
        public int BanJiZhuangTai { get; set; }
        public int bzrbjID { get; set; }
        public bool JiaoXuePlan { get; set; }
    }
}
