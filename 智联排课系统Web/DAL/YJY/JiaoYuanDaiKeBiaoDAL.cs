using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class JiaoYuanDaiKeBiaoDAL : DALBase<JiaoYuanDaiKeBiao>
    {
        //一次排课课表信息
        public List<Models.JiaoYuanDaiKeBiao> List(int PageIndex, int PageSize, out int total, string Name, DateTime date, int Teacher)
        {
            var List = DBContext.JiaoYuanDaiKeBiao.ToList();
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
