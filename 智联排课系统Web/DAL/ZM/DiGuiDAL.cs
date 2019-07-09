using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using Models.ZM;

namespace DAL.ZM
{
    public class DiGuiDAL
    {
        public string strDeptList = "";
        public string strend = "";
        int index = 0;//构建前台脚本数组的索引
        List<DG> dg = new List<DG>();

        public ZM.BuMenBiaoDAL bm1 = new ZM.BuMenBiaoDAL();

        //显示子节点
        public List<DG> AddChildDept(int parentId)
        {
            strend += "&nbsp;";
            List<BuMenBiao> depars = bm1.BMToList().Where(p => p.DuiYingFuJiID == parentId).OrderBy(p => p.ID).ToList();
            if (depars.Count > 0)
            {
                foreach (BuMenBiao dept in depars)
                {
                    DG A = new DG();
                    A.ID = dept.ID;
                    A.BuMenMing = strend + dept.BuMenMingCheng;
                    dg.Add(A);
                    if (dept.ID > parentId)
                    {
                        if (isHasChildDept(dept.ID))
                        {
                            AddChildDept(dept.ID);
                            strend = strend.Substring(0, strend.LastIndexOf("&nbsp;"));
                        }
                    }
                    else if (dept.ID <= parentId)
                    {
                        dg[0].BuMenMing = dept.BuMenMingCheng;
                    }
                }
            }
            return dg;
        }

        public List<DG> Add(int parentId)
        {
            BuMenBiao depars1 = bm1.BMToList().Where(p => p.ID == parentId).OrderBy(p => p.ID).First();
            if (depars1.ID != depars1.DuiYingFuJiID)
            {
                DG A = new DG();
                A.ID = depars1.ID;
                A.BuMenMing = strend + depars1.BuMenMingCheng;
                dg.Add(A);
            }
            return dg;
        }

        //判断是否有子部门
        protected bool isHasChildDept(int parentId)
        {
            if (bm1.BMToList().Where(p => p.DuiYingFuJiID == parentId).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
