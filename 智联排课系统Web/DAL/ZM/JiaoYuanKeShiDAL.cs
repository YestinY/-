using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL.ZM
{
    public class JiaoYuanKeShiDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        public List<BanJiKaiSheKeChengJiHuaBiao> GetOne()
        {
            //List<JiaoXueJiHuaBiao> id = context.JiaoXueJiHuaBiao.OrderByDescending(p => p.ID).ToList();
            List<BanJiKaiSheKeChengJiHuaBiao> jhList = context.Set<BanJiKaiSheKeChengJiHuaBiao>().OrderBy(p=>p.ID).ToList();
            return jhList;
        }

        public void GetKeShi()
        {
            Models.KeShiBiao ke = new Models.KeShiBiao();
            foreach (BanJiKaiSheKeChengJiHuaBiao p in this.GetOne())
            {
                ke.TeacherID = Convert.ToInt32(p.AnPaiJiaoYuan);
                ke.TeacherName = GetName(Convert.ToInt32(p.AnPaiJiaoYuan));
                ke.ClassID = p.BanJiID.ToString();
                ke.ClassName = p.BanJiMing;
                ke.KeChengMingChen = p.KeChengShunXuHao.ToString();
                ke.KeChengBianHao = p.KeChengShunXuHao.ToString();
                ke.ShangKeCount = Convert.ToInt32(p.ShiJiKeShi);
                ke.ShiFouWanChengBeKeCheng = true;
                ke.BeiZhu = null;
                ke.ZhuangTai = null;
                this.Add(ke);
            }
        }

        public int Add(KeShiBiao ke)
        {
            context.KeShiBiao.Add(ke);
            return context.SaveChanges();
        }

        public string GetName(int id)
        {
            return context.Set<YuanGongBiao>().Where(p => p.ID == id).First().Name;
        }

        public int del()
        {
            string sql = "delete from KeShiBiao";
            return context.Database.ExecuteSqlCommand(sql);
        }

        public List<KeShiBiao> GetAll()
        {
            return context.KeShiBiao.ToList();
        }
    }
}
