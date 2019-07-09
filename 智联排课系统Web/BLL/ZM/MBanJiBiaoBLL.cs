using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.ZM;
using Models.ZM;

namespace BLL.ZM
{
    public class MBanJiBiaoBLL
    {
        MXueShengBiaoDAL xsDAL = new MXueShengBiaoDAL();
        MBanJiBiaoDAL mbll = new MBanJiBiaoDAL();

        //新增班级时，获取最新班级
        #region 获取最新班级号
        public string GetNewClassHao(string strData)
        {
            return mbll.GetNewClassHao(strData);
        }
        #endregion

        #region 升阶段时最新班级号
        public string GetGradeID(string strData, int jd, int BanJiID)
        {
            if (jd == 2)
            {
                BanJiBiao bj = mbll.YjDate(BanJiID);
                return bj.BanJiMing;
            }
            else if (jd == 3)
            {
                return mbll.GetGradeID("Java");
            }
            else if (jd == 4)
            {
                return mbll.GetGradeID("IT");
            }
            return null;
        }
        #endregion

        //增加学生时
        #region 查询班级名称ID
        public int GetOneID(string BanJiMing)
        {
            return mbll.GetOneID(BanJiMing);
        }
        #endregion

        //添加学生时判断:班级人数是否达到
        #region 班级人数查询
        public BjRenShu GetRenShu()
        {
            return mbll.GetRenShu();
        }
        #endregion

        #region 新增班级
        public int biao(string banji, string beizhu, int RenShu, int JieDuanID)
        {
            return mbll.biao(banji, beizhu, RenShu, JieDuanID);
        }
        #endregion

        #region 查询年级ID
        public JiaoXueJieDuanBiao jd(int i)
        {
            return mbll.jd(i);
        }
        #endregion

        #region 班级条件分页查询
        public List<BanJiBiao> GetGradeALL(string BanJiMing, int jdID, int bjztID, int p, int size, out int totPage)
        {
            return mbll.GetGradeALL(BanJiMing, jdID, bjztID, p, size, out totPage);//查询班级的功能
        }
        #endregion

        #region 查询该班级现隶属阶段
        public BanJiBiao JieDuan(int BanJiID)
        {
            BanJiBiao bj = mbll.YjDate(BanJiID);
            return bj;
        }
        #endregion

        #region 班级学生毕业
        public int BiYe(int BanJiID)
        {
            return mbll.BiYe(BanJiID);
        }
        #endregion

        #region 留级时，获取下一年级的空余班级
        public List<BanJiBiao> GetBJ(int jdID, int bjID)
        {
            return mbll.GetBJ(jdID, bjID);
        }
        #endregion

        //升阶段时：班级与学生表的新增
        #region 升阶段时：班级与学生表的新增
        public int AddUP()
        {
            BanJiBiao bj = mbll.Getfrist();
            List<XueShengBiao> xsList = mbll.getXueS(bj.ID);
            for (int i = 0; i < xsList.Count(); i++)
            {
                xsDAL.GetXsBj(xsList[i], bj);
            }
            return 1;
        }
        #endregion

        #region 升阶段时：修改学生表班级ID
        public int UpdateStu(int BanJiID)
        {
            BanJiBiao bj = mbll.Getfrist();
            return mbll.UpdateStu(BanJiID, bj);
        }
        #endregion

        #region 升阶段时：修改班级状态
        public int UpdateZT(int BanJiID)
        {
            return mbll.UpdateSN(BanJiID, 3);
        } 
        #endregion

        #region 修改班级信息
        public int UpdateBanJi(int bjID, string BeiZhu)
        {
            return mbll.UpdateBanJi(bjID, BeiZhu);
        }
        #endregion

        #region 判断班级是否已存在
        public List<BanJiBiao> GetPD(string BanJiMing)
        {
            return mbll.GetPD(BanJiMing);
        }
        #endregion

        public List<BanJiBiao> bjList()
        {
            return mbll.bjList();
        }

        public int njID(int njID)
        {
            return mbll.njID(njID);
        }

        public int njID1(int njID)
        {
            return mbll.njID1(njID);
        }
    }
}
