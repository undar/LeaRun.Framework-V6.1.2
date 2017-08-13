(function(n, t) {
    "use strict";
    function u(n, t) {
        var i = Number(t.F_RelationType);
        switch (i) {
        case 0:
            n.find('[name="F_Description"]').val("无关联");
            break;
        case 1:
            n.find('[name="F_ColName"]').attr("readonly", "readonly");
            n.find('[name="F_ColName"]').val("GUID");
            n.find('[name="F_Description"]').val("系统产生GUID");
            break;
        case 2:
            n.find('[name="F_Description"]').val("关联数据字典/" + t.F_DataItemEncode);
            break;
        case 3:
            n.find('[name="F_Description"]').val("关联数据表/" + t.F_DbTable + "/" + t.F_DbRelationFlied + "/" + t.F_DbSaveFlied);
            break;
        case 4:
            n.find('[name="F_ColName"]').attr("readonly", "readonly");
            n.find('[name="F_ColName"]').val(t.F_Value);
            n.find('[name="F_Description"]').val("固定数值");
            break;
        case 5:
            n.find('[name="F_ColName"]').attr("readonly", "readonly");
            n.find('[name="F_ColName"]').val("操作人ID");
            n.find('[name="F_Description"]').val("获取导入时的用户ID");
            break;
        case 6:
            n.find('[name="F_ColName"]').attr("readonly", "readonly");
            n.find('[name="F_ColName"]').val("操作人名字");
            n.find('[name="F_Description"]').val("获取导入时的用户名字");
            break;
        case 7:
            n.find('[name="F_ColName"]').attr("readonly", "readonly");
            n.find('[name="F_ColName"]').val("操作时间");
            n.find('[name="F_Description"]').val("获取导入时的时间")
        }
    }
    var r = request("keyValue"),
    i = {
        one: {
            data: {},
            init: function() {
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
                        formatter: function(n) {
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
                    rowNum: "1000",
                    rownumbers: !0,
                    shrinkToFit: !1,
                    gridview: !0,
                    subGrid: !0,
                    subGridRowExpanded: function(i, r) {
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
                                index: "f_column",
                                width: 250,
                                sortable: !1
                            },
                            {
                                label: "数据类型",
                                name: "datatype",
                                index: "f_datatype",
                                width: 120,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "长度",
                                name: "length",
                                index: "f_length",
                                width: 57,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "允许空",
                                name: "isnullable",
                                index: "f_isnullable",
                                width: 58,
                                align: "center",
                                sortable: !1,
                                formatter: function(n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>': '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "标识",
                                name: "identity",
                                index: "f_identity",
                                width: 58,
                                align: "center",
                                sortable: !1,
                                formatter: function(n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>': '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "主键",
                                name: "key",
                                index: "f_key",
                                width: 57,
                                align: "center",
                                sortable: !1,
                                formatter: function(n) {
                                    return n == 1 ? '<i class="fa fa-check-square-o"><\/i>': '<i class="fa fa-square-o"><\/i>'
                                }
                            },
                            {
                                label: "默认值",
                                name: "default",
                                index: "f_default",
                                width: 120,
                                align: "center",
                                sortable: !1
                            },
                            {
                                label: "说明",
                                name: "remark",
                                index: "f_remark",
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
                n("#btn_Search").click(function() {
                    t.jqGrid("setGridParam", {
                        url: "../../SystemManage/DataBaseTable/GetTableListJson",
                        postData: {
                            dataBaseLinkId: n("#txt_DataBase").attr("data-value"),
                            keyword: n("#txt_Keyword").val()
                        }
                    }).trigger("reloadGrid")
                })
            },
            bind: function() {
                if (i.one.data.tablePk = n("#gridTable").jqGridRowValue("pk"), i.one.data.tableName = n("#gridTable").jqGridRowValue("name"), i.one.data.tableDescription = n("#gridTable").jqGridRowValue("tdescription"), i.one.data.dataBaseLinkId = n("#txt_DataBase").attr("data-value"), i.one.data.tablePk) n("#gridTableFlied").jqGrid("clearGridData"),
                i.two.init();
                else return t.dialogTop({
                    msg: "请选择数据表",
                    type: "error"
                }),
                !1;
                return ! 0
            }
        },
        two: {
            init: function(r, f) {
                if (r) n("#F_ModuleId").comboBoxTree({
                    url: "../../AuthorizeManage/Module/GetTreeJson",
                    description: "==请选择==",
                    maxHeight: "300px",
                    allowSearch: !0,
                    click: function(i) {
                        if (i.F_Target == "iframe") n(".tip_container").remove(),
                        n("#F_ModuleBtnId").comboBox({
                            url: "../../AuthorizeManage/ModuleButton/GetButtonListJson?moduleId=" + i.id,
                            id: "ModuleButtonId",
                            text: "FullName",
                            maxHeight: "300px",
                            allowSearch: !0,
                            dataName: "rows"
                        });
                        else return t.dialogTop({
                            msg: "请选择功能页面",
                            type: "error"
                        }),
                        "false"
                    }
                }),
                n("#F_ModuleBtnId").comboBox({}),
                n("#F_ErrorType").comboBox({
                    data: [{
                        id: 1,
                        text: "跳过"
                    },
                    {
                        id: 0,
                        text: "终止"
                    }],
                    id: "id",
                    text: "text",
                    selectOne: !0,
                    description: ""
                }),
                i.two.initJqGird();
                else t.getDataForm({
                    type: "get",
                    url: "../../SystemManage/DataBaseTable/GetTableFiledTreeJson",
                    param: {
                        dataBaseLinkId: i.one.data.dataBaseLinkId,
                        tableName: i.one.data.tableName,
                        nameId: String([])
                    },
                    async: !0,
                    success: function(t) {
                        n("#FormFieldTree").treeview({
                            height: n(window).height() - 87,
                            showcheck: !0,
                            data: t,
                            oncheckboxclick: function(t, i) {
                                if (i == 1) {
                                    var r = {
                                        F_FliedName: '<input name="F_FliedName" value="' + t.id + '" type="text" />',
                                        F_RelationType: '<input name="F_RelationType" value="0" type="text" />',
                                        F_DataItemEncode: '<input name="F_DataItemEncode" type="text" />',
                                        F_Value: '<input name="F_Value" type="text" />',
                                        F_DbId: '<input name="F_DbId" type="text" />',
                                        F_DbTable: '<input name="F_DbTable" type="text" />',
                                        F_DbSaveFlied: '<input name="F_DbSaveFlied" type="text" />',
                                        F_DbRelationFlied: '<input name="F_DbRelationFlied" type="text" />',
                                        F_FliedLabel: '<input name="F_FliedLabel" type="text" value="' + t.text + '" class="editable"  readonly/>',
                                        F_FiledType: '<input name="F_FiledType" type="text" class="editable" value="' + t.type + '"  readonly />',
                                        F_ColName: '<input name="F_ColName" type="text" class="editable" isvalid="yes" checkexpession="NotNull" />',
                                        F_OnlyOne: '<input name="F_OnlyOne" type="checkbox"   readonly />',
                                        F_Description: '<input name="F_Description" type="text" class="editable" value="无关联" readonly />',
                                        F_SetBtn: '<div class="editable jqbtn"><span data-value="0">上移<\/span><span data-value="1">下移<\/span><span data-value="2">设置<\/span><\/div>'
                                    },
                                    f = n("#gridTableFlied").find('[role="row"]');
                                    n("#gridTableFlied").jqGrid("addRowData", f.length, r);
                                    n('[aria-describedby="gridTableFlied_F_SetBtn"]').removeAttr("title");
                                    n(".jqbtn>span").unbind();
                                    n(".jqbtn>span").on("click",
                                    function() {
                                        var r = n(this),
                                        o = r.attr("data-value"),
                                        t = r.parents('[role="row"]'),
                                        i = r.parents('[role="row"]').attr("id"),
                                        f;
                                        switch (o) {
                                        case "0":
                                            t.index() != 1 && (t.find(".jqgrid-rownum").text(parseInt(i) - 1), t.attr("id", parseInt(i) - 1), t.prev().find(".jqgrid-rownum").text(i), t.prev().attr("id", i), t.prev().before(t));
                                            break;
                                        case "1":
                                            f = n("#gridTableFlied").find('[role="row"]').length;
                                            t.index() != f && (t.find(".jqgrid-rownum").text(parseInt(i) + 1), t.attr("id", parseInt(i) + 1), t.next().find(".jqgrid-rownum").text(i), t.next().attr("id", i), t.next().after(t));
                                            break;
                                        case "2":
                                            var e = t.find('[name="F_FliedLabel"]').val(),
                                            s = t.find('[name="F_RelationType"]').val(),
                                            h = t.find('[name="F_DataItemEncode"]').val(),
                                            c = t.find('[name="F_Value"]').val(),
                                            l = t.find('[name="F_DbId"]').val(),
                                            a = t.find('[name="F_DbTable"]').val(),
                                            v = t.find('[name="F_DbSaveFlied"]').val(),
                                            y = t.find('[name="F_DbRelationFlied"]').val();
                                            dialogOpen({
                                                id: "SetFieldForm",
                                                title: "设置字段关联属性【" + e + "】",
                                                url: encodeURI(encodeURI("/SystemManage/ExcelImport/SetFieldForm?F_FliedLabel=" + e + "&F_RelationType=" + s + "&F_Value=" + c + "&F_DataItemEncode=" + h + "&F_DbId=" + l + "&F_DbTable=" + a + "&F_DbSaveFlied=" + v + "&F_RelationType=" + y)),
                                                width: "500px",
                                                height: "360px",
                                                callBack: function(n) {
                                                    top.frames[n].AcceptClick(function(n) {
                                                        t.find('[name="F_FliedLabel"]').val(n.F_FliedLabel);
                                                        t.find('[name="F_RelationType"]').val(n.F_RelationType);
                                                        t.find('[name="F_DataItemEncode"]').val(n.F_DataItemEncode);
                                                        t.find('[name="F_Value"]').val(n.F_Value);
                                                        t.find('[name="F_DbId"]').val(n.F_DbId);
                                                        t.find('[name="F_DbTable"]').val(n.F_DbTable);
                                                        t.find('[name="F_DbSaveFlied"]').val(n.F_DbSaveFlied);
                                                        t.find('[name="F_DbRelationFlied"]').val(n.F_DbRelationFlied);
                                                        t.find('[name="F_ColName"]').removeAttr("readonly");
                                                        u(t, n)
                                                    })
                                                }
                                            })
                                        }
                                    })
                                } else i == 0 && n("#gridTableFlied").find('[role="row"]').each(function() {
                                    if (n(this).find('[name="F_FliedName"]').val() == t.id) return n("#gridTableFlied").jqGrid("delRowData", n(this).attr("id")),
                                    !0
                                })
                            }
                        });
                        f ? n.each(f,
                        function(t, i) {
                            n("#FormFieldTree").setCheckedNodeOne(i.F_FliedName);
                            n("#gridTableFlied").find('[role="row"]').each(function() {
                                var t = n(this);
                                if (t.find('[name="F_FliedName"]').val() == i.F_FliedName) return t.find('[name="F_ColName"]').val(i.F_ColName),
                                i.F_OnlyOne == 1 && t.find('[name="F_OnlyOne"]').attr("checked", "checked"),
                                t.find('[name="F_DataItemEncode"]').val(i.F_DataItemEncode),
                                t.find('[name="F_DbId"]').val(i.F_DbId),
                                t.find('[name="F_DbRelationFlied"]').val(i.F_DbRelationFlied),
                                t.find('[name="F_DbSaveFlied"]').val(i.F_DbSaveFlied),
                                t.find('[name="F_DbTable"]').val(i.F_DbTable),
                                t.find('[name="F_RelationType"]').val(i.F_RelationType),
                                t.find('[name="F_Value"]').val(i.F_Value),
                                u(t, i),
                                !0
                            })
                        }) : n.each(t[0].ChildNodes,
                        function(t, i) {
                            n("#FormFieldTree").setCheckedNodeOne(i.id)
                        })
                    }
                })
            },
            initJqGird: function() {
                var t = n("#gridTableFlied");
                t.jqGrid({
                    unwritten: !1,
                    datatype: "local",
                    height: n(window).height() - 156,
                    autowidth: !0,
                    colModel: [{
                        label: "字段名",
                        name: "F_FliedName",
                        hidden: !0
                    },
                    {
                        label: "关联类型",
                        name: "F_RelationType",
                        hidden: !0
                    },
                    {
                        label: "数据字典编码",
                        name: "F_DataItemEncode",
                        hidden: !0
                    },
                    {
                        label: "固定数值",
                        name: "F_Value",
                        hidden: !0
                    },
                    {
                        label: "库ID",
                        name: "F_DbId",
                        hidden: !0
                    },
                    {
                        label: "表名",
                        name: "F_DbTable",
                        hidden: !0
                    },
                    {
                        label: "保存数据字段",
                        name: "F_DbSaveFlied",
                        hidden: !0
                    },
                    {
                        label: "对应字段",
                        name: "F_DbRelationFlied",
                        hidden: !0
                    },
                    {
                        label: "字段",
                        name: "F_FliedLabel",
                        width: 200,
                        align: "left",
                        sortable: !1,
                        resizable: !1
                    },
                    {
                        label: "数据类型",
                        name: "F_FiledType",
                        width: 60,
                        align: "left",
                        sortable: !1,
                        resizable: !1
                    },
                    {
                        label: "Excel列名",
                        name: "F_ColName",
                        width: 150,
                        align: "left",
                        sortable: !1,
                        resizable: !1
                    },
                    {
                        label: "唯一性",
                        name: "F_OnlyOne",
                        width: 60,
                        align: "center",
                        sortable: !1,
                        resizable: !1
                    },
                    {
                        label: "描述",
                        name: "F_Description",
                        width: 212,
                        align: "left",
                        sortable: !1,
                        resizable: !1
                    },
                    {
                        label: "操作",
                        name: "F_SetBtn",
                        width: 110,
                        align: "center",
                        sortable: !1,
                        resizable: !1
                    }],
                    pager: !1,
                    rownumbers: !0,
                    shrinkToFit: !1,
                    gridview: !0
                })
            }
        }
    },
    f = {
        initialPage: function() {
            n("#wizard").wizard().on("change",
            function(t, r) {
                var u = n("#btn_finish"),
                f = n("#btn_next");
                if (r.direction == "next") switch (r.step) {
                case 1:
                    if (i.one.bind()) u.removeAttr("disabled"),
                    f.attr("disabled", "disabled");
                    else return ! 1
                } else u.attr("disabled", "disabled"),
                f.removeAttr("disabled")
            });
            i.one.init();
            i.two.init(!0); ! r || (n("#btn_finish").removeAttr("disabled"), n("#btn_next").attr("disabled", "disabled"), n("#btn_last").attr("disabled", "disabled"), n("#wizard li").removeClass("active"), n('#wizard  [data-target="#step-1"]').remove(), n('#wizard  [data-target="#step-2"]').addClass("active"), n("#step-1").removeClass("active"), n("#step-2").addClass("active"), t.setForm({
                url: "../../SystemManage/ExcelImport/GetFormJson",
                param: {
                    keyValue: r
                },
                success: function(t) {
                    console.log(t);
                    i.one.data.tableName = t.templateInfo.F_DbTable;
                    i.one.data.dataBaseLinkId = t.templateInfo.F_DbId;
                    n("#templateInfo").setWebControls(t.templateInfo);
                    i.two.init(!1, t.filedsInfo)
                }
            }));
            n("#btn_finish").on("click",
            function() {
                var u, f;
                if (!n("#templateInfo").Validform()) return ! 1;
                u = n("#templateInfo").getWebControls();
                u.F_DbId = i.one.data.dataBaseLinkId;
                u.F_DbTable = i.one.data.tableName;
                f = [];
                n("#gridTableFlied").find('[role="row"]').each(function() {
                    var i = n(this),
                    r;
                    i.find('[name="F_FliedName"]').length > 0 && (r = {
                        F_FliedName: i.find('[name="F_FliedName"]').val(),
                        F_FiledType: i.find('[name="F_FiledType"]').val(),
                        F_ColName: i.find('[name="F_ColName"]').val(),
                        F_OnlyOne: i.find('[name="F_OnlyOne"]').is(":checked") ? 1 : 0,
                        F_RelationType: i.find('[name="F_RelationType"]').val(),
                        F_DataItemEncode: i.find('[name="F_DataItemEncode"]').val(),
                        F_Value: i.find('[name="F_Value"]').val(),
                        F_DbId: i.find('[name="F_DbId"]').val(),
                        F_DbTable: i.find('[name="F_DbTable"]').val(),
                        F_DbSaveFlied: i.find('[name="F_DbSaveFlied"]').val(),
                        F_DbRelationFlied: i.find('[name="F_DbRelationFlied"]').val(),
                        F_SortCode: i.attr("id")
                    },
                    r.F_ColName || t.dialogTop({
                        msg: "请填写Excel列名",
                        type: "error"
                    }), f.push(r))
                });
                t.saveForm({
                    url: "../../SystemManage/ExcelImport/SaveForm?keyValue=" + r,
                    param: {
                        templateInfo: JSON.stringify(u),
                        filedsInfo: JSON.stringify(f)
                    },
                    loading: "正在保存数据...",
                    success: function() {
                        t.currentIframe().$("#gridTable").trigger("reloadGrid")
                    }
                })
            })
        }
    };
    n(function() {
        f.initialPage()
    })
})(window.jQuery, window.learun)