
using BLL.ZM;
using FluentScheduler;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Time
{
    public class Class1 : Registry
    {
        public BLL.PaiKeJiHuaBLL PaiKeJiHuaBLL = new BLL.PaiKeJiHuaBLL();

        public BLL.BanJiKaiSheKeChengJiHuaBiaoBLL bLL = new BLL.BanJiKaiSheKeChengJiHuaBiaoBLL();

        public BLL.BanJiBiaoBLL BanJiBiao = new BLL.BanJiBiaoBLL();

        public BLL.KeChengPaiKeZongBiaoBLL KeChengPaiKe = new BLL.KeChengPaiKeZongBiaoBLL();

        public BLL.ZhengZaiShangKeBiaoBLL ShangKeBiaoBLL = new BLL.ZhengZaiShangKeBiaoBLL();

        public BLL.KeChengShouKeZhangJieBLL ShouKeZhangJieBLL = new BLL.KeChengShouKeZhangJieBLL();

        public JiaoYuanKeShiBLL j = new JiaoYuanKeShiBLL();
        public Class1()
        {
            // Schedule an IJob to run at an interval
            // 立即执行每两秒一次的计划任务。（指定一个时间间隔运行，根据自己需求，可以是秒、分、时、天、月、年等。）
            Schedule<MyJob>().ToRunNow().AndEvery(2).Seconds();

            // Schedule an IJob to run once, delayed by a specific time interval
            // 延迟一个指定时间间隔执行一次计划任务。（当然，这个间隔依然可以是秒、分、时、天、月、年等。）
            Schedule<MyJob>().ToRunOnceIn(5).Seconds();

            // Schedule a simple job to run at a specific time
            // 在一个指定时间执行计划任务（最常用。这里是在每天的下午 1:10 分执行）
            Schedule(() => Trace.WriteLine("It's 1:10 PM now.")).ToRunEvery(1).Days().At(23, 55);

            Schedule(() =>
            {
                //// 做你想做的事儿。
                //Trace.WriteLine("It's 1:10 PM now.");
                var one = PaiKeJiHuaBLL.one();
                if (one != null)
                {
                    var list = ShangKeBiaoBLL.GetAllData().Where(p => p.BeiZhu == one.JiHuaMingChen && p.KeChengMingChen != "自习").ToList();
                    foreach (var item in list)
                    {
                        var Class = BanJiBiao.GetAllData().Where(p => p.BanJiMing == item.ClassName).First();

                        var OneKC = bLL.GetAllData().Where(p => p.BanJiID.Value == Class.ID && p.KeChengMing.Value == Convert.ToInt32(item.KeChengBianHao) && p.ZhangJieBianHao.Value == Convert.ToInt32(item.ZhangJieBianHao)).First();
                        OneKC.ShiFouYiWanCheng = true;
                        OneKC.ShiJiZiYuan = item.ShiShiShiJian;
                        OneKC.ShiJiShangKeShiJian = Convert.ToDateTime(item.ShiShiShiJian);
                        var Count = ShouKeZhangJieBLL.GetAllData().Where(p => p.ZhangJieBianHao == Convert.ToInt32(item.ZhangJieBianHao) && p.SuoShuKeChengBianHao == Convert.ToInt32(item.KeChengBianHao)).First();
                        OneKC.ShiJiKeShi = Count.JianYiKeShi.ToString();
                        OneKC.AnPaiZiYuan = item.ZiYuanMingChen;
                        //OneKC.
                        bLL.Modify(OneKC);
                    }
                    KeChengPaiKe.Add();
                    one.ShiFouWanCheng = true;
                    PaiKeJiHuaBLL.Modify(one);
                    try
                    {
                        j.GetKeShi();
                    }
                    catch (Exception ex)
                    {
                        Trace.Write("出错了");
                    }
                    //讲数据填充到排课总表
                }
                else
                {
                    Trace.Write("无可执行的任务调度");
                }
            }).ToRunEvery(1).Days().At(23, 50);

            // Schedule a more complex action to run immediately and on an monthly interval
            // 立即执行一个在每月的星期一 3:00 的计划任务（可以看出来这个一个比较复杂点的时间，它意思是它也能做到！）
            Schedule<MyComplexJob>().ToRunNow().AndEvery(1).Months().OnTheFirst(DayOfWeek.Monday).At(3, 0);

            // Schedule multiple jobs to be run in a single schedule
            // 在同一个计划中执行两个（多个）任务
            Schedule<MyJob>().AndThen<MyOtherJob>().ToRunNow().AndEvery(5).Minutes();
        }
    }

    public class MyJob : IJob
    {

        void IJob.Execute()
        {
            Trace.WriteLine("现在时间是：" + DateTime.Now);
        }
    }

    public class MyOtherJob : IJob
    {

        void IJob.Execute()
        {
            Trace.WriteLine("这是另一个 Job ，现在时间是：" + DateTime.Now);
        }
    }

    public class MyComplexJob : IJob
    {

        void IJob.Execute()
        {
            Trace.WriteLine("这是比较复杂的 Job ，现在时间是：" + DateTime.Now);
        }
    }
}
