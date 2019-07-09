using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using System.Data;
using Models.ZM;
using System.Data.SqlClient;

namespace DAL.ZM
{
    public class MBanJiBiaoDAL
    {
        ZhiLianPaiKeXiTongDBEntities context1 = new ZhiLianPaiKeXiTongDBEntities();

        MXueShengBiaoDAL xs = new MXueShengBiaoDAL();
        MBanZhuRenSuoDaiBanJiBiaoDAL mbzr = new MBanZhuRenSuoDaiBanJiBiaoDAL();

        //S1最新自动添加班级 biao(banjiming)
        #region 1.返回最新班级名称
        public string GetNewClassHao(string strData)
        {
            //1.先查询本前缀是否有数据2017
            string data = strData.Substring(2, 2);
            var list1 = context1.BanJiBiao.Where(p => p.BanJiMing.Substring(2, 2).IndexOf(data) >= 0);
            if (list1.Count() == 0)
            {
                //当年还没开班
                var banji = "ZL" + data + "01";//1701
                return banji.ToString();
            }
            else
            {
                //当年已有班级，查询最新班级
                var list2 = context1.BanJiBiao
                    .Where(p => p.BanJiMing.Substring(2, 2).IndexOf(data) >= 0)
                    .OrderByDescending(p => p.BanJiMing).Take(1).First();
                var maxbill = list2;
                var banji = maxbill.BanJiMing.ToString();
                var num = maxbill.BanJiRenShu;
                var jd = maxbill.JieDuanID;
                if (num == 5 || jd != 1)
                {
                    //格式不正确
                    var bjn = maxbill.BanJiMing.ToString().Substring(2, 4);
                    var bjnf = Convert.ToInt32(bjn) + 1;
                    banji = "ZL" + bjnf.ToString();
                }
                return banji.ToString();
            }
        }
        #endregion

        #region 1增加时，班级类数据赋值
        public int biao(string banji, string beizhu, int Renshu, int jieDuan)
        {
            BanJiBiao bj = new BanJiBiao();
            bj.BanJiMing = banji;
            bj.BanJiRenShu = Renshu;
            bj.KaiBanShiJian = DateTime.Now;
            bj.BeiZhu = beizhu;
            bj.YuJiJieShuShiJian = DateTime.Parse(GetTime1());
            bj.BanJiZhuangTai = 1;
            bj.JieDuanID = jieDuan;
            bj.JiaoXuePlan = false;
            return AddBanJi1(bj);
        }
        #endregion

        #region 查询年级ID
        public JiaoXueJieDuanBiao jd(int i)
        {
            string sql = "select * From JiaoXueJieDuanBiao ";
            if (i > 0)
            {
                sql = "select top " + i + " * From JiaoXueJieDuanBiao ";
            }
            List<JiaoXueJieDuanBiao> jdList = context1.Database.SqlQuery<JiaoXueJieDuanBiao>(sql).ToList();
            return jdList.OrderByDescending(p => p.ID).First();
        }
        #endregion

        

        #region 2获取班级预计结束时间
        private static string GetTime1()
        {
            string nian = DateTime.Now.ToString("yyyy");
            string yue = DateTime.Now.ToString("MM");
            string ri = DateTime.Now.ToString("dd");
            int y_1 = Convert.ToInt32(yue) + 6;
            if (y_1 > 12)
            {
                yue = (y_1 - 12).ToString();
                nian = (Convert.ToInt32(nian) + 1).ToString();
            }
            string time = nian + "-" + yue + "-" + ri;
            return time;
        }
        #endregion

        #region 3自动增加班级
        public int AddBanJi1(BanJiBiao bj)
        {
            context1.BanJiBiao.Add(bj);
            return context1.SaveChanges();
        }
        #endregion
        //S1最新自动添加班级

