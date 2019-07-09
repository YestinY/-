using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ZM;

namespace DAL.ZM
{
    public class BuMenBiaoDAL
    {
        ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 查询部门所有
        public List<BuMenBiao> BMToList()
        {
            return context.BuMenBiao.ToList();
        }
        #endregion

        #region 增加部门
        public int AddBM(BuMenBiao bm)
        {
            context.BuMenBiao.Add(bm);
            return context.SaveChanges();
        }
        #endregion

        #region 增加后修改二级ID
        public int UpdateID(int id)
        {
            string sql = "UPdate BuMenBiao set DuiYingFuJiID = " + id + " ,BuMenDengJiGuanXi = " + id + " where ID = " + id;
            return context.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region 查询职位为1的员工
        public List<YuanGongBiao> YGToList()
        {
            return context.YuanGongBiao.Where(p => p.ZhiWeiID == 1).ToList();
        }
        #endregion

        #region 获取部门等级
        public string getDengJi(string DuiYingFuJiID)
        {
            int id = Convert.ToInt32(DuiYingFuJiID);
            int list = context.BuMenBiao.Where(p => p.DuiYingFuJiID == id).ToList().Count();
            return DuiYingFuJiID + "-" + list;
        }
        #endregion

        #region 获取职位表信息
        public List<ZhiWeiBiao> zwToList(int menuId)
        {
            return context.Set<ZhiWeiBiao>().Where(p => p.BuMenID == menuId).ToList();
        }
        public List<ZhiWeiBiao> zw()
        {
            return context.Set<ZhiWeiBiao>().OrderBy(p => p.ID).ToList();
        }
        public List<ZhiWeiBiao> zwShow(int id)
        {
            return context.Set<ZhiWeiBiao>().Where(p => p.ID == id).ToList();
        }
        public List<ZhiWeiBiao> zwList(int id)
        {
            List<BuMenBiao> b2 = context.Set<BuMenBiao>().Where(p => p.DuiYingFuJiID == id).ToList();
            if (b2.Count() > 1)
            {
                string sql = "select B.ID,B.BuMenID,B.ZhiWeiMing,B.BeiZhu,A.BuMenMingCheng,B.BeiZhu,B.ShiFouQiYong From BuMenBiao A Right Join ZhiWeiBiao B on B.BuMenID = A.ID and A.DuiYingFuJiID =" + id + " and A.ID = " + id;
                return context.Database.SqlQuery<ZhiWeiBiao>(sql).ToList();
            }
            return context.Set<ZhiWeiBiao>().Where(p => p.BuMenID == id).ToList();
        }
        #endregion

        #region 显示员工
        public List<YGB> YGShow()
        {
            string sql = "select* From YuanGongBiao A,ZhiWeiBiao B,BuMenBiao C where A.ZhiWeiID = B.ID and B.BuMenID = C.ID";
            return context.Database.SqlQuery<YGB>(sql).ToList();
        }
        #endregion

        #region 职位表增加
        public int AddZW(ZhiWeiBiao t)
        {
            context.ZhiWeiBiao.Add(t);
            return context.SaveChanges();
        }
        #endregion

        #region 修改部门信息
        public int UpdateBM(int ID, string BuMenMingCheng, string Phone)
        {
            string sql = "UPdate BuMenBiao set BuMenMingCheng = '" + BuMenMingCheng + "' ,Phone = '" + Phone + "' where ID = " + ID;

            return context.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region 删除部门信息
        public int delBM(int id)
        {
            BuMenBiao b = context.Set<BuMenBiao>().Find(id);
            context.BuMenBiao.Remove(b);
            return context.SaveChanges();
        }
        #endregion

        #region 修改职位信息
        public int UpdateZW(int ID, string ZhiWeiMing, string BeiZhu)
        {
            string sql = "Update ZhiWeiBiao Set ZhiWeiMing = '" + ZhiWeiMing + "' , BeiZhu = '" + BeiZhu + "' where ID = " + ID;
            return context.Database.ExecuteSqlCommand(sql);
        }
        #endregion

        #region 获取职位中是否有人员任职
        public int YGNum(int id)
        {
            List<YuanGongBiao> ygList = context.Set<YuanGongBiao>().Where(p => p.ZhiWeiID == id).ToList();
            return ygList.Count();
        }
        #endregion

        #region 删除职位信息
        public int delZW(int id)
        {
            ZhiWeiBiao b = context.Set<ZhiWeiBiao>().Find(id);
            context.ZhiWeiBiao.Remove(b);
            return context.SaveChanges();
        }
        #endregion

        public int LiZhi(int id,int i)
        {
            string sql = "Update YuanGongBiao set YuanGongZhuangTai = " + i + " where ID = " + id;

            return context.Database.ExecuteSqlCommand(sql);
        }
        public int JinYong(int id,bool f)
        {
            string sql = "update BuMenBiao set ShiFouQiYong = '" + f + "' where ID = " + id;

            return context.Database.ExecuteSqlCommand(sql);
        }
        public int JiaoXue(string id)
        {
            List<ZhengZaiShangKeBiao> j = context.ZhengZaiShangKeBiao.Where(p => p.JiaoYuanBianHao == id).ToList();
            return j.Count();
        }
    }
}
