using Models;
using Models.ZM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.ZM
{
    public class MXueShengBiaoDAL
    {
        private ZhiLianPaiKeXiTongDBEntities context = new ZhiLianPaiKeXiTongDBEntities();

        #region 学生条件分页查询
        public List<Student> GetStudentALL(int zt, string namekey, int JieDuanID,
          string Address, string BjID, int sex, int p, int size, out int totPage)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = " Select A.ID as XID,A.StudentName,A.Sex,A.Address,B.BanJiMing,A.Age,A.HomePhone,A.Phone,B.BanJiMing,C.JieDuanMing From XueShengBiao A,BanJiBiao B,JiaoXueJieDuanBiao C where A.StudentClassID=B.ID and B.JieDuanID = C.ID ";
            if (!string.IsNullOrEmpty(namekey))
            {
                sql += "  and A.StudentName like @StudentName  ";
                SqlParameter p1 = new SqlParameter("@StudentName", "%" + namekey + "%");
                spls.Add(p1);
            }

            if (!string.IsNullOrEmpty(BjID))
            {
                sql += "   and  B.ID = @BjID  ";
                SqlParameter p1 = new SqlParameter("@BjID", BjID);
                spls.Add(p1);
            }
            if (JieDuanID != -1)
            {
                sql += " and C.ID = @JieDuanID ";
                SqlParameter p2 = new SqlParameter("@JieDuanID", JieDuanID);
                spls.Add(p2);
            }
            if (zt != -1)
            {
                sql += " and A.ZhuangTai = @zt ";
                SqlParameter p2 = new SqlParameter("@zt", zt);
                spls.Add(p2);
            }


            if (!string.IsNullOrEmpty(Address))
            {
                sql += " and A.Address like @Address ";
                SqlParameter p3 = new SqlParameter("@Address", "%" + Address + "%");
                spls.Add(p3);
            }
            if (sex != -1) //-1不限，0,女,1男
            {
                sql += " and A.Sex = @Sex ";
                SqlParameter p4 = new SqlParameter("@Sex", sex);
                spls.Add(p4);
            }
            //////////////////////////////
            List<Student> stuList = context.Database.SqlQuery<Student>
                (sql, spls.ToArray()).ToList();//通过sql语句查询所有符合条件数据
            ///////////算总页码部分//////////////////////////
            //算条数
            int count = stuList.Count();
            //算总页数
            totPage = count;
            ////////分页返回数据//////////////////
            int n = (p - 1) * size;
            return stuList.OrderBy(x => x.JieDuanMing).Skip(n).Take(size).ToList();
        }
        #endregion

        #region 删除[以对象删除]
        public int Delete(XueShengBiao st)
        {
            MBanJiBiaoDAL bj = new MBanJiBiaoDAL();
            if (bj.UpdateNum(st, 1) == 0)
            {
                return 0;
            }
            context.Set<XueShengBiao>().Attach(st);
            context.Set<XueShengBiao>().Remove(st);
            return context.SaveChanges();

        }
        #endregion

        #region 主键查询一条
        public XueShengBiao GetOne(object id)
        {
            return context.Set<XueShengBiao>().Find(id);
        }
        public Student GetOneInfo(object XID)
        {
            XueShengBiao xsb = context.Set<XueShengBiao>().Find(XID);
            BanJiBiao bjb = context.Set<BanJiBiao>().Find(xsb.StudentClassID);
            Student st = new Student
            {
                XID = xsb.ID,
                StudentName = xsb.StudentName,
                Address = xsb.Address,
                Age = xsb.Age,
                BanJiMing = bjb.BanJiMing,
                HomePhone = xsb.HomePhone,
                Phone = xsb.Phone,
                Sex = xsb.Sex,
                StudentClassID = xsb.StudentClassID,
                JieDuanID = Convert.ToInt32(bjb.JieDuanID)
            };
            return st;
        }
        #endregion

        #region 增加学生
        public int Add(XueShengBiao xs)
        {
            xs.ZhuangTai = 1;
            MBanJiBiaoDAL bj = new MBanJiBiaoDAL();
            if (bj.UpdateNum(xs, 2) == 0)
            {
                return 0;
            }
            context.XueShengBiao.Add(xs);
            return context.SaveChanges();
        }
        #endregion

        #region 增加学生1
        public int StuAdd(XueShengBiao xs)
        {
            MBanJiBiaoDAL bj = new MBanJiBiaoDAL();
            if (bj.UpdateNum(xs, 2) == 0)
            {
                return 0;
            }
            context.XueShengBiao.Add(xs);
            return context.SaveChanges();
        }
        #endregion

        #region 修改学生信息
        public int UpdateXS(XueShengBiao st)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "update XueShengBiao Set StudentName = @name,Age = @age,Address = @addr,Phone= @p1,HomePhone = @p2  ";
            SqlParameter p1 = new SqlParameter("@name", st.StudentName);
            SqlParameter p2 = new SqlParameter("@age", st.Age);
            SqlParameter p3 = new SqlParameter("@addr", st.Address);
            SqlParameter p4 = new SqlParameter("@p1", st.Phone);
            SqlParameter p5 = new SqlParameter("@p2", st.HomePhone);
            spls.Add(p1);
            spls.Add(p2);
            spls.Add(p3);
            spls.Add(p4);
            spls.Add(p5);
            if (st.Sex != null)
            {
                sql += "  ,Sex = @sex  ";
                SqlParameter p6 = new SqlParameter("@sex", st.Sex);
                spls.Add(p6);
            }
            sql += "where ID = " + st.ID;

            int n = context.Database.ExecuteSqlCommand(sql, spls.ToArray());
            return n;
        }
        #endregion

        //
        ////新增学生与班级关联表
        #region 增加关联表 学生-班级
        public int AddStuClass()
        {
            XueShengBiao xs = context.Set<XueShengBiao>().OrderByDescending(p => p.ID).First();
            BanJiBiao bj = context.Set<BanJiBiao>().Where(p => p.ID == xs.StudentClassID).First();
            return GetXsBj(xs, bj);
        }
        public int GetXsBj(XueShengBiao xs, BanJiBiao bj)
        {
            XueShengYuBanJiDuiYingBiao xb = new XueShengYuBanJiDuiYingBiao
            {
                StudentID = xs.ID,
                StudentName = xs.StudentName,
                StudentClassID = xs.StudentClassID,
                StudentClass = bj.BanJiMing,
                StartTiem = bj.KaiBanShiJian
            };
            context.XueShengYuBanJiDuiYingBiao.Add(xb);
            return context.SaveChanges();
        }
        #endregion

        #region 学生退学
        public int TuiStu(int XID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "Update XueShengBiao Set ZhuangTai = 3 where ID = @ID";
            SqlParameter p1 = new SqlParameter("@ID", XID);
            spls.Add(p1);
            return context.Database.ExecuteSqlCommand(sql, spls.ToArray());
        }
        #endregion

        #region 学生换班
        public int HuanBan(int ID, int bjID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "Update XueShengBiao Set StudentClassID = @bjID where ID = @ID";
            SqlParameter p1 = new SqlParameter("@ID", ID);
            SqlParameter p2 = new SqlParameter("@bjID", bjID);
            spls.Add(p1);
            spls.Add(p2);
            return context.Database.ExecuteSqlCommand(sql, spls.ToArray());
        }
        #endregion

        #region 修改时间
        public int XiuGaiShiJian(int ID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "Update XueShengYuBanJiDuiYingBiao Set EndTime = GETDATE() where ID= @ID";
            SqlParameter p1 = new SqlParameter("@ID", ID);
            spls.Add(p1);
            return context.Database.ExecuteSqlCommand(sql, spls.ToArray());
        }
        #endregion

        public XueShengYuBanJiDuiYingBiao GetXueShengYuBanJiDuiYingBiao(int ID, int bjID)
        {
            List<SqlParameter> spls = new List<SqlParameter>();
            string sql = "Select * From XueShengYuBanJiDuiYingBiao  where StudentID = @ID and StudentClassID = @bjID and EndTime is null";
            SqlParameter p1 = new SqlParameter("@ID", ID);
            SqlParameter p2 = new SqlParameter("@bjID", bjID);
            spls.Add(p1);
            spls.Add(p2);
            XueShengYuBanJiDuiYingBiao list = context.Database.SqlQuery<XueShengYuBanJiDuiYingBiao>(sql, spls.ToArray()).First();
            return list;
        }

        #region 增加关联表 学生-班级
        public int AddStuClass1(int ID, int BjID)
        {
            XueShengBiao xs = context.Set<XueShengBiao>().Where(p => p.ID == ID).OrderByDescending(p => p.ID).First();
            BanJiBiao bj = context.Set<BanJiBiao>().Where(p => p.ID == BjID).First();
            return GetXsBj1(xs, bj);
        }
        public int GetXsBj1(XueShengBiao xs, BanJiBiao bj)
        {
            XueShengYuBanJiDuiYingBiao xb = new XueShengYuBanJiDuiYingBiao
            {
                StudentID = xs.ID,
                StudentName = xs.StudentName,
                StudentClassID = bj.ID,
                StudentClass = bj.BanJiMing,
                StartTiem = bj.KaiBanShiJian
            };
            context.XueShengYuBanJiDuiYingBiao.Add(xb);
            return context.SaveChanges();
        }
        #endregion
    }
}
