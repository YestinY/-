using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public partial class XueShengBiaoDAL : DALBase<XueShengBiao>
    {
        //学生登录
        public Models.XueShengBiao login(Models.XueShengBiao Student)
        {
            try
            {
                var json = DBContext.XueShengBiao.Where(p => p.MiMa == Student.MiMa && p.Phone == Student.Phone).First();
                return json;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
