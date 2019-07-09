using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DAL.LSM;

namespace BLL.LSM
{
   public class LSMjiaoxuejieduanBLL
    {
       LSMjiaoxuejieduanDAL dal = new LSMjiaoxuejieduanDAL();

       #region 查询所有
       public List<JiaoXueJieDuanBiao> GetAll() 
       {
           return dal.GetAll();
       }
       #endregion

       #region 根据多个条件查询，分页显示
       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"></param>
       /// <param name="name"></param>
       /// <param name="shenhe"></param>
       /// <param name="qiyong"></param>
       /// <param name="p">当前第几页</param>
       /// <param name="size">每页条数</param>
       /// <param name="totpage"></param>
       /// <returns></returns>
       public List<JiaoXueJieDuanBiao> GetjieduanByLike(int id, string name, int shenhe, int qiyong, int p, int size, out int totpage)
       {
           return dal.GetjieduanByLike(id, name, shenhe, qiyong, p, size, out totpage);
       }
       #endregion

       #region 新增时查询阶段名是否重复
       public JiaoXueJieDuanBiao SelectGetName(string name,int ID)
       {
           return dal.SelectGetName(name,ID);
       }

       public JiaoXueJieDuanBiao SelectGetName(string name)
       {
           return dal.SelectGetName(name);
       }
       #endregion

       #region 新增
       public int Addjiaoxuejieduan(JiaoXueJieDuanBiao jxjd)
       {
           return dal.Addjiaoxuejieduan(jxjd);
       }
       #endregion

       #region 主键查询一条
       public JiaoXueJieDuanBiao GetOne(object id)
       {
           return dal.GetOne(id);
       }
       #endregion

       #region 修改
       public int Updatejiaoxuejieduan(JiaoXueJieDuanBiao jxjd)
       {
           return dal.Updatejiaoxuejieduan(jxjd);
       }
       #endregion

       #region 删除
       //1.以对象删除
       public int Detelejiaoxuejieduan(JiaoXueJieDuanBiao jxjd) 
       {
           return dal.Deletejiaoxuejieduan(jxjd);
       }
       //2.以学生编号删除[重载方法]
       public int Delete(int id)
       {
           return this.Detelejiaoxuejieduan(this.GetOne(id));
       }
       #endregion

       #region 批量删除
       public int DeleteManyIds(string ids)
       {
           string sql = "delete from JiaoXueJieDuanBiao where ID in (" + ids + ")";
           return dal.DeleteManyIds(ids);
       }
       #endregion

       #region 删除时判断
       //教学课程表
       public int SelectKC(int ID)
       {
           return dal.SelectKC(ID);
       }
       //班级开设课程计划表
       public int SelectJH(int ID)
       {
           return dal.SelectJH(ID);
       }
       #endregion
       

    }
}
