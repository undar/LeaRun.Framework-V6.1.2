function IndexOut() {
    learun.dialogConfirm({
        msg: "注：您确定要安全退出本次登录吗？",
        callBack: function(n) {
            n && (learun.loading({
                isShow: !0,
                text: "正在安全退出..."
            }), window.setTimeout(function() {
                $.ajax({
                    url: contentPath + "/Login/OutLogin",
                    type: "post",
                    dataType: "json",
                    success: function() {
                        window.location.href = contentPath + "/Login/Index"
                    }
                })
            },
            500))
        }
    })
} (function(n, t) {
    "use strict";
    function r(n, t) {
        var r, u, i;
        try {
            if (r = "", u = t.length, u == undefined) r = t[n];
            else for (i = 0; i < u; i++) if (n(t[i])) {
                r = t[i];
                break
            }
            return r
        } catch(f) {
            return console.log(f.message),
            ""
        }
    }
    function u() {
        n.each(i.excelImportTemplate,
        function(n, t) {
            i.excelImportTemplate[n] = {
                keys: t
            }
        })
    }
    var i = {};
    t.data = {
        init: function(t) {
            n.ajax({
                url: contentPath + "/ClientData/GetClientDataJson",
                type: "post",
                dataType: "json",
                async: !0,
                success: function(r) {
                    i = r;
                    u();
                    t();
                    window.setTimeout(function() {
                        n("#ajax-loader").fadeOut()
                    },
                    50)
                }
            })
        },
        get: function(n) {
            var u;
            if (!n) return "";
            var e = n.length,
            t = "",
            f = i;
            for (u = 0; u < e; u++) if (t = r(n[u], f), t != "" && t != undefined) f = t;
            else break;
            return (t == undefined || t == null) && (t = ""),
            t
        }
    }
})(window.jQuery, window.learun);
var tablist = {
    newTab: function(n) {
        var i = n.id,
        t = n.url,
        e = '<i class="' + n.icon + '"><\/i>' + n.title,
        r = !0,
        u, f;
        if (t == undefined || $.trim(t).length == 0) return ! 1;
        $(".menuTab").each(function() {
            if ($(this).data("id") == t) return $(this).hasClass("active") || ($(this).addClass("active").siblings(".menuTab").removeClass("active"), $.learuntab.scrollToTab(this), $("#mainContent .LRADMS_iframe").each(function() {
                if ($(this).data("id") == t) return $(this).show().siblings(".LRADMS_iframe").hide(),
                !1
            })),
            r = !1,
            !1
        });
        r && (u = '<a href="javascript:;" class="active menuTab" data-id="' + t + '">' + e + ' <div class="tab_close "><\/div><\/a>', $(".menuTab").removeClass("active"), f = '<iframe class="LRADMS_iframe" id="iframe' + i + '" name="iframe' + i + '"  width="100%" height="100%" src="' + t + '" frameborder="0" data-id="' + t + '" seamless><\/iframe>', $("#mainContent").find("iframe.LRADMS_iframe").hide(), $("#mainContent").append(f), learun.loading({
            isShow: !0
        }), $("#mainContent iframe:visible").load(function() {
            learun.loading({
                isShow: !1
            })
        }), $(".menuTabs .page-tabs-content").append(u), $.learuntab.scrollToTab($(".menuTab.active")))
    }
}; (function(n) {
    "use strict";
    n.learuntab = {
        requestFullScreen: function() {
            var n = document.documentElement;
            n.requestFullscreen ? n.requestFullscreen() : n.mozRequestFullScreen ? n.mozRequestFullScreen() : n.webkitRequestFullScreen && n.webkitRequestFullScreen()
        },
        exitFullscreen: function() {
            var n = document;
            n.exitFullscreen ? n.exitFullscreen() : n.mozCancelFullScreen ? n.mozCancelFullScreen() : n.webkitCancelFullScreen && n.webkitCancelFullScreen()
        },
        refreshTab: function() {
            var i = n(".page-tabs-content").find(".active").attr("data-id"),
            t = n('.LRADMS_iframe[data-id="' + i + '"]'),
            r = t.attr("src");
            learun.loading({
                isShow: !0
            });
            t.attr("src", r).load(function() {
                learun.loading({
                    isShow: !1
                })
            })
        },
        activeTab: function() {
            var i = n(this).data("id"),
            t;
            n(this).hasClass("active") || (n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == i) return n(this).show().siblings(".LRADMS_iframe").hide(),
                !1
            }), n(this).addClass("active").siblings(".menuTab").removeClass("active"), n.learuntab.scrollToTab(this));
            t = n(this).attr("data-value");
            t != "" && top.$.cookie("currentmoduleId", t, {
                path: "/"
            })
        },
        closeOtherTabs: function() {
            n(".page-tabs-content").children("[data-id]").find(".tab_close").parents("a").not(".active").each(function() {
                n('.LRADMS_iframe[data-id="' + n(this).data("id") + '"]').remove();
                n(this).remove()
            });
            n(".page-tabs-content").css("margin-left", "0")
        },
        closeTab: function() {
            var i = n(this).parents(".menuTab").data("id"),
            f = n(this).parents(".menuTab").width(),
            r,
            t,
            u;
            return n(this).parents(".menuTab").hasClass("active") ? (n(this).parents(".menuTab").next(".menuTab").size() && (t = n(this).parents(".menuTab").next(".menuTab:eq(0)").data("id"), n(this).parents(".menuTab").next(".menuTab:eq(0)").addClass("active"), n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == t) return n(this).show().siblings(".LRADMS_iframe").hide(),
                !1
            }), r = parseInt(n(".page-tabs-content").css("margin-left")), r < 0 && n(".page-tabs-content").animate({
                marginLeft: r + f + "px"
            },
            "fast"), n(this).parents(".menuTab").remove(), n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == i) return n(this).remove(),
                !1
            })), n(this).parents(".menuTab").prev(".menuTab").size() && (t = n(this).parents(".menuTab").prev(".menuTab:last").data("id"), n(this).parents(".menuTab").prev(".menuTab:last").addClass("active"), n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == t) return n(this).show().siblings(".LRADMS_iframe").hide(),
                !1
            }), n(this).parents(".menuTab").remove(), n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == i) return n(this).remove(),
                !1
            }))) : (n(this).parents(".menuTab").remove(), n("#mainContent .LRADMS_iframe").each(function() {
                if (n(this).data("id") == i) return n(this).remove(),
                !1
            }), n.learuntab.scrollToTab(n(".menuTab.active"))),
            u = n(".menuTab.active").attr("data-value"),
            u != "" && top.$.cookie("currentmoduleId", u, {
                path: "/"
            }),
            !1
        },
        addTab: function() {
            var i = n(this).attr("data-id"),
            t,
            u,
            r,
            f,
            e;
            return (i != "" && top.$.cookie("currentmoduleId", i, {
                path: "/"
            }), t = n(this).attr("href"), u = n.trim(n(this).html()), t == undefined || n.trim(t).length == 0) ? !1 : (r = !0, n(".menuTab").each(function() {
                if (n(this).data("id") == t) return n(this).hasClass("active") || (n(this).addClass("active").siblings(".menuTab").removeClass("active"), n.learuntab.scrollToTab(this), n("#mainContent .LRADMS_iframe").each(function() {
                    if (n(this).data("id") == t) return n(this).show().siblings(".LRADMS_iframe").hide(),
                    !1
                })),
                r = !1,
                !1
            }), r && (f = '<a href="javascript:;" class="active menuTab" data-value=' + i + ' data-id="' + t + '">' + u + '<div class="tab_close "><\/div><\/a>', n(".menuTab").removeClass("active"), e = '<iframe class="LRADMS_iframe" id="iframe' + i + '" name="iframe' + i + '"  width="100%" height="100%" src="' + t + '" frameborder="0" data-id="' + t + '" seamless><\/iframe>', n("#mainContent").find("iframe.LRADMS_iframe").hide(), n("#mainContent").append(e), learun.loading({
                isShow: !0
            }), n("#mainContent iframe:visible").load(function() {
                learun.loading({
                    isShow: !1
                })
            }), n(".menuTabs .page-tabs-content").append(f), n.learuntab.scrollToTab(n(".menuTab.active"))), n(this).parents(".popover-moreMenu").hide(), n(this).parents(".popover-menu").hide(), n(this).parents(".popover-menu-sub").hide(), !1)
        },
        scrollTabRight: function() {
            var f = Math.abs(parseInt(n(".page-tabs-content").css("margin-left"))),
            e = n.learuntab.calSumWidth(n(".lea-tabs").children().not(".menuTabs")),
            u = n(".lea-tabs").outerWidth(!0) - e,
            r = 0,
            t,
            i;
            if (n(".page-tabs-content").width() < u) return ! 1;
            for (t = n(".menuTab:first"), i = 0; i + n(t).outerWidth(!0) <= f;) i += n(t).outerWidth(!0),
            t = n(t).next();
            for (i = 0; i + n(t).outerWidth(!0) < u && t.length > 0;) i += n(t).outerWidth(!0),
            t = n(t).next();
            r = n.learuntab.calSumWidth(n(t).prevAll());
            r > 0 && n(".page-tabs-content").animate({
                marginLeft: 0 - r + "px"
            },
            "fast")
        },
        scrollTabLeft: function() {
            var f = Math.abs(parseInt(n(".page-tabs-content").css("margin-left"))),
            e = n.learuntab.calSumWidth(n(".lea-tabs").children().not(".menuTabs")),
            r = n(".lea-tabs").outerWidth(!0) - e,
            u = 0,
            t,
            i;
            if (n(".page-tabs-content").width() < r) return ! 1;
            for (t = n(".menuTab:first"), i = 0; i + n(t).outerWidth(!0) <= f;) i += n(t).outerWidth(!0),
            t = n(t).next();
            if (i = 0, n.learuntab.calSumWidth(n(t).prevAll()) > r) {
                while (i + n(t).outerWidth(!0) < r && t.length > 0) i += n(t).outerWidth(!0),
                t = n(t).prev();
                u = n.learuntab.calSumWidth(n(t).prevAll())
            }
            n(".page-tabs-content").animate({
                marginLeft: 0 - u + "px"
            },
            "fast")
        },
        scrollToTab: function(t) {
            var f = n.learuntab.calSumWidth(n(t).prevAll()),
            e = n.learuntab.calSumWidth(n(t).nextAll()),
            o = n.learuntab.calSumWidth(n(".lea-tabs").children().not(".menuTabs")),
            r = n(".lea-tabs").outerWidth(!0) - o,
            i = 0,
            u;
            if (n(".page-tabs-content").outerWidth() < r) i = 0;
            else if (e <= r - n(t).outerWidth(!0) - n(t).next().outerWidth(!0)) {
                if (r - n(t).next().outerWidth(!0) > e) for (i = f, u = t; i - n(u).outerWidth() > n(".page-tabs-content").outerWidth() - r;) i -= n(u).prev().outerWidth(),
                u = n(u).prev()
            } else f > r - n(t).outerWidth(!0) - n(t).prev().outerWidth(!0) && (i = f - n(t).prev().outerWidth(!0));
            n(".page-tabs-content").animate({
                marginLeft: 0 - i + "px"
            },
            "fast")
        },
        calSumWidth: function(t) {
            var i = 0;
            return n(t).each(function() {
                i += n(this).outerWidth(!0)
            }),
            i
        },
        init: function() {
            n(".menuTabs").on("click", ".menuTab .tab_close", n.learuntab.closeTab);
            n(".menuTabs").on("click", ".menuTab", n.learuntab.activeTab);
            n(".tabLeft").on("click", n.learuntab.scrollTabLeft);
            n(".tabRight").on("click", n.learuntab.scrollTabRight);
            n(".tabReload").on("click", n.learuntab.refreshTab);
            n(".tabCloseCurrent").on("click",
            function() {
                n(".page-tabs-content").find(".active .tab_close").trigger("click")
            });
            n(".tabCloseAll").on("click",
            function() {
                n(".page-tabs-content").children("[data-id]").find(".tab_close").each(function() {
                    n(this).parents("a").remove()
                });
                n(".page-tabs-content").children("[data-id]:first").each(function() {
                    n('.LRADMS_iframe[data-id="' + n(this).data("id") + '"]').show();
                    n(this).addClass("active")
                });
                n(".page-tabs-content").css("margin-left", "0")
            });
            n(".tabCloseOther").on("click", n.learuntab.closeOtherTabs);
            n(".fullscreen").on("click",
            function() {
                n(this).attr("fullscreen") ? (n(this).removeAttr("fullscreen"), n.learuntab.exitFullscreen()) : (n(this).attr("fullscreen", "true"), n.learuntab.requestFullScreen())
            })
        }
    };
    n.learunindex = {
        load: function() {
            n("#mainContent").height(n(window).height() - 128);
            n(window).resize(function() {
                n("#mainContent").height(n(window).height() - 128);
                n.learunindex.loadMenu(!0)
            });
            n("#UserSetting").click(function() {
                tablist.newTab({
                    id: "UserSetting",
                    title: "个人中心",
                    closed: !0,
                    icon: "fa fa fa-user",
                    url: contentPath + "/PersonCenter/Index"
                })
            })
        },
        jsonWhere: function(t, i) {
            if (i != null) {
                var r = [];
                return n(t).each(function(n, t) {
                    i(t) && r.push(t)
                }),
                r
            }
        },
        loadMenu: function(t) {
            var e = !1,
            o = n(".lea-Head").width() - n.learuntab.calSumWidth(n(".lea-Head").children().not(".left-bar")),
            i = top.learun.data.get(["authorizeMenu"]),
            r = "",
            u = 0,
            s = "",
            f = "";
            if (n.each(i,
            function(t) {
                var h = i[t],
                e,
                c;
                h.F_ParentId == "0" && (e = "", e += '<li class="treeview">', e += "<a>", e += '<i class="' + h.F_Icon + '"><\/i><span>' + h.F_FullName + "<\/span>", e += "<\/a>", c = n.learunindex.jsonWhere(i,
                function(n) {
                    return n.F_ParentId == h.F_ModuleId
                }), c.length > 0 && (e += '<div class="popover-menu"><div class="arrow"><em><\/em><span><\/span><\/div><ul class="treeview-menu">', n.each(c,
                function(t) {
                    var r = c[t],
                    u = n.learunindex.jsonWhere(i,
                    function(n) {
                        return n.F_ParentId == r.F_ModuleId
                    });
                    e += "<li>";
                    u.length > 0 ? (e += '<a class="menuTreeItem menuItem" href="#"><i class="' + r.F_Icon + ' firstIcon"><\/i>' + r.F_FullName + "", e += '<i class="fa fa-angle-right pull-right"><\/i><\/a>', e += '<div class="popover-menu-sub"><ul class="treeview-menu">', n.each(u,
                    function(n) {
                        var t = u[n];
                        e += '<li><a class="menuItem menuiframe" data-id="' + t.F_ModuleId + '" href="' + t.F_UrlAddress + '"><i class="' + t.F_Icon + ' firstIcon"><\/i>' + t.F_FullName + "<\/a><\/li>"
                    }), e += "<\/ul><\/div>") : e += '<a class="menuItem menuiframe" data-id="' + r.F_ModuleId + '" href="' + r.F_UrlAddress + '"><i class="' + r.F_Icon + ' firstIcon"><\/i>' + r.F_FullName + "<\/a>";
                    e += "<\/li>"
                }), e += "<\/ul><\/div>"), e += "<\/li>", u += 88, u > o ? f += e: u + 88 > o ? s = e: r += e)
            }), u > o ? (f = s + f, r += ' <li class="treeview" id="moreMenu"><a ><i class="fa fa-reorder"><\/i><span>更多应用<\/span><\/a><\/li>', e = !0) : r += s, t || e) {
                if (n("#top-menu").html(r), e) {
                    n("#moreMenu").append('<div class="popover-moreMenu"><div class="arrow"><em><\/em><span><\/span><\/div><div class="title">更多应用<\/div><div class="moresubmenu"><\/div><\/div>');
                    n(".moresubmenu").html(f);
                    n(".moresubmenu > .treeview > a").unbind();
                    n(".moresubmenu > .treeview > a").on("click",
                    function() {
                        n(".moresubmenu > .treeview > a.active").parent().find(".popover-menu").hide();
                        n(".moresubmenu > .treeview > a.active").removeClass("active");
                        var t = n(this);
                        t.addClass("active");
                        t.parent().find(".popover-menu").show()
                    })
                }
                n("#top-menu>.treeview").unbind();
                n(".popover-menu>ul>li").unbind();
                n("#top-menu>.treeview").hover(function() {
                    var t = n(this),
                    i = t.find(".popover-moreMenu"),
                    r;
                    t.addClass("active");
                    i.length > 0 ? (i.slideDown(150), n(i.find(".treeview>a")[0]).trigger("click")) : (r = t.find(".popover-menu"), r.slideDown(150))
                },
                function() {
                    var t = n(this),
                    r = t.find(".popover-menu"),
                    i = t.find(".popover-moreMenu");
                    i.length == 0 ? r.slideUp(50) : i.hide();
                    t.removeClass("active")
                });
                n(".popover-menu>ul>li").hover(function() {
                    var t = n(this),
                    f;
                    if (t.parents(".moresubmenu").length == 0) {
                        var e = n(window).width(),
                        r = n(window).height(),
                        i = t.find(".popover-menu-sub"),
                        u = i.height();
                        e - t.offset().left - 154 < 152 && i.css("left", "-156px");
                        u - 10 + t.offset().top > r && (f = u - 10 + t.offset().top - r + 46, i.css("margin-top", "-" + f + "px"));
                        t.addClass("active");
                        i.slideDown(150)
                    }
                },
                function() {
                    var t = n(this),
                    i;
                    t.parents(".moresubmenu").length == 0 && (i = t.find(".popover-menu-sub"), t.removeClass("active"), i.css("margin-top", "-46px"), i.slideUp(50))
                });
                n(".menuiframe").unbind();
                n(".menuiframe").on("click", n.learuntab.addTab);
                n(".moresubmenu .menuTreeItem ").unbind();
                n(".menuTreeItem ").on("click",
                function() {
                    var t, i;
                    n(".moresubmenu .popover-menu-sub").slideUp(300);
                    t = n(this);
                    t.hasClass("active") ? t.removeClass("active") : (i = n(this).parent().find(".popover-menu-sub"), t.addClass("active"), i.slideDown(300))
                })
            }
        },
        indexOut: function() {
            dialogConfirm("注：您确定要安全退出本次登录吗？",
            function(t) {
                t && (learun.loading({
                    isShow: !0,
                    text: "正在安全退出..."
                }), window.setTimeout(function() {
                    n.ajax({
                        url: contentPath + "/Login/OutLogin",
                        type: "post",
                        dataType: "json",
                        success: function() {
                            window.location.href = contentPath + "/Login/Index"
                        }
                    })
                },
                500))
            })
        }
    };
    n(function() {
        learun.init({
            callBack: function() {
                n.learunindex.loadMenu(!0);
                n.learuntab.init();
                n.learunindex.load()
            },
            themeType: "4"
        })
    })
})(window.jQuery)