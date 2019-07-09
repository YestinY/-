using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace 智联排课系统Web.Models
{
    public class All
    {
        public List<YuanGongBiao> List; //教员

        public List<YuanGongBiao> List2; //班主任

        public List<JiaoXueKeCheng> Course; //课程

        public List<PaiKeJiHua> PaiKeJih; //排课计划

        public List<PaiKeFangAnBiao> paiKeFangs; //全部集合

        public List<PaiKeFangAnBiao> TuiJianList; //推荐集合


    }
}