using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BLL.CHB
{
    public class YuanGongBiaoBLL :BLLBase<Models.YuanGongBiao>
    {
        public DAL.CHB.YuanGongBiaoDAL Bll = new DAL.CHB.YuanGongBiaoDAL();

        #region 分页查询所有数据
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">当前页码</param>
        /// <param name="size">每页条数</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByPindex(int p, int size)
        {
            return Bll.GetStudentByPindex(p, size);
        }
        #endregion

        #region 以主键删除
        public int DeleteByPkey(string id)
        {
            return Bll.DeleteManyByIdS(id);
        }
        #endregion

        #region 以主键删除
        public int DeleteByPkey(int id)
        {
            return Bll.DeleteManyByIdS(id);
        }
        #endregion

        #region 根据名字关键字及年级ID多条件查询学生（两个可以组合）
        public List<YuanGongBiao> GetStudentByManyIf(string key, int gradeid,
            int p, int size, out int totPage)
        {
            return Bll.GetStudentByManyIf(key, gradeid, p,
                size, out totPage);
        }
        #endregion

        #region 多条件查询学生[H-ui-admin框架查询数据 多条件]不需分页
        ////////////多条件查询学生[H-ui-admin框架查询数据 多条件]//////
        /// <summary>
        /// 多条件查询数据
        /// </summary>
        /// <param name="namekey">名字关键字</param>
        /// <param name="grade">年纪编号-1为查询所有</param>
        /// <param name="dzkey">住址关键字</param>
        /// <param name="sex">性别，传"-1",不限男女，0,女,1(男）</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByManyIf(string namekey, int grade,
            string dzkey, int sex)
        {
            return Bll.GetStudentByManyIf(namekey, grade, dzkey, sex);
        }

        #endregion


        #region 根据多个条件查询符合条件数据，并分页显示 本方法给fxlayui查询显示用
        /// <summary>
        /// 按照多个条件查询出分页数据
        /// </summary>
        /// <param name="namekey"></param>
        /// <param name="grade"></param>
        /// <param name="dzkey"></param>
        /// <param name="sex"></param>
        /// <param name="p">当前第几页</param>
        /// <param name="size">每页条数</param>
        /// <param name="totPage">总页码</param>
        /// <returns></returns>
        public List<YuanGongBiao> GetStudentByManyIf2_fxlayui(string namekey, int zw,
            string dzkey, int zt, int p, int size, out int totPage)
        {
            return Bll.GetStudentByManyIf2_fxlayui(namekey, zw, dzkey, zt, p, size, out totPage);
        }

        public override void SetDAL()
        {
            this.dal = Bll;
        }
        #endregion

        public int Update(YuanGongBiao st)
        {
            return Bll.Update(st);
        }
    }
}
