using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class ZhengZaiShangKeBiaoDAL : DALBase<ZhengZaiShangKeBiao>
    {
        //教务导出课表后,添加到正在上课表
        public bool Add()
        {
            try
            {
                StringBuilder @string = new StringBuilder();
                //@string.Append(" delete from  BenCiPaiKeBanJiJiCiShu where ZhuangTai=4;");
                @string.Append(" truncate table BenCiPaiKeBanJiJiCiShu;");
                @string.Append("insert into ZhengZaiShangKeBiao (RiQi,ClassName,ShiJianDuan,ShiJianMing,KeChengBianHao,KeChengMingChen,ZhangJieBianHao,ZhangJieMingChen,ZiYuanBianHao,[ZiYuanMingChen],JiaoYuanBianHao,JiaoYuanMingChen,ShiShiShiJian,CanJiaRenYuan,BeiZhu,ZhuangTai)");
                @string.Append("select  RiQi,ClassName,ShiJianDuan,ShiJianMing,KeChengBianHao,KeChengMingChen,ZhangJieBianHao,ZhangJieMingChen,ZiYuanBianHao,[ZiYuanMingChen],JiaoYuanBianHao,JiaoYuanMingChen,ShiShiShiJian,CanJiaRenYuan,BeiZhu,'正在上课' from [dbo].[YiCiPaiKeDeKeBiaoXinXiBiao]");
                ExcuteCommand(@string.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //截断正在上课表数据
        public bool TruncateZhengzaishangkebiao()
        {
            try
            {
                string sql = "truncate table ZhengZaiShangKeBiao";
                ExcuteCommand(sql);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        //正在上课表课表信息
        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, out int total, string Name, DateTime date, int Teacher)
        {
            var List = DBContext.ZhengZaiShangKeBiao.ToList();
            DateTime date2 = new DateTime(0001, 1, 1);
            if (!string.IsNullOrEmpty(Name))
            {
                List = List.Where(p => p.ClassName.Contains(Name)).ToList();
            }
            if (date != date2)
            {
                List = List.Where(p => p.RiQi == date).ToList();
            }
            if (Teacher != -1)
            {
                List = List.Where(p => p.JiaoYuanBianHao == Teacher.ToString()).ToList();
            }
            total = List.Count();
            return List.OrderBy(p => p.RiQi).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }


        //手机端
        public List<Models.ZhengZaiShangKeBiao> List(int PageIndex, int PageSize, string Name, DateTime date, out int Total)
        {
            var List = DBContext.ZhengZaiShangKeBiao.ToList();
            DateTime date2 = new DateTime(0001, 1, 1);
            if (!string.IsNullOrEmpty(Name))
            {
                //string[] vs = Name.Split(',');
                List = List.Where(p => p.ClassName.Contains(Name)).ToList();
            }
            if (date != date2)
            {
                List = List.Where(p => p.RiQi == date).ToList();
            }
            //Total = List.Count() % PageSize == 0 ? PageSize % List.Count() : PageSize % List.Count() + 1;
            Total = List.Count();
            return List.OrderBy(p => p.RiQi).Skip((PageIndex - 1) * PageSize).Take(PageSize).ToList();
        }

    }
}
