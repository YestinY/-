using Submail.AppConfig;
using Submail.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendMessage
{
    public class Class1
    {
        public BLL.YuanGongBiaoBLL yuan = new BLL.YuanGongBiaoBLL();
        public void Message()
        {
            var TeacherList = yuan.PhoneList();

            string returnMessage = string.Empty;
            foreach (var item in TeacherList)
            {
                IAppConfig appConfig = new MessageConfig("26920", "d22f4cccf0441fcafcab541dd7d6e646", SignType.md5);
                MessageSend messageSend = new MessageSend(appConfig);
                messageSend.AddTo(item.Phone);
                messageSend.AddContent("【智联科技有限公司】尊敬的" + item.Name + "(先生),本周的课表已出,请去官网or手机端进行查看。生话愉快。");
                messageSend.AddTag("xxxxx");
                messageSend.Send(out returnMessage);
            }
        }
    }
}
