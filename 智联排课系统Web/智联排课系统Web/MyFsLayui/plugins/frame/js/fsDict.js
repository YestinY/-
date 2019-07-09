/**
 * @Description: 字典配置
 * @Copyright: 2017 wueasy.com Inc. All rights reserved.
 * @author: fallsea
 * @version 1.8.2
 * @License：MIT
 */
layui.fsDict = {
    //城市
    city: {
        formatType: "local",
        labelField: "name",
        valueField: "code",
        //静态数据
        data: [{ "code": 0, "name": "北京", "style": "color:#F00;" },
        { "code": 1, "name": "上海" },
        { "code": 2, "name": "广州" },
        { "code": 3, "name": "深圳" },
        { "code": 4, "name": "杭州" }
        ]
    },
    //类型
    type: {
        formatType: "local",
        labelField: "name",
        valueField: "code",
        spaceMode: " ",//展示多个数据分隔符，默认,
        data: [{ "code": "write", "name": "写作", "css": "layui-badge layui-bg-orange" },
        { "code": "read", "name": "阅读", "css": "layui-badge layui-bg-green" },
        { "code": "dai", "name": "发呆", "css": "layui-badge layui-bg-cyan" }]
    }
    ,
    //性别
    sex: {
        formatType: "local",
        labelField: "name",
        valueField: "code",
        spaceMode: "",//展示多个数据分隔符，默认,
        data: [{ "code": "男", "name": "男" },
        { "code": "女", "name": "女" }]
    }
    ,
    //省份
    area1: {
        formatType: "server",
        loadUrl: "/fsbus/DEMO1006",
        method: "get",
        inputs: "parentid:0",
        labelField: "name",
        valueField: "id"
    },
    //城市
    area2: {
        formatType: "server",
        loadUrl: "/fsbus/DEMO1006",
        inputs: "parentid:",
        labelField: "name",
        valueField: "id"
    },
    //区
    area3: {
        formatType: "server",
        loadUrl: "/fsbus/DEMO1006",
        inputs: "parentid:,area1:#area2222222",
        labelField: "name",
        valueField: "id"
    },
    //教学阶段
    JiaoXueJieDuan: {
        formatType: "server",
        loadUrl: "/Common/JiaoXueJieDuan",
        //inputs: "parentid:,area1:#area2222222",
        method: "get",
        labelField: "JieDuanMing",
        valueField: "ID"
    },

    //学生集合
    StudentList: {
        formatType: "server",
        loadUrl: "/Common/Studentlist",
        //inputs: "parentid:,area1:#area2222222",
        method: "get",
        labelField: "Name",
        valueField: "BanJiId"
    },
    //计划
    Plan: {
        formatType: "server",
        loadUrl: "/Common/Plan",
        //inputs: "parentid:,area1:#area2222222",
        method: "get",
        labelField: "Name",
        valueField: "ID"
    },
    ErZhiWEi: {
        formatType: "server",
        loadUrl: "/YuanGong/ErZhiWEi",
        inputs: "id",
        labelField: "name",
        valueField: "id"
    },
    YiBuMen: {
        formatType: "server",
        loadUrl: "/BuMen/YiBuMen",
        //inputs: "parentid:,area1:#area2222222",
        labelField: "name",
        valueField: "id"
    },
    ErBuMen: {
        formatType: "server",
        loadUrl: "/BuMen/ErBuMen",
        inputs: "id",
        labelField: "name",
        valueField: "id"
    }
};

