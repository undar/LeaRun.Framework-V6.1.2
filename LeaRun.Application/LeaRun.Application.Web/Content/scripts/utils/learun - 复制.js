$(function () {
    window.onload = function () {
        Loading(!0)
    };
    $(".ui-filter-text").click(function () {
        $(this).next(".ui-filter-list").is(":hidden") ? ($(this).css("border-bottom-color", "#fff"), $(".ui-filter-list").slideDown(10), $(this).addClass("active")) : ($(this).css("border-bottom-color", "#ccc"), $(".ui-filter-list").slideUp(10), $(this).removeClass("active"))
    });
    $(".profile-nav li").click(function () {
        $(".profile-nav li").removeClass("active");
        $(".profile-nav li").removeClass("hover");
        $(this).addClass("active")
    }).hover(function () {
        $(this).hasClass("active") || $(this).addClass("hover")
    },
    function () {
        $(this).removeClass("hover")
    })
});
Loading = function (n, t) {
    var i = top.$("#loading_background,#loading_manage");
    n ? i.show() : top.$("#loading_manage").attr("istableloading") == undefined && (i.hide(), top.$(".ajax-loader").remove());
    t ? top.$("#loading_manage").html(t) : top.$("#loading_manage").html("正在拼了命为您加载…");
    top.$("#loading_manage").css("left", (top.$("body").width() - top.$("#loading_manage").width()) / 2 - 54);
    top.$("#loading_manage").css("top", (top.$("body").height() - top.$("#loading_manage").height()) / 2)
};
tabiframeId = function () {
    return top.$(".LRADMS_iframe:visible").attr("id")
};
$.fn.ComboBox = function (n) {
    function f(r, u, f) {
        if (r.length > 0) {
            var e = $("<ul><\/ul>");
            n.description && e.append('<li data-value="">' + n.description + "<\/li>");
            $.each(r,
            function (t) {
                var i = r[t],
                o = i[n.title];
                o == undefined && (o = "");
                u != undefined ? i[f.text].indexOf(u) != -1 && e.append('<li data-value="' + i[n.id] + '" title="' + o + '">' + i[n.text] + "<\/li>") : e.append('<li data-value="' + i[n.id] + '" title="' + o + '">' + i[n.text] + "<\/li>")
            });
            i.find(".ui-select-option-content").html(e);
            i.find("li").css("padding", "0 5px");
            i.find("li").click(function (n) {
                var r = $(this).text(),
                u = $(this).attr("data-value");
                t.attr("data-value", u).attr("data-text", r);
                t.find(".ui-select-text").html(r).css("color", "#000");
                i.slideUp(150);
                t.trigger("change");
                n.stopPropagation()
            }).hover(function (n) {
                $(this).hasClass("liactive") || $(this).toggleClass("on");
                n.stopPropagation()
            })
        }
    }
    var t = $(this),
    r,
    u,
    i,
    e;
    return t.attr("id") ? (n && t.find(".ui-select-text").length == 0 && (r = "", r += "<div class=\"ui-select-text\" style='color:#999;'>" + n.description + "<\/div>", r += '<div class="ui-select-option">', r += '<div class="ui-select-option-content" style="max-height: ' + n.height + '">' + t.html() + "<\/div>", n.allowSearch && (r += '<div class="ui-select-option-search"><input type="text" class="form-control" placeholder="搜索关键字" /><span class="input-query" title="Search"><i class="fa fa-search"><\/i><\/span><\/div>'), r += "<\/div>", t.html(""), t.append(r)), u = $($("<p>").append(t.find(".ui-select-option").clone()).html()), u.attr("id", t.attr("id") + "-option"), t.find(".ui-select-option").remove(), u.length > 0 && $("body").find("#" + t.attr("id") + "-option").remove(), $("body").prepend(u), i = $("#" + t.attr("id") + "-option"), n.url != undefined ? (i.find(".ui-select-option-content").html(""), $.ajax({
        url: n.url,
        data: n.param,
        type: "GET",
        dataType: "json",
        async: !1,
        success: function (t) {
            n.data = t;
            var i = t;
            f(i)
        },
        error: function (n, t, i) {
            dialogMsg(i, -1)
        }
    })) : n.data != undefined ? (e = n.data, f(e)) : (i.find("li").css("padding", "0 5px"), i.find("li").click(function (n) {
        var r = $(this).text(),
        u = $(this).attr("data-value");
        t.attr("data-value", u).attr("data-text", r);
        t.find(".ui-select-text").html(r).css("color", "#000");
        i.slideUp(150);
        t.trigger("change");
        n.stopPropagation()
    }).hover(function (n) {
        $(this).hasClass("liactive") || $(this).toggleClass("on");
        n.stopPropagation()
    })), n.allowSearch && (i.find(".ui-select-option-search").find("input").bind("keypress",
    function () {
        if (event.keyCode == "13") {
            var n = $(this).val();
            f($(this)[0].options.data, n, $(this)[0].options)
        }
    }).focus(function () {
        $(this).select()
    })[0].options = n), t.unbind("click"), t.bind("click",
    function (r) {
        var o;
        if (t.attr("readonly") == "readonly" || t.attr("disabled") == "disabled") return !1;
        if ($(this).addClass("ui-select-focus"), i.is(":hidden")) {
            t.find(".ui-select-option").hide();
            $(".ui-select-option").hide();
            var e = t.offset().left,
            u = t.offset().top + 29,
            f = t.width();
            n.width && (f = n.width);
            i.height() + u < $(window).height() ? i.slideDown(150).css({
                top: u,
                left: e,
                width: f
            }) : (o = u - i.height() - 32, i.show().css({
                top: o,
                left: e,
                width: f
            }), i.attr("data-show", !0));
            i.css("border-top", "1px solid #ccc");
            i.find("li").removeClass("liactive");
            i.find("[data-value=" + t.attr("data-value") + "]").addClass("liactive");
            i.find(".ui-select-option-search").find("input").select()
        } else i.attr("data-show") ? i.hide() : i.slideUp(150);
        r.stopPropagation()
    }), $(document).click(function (n) {
        var n = n ? n : window.event,
        r = n.srcElement || n.target;
        $(r).hasClass("form-control") || (i.attr("data-show") ? i.hide() : i.slideUp(150), t.removeClass("ui-select-focus"), n.stopPropagation())
    }), t) : !1
};
$.fn.ComboBoxSetValue = function (n) {
    var t, r, i;
    if (!$.isNullOrEmpty(n)) return t = $(this),
    r = $("#" + t.attr("id") + "-option"),
    t.attr("data-value", n),
    i = r.find("ul").find("[data-value=" + n + "]").html(),
    i && (t.attr("data-text", i), t.find(".ui-select-text").html(i).css("color", "#000"), r.find("ul").find("[data-value=" + n + "]").addClass("liactive")),
    t
};
$.fn.ComboBoxTree = function (n) {
    function e(i) {
        f.treeview({
            onnodeclick: function (i) {
                t.attr("data-value", i.id).attr("data-text", i.text);
                t.find(".ui-select-text").html(i.text).css("color", "#000");
                t.trigger("change");
                n.click && n.click(i)
            },
            height: n.height,
            url: i,
            param: n.param,
            method: n.method,
            description: n.description
        })
    }
    var t = $(this),
    r,
    u,
    i,
    f;
    return t.attr("id") ? (t.find(".ui-select-text").length == 0 && (r = "", r += "<div class=\"ui-select-text\"  style='color:#999;'>" + n.description + "<\/div>", r += '<div class="ui-select-option">', r += '<div class="ui-select-option-content" style="max-height: ' + n.height + '"><\/div>', n.allowSearch && (r += '<div class="ui-select-option-search"><input type="text" class="form-control" placeholder="搜索关键字" /><span class="input-query" title="Search"><i class="fa fa-search" title="按回车查询"><\/i><\/span><\/div>'), r += "<\/div>", t.append(r)), u = $($("<p>").append(t.find(".ui-select-option").clone()).html()), u.attr("id", t.attr("id") + "-option"), t.find(".ui-select-option").remove(), n.appendTo ? $(n.appendTo).prepend(u) : $("body").prepend(u), i = $("#" + t.attr("id") + "-option"), f = $("#" + t.attr("id") + "-option").find(".ui-select-option-content"), e(n.url), n.allowSearch && (i.find(".ui-select-option-search").find("input").attr("data-url", n.url), i.find(".ui-select-option-search").find("input").bind("keypress",
    function () {
        if (event.keyCode == "13") {
            var n = $(this).val(),
            t = changeUrlParam(i.find(".ui-select-option-search").find("input").attr("data-url"), "keyword", escape(n));
            e(t)
        }
    }).focus(function () {
        $(this).select()
    })), n.icon && (i.find("i").remove(), i.find("img").remove()), t.find(".ui-select-text").unbind("click"), t.find(".ui-select-text").bind("click",
    function (r) {
        var o;
        if (t.attr("readonly") == "readonly" || t.attr("disabled") == "disabled") return !1;
        if ($(this).parent().addClass("ui-select-focus"), i.is(":hidden")) {
            t.find(".ui-select-option").hide();
            $(".ui-select-option").hide();
            var e = t.offset().left,
            u = t.offset().top + 29,
            f = t.width();
            n.width && (f = n.width);
            i.height() + u < $(window).height() ? i.slideDown(150).css({
                top: u,
                left: e,
                width: f
            }) : (o = u - i.height() - 32, i.show().css({
                top: o,
                left: e,
                width: f
            }), i.attr("data-show", !0));
            i.css("border-top", "1px solid #ccc");
            n.appendTo && i.css("position", "inherit");
            i.find(".ui-select-option-search").find("input").select()
        } else i.attr("data-show") ? i.hide() : i.slideUp(150);
        r.stopPropagation()
    }), t.find("li div").click(function (n) {
        var n = n ? n : window.event,
        t = n.srcElement || n.target;
        $(t).hasClass("bbit-tree-ec-icon") || (i.slideUp(150), n.stopPropagation())
    }), $(document).click(function (n) {
        var n = n ? n : window.event,
        r = n.srcElement || n.target;
        $(r).hasClass("bbit-tree-ec-icon") || $(r).hasClass("form-control") || (i.attr("data-show") ? i.hide() : i.slideUp(150), t.removeClass("ui-select-focus"), n.stopPropagation())
    }), t) : !1
};
$.fn.ComboBoxTreeSetValue = function (n) {
    var t, r, i;
    if (n != "") return t = $(this),
    r = $("#" + t.attr("id") + "-option"),
    t.attr("data-value", n),
    i = r.find("ul").find("[data-value=" + n + "]").html(),
    i && (t.attr("data-text", i), t.find(".ui-select-text").html(i).css("color", "#000"), r.find("ul").find("[data-value=" + n + "]").parent().parent().addClass("bbit-tree-selected")),
    t
};
$.fn.GetWebControls = function (n) {
    var t = "";
    return $(this).find("input,select,textarea,.ui-select").each(function () {
        var i = $(this).attr("id"),
        r = $(this).attr("type"),
        n;
        switch (r) {
            case "checkbox":
                t += $("#" + i).is(":checked") ? '"' + i + '":"1",' : '"' + i + '":"0",';
                break;
            case "select":
                n = $("#" + i).attr("data-value");
                n == "" && (n = "&nbsp;");
                t += '"' + i + '":"' + $.trim(n) + '",';
                break;
            case "selectTree":
                n = $("#" + i).attr("data-value");
                n == "" && (n = "&nbsp;");
                t += '"' + i + '":"' + $.trim(n) + '",';
                break;
            default:
                n = $("#" + i).val();
                n == "" && (n = "&nbsp;");
                t += '"' + i + '":"' + $.trim(n) + '",'
        }
    }),
    t = t.substr(0, t.length - 1),
    n || (t = t.replace(/&nbsp;/g, "")),
    t = t.replace(/\\/g, "\\\\"),
    t = t.replace(/\n/g, "\\n"),
    jQuery.parseJSON("{" + t + "}")
};
$.fn.SetWebControls = function (n) {
    var f = $(this),
    r,
    t,
    u,
    i;
    for (r in n) if (t = f.find("#" + r), t.attr("id")) {
        u = t.attr("type");
        t.hasClass("input-datepicker") && (u = "datepicker");
        i = $.trim(n[r]).replace(/&nbsp;/g, "");
        switch (u) {
            case "checkbox":
                i == 1 ? t.attr("checked", "checked") : t.removeAttr("checked");
                break;
            case "select":
                t.ComboBoxSetValue(i);
                break;
            case "selectTree":
                t.ComboBoxTreeSetValue(i);
                break;
            case "datepicker":
                t.val(formatDate(i, "yyyy-MM-dd"));
                break;
            default:
                t.val(i)
        }
    }
};
$.fn.Contextmenu = function () {
    var e = $(this),
    t = $(".contextmenu");
    $(document).click(function () {
        t.hide()
    });
    $(document).mousedown(function (n) {
        3 == n.which && t.hide()
    });
    var o = t.find("ul"),
    i = t.find("li"),
    f = hideTimer = null,
    n = 0,
    r = maxHeight = 0,
    u = [document.documentElement.offsetWidth, document.documentElement.offsetHeight];
    for (t.hide(), n = 0; n < i.length; n++) i[n].getElementsByTagName("ul")[0] && (i[n].className = "sub"),
    i[n].onmouseover = function () {
        var i = this,
        t = i.getElementsByTagName("ul");
        i.className += " active";
        t[0] && (clearTimeout(hideTimer), f = setTimeout(function () {
            for (n = 0; n < i.parentNode.children.length; n++) i.parentNode.children[n].getElementsByTagName("ul")[0] && (i.parentNode.children[n].getElementsByTagName("ul")[0].style.display = "none");
            t[0].style.display = "block";
            t[0].style.top = i.offsetTop + "px";
            t[0].style.left = i.offsetWidth + "px";
            r = u[0] - t[0].offsetWidth;
            maxHeight = u[1] - t[0].offsetHeight;
            r < getOffset.left(t[0]) && (t[0].style.left = -t[0].clientWidth + "px");
            maxHeight < getOffset.top(t[0]) && (t[0].style.top = -t[0].clientHeight + i.offsetTop + i.clientHeight + "px")
        },
        300))
    },
    i[n].onmouseout = function () {
        var t = this,
        i = t.getElementsByTagName("ul");
        t.className = t.className.replace(/\s?active/, "");
        clearTimeout(f);
        hideTimer = setTimeout(function () {
            for (n = 0; n < t.parentNode.children.length; n++) t.parentNode.children[n].getElementsByTagName("ul")[0] && (t.parentNode.children[n].getElementsByTagName("ul")[0].style.display = "none")
        },
        300)
    };
    $(e).bind("contextmenu",
    function () {
        var n = n || window.event;
        return t.show(),
        t.css("top", n.clientY + "px"),
        t.css("left", n.clientX + "px"),
        r = u[0] - t.width(),
        maxHeight = u[1] - t.height(),
        t.offset().top > maxHeight && t.css("top", maxHeight + "px"),
        t.offset().left > r && t.css("left", r + "px"),
        !1
    }).bind("click",
    function () {
        t.hide()
    })
};
$.fn.panginationEx = function (n) {
    var t = $(this);
    if (!t.attr("id")) return !1;
    var n = $.extend({
        firstBtnText: "首页",
        lastBtnText: "尾页",
        prevBtnText: "上一页",
        nextBtnText: "下一页",
        showInfo: !0,
        showJump: !0,
        jumpBtnText: "跳转",
        showPageSizes: !0,
        infoFormat: "{start} ~ {end}条，共{total}条",
        sortname: "",
        url: "",
        success: null,
        beforeSend: null,
        complete: null
    },
    n),
    i = $.extend({
        sidx: n.sortname,
        sord: "asc"
    },
    n.params);
    n.remote = {
        url: n.url,
        params: i,
        beforeSend: function (t) {
            n.beforeSend != null && n.beforeSend(t)
        },
        success: function (t, i) {
            n.success != null && n.success(t.rows, i)
        },
        complete: function (t, i) {
            n.complete != null && n.complete(t, i)
        },
        pageIndexName: "page",
        pageSizeName: "rows",
        totalName: "records"
    };
    t.page(n)
};
$.fn.LeftListShowOfemail = function (n) {
    var t = $(this),
    i,
    n;
    if (!t.attr("id")) return !1;
    t.append('<ul  style="padding-top: 10px;"><\/ul>');
    i = {
        id: "id",
        name: "text",
        img: "fa fa-file-o"
    };
    n = $.extend(i, n);
    t.height(n.height);
    $.ajax({
        url: n.url,
        data: n.param,
        type: "GET",
        dataType: "json",
        async: !1,
        success: function (i) {
            $.each(i,
            function (i, r) {
                var u = $('<li class="" data-value="' + r[n.id] + '"  data-text="' + r[n.name] + '" ><i class="' + n.img + '" style="vertical-align: middle; margin-top: -2px; margin-right: 8px; font-size: 14px; color: #666666; opacity: 0.9;"><\/i>' + r[n.name] + "<\/li>");
                i == 0 && u.addClass("active");
                t.find("ul").append(u)
            });
            t.find("li").click(function () {
                var i = $(this).attr("data-value"),
                r = $(this).attr("data-text");
                t.find("li").removeClass("active");
                $(this).addClass("active");
                n.onnodeclick({
                    id: i,
                    name: r
                })
            })
        },
        error: function (n, t, i) {
            dialogMsg(i, -1)
        }
    })
};
$.fn.authorizeButton = function () {
    var n = $(this),
    i,
    t;
    n.find("a.btn").attr("authorize", "no");
    n.find("ul.dropdown-menu").find("li").attr("authorize", "no");
    i = tabiframeId().substr(6);
    t = top.authorizeButtonData[i];
    t != undefined && $.each(t,
    function (i) {
        n.find("#" + t[i].EnCode).attr("authorize", "yes")
    });
    n.find("[authorize=no]").remove()
};
$.fn.authorizeColModel = function () {
    var t = $(this),
    i = t.jqGrid("getGridParam", "colModel"),
    r,
    n;
    $.each(i,
    function (n) {
        i[n].name != "rn" && t.hideCol(i[n].name)
    });
    r = tabiframeId().substr(6);
    n = top.authorizeColumnData[r];
    n != undefined && $.each(n,
    function (i) {
        t.showCol(n[i].EnCode)
    })
};
$.fn.jqGridEx = function (n) {
    var t = $(this),
    i,
    r,
    n;
    if (!t.attr("id")) return !1;
    r = {
        url: "",
        datatype: "json",
        height: $(window).height() - 139.5,
        autowidth: !0,
        colModel: [],
        viewrecords: !0,
        rowNum: 30,
        rowList: [30, 50, 100],
        pager: "#gridPager",
        sortname: "CreateDate desc",
        rownumbers: !0,
        shrinkToFit: !1,
        gridview: !0,
        onSelectRow: function () {
            i = $("#" + this.id).getGridParam("selrow")
        },
        gridComplete: function () {
            $("#" + this.id).setSelection(i, !1)
        }
    };
    n = $.extend(r, n);
    t.jqGrid(n)
};
$.fn.jqGridRowValue = function (n) {
    var i = $(this),
    f = [],
    r = i.jqGrid("getGridParam", "selarrrow"),
    e,
    u,
    t;
    if (r != undefined && r != "") for (e = r.length, u = 0; u < e; u++) t = i.jqGrid("getRowData", r[u]),
    f.push(t[n]);
    else t = i.jqGrid("getRowData", i.jqGrid("getGridParam", "selrow")),
    f.push(t[n]);
    return String(f)
};
$.fn.jqGridRow = function () {
    var t = $(this),
    r = [],
    u = t.jqGrid("getGridParam", "selarrrow"),
    f,
    i,
    n;
    if (u != "") for (f = u.length, i = 0; i < f; i++) n = t.jqGrid("getRowData", u[i]),
    r.push(n);
    else n = t.jqGrid("getRowData", t.jqGrid("getGridParam", "selrow")),
    r.push(n);
    return r
};
dialogTop = function (n, t) {
    var i;
    $(".tip_container").remove();
    i = parseInt(Math.random() * 1e5);
    $("body").prepend('<div id="tip_container' + i + '" class="container tip_container"><div id="tip' + i + '" class="mtip"><i class="micon"><\/i><span id="tsc' + i + '"><\/span><i id="mclose' + i + '" class="mclose"><\/i><\/div><\/div>');
    var e = $(this),
    r = $("#tip_container" + i),
    u = $("#tip" + i),
    f = $("#tsc" + i);
    clearTimeout(window.timer);
    u.attr("class", t).addClass("mtip");
    f.html(n);
    r.slideDown(300);
    window.timer = setTimeout(function () {
        r.slideUp(300);
        $(".tip_container").remove()
    },
    4e3);
    $("#tip_container" + i).css("left", ($(window).width() - $("#tip_container" + i).width()) / 2)
};
dialogOpen = function (n) {
    Loading(!0);
    var n = $.extend({
        id: null,
        title: "系统窗口",
        width: "100px",
        height: "100px",
        url: "",
        shade: .3,
        btn: ["确认", "关闭"],
        callBack: null
    },
    n),
    t = n.url,
    i = top.$.windowWidth() > parseInt(n.width.replace("px", "")) ? n.width : top.$.windowWidth() + "px",
    r = top.$.windowHeight() > parseInt(n.height.replace("px", "")) ? n.height : top.$.windowHeight() + "px";
    top.layer.open({
        id: n.id,
        type: 2,
        shade: n.shade,
        title: n.title,
        fix: !1,
        area: [i, r],
        content: top.contentPath + t,
        btn: n.btn,
        yes: function () {
            n.callBack(n.id)
        },
        cancel: function () {
            return n.cancel != undefined && n.cancel(),
            !0
        }
    })
};
dialogContent = function (n) {
    var n = $.extend({
        id: null,
        title: "系统窗口",
        width: "100px",
        height: "100px",
        content: "",
        btn: ["确认", "关闭"],
        callBack: null
    },
    n);
    top.layer.open({
        id: n.id,
        type: 1,
        title: n.title,
        fix: !1,
        area: [n.width, n.height],
        content: n.content,
        btn: n.btn,
        yes: function () {
            n.callBack(n.id)
        }
    })
};
dialogAlert = function (n, t) {
    t == -1 && (t = 2);
    top.layer.alert(n, {
        icon: t,
        title: "聚久提示"
    })
};
dialogConfirm = function (n, t) {
    top.layer.confirm(n, {
        icon: 7,
        title: "聚久提示",
        btn: ["确认", "取消"]
    },
    function () {
        t(!0)
    },
    function () {
        t(!1)
    })
};
dialogMsg = function (n, t) {
    t == -1 && (t = 2);
    top.layer.msg(n, {
        icon: t,
        time: 4e3,
        shift: 5
    })
};
dialogClose = function () {
    try {
        var n = top.layer.getFrameIndex(window.name),
        t = top.$("#layui-layer" + n).find(".layui-layer-btn").find("#IsdialogClose"),
        i = t.is(":checked");
        t.length == 0 && (i = !0);
        i ? top.layer.close(n) : location.reload()
    } catch (r) {
        alert(r)
    }
};
reload = function () {
    return location.reload(),
    !1
};
newGuid = function () {
    for (var t = "",
    i, n = 1; n <= 32; n++) i = Math.floor(Math.random() * 16).toString(16),
    t += i,
    (n == 8 || n == 12 || n == 16 || n == 20) && (t += "-");
    return t
};
formatDate = function (n, t) {
    var i, r, u;
    if (!n) return "";
    i = n;
    typeof n == "string" && (i = n.indexOf("/Date(") > -1 ? new Date(parseInt(n.replace("/Date(", "").replace(")/", ""), 10)) : new Date(Date.parse(n.replace(/-/g, "/").replace("T", " ").split(".")[0])));
    r = {
        "M+": i.getMonth() + 1,
        "d+": i.getDate(),
        "h+": i.getHours(),
        "m+": i.getMinutes(),
        "s+": i.getSeconds(),
        "q+": Math.floor((i.getMonth() + 3) / 3),
        S: i.getMilliseconds()
    };
    /(y+)/.test(t) && (t = t.replace(RegExp.$1, (i.getFullYear() + "").substr(4 - RegExp.$1.length)));
    for (u in r) new RegExp("(" + u + ")").test(t) && (t = t.replace(RegExp.$1, RegExp.$1.length == 1 ? r[u] : ("00" + r[u]).substr(("" + r[u]).length)));
    return t
};
toDecimal = function (n) {
    n == null && (n = "0");
    n = n.toString().replace(/\$|\,/g, "");
    isNaN(n) && (n = "0");
    sign = n == (n = Math.abs(n));
    n = Math.floor(n * 100 + .50000000001);
    cents = n % 100;
    n = Math.floor(n / 100).toString();
    cents < 10 && (cents = "0" + cents);
    for (var t = 0; t < Math.floor((n.length - (1 + t)) / 3) ; t++) n = n.substring(0, n.length - (4 * t + 3)) + "" + n.substring(n.length - (4 * t + 3));
    return (sign ? "" : "-") + n + "." + cents
};
Date.prototype.DateAdd = function (n, t) {
    var i = this;
    switch (n) {
        case "s":
            return new Date(Date.parse(i) + 1e3 * t);
        case "n":
            return new Date(Date.parse(i) + 6e4 * t);
        case "h":
            return new Date(Date.parse(i) + 36e5 * t);
        case "d":
            return new Date(Date.parse(i) + 864e5 * t);
        case "w":
            return new Date(Date.parse(i) + 6048e5 * t);
        case "q":
            return new Date(i.getFullYear(), i.getMonth() + t * 3, i.getDate(), i.getHours(), i.getMinutes(), i.getSeconds());
        case "m":
            return new Date(i.getFullYear(), i.getMonth() + t, i.getDate(), i.getHours(), i.getMinutes(), i.getSeconds());
        case "y":
            return new Date(i.getFullYear() + t, i.getMonth(), i.getDate(), i.getHours(), i.getMinutes(), i.getSeconds())
    }
};
request = function (n) {
    for (var u = location.search.slice(1), r = u.split("&"), i, t = 0; t < r.length; t++) if (i = r[t].split("="), i[0] == n) return unescape(i[1]) == "undefined" ? "" : unescape(i[1]);
    return ""
};
changeUrlParam = function (url, key, value) {
    var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)"),
    tmp = key + "=" + value;
    return url.match(reg) != null ? url.replace(eval(reg), tmp) : url.match("[?]") ? url + "&" + tmp : url + "?" + tmp
};
$.currentIframe = function () {
    return top.frames[tabiframeId()].contentWindow != undefined && ($.isbrowsername() == "Chrome" || $.isbrowsername() == "FF") ? top.frames[tabiframeId()].contentWindow : top.frames[tabiframeId()]
};
$.isbrowsername = function () {
    var n = navigator.userAgent,
    t = n.indexOf("Opera") > -1;
    return t ? "Opera" : n.indexOf("Firefox") > -1 ? "FF" : n.indexOf("Chrome") > -1 ? window.navigator.webkitPersistentStorage.toString().indexOf("DeprecatedStorageQuota") > -1 ? "Chrome" : "360" : n.indexOf("Safari") > -1 ? "Safari" : n.indexOf("compatible") > -1 && n.indexOf("MSIE") > -1 && !t ? "IE" : void 0
};
$.download = function (n, t, i) {
    if (n && t) {
        t = typeof t == "string" ? t : jQuery.param(t);
        var r = "";
        $.each(t.split("&"),
        function () {
            var n = this.split("=");
            r += '<input type="hidden" name="' + n[0] + '" value="' + n[1] + '" />'
        });
        $('<form action="' + n + '" method="' + (i || "post") + '">' + r + "<\/form>").appendTo("body").submit().remove()
    }
};
$.standTabchange = function (n, t) {
    $(".standtabactived").removeClass("standtabactived");
    $(n).addClass("standtabactived");
    $(".standtab-pane").css("display", "none");
    $("#" + t).css("display", "block")
};
$.isNullOrEmpty = function (n) {
    return typeof n == "string" && n == "" || n == null || n == undefined ? !0 : !1
};
$.arrayClone = function (n) {
    return $.map(n,
    function (n) {
        return $.extend(!0, {},
        n)
    })
};
$.windowWidth = function () {
    return $(window).width()
};
$.windowHeight = function () {
    return $(window).height()
};
IsNumber = function (n) {
    $("#" + n).bind("contextmenu",
    function () {
        return !1
    });
    $("#" + n).css("ime-mode", "disabled");
    $("#" + n).keypress(function (n) {
        if (n.which != 8 && n.which != 0 && (n.which < 48 || n.which > 57)) return !1
    })
};
IsMoney = function (n) {
    function t(n) {
        return n >= 48 && n <= 57
    }
    function i(n) {
        return n == 8 || n == 46 || n >= 37 && n <= 40 || n == 35 || n == 36 || n == 9 || n == 13
    }
    function r(n) {
        return n == 190 || n == 110
    }
    $("#" + n).bind("contextmenu",
    function () {
        return !1
    });
    $("#" + n).css("ime-mode", "disabled");
    $("#" + n).bind("keydown",
    function (n) {
        var u = window.event ? n.keyCode : n.which;
        return r(u) ? $(this).val().indexOf(".") < 0 : i(u) || t(u) && !n.shiftKey
    })
};
checkedArray = function (n) {
    var t = !0;
    return (n == undefined || n == "" || n == "null" || n == "undefined") && (t = !1, dialogMsg("您没有选中任何项,请您选中后再操作。", 0)),
    t
};
checkedRow = function (n) {
    var t = !0;
    return n == undefined || n == "" || n == "null" || n == "undefined" ? (t = !1, dialogMsg("您没有选中任何数据项,请选中后再操作！", 0)) : n.split(",").length > 1 && (t = !1, dialogMsg("很抱歉,一次只能选择一条记录！", 0)),
    t
};
$.SaveForm = function (n) {
    var n = $.extend({
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        loading: "正在处理数据...",
        success: null,
        close: !0
    },
    n);
    Loading(!0, n.loading);
    $("[name=__RequestVerificationToken]").length > 0 && (n.param.__RequestVerificationToken = $("[name=__RequestVerificationToken]").val());
    window.setTimeout(function () {
        $.ajax({
            url: n.url,
            data: n.param,
            type: n.type,
            dataType: n.dataType,
            success: function (t) {
                t.type == "3" ? dialogAlert(t.message, -1) : (Loading(!1), dialogMsg(t.message, 1), n.success(t), n.close == !0 && dialogClose())
            },
            error: function (n, t, i) {
                Loading(!1);
                dialogMsg(i, -1)
            },
            beforeSend: function () {
                Loading(!0, n.loading)
            },
            complete: function () {
                Loading(!1)
            }
        })
    },
    500)
};
$.SetForm = function (n) {
    var n = $.extend({
        url: "",
        param: [],
        type: "get",
        dataType: "json",
        success: null,
        async: !1
    },
    n);
    $.ajax({
        url: n.url,
        data: n.param,
        type: n.type,
        dataType: n.dataType,
        async: n.async,
        success: function (t) {
            t != null && t.type == "3" ? dialogAlert(t.message, -1) : n.success(t)
        },
        error: function (n, t, i) {
            dialogMsg(i, -1)
        },
        beforeSend: function () {
            Loading(!0)
        },
        complete: function () {
            Loading(!1)
        }
    })
};
$.RemoveForm = function (n) {
    var n = $.extend({
        msg: "注：您确定要删除吗？该操作将无法恢复",
        loading: "正在删除数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    },
    n);
    dialogConfirm(n.msg,
    function (t) {
        t && (Loading(!0, n.loading), window.setTimeout(function () {
            var t = n.param;
            $("[name=__RequestVerificationToken]").length > 0 && (t.__RequestVerificationToken = $("[name=__RequestVerificationToken]").val());
            $.ajax({
                url: n.url,
                data: t,
                type: n.type,
                dataType: n.dataType,
                success: function (t) {
                    t.type == "3" ? dialogAlert(t.message, -1) : (dialogMsg(t.message, 1), n.success(t))
                },
                error: function (n, t, i) {
                    Loading(!1);
                    dialogMsg(i, -1)
                },
                beforeSend: function () {
                    Loading(!0, n.loading)
                },
                complete: function () {
                    Loading(!1)
                }
            })
        },
        500))
    })
};
$.ConfirmAjax = function (n) {
    var n = $.extend({
        msg: "提示信息",
        loading: "正在处理数据...",
        url: "",
        param: [],
        type: "post",
        dataType: "json",
        success: null
    },
    n);
    dialogConfirm(n.msg,
    function (t) {
        t && (Loading(!0, n.loading), window.setTimeout(function () {
            var t = n.param;
            $("[name=__RequestVerificationToken]").length > 0 && (t.__RequestVerificationToken = $("[name=__RequestVerificationToken]").val());
            $.ajax({
                url: n.url,
                data: t,
                type: n.type,
                dataType: n.dataType,
                success: function (t) {
                    Loading(!1);
                    t.type == "3" ? dialogAlert(t.message, -1) : (dialogMsg(t.message, 1), n.success(t))
                },
                error: function (n, t, i) {
                    Loading(!1);
                    dialogMsg(i, -1)
                },
                beforeSend: function () {
                    Loading(!0, n.loading)
                },
                complete: function () {
                    Loading(!1)
                }
            })
        },
        200))
    })
};
$.ExistField = function (n, t, i) {
    var r = $("#" + n),
    u,
    f;
    if (!r.val()) return !1;
    u = {
        keyValue: request("keyValue")
    };
    u[n] = r.val();
    f = $.extend(u, i);
    $.ajax({
        url: t,
        data: f,
        type: "get",
        dataType: "text",
        async: !1,
        success: function (n) {
            n.toLocaleLowerCase() == "false" ? (ValidationMessage(r, "已存在,请重新输入"), r.attr("fieldexist", "yes")) : r.attr("fieldexist", "no")
        },
        error: function (n, t, i) {
            dialogMsg(i, -1)
        }
    })
}