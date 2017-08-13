(function (n, t) {
    "use strict";
    var i = {
        one: {
            dbId: "",
            dbTable: "",
            dbFields: [],
            init: function () {
                n("#step-1 .panel-body").height(n(window).height() - 228);
                n("#FrmCategory").comboBoxTree({
                    description: "==请选择==",
                    maxHeight: "291px",
                    param: {
                        EnCode: "FormSort"
                    },
                    url: "../../SystemManage/DataItemDetail/GetDataItemTreeJson",
                    allowSearch: !0
                })
            },
            valid: function (t) {
                if (!n("#step-1").Validform()) return !1;
                t == 1 && n("#step-2").custmerForm("updateDbFilds", {
                    dbId: i.one.dbId,
                    dbTable: i.one.dbTable,
                    dbFields: i.one.dbFields
                })
            },
            setData: function (t) {
                n("#step-1").setWebControls(t)
            },
            getData: function () {
                return n("#step-1").getWebControls()
            },
            0: {
                init: function () {
                    i.one.init();
                    n(".systemTable").remove();
                    n("#step-1 .message").text("(自定义表表单-不建表)");
                    n("#step-1 textarea").height(n(window).height() - 360)
                }
            },
            1: {
                init: function () {
                    i.one.init();
                    n("#step-1 .message").text("(自定义表表单-建表)");
                    n("#step-1 textarea").height(n(window).height() - 475);
                    n(".systemTable").show();
                    n("#FrmDbId").comboBoxTree({
                        maxHeight: "252px",
                        url: "../../SystemManage/DataBaseLink/GetTreeJson",
                        allowSearch: !0
                    }).bind("change",
                    function () {
                        var t = n(this).attr("data-value");
                        i.one.dbId = t;
                        n("#FrmDbTable").comboBox({
                            description: "",
                            param: {
                                dataBaseLinkId: t
                            },
                            maxHeight: "215px",
                            allowSearch: !0,
                            url: "../../SystemManage/DataBaseTable/GetTableListJson",
                            id: "name",
                            text: "name",
                            selectOne: !0
                        })
                    });
                    n("#FrmDbTable").comboBox({}).bind("change",
                    function () {
                        var t = n(this).attr("data-value");
                        i.one.dbTable = t;
                        n.ajax({
                            url: "../../SystemManage/DataBaseTable/GetTableFiledListJson",
                            data: {
                                dataBaseLinkId: i.one.dbId,
                                tableName: t
                            },
                            type: "GET",
                            dataType: "json",
                            async: !1,
                            success: function (t) {
                                i.one.dbFields = [];
                                n.each(t,
                                function (n, t) {
                                    t.remark = t.column + "(" + t.remark + ")";
                                    i.one.dbFields.push(t)
                                });
                                n("#FrmDbPkey").comboBox({
                                    description: "",
                                    maxHeight: "185px",
                                    allowSearch: !0,
                                    data: t,
                                    id: "column",
                                    text: "remark",
                                    selectOne: !0
                                })
                            },
                            error: function (n, t, i) {
                                dialogMsg(i, -1)
                            }
                        })
                    });
                    n("#FrmDbPkey").comboBox({})
                }
            },
            2: {
                init: function () {
                    i.one.init();
                    n(".systemTable").remove();
                    n("#step-1 .message").text("(系统表单)");
                    n("#step-1 textarea").height(n(window).height() - 360)
                }
            }
        },
        two: {
            isSystemFormLoaded: "btn",
            isSystemFormOk: !1,
            systemFormUrl: "",
            systemFormfields: [],
            custmerFormInit: function (t, r) {
                var u; !r || (u = JSON.parse(r.FrmContent).data, i.one.dbId = r.FrmDbId, i.one.dbTable = r.FrmDbTable);
                n("#step-2").custmerForm("init", {
                    height: n(window).height() - 87,
                    width: n(window).width(),
                    data: u,
                    type: t,
                    dbId: i.one.dbId,
                    dbTable: i.one.dbTable,
                    dbFields: i.one.dbFields
                })
            },
            getData: function (t) {
                return t != 2 ? n("#step-2").custmerForm("get") : {
                    url: n("#F_url").val(),
                    fields: i.two.systemFormfields
                }
            },
            valid: function (r) {
                if (r != 2) return n("#step-2").custmerForm("get", {
                    isValid: !0
                });
                var u = n("#F_url").val();
                return u == "" ? (t.dialogTop({
                    msg: "请输入系统表单地址！",
                    type: "error"
                }), !1) : u != i.two.systemFormUrl ? (i.two.systemFormUrl = u, i.two.isSystemFormLoaded = "next", i.two.isSystemFormOk = !1, n("#systemFormiframe").attr("src", u), !1) : i.two.isSystemFormOk ? !0 : (t.dialogTop({
                    msg: "当前输入的系统表单地址无法被加载成功,请核实！",
                    type: "error"
                }), !1)
            },
            0: {
                init: function (n) {
                    i.two.custmerFormInit(0, n)
                }
            },
            1: {
                init: function (n) {
                    i.two.custmerFormInit(1, n)
                }
            },
            2: {
                init: function (r) {
                    function e() {
                        var u = n("#F_url").val(),
                        r;
                        u != "" && (t.loading({
                            isShow: !1
                        }), i.two.systemFormfields = t.getSystemFormFields("systemFormiframe"), i.two.systemFormfields ? (n("#step-2 .Selectsystemform").fadeOut(), i.two.systemFormfields.length > 0 ? (n("#step-2 .filedBody").fadeIn(), n("#step-2 .iframeBody").width(n(window).width() - 200)) : (n("#step-2 .filedBody").fadeOut(), n("#step-2 .iframeBody").width(n(window).width())), r = "", n.each(i.two.systemFormfields,
                        function (n, t) {
                            r += '<li class="bbit-tree-node">';
                            r += '<div  title="' + t.field + '" class="bbit-tree-node-el bbit-tree-node-leaf">';
                            r += '<i class="fa fa-tags"><\/i>&nbsp;';
                            r += '<span class="bbit-tree-node-text">' + t.label + "<\/span><\/div><\/li>"
                        }), n("#systemFormFieldList").html(r), i.two.isSystemFormOk = !0, i.two.isSystemFormLoaded == "next" && n("#btn_next").trigger("click")) : (i.two.isSystemFormLoaded != "init" && t.dialogTop({
                            msg: "系统表单无法加载！",
                            type: "error"
                        }), n("#step-2 .Selectsystemform").show()))
                    }
                    var f, u;
                    n("#step-2 .panelBody").height(n(window).height() - 146);
                    n("#step-2 .Selectsystemform").height(n(window).height() - 146);
                    n("#step-2 .filedBody").height(n(window).height() - 146).hide();
                    n("#step-2 .filedBody>ul").height(n(window).height() - 186);
                    n("#step-2 .iframeBody").width(n(window).width());
                    f = document.getElementById("systemFormiframe");
                    f.attachEvent ? f.attachEvent("onload", e) : f.onload = e;
                    n("#loadSystemForm").on("click",
                    function () {
                        var t = n("#F_url").val();
                        t != "" && (i.two.systemFormUrl = t, i.two.isSystemFormLoaded = "btn", i.two.isSystemFormOk = !1, n("#systemFormiframe").attr("src", t))
                    }); !r || (u = JSON.parse(r.FrmContent), u.data.url != "" && u.data.url != undefined && u.data.url != null && (n("#F_url").val(u.data.url), i.two.isSystemFormLoaded = "init", i.two.isSystemFormOk = !1, n("#systemFormiframe").attr("src", u.data.url)))
                }
            }
        }
    },
    r = {
        keyValue: "",
        formType: 0,
        init: function () {
            r.keyValue = t.request("keyValue"); !r.keyValue || (n("#createPanel").hide(), t.setForm({
                url: "../../FlowManage/FormModule/GetEntityJson",
                param: {
                    keyValue: r.keyValue
                },
                success: function (n) {
                    r.initByType(n.FrmType, n)
                }
            }));
            r.bindButtonEvent();
            r.wizardInit()
        },
        initByType: function (t, u) {
            r.formType = parseInt(t);
            i.one[r.formType].init();
            i.two[r.formType].init(u); !u || i.one.setData(u);
            n("#createPanel").hide()
        },
        wizardInit: function () {
            n("#wizard").wizard().on("change",
            function (t, u) {
                var f = n("#btn_finish"),
                e = n("#btn_next");
                if (u.direction == "next") switch (u.step) {
                    case 1:
                        return i.one.valid(r.formType);
                    case 2:
                        if (!i.two.valid(r.formType)) return !1;
                        f.removeAttr("disabled");
                        e.attr("disabled", "disabled");
                        n("#btn_draft").attr("disabled", "disabled")
                } else f.attr("disabled", "disabled"),
                e.removeAttr("disabled"),
                n("#btn_caogao").removeAttr("disabled")
            })
        },
        bindButtonEvent: function () {
            n("#createPanel .box").on("click", r.createFormButton);
            n("#btn_draft").on("click", r.draftButtton);
            n("#btn_finish").on("click", r.finishButtton)
        },
        createFormButton: function () {
            var t = n(this).attr("data-value");
            r.initByType(t)
        },
        draftButtton: function () {
            if (!n("#step-1").Validform()) return !1;
            var u = i.one.getData(),
            f = {
                type: r.formType,
                dbId: u.FrmDbId,
                dbTable: u.FrmDbTable,
                dbPkey: u.FrmDbPkey,
                data: i.two.getData(r.formType)
            };
            u.FrmContent = JSON.stringify(f);
            u.EnabledMark = 3;
            u.FrmType = r.formType;
            t.saveForm({
                url: "../../FlowManage/FormModule/SaveForm?keyValue=" + r.keyValue,
                param: u,
                loading: "正在保存数据...",
                success: function () {
                    n.currentIframe().$("#gridTable").trigger("reloadGrid")
                }
            })
        },
        finishButtton: function () {
            var u = i.one.getData(),
            f = {
                type: r.formType,
                dbId: u.FrmDbId,
                dbTable: u.FrmDbTable,
                dbPkey: u.FrmDbPkey,
                data: i.two.getData(r.formType)
            };
            u.FrmContent = JSON.stringify(f);
            u.EnabledMark = 1;
            u.FrmType = r.formType;
            t.saveForm({
                url: "../../FlowManage/FormModule/SaveForm?keyValue=" + r.keyValue,
                param: u,
                loading: "正在保存数据...",
                success: function () {
                    n.currentIframe().$("#gridTable").trigger("reloadGrid")
                }
            })
        }
    };
    n(function () {
        r.init()
    })
})(window.jQuery, window.learun)