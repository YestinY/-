using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace 智联排课系统Web.Models
{
    public class TreeNode
    {
        /// <summary>
        /// 节点编号唯一
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 是否是父节点
        /// </summary>
        public bool isParent { get; set; }
        /// <summary>
        /// 节点显示名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 节点是否是展开状态
        /// </summary>
        public bool open { get; set; }
        /// <summary>
        /// 节点对应的父节点
        /// </summary>
        public string pId { get; set; }
    }
}