(function (n) {
    "use strict";
    var LookCodeJson = [];
    var t = {
        one: {
            data: {},
            init: function () {
                n("#txt_DataBase").ComboBox({
                    url: "../../SystemManage/DataBaseLink/GetListJson",
                    id: "DatabaseLinkId",
                    text: "DBAlias",
                    height: "400px",
                    description: "",
                    allowSearch: !0,
                    selectOne: !0
                });
                var t = n("#gridTable");
                t.jqGrid({
                    url: "../../SystemManage/DataBaseTable/GetTableListJson",
                    postData: {
                        dataBaseLinkId: n("#txt_DataBase").attr("data-value"),
                        keyword: n("#txt_Keyword").val()
                    },
                    datatype: "json",
                    height: n(window).height() - 197,
                    autowidth: !0,
                    colModel: [{
                        label: "表名",
                        name: "name",
                        width: 260,
                        align: "left",
                        sortable: !1
                    },
                    {
                        label: "主键",
                        name: "pk",
                        width: 150,
                        align: "left",
                        sortable: !1
                    },
                    {
                        label: "记录数",
                        name: "sumrows",
                        width: 100,
                        align: "left",
                        sortable: !1,
                        formatter: function (n) {
                            return n + "条"
                        }
                    },
                    {
                        label: "使用大小",
                        name: "reserved",
                        width: 100,
                        align: "left",
                        sortable: !1
                    },
                    {
                        label: "更新时间",
                        name: "updatetime",
                        width: 120,
                        align: "left",
                        sortable: !1
                    },
                    {
                        label: "说明",
                        name: "tdescription",
                        width: 120,
                        align: "left",
                        sortable: !1
                    }],
                    multiselect: !0,
                    rowNum: "1000",
                    rownumbers: !0,
                    shrinkToFit: !1,
                    gridview: !0,
                    subGrid: !0,
                    subGridRowExpanded: function (i, r) {
                        var f = t.jqGrid("getRowData", r).name,
                        u = i + "_t";
                        n("#" + i).html("<table id='" + u + "'><\/table>");
                        n("#" + u).jqGrid({
                            url: "../../SystemManage/DataBaseTable/GetTableFiledListJson",
                            postData: {
                                dataBaseLinkId: n("#txt_DataBase").attr("data-value"),
                                tableName: f
                            },
                            datatype: "json",
                            height: "100%",
                            colModel: [{
                                label: "列名",
                                name: "column",
                                index: "column",
                                width: 250,
                                sortable: !1
                            },
                            {
                                label: "数据类型",
                                name: "datatype",
                                index: "datatype",
                                width: 120,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "长度",
                                name: "length",
                                index: "length",
                                width: 57,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "允许空",
                                name: "isnullable",
                                index: "isnullable",
                                width: 58,
                                align: "center",
                                sortable: !1,
                                formatter: function (n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>' : '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "标识",
                                name: "identity",
                                index: "identity",
                                width: 58,
                                align: "center",
                                sortable: !1,
                                formatter: function (n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>' : '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "主键",
                                name: "key",
                                index: "key",
                                width: 57,
                                align: "center",
                                sortable: !1,
                                formatter: function (n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>' : '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "默认值",
                                name: "default",
                                index: "default",
                                width: 120,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "说明",
                                name: "remark",
                                index: "remark",
                                width: 100,
                                sortable: !1
                            }],
                            caption: "表字段信息",
                            rowNum: "1000",
                            rownumbers: !0,
                            shrinkToFit: !1,
                            gridview: !0,
                            hidegrid: !1
                        })
                    }
                });
                n("#btn_Search").click(function () {
                    t.jqGrid("setGridParam", {
                        url: "../../SystemManage/DataBaseTable/GetTableListJson",
                        postData: {
                            dataBaseLinkId: n("#txt_DataBase").attr("data-value"),
                            keyword: n("#txt_Keyword").val()
                        }
                    }).trigger("reloadGrid")
                })
            },
            bind: function () {
                if (t.one.data.tablePk = n("#gridTable").jqGridRowValue("pk"), t.one.data.tableName = n("#gridTable").jqGridRowValue("name"), t.one.data.tableDescription = n("#gridTable").jqGridRowValue("tdescription"), t.one.data.dataBaseLinkId = n("#txt_DataBase").attr("data-value"), t.one.data.tablePk) t.two.init();
                else return learun.dialogTop({
                    msg: "请选择数据表",
                    type: "error"
                }),
                !1;
                return !0
            }
        },
        two: {
            init: function (i) {
                if (i) n("#OutputAreas").comboBox({
                    url: "../../SystemManage/DataItemDetail/GetDataItemListJson",
                    param: {
                        EnCode: "AreaName"
                    },
                    id: "ItemValue",
                    text: "ItemName",
                    maxHeight: "200px",
                    description: "",
                    selectOne: !0,
                    allowSearch: !0
                });
                else {
                    var u = n("#tableList"),
                    f = n("#tableList2"),
                    e = t.one.data.tableName.split(","),
                    o = t.one.data.tablePk.split(","),
                    s = t.one.data.tableDescription.split(","),
                    r = "";
                    n.each(e,
                    function (n, i) {
                        var u = o[n];
                        n == 0 ? (t.two.set(i, u, s[n]), r += '<li data-value="' + n + '" class="active tableListOne"><i class="fa fa-file-text-o"><\/i>' + i + "<\/li>") : r += '<li data-value="' + n + '" class="tableListOne"><i class="fa fa-file-text-o"><\/i>' + i + "<\/li>"
                    });
                    u.html(r);
                    u.find("li").unbind();
                    u.find("li").on("click",
                    function () {
                        n(".profile-nav li").removeClass("active");
                        n(".profile-nav li").removeClass("hover");
                        n(this).addClass("active");
                        var i = n(this).attr("data-value");
                        t.two.set(e[i], o[i], s[i])
                    }).hover(function () {
                        n(this).hasClass("active") || n(this).addClass("hover")
                    },
                    function () {
                        n(this).removeClass("hover")
                    });
                    f.html(r);
                    f.find("li").unbind();
                    f.find("li").on("click",
                    function () {
                        n(".profile-nav li").removeClass("active");
                        n(".profile-nav li").removeClass("hover");
                        n(this).addClass("active");
                        var i = n(this).attr("data-value");
                        t.three.lookCode(i)
                    }).hover(function () {
                        n(this).hasClass("active") || n(this).addClass("hover")
                    },
                    function () {
                        n(this).removeClass("hover")
                    })
                }
            },
            set: function (t, i, r) {
                n("#Description").val(r);
                n("#DataBaseTablePK").val(i);
                n("#EntityClassName").val(t + "Entity").attr("tableName", t);
                n("#MapClassName").val(t + "Map");
                n("#ServiceClassName").val(t + "Service");
                n("#IServiceClassName").val(t + "IService");
                n("#BusinesClassName").val(t + "BLL")
            },
            bind: function () {
                return n("#step-2").Validform() ? (t.three.lookCode(0), !0) : !1
            }
        },
        three: {
            lookCode: function (i) {
                learun.loading({
                    isShow: !0,
                    text: "正在生成代码..."
                });
                window.setTimeout(function () {
                    var u = t.one.data.tableName.split(",")[i],
                    r = n("#baseconfig").getWebControls();
                    r.DataBaseLinkId = t.one.data.dataBaseLinkId;
                    r.DataBaseTableName = u;
                    r.DataBaseTablePK = t.one.data.tablePk.split(",")[i];
                    r.Description = t.one.data.tableDescription.split(",")[i];
                    r.CreateUser = n("#CreateUser").val();
                    r.CreateDate = n("#CreateDate").val();
                    r.EntityClassName = u + "Entity";
                    r.MapClassName = u + "Map";
                    r.ServiceClassName = u + "Service";
                    r.IServiceClassName = u + "IService";
                    r.BusinesClassName = u + "BLL";
                    n.ajax({
                        url: "../../GeneratorManage/ServiceCode/LookCode",
                        data: r,
                        type: "post",
                        dataType: "json",
                        async: !1,
                        success: function (t) {
                            LookCodeJson = t;
                            var i = n("#step-3 .nav-tabs li.active").attr("id");
                            n("#showCodeAreas").html('<textarea name="SyntaxHighlighter" class="brush: c-sharp;">' + t[i.substring(4)] + "<\/textarea>");
                            SyntaxHighlighter.highlight();
                            n("#step-3 .nav-tabs li").unbind();
                            n("#step-3 .nav-tabs li").click(function () {
                                var i = n(this).attr("id");
                                n("#showCodeAreas").html('<textarea name="SyntaxHighlighter" class="brush: c-sharp;">' + t[i.substring(4)] + "<\/textarea>");
                                SyntaxHighlighter.highlight()
                            })
                        },
                        complete: function () {
                            learun.loading({
                                isShow: !1
                            })
                        }
                    })
                },
                500)
            }
        },
        four: {
            createCode: function () {
                learun.loading({
                    isShow: !0,
                    text: "正在创建代码..."
                });
                window.setTimeout(function () {
                  
                     i = n("#baseconfig").getWebControls();
                    i.DataBaseLinkId = t.one.data.dataBaseLinkId;
                    i.DataBaseTableName = t.one.data.tableName;
                    i.DataBaseTablePK = t.one.data.tablePk;
                    i.Description = t.one.data.tableDescription;
                    i.CreateUser = n("#CreateUser").val();
                    i.CreateDate = n("#CreateDate").val();
                    i.EntityClassName = i.DataBaseTableName + "Entity";
                    i.MapClassName = i.DataBaseTableName + "Map";
                    i.ServiceClassName = i.DataBaseTableName + "Service";
                    i.IServiceClassName = i.DataBaseTableName + "IService";
                    i.BusinesClassName = i.DataBaseTableName + "BLL";
                    n.ajax({
                        url: "../../GeneratorManage/ServiceCode/CreateCode",
                        data: {hi:i,strCode: encodeURIComponent(JSON.stringify(LookCodeJson))},
                        type: "post",
                        dataType: "json",
                        async: !1,
                        success: function (t) {
                            n(".drag-tip").show();
                            t.type == 1 ? (n("#finish-msg").html(t.message).css("color", "#0FA74F"), n("#finish-msg").prev("i").attr("class", "fa fa-check-circle").css("color", "#0FA74F"), n("#finish-msg").next("p").show()) : (n("#finish-msg").html(t.message).css("color", "#d9534f"), n("#finish-msg").prev("i").attr("class", "fa fa-times-circle").css("color", "#d9534f"), n("#finish-msg").next("p").hide())
                        },
                        complete: function () {
                            learun.loading({
                                isShow: !1
                            })
                        }
                    })
                },
                500)
            }
        }
    },
    i = {
        initialPage: function () {
            n("#step-2 > div").css("overflow-y", "auto").height(n(window).height() - 84);
            n("#tableList").css("overflow-y", "auto").height(n(window).height() - 137);
            n("#tableList2").css("overflow-y", "auto").height(n(window).height() - 137);
            n("#showCodeAreas").height(n(window).height() - 131);
            n("#wizard").wizard().on("change",
            function (i, r) {
                var u = n("#btn_finish"),
                f = n("#btn_next");
                if (r.direction == "next") switch (r.step) {
                    case 1:
                        return t.one.bind();
                    case 2:
                        return t.two.bind();
                    case 3:
                        t.four.createCode();
                        u.removeAttr("disabled");
                        f.attr("disabled", "disabled")
                } else u.attr("disabled", "disabled"),
                f.removeAttr("disabled")
            });
            n("#btn_finish").click(function () {
                dialogClose()
            });
            t.one.init();
            t.two.init(!0)
        }
    };
    n(function () {
        i.initialPage()
    })
})(window.jQuery)