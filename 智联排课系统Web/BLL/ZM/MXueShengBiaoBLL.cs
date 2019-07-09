using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.ZM;
using Models;
using DAL.ZM;
using System.Transactions;

namespace BLL.ZM
{
    public class MXueShengBiaoBLL
    {
        MXueShengBiaoDAL mbll = new MXueShengBiaoDAL();
        MBanJiBiaoDAL mbj = new MBanJiBiaoDAL();

        #region 学生条件分页查询
        public List<Student> GetStudentALL(int zt, string namekey, int JieDuanID,
          string Address, string BjID, int sex, int p, int size, out int totPage)
        {
            return mbll.GetStudentALL(zt,namekey, JieDuanID, Address, BjID, sex, p, size, out totPage);
        }
        #endregion

        #region 以学生编号删除 [重载方法]
        public int Delete(int XSID)
        {
            return mbll.Delete(this.GetOne(XSID));//先获取一个对象再删除
        }
        #endregion

        #region 获取一条信息
        public XueShengBiao GetOne(int XID)
        {
            return mbll.GetOne(XID);
        }
        public Student GetOneInfo(object XID)
        {
            return mbll.GetOneInfo(XID);
        }
        #endregion

        #region 增加学生信息
        public int Add(XueShengBiao xs)
        {
            int n = mbll.Add(xs);
            mbll.AddStuClass();
            return n;
        }
        #endregion

        #region 增加学生1
        public bool StuAdd(List<Models.XueShengBiao> exceles)
        {
            bool error = false;
            try
            {
                TransactionScope transaction = null;
                //因为每个章节只能出现一次，所以new一个new匿名类

                foreach (var item in exceles)
                {
                    XueShengBiao xs = new XueShengBiao();
                    xs.Address = item.Address;
                    xs.Age = item.Age;
                    xs.HomePhone = item.HomePhone;
                    xs.MiMa = item.MiMa;
                    xs.Phone = item.Phone;
                    xs.StudentClassID = item.StudentClassID;
                    xs.Sex = item.Sex;
                    xs.StudentName = item.StudentName;
                    xs.ZhuangTai = item.ZhuangTai;
                    mbll.StuAdd(xs);
                    mbll.AddStuClass();
                }
                error = true;
            }
            catch (Exception ex)
            {
                error = false;
                throw ex;
            }
            return error;
        }
        #endregion


        #region 修改学生信息
        public int UpdateXS(XueShengBiao st)
        {
            return mbll.UpdateXS(st);
        }
        #endregion

        #region 学生退学
        public int TuiStu(int XID)
        {
            int n = mbll.TuiStu(XID);
            XueShengBiao xs = mbll.GetOne(XID);
            mbj.UpdateNum(xs, 1);
            return n;
        }
        #endregion

        public int HuanBan(string name, int bjID,int Xid,string YuanBanJiID)
        {
            XueShengBiao xs = mbll.GetOne(Xid);
            string id =xs.StudentClassID.ToString();
            mbj.UpdateNum(xs, 1);
            mbll.XiuGaiShiJian(mbll.GetXueShengYuBanJiDuiYingBiao(Xid, Convert.ToInt32(YuanBanJiID)).ID);
            int n = mbll.HuanBan(Xid,bjID);
            mbll.AddStuClass1(Xid,bjID);
            if (n > 0) 
            {
                XueShengBiao xs1 = new XueShengBiao();
                xs1.StudentClassID = bjID;
                mbj.UpdateNum(xs1, 2);
            }
            return n;
        }
    }
}
