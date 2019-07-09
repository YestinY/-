using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL;
namespace BLL
{
    public partial class ZiYuanGuanLiBLL : BLLBase<ZiYuanGuanLi>
    {
        ZiYuanGuanLiDAL z = new ZiYuanGuanLiDAL();
        public List<ZiYuanGuanLi> GetStudentByManyIf2_fxlayui(string ZiYuanMing, string ZiYuanLeiXing,
      string ZiYuanWeiZhi, int p, int size, out int totPage)
        {
            return z.GetStudentByManyIf2_fxlayui(ZiYuanMing, ZiYuanLeiXing, ZiYuanWeiZhi, p, size, out totPage);
        }
        public ZiYuanGuanLi SelectGetName(string name)
        {
            return z.SelectGetName(name);
        }
        #region 多条数据删除
        /// <summary>
        /// 多条数据删除方法
        /// </summary>
        /// <param name="ids">ids格式1,2,3,4</param>
        /// <returns>删除成功的行数</returns>
        public int DeleteManyByIdS(string ids)
        {
            string sql = "delete from ZiYuanGuanLi where ID in (" + ids + ")";
            //直接执行sql语句
            return z.DeleteManyByIdS(ids);
        }

        #endregion
        public int SelectGetWeiZhi(int ID)
        {
            return z.SelectGetWeiZhi(ID);
        }
        public int SelectGetSk(string ID)
        {
            return z.SelectGetSk(ID);
        }
        public ZiYuanGuanLi SelectGetName(string name, int ID)
        {
            return z.SelectGetName(name, ID);

        }
    }
}
