using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class KeChengPaiKeZongBiaoDAL : DALBase<KeChengPaiKeZongBiao>
    {

        //添加到排课总表
        public bool Add()
        {
            try
            {
                StringBuilder @string = new StringBuilder();
                //@string.Append(" delete from  BenCiPaiKeBanJiJiCiShu where ZhuangTai=4;");
                @string.Append("insert into KeChengPaiKeZongBiao (RiQi,ClassName,ShiJianDuan,ShiJianMing,KeChengBianHao,KeChengMingChen,ZhangJieBianHao,ZhangJieMingChen,ZiYuanBianHao,[ZiYuanMingChen],JiaoYuanBianHao,JiaoYuanMingChen,ShiShiShiJian,CanJiaRenYuan,BeiZhu,ZhuangTai)");
                @string.Append("select  RiQi,ClassName,ShiJianDuan,ShiJianMing,KeChengBianHao,KeChengMingChen,ZhangJieBianHao,ZhangJieMingChen,ZiYuanBianHao,[ZiYuanMingChen],JiaoYuanBianHao,JiaoYuanMingChen,ShiShiShiJian,CanJiaRenYuan,BeiZhu,'正在上课' from [dbo].[ZhengZaiShangKeBiao]");
                ExcuteCommand(@string.ToString());
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //一次排课课表信息
        public List<Models.KeChengPaiKeZongBiao> List(int PageIndex, int PageSize, out int total, string Name, DateTime date, int Teacher)
        {
            var List = DBContext.KeChengPaiKeZongBiao.ToList();
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
    }
}
