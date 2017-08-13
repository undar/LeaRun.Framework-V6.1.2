function btn_add() {
    dialogOpen({
        id: "InstanceForm",
        title: "新增",
        url: "/FlowManage/FormModule/CustmerFormForm?formRelationId=" + formRelationId,
        width: "800px",
        height: "600px",
        callBack: function (n) {
            top.frames[n].AcceptClick()
        }
    })
}
function btn_edit() {
    var n = $("#gridTable").jqGridRowValue("leaCustmerFormId");
    checkedRow(n) && dialogOpen({
        id: "Form",
        title: "编辑",
        url: "/FlowManage/FormModule/CustmerFormForm?keyValue=" + n + "&formRelationId=" + formRelationId,
        width: "800px",
        height: "600px",
        callBack: function (n) {
            top.frames[n].AcceptClick()
        }
    })
}
function btn_delete() {
    var n = $("#gridTable").jqGridRowValue("leaCustmerFormId");
    n ? $.RemoveForm({
        url: "../../FlowManage/FormModule/RemoveInstanceForm",
        param: {
            keyValue: n,
            frmContentId: $.pageFn.frmEntity.Id
        },
        success: function () {
            $("#gridTable").trigger("reloadGrid")
        }
    }) : dialogMsg("请选择需要删除的表单模板！", 0)
}
var formRelationId = request("Id"),
dbData = {};
$(function () {
    $.pageFn.initialPage();
    $.pageFn.init()
}),
function (n) {
    var i = function () {
        !formRelationId || n.SetForm({
            url: "../../FlowManage/FormModule/GetFormContentJson",
            param: {
                keyValue: formRelationId
            },
            success: function (i) {
                t.init(i);
                n.pageFn.frmEntity = i
            }
        })
    },
    t = {
        selectedRowIndex: "",
        init: function (i) {
            var r = [];
            frmJson = JSON.parse(i.FrmContent);
            r.push({
                label: "leaCustmerFormId",
                name: "leaCustmerFormId",
                hidden: !0
            });
            n.each(frmJson.data,
            function (t, i) {
                n.each(i.fields,
                function (t, i) {
                    var u;
                    i.type != "upload" && i.type != "image" && (i.type == "radio" || i.type == "checkbox" || i.type == "select" ? (u = {},
                    i.dataSource == "dataItem" ? u = {
                        label: i.label,
                        name: i.field,
                        index: i.field,
                        width: 120,
                        align: "left",
                        formatter: function (n) {
                            return top.clientdataItem[i.dataItemCode][n];//top.learun.data.get(["dataItem", i.dataItemCode, n]) 
                        }
                    } : (dbData[i.dbId] == undefined && (dbData[i.dbId] = {}), dbData[i.dbId][i.dbTable] == undefined && (dbData[i.dbId][i.dbTable] = {},
                    learun.getDataForm({
                        type: "get",
                        url: "../../SystemManage/DataSource/GetTableData?dbLinkId=" + i.dbId + "&tableName=" + i.dbTable,
                        async: !1,
                        success: function (t) {
                            n.each(t,
                            function (n, t) {
                                dbData[i.dbId][i.dbTable][t[i.dbFiledValue.toLowerCase()]] = t[i.dbFiledText.toLowerCase()]
                            })
                        }
                    })), u = {
                        label: i.label,
                        name: i.field,
                        index: i.field,
                        width: 120,
                        align: "left",
                        formatter: function (n) {
                            return dbData[i.dbId][i.dbTable][n]
                        }
                    }), r.push(u)) : i.type == "baseSelect" ? (u = {
                        label: i.label,
                        name: i.field,
                        index: i.field,
                        width: 120,
                        align: "left",
                        formatter: function (n) {
                            var t = "";
                            
                            switch (t.baseType) {
                                case "user":
                                    t = top.clientuserData[n].RealName;
                                    break;
                                case "department":
                                    t = top.clientdepartmentData[n].FullName;
                                    break;
                                case "organize":
                                    t = top.clientorganizeData[n].FullName;//top.learun.data.get(["organize", n, "FullName"]);
                                    break;
                                case "post":
                                    t = top.clientpostData[n].FullName;
                                    break;
                                case "job":
                                    t = top.clientpostData[n].FullName;
                                    break;
                                case "role":
                                    t = top.clientroleData[n].FullName;
                            }
                            return t
                        }
                    },
                    r.push(u)) : i.type == "currentInfo" ? (u = {
                        label: i.label,
                        name: i.field,
                        index: i.field,
                        width: 120,
                        align: "left",
                        formatter: function (n) {
                            var t = "";
                           
                            switch (i.infoType) {
                                case "user":
                                    t = top.clientuserData[n].RealName;
                                    break;
                                case "department":
                                    t = top.clientdepartmentData[n].FullName;
                                    break;
                                case "organize":
                                    t = top.clientorganizeData[n].FullName;
                                    break;
                                case "date":
                                    t = n
                            }
                            return t
                        }
                    },
                    r.push(u)) : (u = {
                        label: i.label,
                        name: i.field,
                        index: i.field,
                        width: 120,
                        align: "left"
                    },
                    r.push(u)))
                })
            });
            n("#gridTable").jqGrid({
                url: "../../FlowManage/FormModule/GetInstancePageList?relationFormId=" + formRelationId,
                datatype: "json",
                height: n(window).height() - 139.5,
                autowidth: !0,
                colModel: r,
                viewrecords: !0,
                rowNum: 30,
                rowList: [30, 50, 100],
                pager: "#gridPager",
                sortname: "CreateDate",
                rownumbers: !0,
                shrinkToFit: !1,
                gridview: !0,
                onSelectRow: function () {
                    t.selectedRowIndex = n("#" + this.id).getGridParam("selrow")
                },
                gridComplete: function () {
                    n("#" + this.id).setSelection(t.selectedRowIndex, !1)
                }
            });
            //n("#btn_Search").click(function () {
            //    n("#gridTable").trigger("reloadGrid")
            //})
        }
    };
    n.pageFn = {
        frmEntity: "",
        init: function () {
            i()
        },
        initialPage: function () {
            n(window).resize(function (t) {
                window.setTimeout(function () {
                    n("#gridTable").setGridWidth(n(".gridPanel").width());
                    n("#gridTable").setGridHeight(n(window).height() - 139.5)
                },
                200);
                t.stopPropagation()
            })
        }
    }
}(window.jQuery)