        //升阶段时使用
        #region 获取最新班级名
        public string GetGradeID(string mz)
        {
            //当年已有班级，查询最新班级
            var list1 = context1.BanJiBiao
                .Where(p => p.BanJiMing.IndexOf(mz) >= 0);
            if (list1.Count() == 0)
            {
                return mz + "01";
            }
            else
            {
                var list2 = context1.BanJiBiao
                   .Where(p => p.BanJiMing.IndexOf(mz) >= 0)
                   .OrderByDescending(p => p.BanJiMing).Take(1).First();
                var maxbill = list2;
                var banji = maxbill.BanJiMing.ToString();
                //格式不正确
                var bjn = maxbill.BanJiMing.ToString().Substring(2, 4);
                var bjnf = Convert.ToInt32(bjn) + 1;
                banji = mz + bjnf.ToString();
                return banji;
            }
        }

        #endregion

        //增加学生，删除学生时，班级人数的变动
        #region 2修改班级人数
        public int Update1(BanJiBiao bj)
        {
            string sql = "update BanJiBiao Set BanJiRenShu = @rs where ID = @id";
            List<SqlParameter> spls = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@rs", bj.BanJiRenShu);
            SqlParameter p2 = new SqlParameter("@id", bj.ID);
            spls.Add(p1);
            spls.Add(p2);
            int n = context1.Database.ExecuteSqlCommand(sql, spls.ToArray());
            return n;
        }
        #endregion

        #region 1修改班级人数
        public int UpdateNum(XueShengBiao st, int i)
        {
            BanJiBiao bj = context1.Set<BanJiBiao>().Find(st.StudentClassID);
            if (i == 1)
            {
                bj.BanJiRenShu = bj.BanJiRenShu - 1;
            }
            else
            {
                bj.BanJiRenShu = bj.BanJiRenShu + 1;
            }
            return Update1(bj);
        }
        #endregion

        //增加学生时，查询班级id
        #region 返回班级ID
        public int GetOneID(string BanJiMing)
        {
            string sql = "select * From BanJiBiao where BanJiMing = @banjiming";
            List<SqlParameter> spls = new List<SqlParameter>();
            SqlParameter p = new SqlParameter("@banjiming", BanJiMing);
            spls.Add(p);
            BanJiBiao stuList = context1.Database.SqlQuery<BanJiBiao>(sql, spls.ToArray()).First();
            return stuList.ID;
        }
        #endregion

        //增加学生时，判断是否增加班级
        #region 班级人数查询
        public BjRenShu GetRenShu()
        {
            BjRenShu br = new BjRenShu();
            List<BanJiBiao> bjList = context1.Set<BanJiBiao>().ToList();
            if (bjList.Count() > 0)
            {
                int i = this.jd(1).ID;
                List<BanJiBiao> List = context1.Set<BanJiBiao>().Where(p => p.JieDuanID == i).OrderByDescending(p => p.ID).ToList();
                if (List.Count() > 0)
                {
                    BanJiBiao bj = context1.Set<BanJiBiao>().Where(p => p.JieDuanID == 1).OrderByDescending(p => p.ID).First();
                    if (bj.JieDuanID == 1)
                    {
                        List<SqlParameter> spls1 = new List<SqlParameter>();
                        string sql = " select * From XueShengBiao A,BanJiBiao B where A.StudentClassID = B.ID and B.JieDuanID = 1 and  A.StudentClassID = @ID ";
                        SqlParameter p1 = new SqlParameter("@ID", bj.ID);
                        spls1.Add(p1);
                        List<XueShengBiao> xs1 = context1.Database.SqlQuery<XueShengBiao>(sql, spls1.ToArray()).ToList();
                        br.BanJiMing = bj.BanJiMing;
                        br.banjirenshu = xs1.Count();
                    }
                }
            }
            return br;
        }
        #endregion

