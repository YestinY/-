using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.ZM;
using Models;
using Models.ZM;

namespace BLL.ZM
{
    public class BuMenBiaoBLL
    {
        BuMenBiaoDAL bm = new BuMenBiaoDAL();

        #region 查询部门所有信息
        public List<BuMenBiao> BMToList()
        {
            return bm.BMToList();
        }
        #endregion

        #region 增加部门信息
        public int AddBM(BuMenBiao bm1)
        {
            if (bm1.DuiYingFuJiID != 0)
            {
                bm1.BuMenDengJiGuanXi = bm.getDengJi(bm1.DuiYingFuJiID.ToString());
            }
            int c = bm.AddBM(bm1);
            if (bm1.DuiYingFuJiID == 0)
            {
                BuMenBiao t = bm.BMToList().OrderByDescending(p => p.ID).First();
                bm.UpdateID(t.ID);
            }
            return c;
        }
        #endregion
        #region 获取员工信息
        public List<YuanGongBiao> YGToList()
        {
            return bm.YGToList();
        }
        #endregion

        #region 获取职位表信息
        public List<ZhiWeiBiao> zwToList(int menuId)
        {
            return bm.zwToList(menuId);
        }
        public List<ZhiWeiBiao> zw()
        {
            return bm.zw();
        }
        public List<ZhiWeiBiao> zwShow(int id)
        {
            return bm.zwShow(id);
        }
        public List<ZhiWeiBiao> zwList(int id)
        {
            return bm.zwList(id);
        }
     
        #endregion

        public List<YGB> YGShow()
        {
            return bm.YGShow();
        }
        public int AddZW(ZhiWeiBiao t)
        {
            return bm.AddZW(t);
        }
        public int UpdateZW(int ID, string ZhiWeiMing, string BeiZhu)
        {
            return bm.UpdateZW(ID, ZhiWeiMing, BeiZhu);
        }

        #region 修改部门信息
        public int UpdateBM(int ID, string BuMenMingCheng, string Phone)
        {
            return bm.UpdateBM(ID, BuMenMingCheng, Phone);
        }
        #endregion

        #region 删除部门信息
        public int delBM(int id)
        {
            return bm.delBM(id);
        }
        #endregion

        #region 获取职位中是否有人员任职
        public int YGNum(int id)
        {
            return bm.YGNum(id);
        }
        #endregion

        #region 删除职位信息
        public int delZW(int id)
        {
            return bm.delZW(id);
        }
        #endregion

        public int LiZhi(int id,int i)
        {
            return bm.LiZhi(id,i);
        }
        public int JinYong(int id,bool f)
        {
            return bm.JinYong(id,f);
        }

        public int JiaoXue(string ID)
        {
            return bm.JiaoXue(ID);
        }
    }
}
