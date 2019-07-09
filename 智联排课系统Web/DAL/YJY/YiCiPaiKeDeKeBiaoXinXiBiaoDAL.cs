using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class YiCiPaiKeDeKeBiaoXinXiBiaoDAL : DALBase<YiCiPaiKeDeKeBiaoXinXiBiao>
    {
        //一次排课课表信息
        public List<Models.YiCiPaiKeDeKeBiaoXinXiBiao> List(int PageIndex, int PageSize, out int total, string Name, DateTime date, int Teacher)
        {
            var List = DBContext.YiCiPaiKeDeKeBiaoXinXiBiao.ToList();
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
        public List<Models.YiCiPaiKeDeKeBiaoXinXiBiao> List(int PageIndex, int PageSize, string Name, DateTime date, out int Total)
        {
            var List = DBContext.YiCiPaiKeDeKeBiaoXinXiBiao.ToList();
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

        //带班的教员
        public List<Models.Hi> banJiKaiSheKeChengJiHuaBiaos()
        {
            string sql = "select AnPaiJiaoYuan from [BanJiKaiSheKeChengJiHuaBiao]  where AnPaiJiaoYuan is not null group by AnPaiJiaoYuan";
            var a = DBContext.Database.SqlQuery<Models.Hi>(sql).ToList();
            return a.ToList();
        }

        //班级课程


        //教员带的班级
        public List<Hi> JioaYunaShouDaiBanJi(int ID)
        {
            string sql = "select distinct BanJiMing,BanJiID ID from [BanJiKaiSheKeChengJiHuaBiao] where  AnPaiJiaoYuan=@AnPaiJiaoYuan";
            SqlParameter sqr = new SqlParameter("@AnPaiJiaoYuan", ID);
            var list = DBContext.Database.SqlQuery<Models.Hi>(sql, sqr).ToList();
            return list;
        }

        //删除表数据
        public void DeleteTable()
        {
            string sql = "truncate table YiCiPaiKeDeKeBiaoXinXiBiao";
            ExcuteCommand(sql);
        }

    }
}
