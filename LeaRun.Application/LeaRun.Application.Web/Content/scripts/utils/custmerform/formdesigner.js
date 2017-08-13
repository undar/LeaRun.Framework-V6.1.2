(function (n, t) {
    "use strict";
    var r = {
        height: 0,
        width: 0,
        type: 0
    },
    i = {
        init: function (t, u) {
            var t = n.extend(r, t);
            r = t;
            u.html('<div class="filedLayout"><\/div><div class="areaLayout"><\/div><div class="previewLayout"><\/div>');
            var f = u.find(".areaLayout"),
            e = u.find(".filedLayout"),
            o = u.find(".previewLayout");
            i.previewLayout.init(t, u);
            i.areaLayout.init(t, f);
            i.filedLayout.init(t, e);
            o.find(".previewClosed i").trigger("click");
            f.find(".btn_toFiledLayout").trigger("click")
        },
        get: function (r, u) {
            i.filedLayout.getCurrentPanelData();
            var h = r.find(".filedLayout .topToolBar .areaSelect")[0].formData,
            o = [],
            f = !0,
            s = {},
            e = 0;
            return (n.each(h,
            function (i, r) {
                if (u ? (e += r.fields.length, n.each(r.fields,
                function (n, i) {
                    return i.field ? s[i.field] ? (t.dialogTop({
                    msg: "第" + (r.area.sortCode + 1) + "区域,字段【" + i.label + "】绑定数据库字段【" + i.field + "】有重复！",
                    type: "error"
                }), f = !1) : s[i.field] = 1 : (t.dialogTop({
                    msg: "第" + (r.area.sortCode + 1) + "区域,字段【" + i.label + "】没有绑定数据库字段！",
                    type: "error"
                }), f = !1),
                    f ? void 0 : !1
                })) : e = 1, !f) return !1;
                r.area = {
                    id: r.area.id,
                    type: r.area.type,
                    margintop: r.area.margintop,
                    sortCode: r.area.sortCode
                };
                o.push(r)
            }), !f) ? f : e == 0 ? (t.dialogTop({
                msg: "请布局字段！",
                type: "error"
            }), !1) : o
        },
        updateFilds: function (t, u) {
            if (r.dbId != u.dbId || r.dbTable != u.dbTable) {
                r.dbId = u.dbId;
                r.dbTable = u.dbTable;
                r.dbFields = u.dbFields;
                i.filedLayout.getCurrentPanelData();
                var f = t.find(".filedLayout .topToolBar .areaSelect")[0].formData;
                n.each(f,
                function (t, i) {
                    n.each(i.fields,
                    function (n, t) {
                        t.field = ""
                    })
                });
                n(t.find("#app_layout_list .item_row")[0]).trigger("click")
            } else r.dbFields.length == 0 && (r.dbFields = u.dbFields, n(t.find("#app_layout_list .item_row")[0]).trigger("click"))
        },
        previewLayout: {
            init: function (n, t) {
                var i = t.find(".previewLayout");
                i.html('<div class="previewClosed"><i class="fa fa-close" title="关闭预览页" ><\/i><\/div><div id="custmerFormPreview" class="showPanel"><\/div>');
                i.height(n.height);
                i.width(n.width);
                i.find(".showPanel").height(n.height - 21);
                i.find(".previewClosed i").on("click",
                function () {
                    i.animate({
                        opacity: 0,
                        top: n.height,
                        "z-index": -2e3,
                        speed: 2e3
                    })
                })
            },
            rendering: function (n, t) {
                var i = t.find(".previewLayout .showPanel");
                i.formRendering("init", {
                    formData: n
                })
            }
        },
        areaLayout: {
            init: function (r, u) {
                var f = {
                    init: function (t) {
                        var i = '<div class="form_layout_panel">';
                        i += '<div class="leftbody">';
                        i += '<div class="topToolBar">';
                        i += '<div class="btn-group">';
                        i += '<a class="btn btn_areaAdd"><i class="fa fa-plus"><\/i>&nbsp;添加区域<\/a>';
                        i += '<a class="btn btn_toFiledLayout"><i class="fa fa-list"><\/i>&nbsp;字段布局<\/a>';
                        i += "<\/div>";
                        i += "<\/div>";
                        i += '<div class="showBar" ><\/div>';
                        i += "<\/div>";
                        i += '<div class="rightbody">';
                        i += '<div class="set_title"><i class="fa fa-info-circle"><\/i><span>设置区域的属性<\/span><\/div>';
                        i += '<div class="attr_title">显示方式<\/div>';
                        i += '<div class="attr_control"><select class="form-control showType"><option value="1">1列<\/option><option value="2">2列<\/option><option value="3">3列<\/option><option value="4">4列<\/option><option value="5">5列<\/option><option value="6">6列<\/option><\/select><\/div>';
                        i += '<div class="attr_title">间隔上一区域(px)<\/div>';
                        i += '<div class="attr_control"><input type="number" class="form-control margintop" value="0" /><\/div>';
                        i += "<\/div>";
                        i += "<\/div>";
                        u.html(i);
                        u.height(t.height);
                        u.width(t.width);
                        u.find(".form_layout_panel").height(t.height);
                        u.find(".leftbody").width(t.windowW - 240);
                        u.find(".showBar").height(t.height - 30);
                        u.find(".rightbody").height(t.height);
                        u.find(".btn_areaAdd").unbind();
                        u.find(".btn_areaAdd").on("click", f.addbtn);
                        u.find(".btn_toFiledLayout").unbind();
                        u.find(".btn_toFiledLayout").on("click", f.totoFiledLayout);
                        f.initAttributePanel(); !t.data || n.each(t.data,
                        function (n, t) {
                            f.addArea(t.area)
                        });
                        u.find(".areaItem").length != 0 || u.find(".btn_areaAdd").trigger("click")
                    },
                    initAttributePanel: function () {
                        if (typeof t == "undefined") return !1;
                        u.find(".rightbody select,.rightbody input").bind("input propertychange",
                        function () {
                            var r = n(this),
                            t = r.val(),
                            f = r.hasClass("showType"),
                            i = n("#" + u.find(".rightbody").attr("data-value")); !t || (f ? (i[0].areaItem.type = t, i.find(".typeValue").html(t)) : (i[0].areaItem.margintop = t, i.find(".margintopValue").html(t)))
                        })
                    },
                    renderingAttributePanel: function (n, t) {
                        u.find(".showType").val(n.type);
                        u.find(".margintop").val(n.margintop);
                        u.find(".rightbody").attr("data-value", t)
                    },
                    addbtn: function () {
                        f.addArea(u, {})
                    },
                    minusbtn: function () {
                        var r = n(this).parents(".areaItem"),
                        u = r.parent().find(".areaItem"),
                        i;
                        u.length == 1 ? t.dialogTop({
                            msg: "必须保留一个区域",
                            type: "error"
                        }) : (r.hasClass("active") && (i = n(u[0]), i.attr("id") == r.attr("id") && (i = n(u[1])), i.trigger("click")), r.remove())
                    },
                    clickItem: function () {
                        var i = n(this),
                        r;
                        if (typeof t == "undefined") return !1;
                        r = i.attr("id");
                        i.hasClass("active") ? f.renderingAttributePanel(i[0].areaItem, r) : (u.find(".showBar").find(".areaItem").removeClass("active"), i.addClass("active"), f.renderingAttributePanel(i[0].areaItem, r))
                    },
                    totoFiledLayout: function () {
                        u.animate({
                            opacity: 0,
                            top: r.height,
                            "z-index": -2e3,
                            speed: 2e3
                        });
                        f.refreshAreaData(u)
                    },
                    refreshAreaData: function (t) {
                        var r = t.parent().find(".filedLayout .topToolBar .areaSelect"),
                        o = r.val(),
                        u = {},
                        f,
                        e;
                        r[0].formData == undefined ? r[0].formData = {} : (u = n.extend(!0, {},
                        r[0].formData), r[0].formData = {});
                        f = "";
                        r.unbind();
                        t.find(".areaItem").each(function (t) {
                            var i = n(this)[0].areaItem;
                            t == 0 && (e = i.id);
                            i.sortCode = t;
                            f += '<option value="' + i.id + '">第' + (t + 1) + "区域(" + i.type + "列/向上间隔" + i.margintop + "px)<\/option>";
                            r[0].formData[i.id] = {
                                area: i,
                                fields: []
                            };
                            u[i.id] != undefined && (r[0].formData[i.id].fields = u[i.id].fields, o == i.id && (e = o))
                        });
                        r.html(f);
                        r.on("change", i.filedLayout.changeArea);
                        r.val(e);
                        r.trigger("change")
                    },
                    addArea: function (i) {
                        var r = n.extend({
                            type: "1",
                            margintop: 0,
                            id: t.createGuid()
                        },
                        i),
                        e = n('<div class="areaItem active"  id="' + r.id + '" >表单布局区域（<span class="typeValue">' + r.type + '<\/span>列/向上间隔<span class="margintopValue">' + r.margintop + '<\/span>px）<div class="areaItem_remove"><i title="移除区域" class="fa fa-close"><\/i><\/div><\/div>');
                        e[0].areaItem = r;
                        f.renderingAttributePanel(e[0].areaItem, r.id);
                        e.on("click", f.clickItem);
                        e.find(".fa-close").on("click", f.minusbtn);
                        u.find(".showBar").find(".areaItem").removeClass("active");
                        u.find(".showBar").append(e)
                    }
                };
                f.init(r)
            },
            refreshShow: function () {
                n(".areaLayout .areaItem").each(function () {
                    var t = n(this)[0].areaItem;
                    n(this).find(".typeValue").text(t.type)
                });
                n(n(".areaLayout .areaItem")[0]).trigger("click")
            }
        },
        filedLayout: {
            areaType: 1,
            init: function (r, u) {
                var f = {
                    init: function (t) {
                        if (f.renderHtml(t), f.initHeight(t, r), f.componentInit(), !!r.data) {
                            var i = t.find(" .topToolBar .areaSelect");
                            i[0].formData = {};
                            n.each(r.data,
                            function (n, r) {
                                i[0].formData[r.area.id] = r;
                                t.find(".guideareas").hide()
                            })
                        }
                    },
                    renderHtml: function (t) {
                        var i = '<div class="app_body"><div id="move_item_list" class="app_field"><\/div>';
                        i += '<div class="topToolBar" ><select class="areaSelect"><\/select><select class="typeSelect" ><option value="1">1列<\/option><option value="2">2列<\/option><option value="3">3列<\/option><option value="4">4列<\/option><option value="5">5列<\/option><option value="6">6列<\/option><\/select><a id="leaAreaLayout" class="btn"><i class="fa fa-th"><\/i>&nbsp;区域布局<\/a><a id="leaformPreview" class="btn"><i class="fa fa-eye"><\/i>&nbsp;整体预览<\/a><\/div>';
                        i += '<div id="app_layout_list" class="item_table connectedSortable"><\/div>';
                        i += '<div id="app_layout_option" class="field_option"><\/div><div class="guideareas"><\/div>';
                        i += "<\/div>";
                        t.html(i);
                        n("#leaAreaLayout").on("click", f.switchToAreaLayout);
                        t.find(".topToolBar .typeSelect").on("click", f.changeType);
                        n("#leaformPreview").on("click", f.previewForm)
                    },
                    changeType: function () {
                        var r = n(this).val(),
                        i = n(this).parent().find(".areaSelect"),
                        u = i.val(),
                        t = i[0].formData[u].area;
                        t.type != r && (t.type = r, i.find('[value="' + u + '"]').html("第" + (t.sortCode + 1) + "区域(" + t.type + "列/向上间隔" + t.margintop + "px)"), i.trigger("change"))
                    },
                    switchToAreaLayout: function () {
                        i.filedLayout.getCurrentPanelData();
                        u.parent().find(".areaLayout").animate({
                            opacity: 1,
                            top: 46,
                            "z-index": 2e3,
                            speed: 2e3
                        });
                        i.areaLayout.refreshShow()
                    },
                    previewForm: function () {
                        var n = i.get(u.parent());
                        i.previewLayout.rendering(n, u.parent());
                        u.parent().find(".previewLayout").animate({
                            opacity: 1,
                            top: 46,
                            "z-index": 2e3,
                            speed: 2e3
                        })
                    },
                    initHeight: function (n, t) {
                        n.find(".app_body").height(t.height);
                        n.find(".field_option").height(t.height - 44).css("right", -240);
                        n.find(".guideareas").height(t.height - 63);
                        n.find(".item_table").css("height", t.height - 30)
                    },
                    componentInit: function () {
                        n.each(t.components,
                        function (n, t) {
                            var i = t.init();
                            u.find("#move_item_list").append(i)
                        });
                        n("#move_item_list .item_row").draggable({
                            connectToSortable: "#app_layout_list",
                            helper: "clone",
                            revert: "invalid"
                        });
                        n("#app_layout_list").sortable({
                            opacity: .4,
                            delay: 300,
                            cursor: "move",
                            placeholder: "ui-state-highlight",
                            stop: function (r, u) {
                                var o = t.createGuid(),
                                f = n(u.item[0]),
                                e = f.attr("data-type");
                                e ? (f.css({
                                    width: 100 / i.filedLayout.areaType + "%",
                                    float: "left"
                                }), f.removeAttr("data-type"), f[0].itemdata = {
                                    id: o
                                },
                                t.components[e].render(f), f.unbind("click"), f.click(i.filedLayout.itemRowClick), f.find(".item_field_remove i").unbind("click"), f.find(".item_field_remove i").click(i.filedLayout.itemRowRemoveClick), f.trigger("click")) : f.trigger("click")
                            },
                            start: function () {
                                u.find(".guideareas").hide();
                                u.find(".ui-state-highlight").html("拖放控件到这里");
                                u.find(".ui-state-highlight").css({
                                    width: 100 / i.filedLayout.areaType + "%",
                                    float: "left"
                                });
                                u.find("#app_layout_list .item_row").removeClass("active")
                            },
                            out: function (t, i) {
                                if (i.helper != null) {
                                    var f = n("#app_layout_list .item_row");
                                    f.length <= 1 && (f.length == 1 ? f.find(".item_field_value").length == 0 && (n(".field_option").animate({
                                        right: -240,
                                        speed: 2e3
                                    }), n("#app_layout_list").width(r.width - 149), u.find(".guideareas").show()) : (n(".field_option").animate({
                                        right: -240,
                                        speed: 2e3
                                    }), n("#app_layout_list").width(r.width - 149), u.find(".guideareas").show()))
                                }
                            }
                        })
                    }
                };
                f.init(u)
            },
            getCurrentPanelData: function () {
                var t = [],
                i = n(".filedLayout .topToolBar .areaSelect"),
                u = n("#app_layout_list"),
                r = u.attr("data-currentId");
                n("#app_layout_list .item_row").each(function (i) {
                    var r = n(this)[0].itemdata;
                    r.sortCode = i;
                    t.push(r)
                });
                t.length > 0 && (!i[0].formData[r] || (i[0].formData[r].fields = t))
            },
            changeArea: function () {
                i.filedLayout.getCurrentPanelData();
                var r = n(this).val(),
                t = n(this)[0].formData[r];
                n(".filedLayout .topToolBar .typeSelect").val(t.area.type);
                i.filedLayout.areaType = t.area.type;
                i.filedLayout.renderFiledLayot(r, t.fields)
            },
            itemRowClick: function () {
                var i = n(this),
                u = n(".field_option");
                n("#app_layout_list .item_row").removeClass("active").removeClass("activeerror");
                n("#app_layout_list").width(r.width - 389);
                i.addClass("active");
                u.animate({
                    right: 0,
                    speed: 2e3
                }).show();
                t.components[i[0].itemdata.type].property(r, i)
            },
            itemRowRemoveClick: function () {
                var t = n(this).parents(".item_row");
                t.remove();
                n("#app_layout_list .item_row").length == 0 ? (n(".field_option").animate({
                    right: -240,
                    speed: 2e3
                }), n("#app_layout_list").width(r.width - 149), n(".guideareas").show()) : n(n("#app_layout_list .item_row")[0]).trigger("click")
            },
            renderFiledLayot: function (r, u) {
                var f = n("#app_layout_list");
                f.attr("data-currentId", r);
                f.html("");
                n.each(u,
                function (r, u) {
                    var e = n('<div class="item_row" style="display: block;"><\/div>');
                    e.css({
                        width: 100 / i.filedLayout.areaType + "%",
                        float: "left"
                    });
                    e[0].itemdata = u;
                    t.components[u.type].render(e);
                    e.unbind("click");
                    e.click(i.filedLayout.itemRowClick);
                    e.find(".item_field_remove i").unbind("click");
                    e.find(".item_field_remove i").click(i.filedLayout.itemRowRemoveClick);
                    f.append(e)
                });
                n(f.find(".item_row")[0]).trigger("click")
            }
        }
    };
    n.fn.custmerForm = function (t, r) {
        var u = n(this);
        if (!u.attr("id")) return !1;
        switch (t) {
            case "init":
                i.init(r, u);
                break;
            case "get":
                return i.get(u, r == undefined ? !1 : r.isValid);
            case "updateDbFilds":
                i.updateFilds(u, r)
        }
    }
})(window.jQuery, window.learun)