using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StageCourse
    {
        /// <summary>
        /// 阶段名
        /// </summary>
        public string StageName { get; set; }

        /// <summary>
        /// 阶段ID
        /// </summary>
        public int StageID { get; set; }

        /// <summary>
        /// 计划名
        /// </summary>
        public string PlayName { get; set; }

        /// <summary>
        /// 计划ID
        /// </summary>
        public int PlayID { get; set; }

        /// <summary>
        /// 课程名
        /// </summary>
        public string Course { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public int CourseID { get; set; }


        /// <summary>
        /// 章节
        /// </summary>
        public string section { get; set; }

        /// <summary>
        /// 章节ID
        /// </summary>
        public int sectionID { get; set; }

        /// <summary>
        /// 课时
        /// </summary>
        public int Hour { get; set; }

        /// <summary>
        /// 资源
        /// </summary>
        public string resource { get; set; }


        /// <summary>
        /// 教学计划的开展顺序号
        /// </summary>
        public int JiaoXueJiHuaDeKaiZhanShunXuHao { get; set; }


        /// <summary>
        /// 章节顺序号
        /// </summary>
        public int ZhangJieShunXuHao { get; set; }




    }
}
