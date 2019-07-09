/**
 * @Description: 菜单配置
 * @Copyright: 2017 wueasy.com Inc. All rights reserved.
 * @author: fallsea
 * @version 1.8.2
 * @License：MIT
 */
layui.define(['element', "fsConfig", "fsCommon"], function (exports) {

    var menuConfig = {
        dataType: "local", //获取数据方式，local本地获取，server 服务端获取
        loadUrl: "", //加载数据地址
        method: "post",//请求类型，默认post
        rootMenuId: "0", //根目录菜单id
        defaultSelectTopMenuId: "1", //默认选中头部菜单id
        defaultSelectLeftMenuId: "111", //默认选中左边菜单id
        menuIdField: "menuId", //菜单id
        menuNameField: "menuName", //菜单名称
        menuIconField: "menuIcon", //菜单图标，图标必须用css
        menuHrefField: "menuHref", //菜单链接
        parentMenuIdField: "parentMenuId",//父菜单id
        data: [
            //一级顶部定义start
            { "menuId": "1", "menuName": "基础管理", "menuIcon": "fa-cog", "menuHref": "", "parentMenuId": "0" },

            { "menuId": "2", "menuName": "排课管理", "menuIcon": "", "menuHref": "", "parentMenuId": "0" },

            { "menuId": "3", "menuName": "查看课表", "menuIcon": "", "menuHref": "", "parentMenuId": "0" },
            //顶级end




            //左边一级start
            { "menuId": "111", "menuName": "首页", "menuIcon": "&#xe68e;", "menuHref": "/Home/Main", "parentMenuId": "1" },

            { "menuId": "112", "menuName": "教学计划", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },

            { "menuId": "113", "menuName": "教学阶段", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },

            //{ "menuId": "114", "menuName": "教学课程", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },

            { "menuId": "115", "menuName": "班级管理", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },

            { "menuId": "116", "menuName": "资源管理", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },
            { "menuId": "117", "menuName": "部门业务", "menuIcon": "fa-table", "menuHref": "", "parentMenuId": "1" },

            { "menuId": "211", "menuName": "排课计划", "menuIcon": "fa-table", "menuHref": "/PaiKe/Index", "parentMenuId": "2" },

            { "menuId": "212", "menuName": "排课时段", "menuIcon": "fa-table", "menuHref": "/ShiDuan/Index", "parentMenuId": "2" },

            { "menuId": "213", "menuName": "班级默认排课设置", "menuIcon": "fa-table", "menuHref": "/BanJiMoRenShezi/Index", "parentMenuId": "2" },

            { "menuId": "214", "menuName": "本次排班可用资源", "menuIcon": "fa-table", "menuHref": "/BeciPaikeKeYongZhiYuan/Index", "parentMenuId": "2" },

            { "menuId": "215", "menuName": "本次排班班级", "menuIcon": "fa-table", "menuHref": "/BeciPaikeBanji/Index", "parentMenuId": "2" },

            { "menuId": "216", "menuName": "本次排课课次", "menuIcon": "fa-table", "menuHref": "/Mainbusiness/SetUp", "parentMenuId": "2" },

            { "menuId": "217", "menuName": "本次排课方案", "menuIcon": "fa-table", "menuHref": "/Mainbusiness/Main", "parentMenuId": "2" },

            { "menuId": "301", "menuName": "一次课表信息调课", "menuIcon": "fa-table", "menuHref": "/Mainbusiness/Index", "parentMenuId": "3" },

            { "menuId": "302", "menuName": "正在上课", "menuIcon": "fa-table", "menuHref": "/ClassNow/Index", "parentMenuId": "3" },

            { "menuId": "303", "menuName": "历史课表记录", "menuIcon": "fa-table", "menuHref": "/AllKB/Index", "parentMenuId": "3" },

            { "menuId": "304", "menuName": "教员代课情况", "menuIcon": "fa-table", "menuHref": "/TeacherDaiKe/TeacherHome", "parentMenuId": "3" },

            //{ "menuId": "214", "menuName": "本次排班可用资源", "menuIcon": "fa-table", "menuHref": "/BeciPaikeKeYongZhiYuan/Index", "parentMenuId": "2" },

            //左一级end

            //左边start
            { "menuId": "112_1", "menuName": "教学计划管理", "menuIcon": "fa-list", "menuHref": "/JiaoXueJiHua/Index", "parentMenuId": "112" },


            { "menuId": "115_1", "menuName": "学生信息管理", "menuIcon": "fa-list", "menuHref": "/ZM/ZMIndex", "parentMenuId": "115" },

            { "menuId": "115_2", "menuName": "班级信息管理", "menuIcon": "fa-list", "menuHref": "/ZM/BJIndex", "parentMenuId": "115" },

            { "menuId": "113_1", "menuName": "教学阶段", "menuIcon": "fa-list", "menuHref": "/LSM/Index", "parentMenuId": "113" },

            { "menuId": "116_1", "menuName": "资源管理", "menuIcon": "fa-list", "menuHref": "/LZJZhiYuanGuanLi/Index", "parentMenuId": "116" },
            { "menuId": "117_1", "menuName": "员工管理", "menuIcon": "fa-list", "menuHref": "/YuanGong/Index", "parentMenuId": "117" },
            { "menuId": "117_2", "menuName": "部门管理", "menuIcon": "fa-list", "menuHref": "/BuMen/Index", "parentMenuId": "117" },
            { "menuId": "117_3", "menuName": "部门信息", "menuIcon": "fa-list", "menuHref": "/BuMen/BuMenIndex", "parentMenuId": "117" }

        ] //本地数据
    };

    var element = layui.element,
        fsCommon = layui.fsCommon,
        fsConfig = layui.fsConfig,
        statusName = $.result(fsConfig, "global.result.statusName", "errorNo"),
        msgName = $.result(fsConfig, "global.result.msgName", "errorInfo"),
        successNo = $.result(fsConfig, "global.result.successNo", "0"),
        dataName = $.result(fsConfig, "global.result.dataName", "results.data"),
        FsMenu = function () {

        };


    FsMenu.prototype.render = function () {

        this.loadData();

        this.showMenu();
    };

	/**
	 * 加载数据
	 */
    FsMenu.prototype.loadData = function () {

        if (menuConfig.dataType == "server") {//服务端拉取数据

            var url = menuConfig.loadUrl;
            if ($.isEmpty(url)) {
                fsCommon.errorMsg("未配置请求地址！");
                return;
            }

            fsCommon.invoke(url, {}, function (data) {
                if (data[statusName] == successNo) {
                    menuConfig.data = $.result(data, dataName);
                }
                else {
                    //提示错误消息
                    fsCommon.errorMsg(data[msgName]);
                }
            }, false, menuConfig.method);

        }

    }


	/**
	 * 获取图标
	 */
    FsMenu.prototype.getIcon = function (menuIcon) {

        if (!$.isEmpty(menuIcon)) {

            if (menuIcon.indexOf("<i") == 0) {
                return menuIcon;
            } else if (menuIcon.indexOf("&#") == 0) {
                return '<i class="layui-icon">' + menuIcon + '</i>';
            } else if (menuIcon.indexOf("fa-") == 0) {
                return '<i class="fa ' + menuIcon + '"></i>';
            } else {
                return '<i class="' + menuIcon + '"></i>';
            }
        }
        return "";
    };

	/**
	 * 清空菜单
	 */
    FsMenu.prototype.cleanMenu = function () {
        $("#fsTopMenu").html("");
        $("#fsLeftMenu").html("");
    }
	/**
	 * 显示菜单
	 */
    FsMenu.prototype.showMenu = function () {
        var thisMenu = this;
        var data = menuConfig.data;
        if (!$.isEmpty(data)) {
            var _index = 0;
            //显示顶部一级菜单
            var fsTopMenuElem = $("#fsTopMenu");
            var fsLeftMenu = $("#fsLeftMenu");
            $.each(data, function (i, v) {
                if (menuConfig.rootMenuId === v[menuConfig.parentMenuIdField]) {

                    var topStr = '<li class="layui-nav-item';
                    if ($.isEmpty(menuConfig.defaultSelectTopMenuId) && _index === 0) {//为空默认选中第一个
                        topStr += ' layui-this';
                    } else if (!$.isEmpty(menuConfig.defaultSelectTopMenuId) && menuConfig.defaultSelectTopMenuId == v[menuConfig.menuIdField]) {//默认选中处理
                        topStr += ' layui-this';
                    }
                    _index++;
                    topStr += '" dataPid="' + v[menuConfig.menuIdField] + '"><a href="javascript:;">' + thisMenu.getIcon(v[menuConfig.menuIconField]) + ' <cite>' + v[menuConfig.menuNameField] + '</cite></a></li>';
                    fsTopMenuElem.append(topStr);

                    //显示二级菜单，循环判断是否有子栏目
                    $.each(data, function (i2, v2) {
                        if (v[menuConfig.menuIdField] === v2[menuConfig.parentMenuIdField]) {

                            var menuRow = '<li class="layui-nav-item';
                            if (!$.isEmpty(menuConfig.defaultSelectLeftMenuId) && menuConfig.defaultSelectLeftMenuId == v2[menuConfig.menuIdField]) {//默认选中处理
                                menuRow += ' layui-this';
                            }
                            //显示三级菜单，循环判断是否有子栏目
                            var menuRow3 = "";
                            $.each(data, function (i3, v3) {
                                if (v2[menuConfig.menuIdField] === v3[menuConfig.parentMenuIdField]) {
                                    if ($.isEmpty(menuRow3)) {
                                        menuRow3 = '<dl class="layui-nav-child">';
                                    }
                                    menuRow3 += '<dd';
                                    if (!$.isEmpty(menuConfig.defaultSelectLeftMenuId) && menuConfig.defaultSelectLeftMenuId == v3[menuConfig.menuIdField]) {//默认选中处理
                                        menuRow3 += ' class="layui-this"';
                                        menuRow += ' layui-nav-itemed';//默认展开二级菜单
                                    }

                                    menuRow3 += ' lay-id="' + v3[menuConfig.menuIdField] + '"><a href="javascript:;" menuId="' + v3[menuConfig.menuIdField] + '" dataUrl="' + v3[menuConfig.menuHrefField] + '">' + thisMenu.getIcon(v3[menuConfig.menuIconField]) + ' <cite>' + v3[menuConfig.menuNameField] + '</cite></a></dd>';

                                }

                            });

                            menuRow += '" lay-id="' + v2[menuConfig.menuIdField] + '" dataPid="' + v2[menuConfig.parentMenuIdField] + '" style="display: none;"><a href="javascript:;" menuId="' + v2[menuConfig.menuIdField] + '" dataUrl="' + v2[menuConfig.menuHrefField] + '">' + thisMenu.getIcon(v2[menuConfig.menuIconField]) + ' <cite>' + v2[menuConfig.menuNameField] + '</cite></a>';


                            if (!$.isEmpty(menuRow3)) {
                                menuRow3 += '</dl>';

                                menuRow += menuRow3;
                            }

                            menuRow += '</li>';

                            fsLeftMenu.append(menuRow);
                        }

                    });

                }
            });
        }
        element.render("nav");
    };

    var fsMenu = new FsMenu();
    exports("fsMenu", fsMenu);
});
