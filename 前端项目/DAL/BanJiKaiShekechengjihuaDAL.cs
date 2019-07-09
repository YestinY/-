using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class BanJiKaiShekechengjihuaDAL
    {
        private ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 查询所有
        public List<BanJiKaiSheKeChengJiHuaBiao> GetAll()
        {
            return context.Set<BanJiKaiSheKeChengJiHuaBiao>().ToList();
        }
        #endregion

        #region 分页 班级的课程
        public List<BanJiKaiSheKeChengJiHuaBiao> GetPage(int p, int size, out int totpage, int ClassID)
        {
            List<BanJiKaiSheKeChengJiHuaBiao> lists = context.BanJiKaiSheKeChengJiHuaBiao.ToList().Where(po => po.BanJiID == ClassID).ToList();
            int count = lists.Count();
            totpage = count;
            return lists.Skip((p - 1) * size).Take(size).ToList();
        }
        #endregion

        #region 分页 教员所带班级的课程
        public List<BanJiKaiSheKeChengJiHuaBiao> GetPage(int p, int size, out int totpage, string TeacherID)
        {
            List<BanJiKaiSheKeChengJiHuaBiao> lists = context.BanJiKaiSheKeChengJiHuaBiao.ToList().Where(po => po.AnPaiJiaoYuan == Convert.ToInt32(TeacherID)).ToList();
            int count = lists.Count();
            totpage = count;
            return lists.Skip((p - 1) * size).Take(size).ToList();
        }
        #endregion
    }
}
