using DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
namespace BLL
{
    public partial class JiaoXueJiHuaBiaoBLL : BLLBase<Models.JiaoXueJiHuaBiao>
    {
        /// <summary>
        /// 教学计划DAL
        /// </summary>
        private JiaoXueJiHuaBiaoDAL bLL = new JiaoXueJiHuaBiaoDAL();

        /// <summary>
        /// 教学课程DAL
        /// </summary>
        private JiaoXueKeChengDAL keChengDAL = new JiaoXueKeChengDAL();

        /// <summary>
        /// 课程授课章节DAL
        /// </summary>
        private KeChengShouKeZhangJieDAL zhangJieDAL = new KeChengShouKeZhangJieDAL();

        #region 教学计划
        /// <summary>
        /// 教学计划
        /// </summary>
        /// <param name="JiHuaBianHaoJiBanBen">计划名称</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">大小</param>
        /// <param name="Total">总条数</param>
        /// <returns></returns>
        public List<Models.JiaoXueJiHuaBiao> Pageing(string JiHuaBianHaoJiBanBen, string KaiShiShiYongShiJian, string ZhongZhiShiYongShiJian, string ShenHeRen, int ShenHeShiFouTongGuo, int ShiFouQiYong, int PageIndex, int PageSize, out int Total)
        {
            return bLL.Pageing(JiHuaBianHaoJiBanBen, KaiShiShiYongShiJian, ZhongZhiShiYongShiJian, ShenHeRen, ShenHeShiFouTongGuo, ShiFouQiYong, PageIndex, PageSize, out Total);
        }
        #endregion


        #region 教学阶段,教学课程，教学章节
        /// <summary>
        /// 教学阶段,教学课程，教学章节
        /// </summary>
        /// <param name="exceles">Excele集合</param>
        /// <param name="jiao">教学阶段对象</param>
        /// <param name="JD">教学ID</param>
        /// <returns></returns>
        public bool AddJiaoXueJihua(List<Models.Excele> exceles, Models.JiaoXueJiHuaBiao jiao, int JD)
        {

            bool error = false;
            try
            {
                TransactionScope transaction = null;
                bLL.Add(jiao);
                bLL.Save();
                //因为每个章节只能出现一次，所以new一个new匿名类
                var KS = exceles.Select(p => new
                {
                    p.KCM,
                    p.KCID
                }).Distinct(); //去重
                int index = 1;
                foreach (var item in KS)
                {
                    Models.JiaoXueKeCheng jiaoXueKe = new Models.JiaoXueKeCheng
                    {
                        JiaoXueJiHuaDeKaiZhanShunXuHao = index++,
                        ShiFouShanChu = false,
                        SuoShuJiaoXueJiHua = jiao.ID,
                        KeChengMing = item.KCM,
                        ZengJiaShiJian = DateTime.Now,
                        ZengJiaLaiYuan = "Eccele导入",
                        SuoShuJiaoXueJieDuan = JD,
                        BeiZhu = ""
                    };
                    keChengDAL.Add(jiaoXueKe);
                    bLL.Save();
                    //var ZJ = exceles.Where(p => p.KCM == item.KCM);
                    //int Index2 = 1;
                    //foreach (var item2 in ZJ)
                    //{
                    //    Models.KeChengShouKeZhangJie zhangJie = new Models.KeChengShouKeZhangJie
                    //    {
                    //        JianYiKeShi = item2.Count,
                    //        JianYiShouKeZiYuan = "教室",
                    //        ShiFouShanChu = false,
                    //        SuoShuKeChengMing = item.KCM,
                    //        SuoShuKeChengBianHao = jiaoXueKe.ID,
                    //        ShiFouKeGeBanKaiZhan = false,
                    //        ZengJiaLaiYuan = "Eccele导入",
                    //        ZhangJieBianHao = Index2++,
                    //        ZhangJieMingChen = item2.ZJM,
                    //        ZhangJieShunXuHao = item2.KCXH
                    //    };
                    //    zhangJieDAL.Add(zhangJie);
                    //    bLL.Save();
                    //}
                }
                foreach (var item in exceles)
                {
                    int i;
                    var keCheng = keChengDAL.GetAllData().Where(p => p.KeChengMing == item.KCM && p.SuoShuJiaoXueJiHua == jiao.ID).First();
                    var ZJ = zhangJieDAL.GetAllData().Where(p => p.SuoShuKeChengBianHao == Convert.ToInt32(keCheng.ID)).Count();
                    if (ZJ== 0)
                    {
                        i = 1;
                    }
                    else
                    {
                        var ke = zhangJieDAL.GetAllData().OrderByDescending(p=>p.ID).Where(p => p.SuoShuKeChengBianHao == Convert.ToInt32(keCheng.ID)).First();
                        i =(int)ke.ZhangJieBianHao+1;
                    }
                    Models.KeChengShouKeZhangJie zhangJie = new Models.KeChengShouKeZhangJie
                    {
                        JianYiKeShi = item.Count,
                        JianYiShouKeZiYuan = "教室",
                        ShiFouShanChu = false,
                        SuoShuKeChengMing = item.KCM,
                        SuoShuKeChengBianHao = keCheng.ID,
                        ShiFouKeGeBanKaiZhan = false,
                        ZengJiaLaiYuan = "Eccele导入",
                        ZhangJieBianHao = i,
                        ZhangJieMingChen = item.ZJM,
                        ZhangJieShunXuHao = item.KCXH
                    };
                    zhangJieDAL.Add(zhangJie);
                    bLL.Save();
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


        #region 教学计划
        /// <summary>
        /// 教学详细
        /// </summary>
        /// <param name="ID">计划ID</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">大小</param>
        /// <param name="Total">总条数</param>
        /// <returns></returns>
        public List<Models.StageCourse> StageCoursesList(int ID, int PageIndex, int PageSize, out int Total)
        {
            return bLL.stageCoursesList(ID, PageIndex, PageSize, out Total);
        }
        #endregion

        #region 多表删除
        public int Deleteduobiao(int id)
        {
            return bLL.Deleteduobiao(id);
        }
        #endregion

        #region 删除时判断
        //班级开设课程计划表
        public int Selectjiaoxuejihua(int ID)
        {
            return bLL.Selectjiaoxuejihua(ID);
        }
        #endregion

    }
}