        #region 班级条件分页查询
        public List<BanJiBiao> GetGradeALL(string BanJiMing, int jdID, int bjztID, int p, int size, out int totPage)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "select * From BanJiBiao where 1=1";
            if (!string.IsNullOrEmpty(BanJiMing))
            {
                sql += " and BanJiMing like @BJM";
                SqlParameter p1 = new SqlParameter("@BJM", "%" + BanJiMing + "%");
                spls.Add(p1);
            }
            if (jdID != -1)
            {
                sql += " and JieDuanID = @JieDuanID ";
                SqlParameter p2 = new SqlParameter("@JieDuanID", jdID);
                spls.Add(p2);
            }
            if (bjztID != -1)
            {
                sql += " and BanJiZhuangTai = @BJZT ";
                SqlParameter p4 = new SqlParameter("@BJZT", bjztID);
                spls.Add(p4);
            }
            //////////////////////////////
            List<BanJiBiao> gList = context1.Database.SqlQuery<BanJiBiao>
                (sql, spls.ToArray()).ToList();//通过sql语句查询所有符合条件数据
            ///////////算总页码部分//////////////////////////
            //算条数
            int count = gList.Count();
            //算总页数
            totPage = gList.Count();
            ////////分页返回数据//////////////////
            int n = (p - 1) * size;
            return gList.OrderBy(x => x.ID).Skip(n).Take(size).ToList();
        }
        #endregion

        #region 查询一条班级信息
        public BanJiBiao YjDate(int BanJiID)
        {
            return context1.Set<BanJiBiao>().Find(BanJiID);
        }
        #endregion

        //Y2=>毕业
        #region 班级学生毕业
        public int BiYe(int BanJiID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "Update XueShengBiao Set ZhuangTai = 2 where StudentClassID = @BJID";
            SqlParameter p1 = new SqlParameter("@BJID", BanJiID);
            spls.Add(p1);
            int n = context1.Database.ExecuteSqlCommand(sql, spls.ToArray());
            if (n > 0)
            {
                UpdateSN(BanJiID, 2);
            }
            return 0;
        }
        #endregion

        //修改现=》原班级状态，1：正在教学  2：已毕  3：升学
        #region S1升入S2，修改班级状态
        public int UpdateSN(int BanJiID, int zt)
        {
            List<SqlParameter> spls1 = new List<SqlParameter>();
            string sql1 = "Update BanJiBiao Set BanJiZhuangTai = @zt where ID = @ID";
            SqlParameter p11 = new SqlParameter("@ID", BanJiID);
            SqlParameter p2 = new SqlParameter("@zt", zt);
            spls1.Add(p2);
            spls1.Add(p11);
            return context1.Database.ExecuteSqlCommand(sql1, spls1.ToArray());
        }
        #endregion

        //升阶段时修改学生表的班级ID 1.正在学习  2.已毕业  3.已退学
        #region 修改班级ID 修改学生表的班级ID
        public int UpdateStu(int BanJiID, BanJiBiao bj)
        {
            List<SqlParameter> spls1 = new List<SqlParameter>();
            string sql1 = "Update XueShengBiao Set StudentClassID = @ID where StudentClassID = @BJID  and ZhuangTai = 1 ";
            SqlParameter p1 = new SqlParameter("@ID", bj.ID);
            SqlParameter p2 = new SqlParameter("@BJID", BanJiID);
            spls1.Add(p1);
            spls1.Add(p2);
            return context1.Database.ExecuteSqlCommand(sql1, spls1.ToArray());
        }
        #endregion

        //升阶段时，该班所有学生信息
        #region 获取学生，修改后学生信息
        public List<XueShengBiao> getXueS(int BanJiID)
        {
            List<SqlParameter> spls1 = new List<SqlParameter>();
            string sql1 = "select * From XueShengBiao where StudentClassID = @ID and ZhuangTai = 1";
            SqlParameter p1 = new SqlParameter("@ID", BanJiID);
            spls1.Add(p1);
            return context1.Database.SqlQuery<XueShengBiao>(sql1, spls1.ToArray()).ToList();
        }
        #endregion

