﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class ZhiLianPaiKeXiTongDBEntities : DbContext
    {
        public ZhiLianPaiKeXiTongDBEntities()
            : base("name=ZhiLianPaiKeXiTongDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<BanJiBiao> BanJiBiao { get; set; }
        public DbSet<BanJiKaiSheKeChengJiHuaBiao> BanJiKaiSheKeChengJiHuaBiao { get; set; }
        public DbSet<BanZhuRenSuoDaiBanJiBiao> BanZhuRenSuoDaiBanJiBiao { get; set; }
        public DbSet<BenCiPaiBanKeYongZiYuan> BenCiPaiBanKeYongZiYuan { get; set; }
        public DbSet<BenCiPaiKeBanJiJiCiShu> BenCiPaiKeBanJiJiCiShu { get; set; }
        public DbSet<BenCiPaiKeShiDuanBiao> BenCiPaiKeShiDuanBiao { get; set; }
        public DbSet<BuMenBiao> BuMenBiao { get; set; }
        public DbSet<JiaoXueJieDuanBiao> JiaoXueJieDuanBiao { get; set; }
        public DbSet<JiaoXueJiHuaBiao> JiaoXueJiHuaBiao { get; set; }
        public DbSet<JiaoXueKeCheng> JiaoXueKeCheng { get; set; }
        public DbSet<JiaoYuanDaiKeBiao> JiaoYuanDaiKeBiao { get; set; }
        public DbSet<JiaoYuanDuiYingJiHuaZhangJie> JiaoYuanDuiYingJiHuaZhangJie { get; set; }
        public DbSet<KeChengPaiKeZongBiao> KeChengPaiKeZongBiao { get; set; }
        public DbSet<KeChengShouKeZhangJie> KeChengShouKeZhangJie { get; set; }
        public DbSet<PaiKeBanJiMoRenSheZhi> PaiKeBanJiMoRenSheZhi { get; set; }
        public DbSet<PaiKeJiHua> PaiKeJiHua { get; set; }
        public DbSet<PaiKeShiDuanYuZiYuanZuHe> PaiKeShiDuanYuZiYuanZuHe { get; set; }
        public DbSet<XueShengBiao> XueShengBiao { get; set; }
        public DbSet<XueShengYuBanJiDuiYingBiao> XueShengYuBanJiDuiYingBiao { get; set; }
        public DbSet<YiCiPaiKeDeKeBiaoXinXiBiao> YiCiPaiKeDeKeBiaoXinXiBiao { get; set; }
        public DbSet<YuanGongBiao> YuanGongBiao { get; set; }
        public DbSet<ZhengZaiShangKeBiao> ZhengZaiShangKeBiao { get; set; }
        public DbSet<ZhiWeiBiao> ZhiWeiBiao { get; set; }
        public DbSet<ZiYuanGuanLi> ZiYuanGuanLi { get; set; }
        public DbSet<BeCiPaiKeMorenKeCiBioa> BeCiPaiKeMorenKeCiBioa { get; set; }
        public DbSet<PaiKeFangAnBiao> PaiKeFangAnBiao { get; set; }
        public DbSet<PaiKeList> PaiKeList { get; set; }
        public DbSet<PaikeLinshiBiao> PaikeLinshiBiao { get; set; }
        public DbSet<KeShiBiao> KeShiBiao { get; set; }
    }
}
