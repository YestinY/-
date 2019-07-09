using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ZM
{
    public class YGB
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string RuGangShiJian { get; set; }
        public Nullable<int> ZhiWeiID { get; set; }
        public string ShanChangKeCheng { get; set; }
        public string BeiZhu { get; set; }
        public Nullable<int> YuanGongZhuangTai { get; set; }
        public string MiMa { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }

        public Nullable<int> BuMenID { get; set; }
        public string BuMenMingCheng { get; set; }
        public Nullable<int> DuiYingFuJiID { get; set; }
        public string ZhiWeiMing { get; set; }
    }
}
