using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ClassKC
    {
        public string ClassName { get; set; } //班级名称

        public int ClassID { get; set; } //班级ID

        public string KeChengMing { get; set; } //课程名

        public int KCID { get; set; } //课程ID

        public string CaiYongJiaoXueJiHua { get; set; } //采用教学计划

        public string KaiSheJiaoXueJieDuan { get; set; } //开设教学阶段

        public string ZhangJieMingChen { get; set; } //章节名

        public string AnPaiJiaoYuan { get; set; } //安排教员

        public int AnPaiJiaoYuanID { get; set; } //安排教员

        public bool ShiFouYiWanCheng { get; set; } //是否已完成
    }
}
