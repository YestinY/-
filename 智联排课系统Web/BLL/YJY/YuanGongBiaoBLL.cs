using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL
{
    public partial class YuanGongBiaoBLL : BLLBase<YuanGongBiao>
    {
        public YuanGongBiao YuanGongBiao(Models.YuanGongBiao yuanGongBiao)
        {
            return dAL.login(yuanGongBiao);
        }

        public List<Models.YuanGongBiao> PhoneList()
        {
            return dAL.PhoneList();
        }

        public List<Models.YuanGongBiao> DaiKeTeacher(DateTime date, string KC)
        {
            return dAL.DaiKeTeacher(date, KC);
        }

        public List<Models.YuanGongBiao> TeacherBZR()
        {
            return dAL.yuanGongBiaos();
        }

    }
}