        //获取最新班级信息
        #region 获取最新班级信息
        public BanJiBiao Getfrist()
        {
            BanJiBiao bj1 = context1.Set<BanJiBiao>().OrderByDescending(p => p.ID).First();
            return bj1;
        }
        #endregion

        //留级时，获取下一年级的空余班级
        #region 留级时，获取下一年级的空余班级
        public List<BanJiBiao> GetBJ(int jdID, int bjID)
        {
            string sql = "Select * From BanJiBiao where BanJiRenShu < 30 and BanJiZhuangTai = 1 and  JieDuanID = @ID and ID != @bjID ";
            List<SqlParameter> spls1 = new List<SqlParameter>();
            SqlParameter p1 = new SqlParameter("@ID", jdID);
            SqlParameter p2 = new SqlParameter("@bjID", bjID);
            spls1.Add(p1);
            spls1.Add(p2);
            List<BanJiBiao> bjList = context1.Database.SqlQuery<BanJiBiao>(sql, spls1.ToArray()).ToList();
            if (bjList.Count() == 0)
            {
                sql = "Select * From BanJiBiao where BanJiRenShu <= 30 and BanJiZhuangTai = 1 and  JieDuanID = @ID and ID != @bjID ";
                List<SqlParameter> spls = new List<SqlParameter>();
                SqlParameter p = new SqlParameter("@ID", jdID);
                SqlParameter o = new SqlParameter("@bjID", bjID);
                spls.Add(p);
                spls.Add(o);
                bjList = context1.Database.SqlQuery<BanJiBiao>(sql, spls.ToArray()).ToList();
            }
            return bjList;
        }
        #endregion

        #region 修改班级信息
        public int UpdateBanJi(int BanJiID, string BeiZhu)
        {
            List<SqlParameter> spls1 = new List<SqlParameter>();
            string sql1 = "Update BanJiBiao Set BeiZhu = @bz where ID = @ID";
            SqlParameter p11 = new SqlParameter("@ID", BanJiID);
            SqlParameter p2 = new SqlParameter("@bz", BeiZhu);
            spls1.Add(p2);
            spls1.Add(p11);
            return context1.Database.ExecuteSqlCommand(sql1, spls1.ToArray());
        }
        #endregion

        #region 判断班级是否已存在
        public List<BanJiBiao> GetPD(string BanJiMing)
        {
            string sql = "Select * From BanJiBiao where BanJiMing = '" + BanJiMing + "'";
            //////List<SqlParameter> spls = new List<SqlParameter>();
            //////SqlParameter p = new SqlParameter("@bjM", BanJiMing);
            //////spls.Add(p);
            return context1.Database.SqlQuery<BanJiBiao>(sql).ToList();

        }
        #endregion

        public List<BanJiBiao> bjList()
        {
            int id = this.jd(1).ID;
            List<BanJiBiao> bjList = context1.Set<BanJiBiao>().Where(p => p.BanJiRenShu < 30 && p.JieDuanID == id).ToList();
            return bjList;
        }

        public int njID(int njID)
        {
            string sql = "select top(select COUNT(ID) From JiaoXueJieDuanBiao where ID < "+njID+") *From JiaoXueJieDuanBiao ";
            List<JiaoXueJieDuanBiao> jdList = context1.Database.SqlQuery<JiaoXueJieDuanBiao>(sql).ToList();
            return jdList.OrderByDescending(p => p.ID).First().ID;
        }
        public int njID1(int njID)
        {
            string sql = "select top(select COUNT(ID) From JiaoXueJieDuanBiao where ID > " + njID + ") *From JiaoXueJieDuanBiao ";
            List<JiaoXueJieDuanBiao> jdList = context1.Database.SqlQuery<JiaoXueJieDuanBiao>(sql).ToList();
            return jdList.OrderBy(p => p.ID).First().ID;
        }

        

    }
}
