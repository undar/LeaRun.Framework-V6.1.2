window.learun = {},
function(n, t) {
    "use strict";
    n.extend(t, {
        init: function(n) {
            t.theme.type = n.themeType;
            t.data.init(n.callBack)
        },
        childInit: function() {
            n(".toolbar").authorizeButton();
            t.theme.setType();
            t.excel != undefined && t.excel.init();
            t.ajaxLoading(!1)
        },
        theme: {
            type: "1",
            setType: function() {
                switch (top.learun.theme.type) {
                case "1":
                    n("body").addClass("uiDefault");
                    break;
                case "2":
                    n("body").addClass("uiLTE");
                    break;
                case "3":
                    n("body").addClass("uiWindows");
                    break;
                case "4":
                    n("body").addClass("uiPretty")
                }
            }
        },
        loading: function(n) {
            var t = top.$("#loading_background,#loading_manage");
            n.isShow ? t.show() : top.$("#loading_manage").attr("istableloading") == undefined && (t.hide(), top.$(".ajax-loader").remove());
            n.text ? top.$("#loading_manage").html(n.text) : top.$("#loading_manage").html("正在拼了命为您加载…");
            top.$("#loading_manage").css("left", (top.$("body").width() - top.$("#loading_manage").width()) / 2 - 54);
            top.$("#loading_manage").css("top", (top.$("body").height() - top.$("#loading_manage").height()) / 2)
        },
        ajaxLoading: function(t) {
            var i = n("#ajaxLoader");
            t ? i.show() : i.fadeOut()
        },
        tabiframeId: function() {
            return top.$(".LRADMS_iframe:visible").attr("id")
        },
        currentIframe: function() {
            return top.frames[t.tabiframeId()].contentWindow != undefined ? top.frames[t.tabiframeId()].contentWindow: top.frames[t.tabiframeId()]
        },
        getIframe: function(n) {
            var t = frames[n];
            return t != undefined ? t.contentWindow != undefined ? t.contentWindow: t: null
        },
        reload: function() {
            return location.reload(),
            !1
        },
        dialogTop: function(t) {
            var i;
            n(".tip_container").remove();
            i = parseInt(Math.random() * 1e5);
            n("body").prepend('<div id="tip_container' + i + '" class="container tip_container"><div id="tip' + i + '" class="mtip"><i class="micon"><\/i><span id="tsc' + i + '"><\/span><i id="mclose' + i + '" class="mclose"><\/i><\/div><\/div>');
            var e = n(this),
            r = n("#tip_container" + i),
            u = n("#tip" + i),
            f = n("#tsc" + i);
            clearTimeout(window.timer);
            u.attr("class", t.type).addClass("mtip");
            f.html(t.msg);
            r.slideDown(300);
            window.timer = setTimeout(function() {
                r.slideUp(300);
                n(".tip_container").remove()
            },
            4e3);
            n("#tip_container" + i).css("left", (n(window).width() - n("#tip_container" + i).width()) / 2)
        },
        dialogOpen: function(i) {
            t.loading({
                isShow: !0
            });
            var i = n.extend({
                id: null,
                title: "系统窗口",
                width: "100px",
                height: "100px",
                url: "",
                shade: .3,
                btn: ["确认", "关闭"],
                callBack: null
            },
            i),
            r = i.url,
            u = top.$.windowWidth() > parseInt(i.width.replace("px", "")) ? i.width : top.$.windowWidth() + "px",
            f = top.$.windowHeight() > parseInt(i.height.replace("px", "")) ? i.height : top.$.windowHeight() + "px";
            top.layer.open({
                id: i.id,
                type: 2,
                shade: i.shade,
                title: i.title,
                fix: !1,
                area: [u, f],
                content: top.contentPath + r,
                btn: i.btn,
                success: function() {
                    t.loading({
                        isShow: !1
                    })
                },
                yes: function() {
                    i.callBack(i.id)
                },
                cancel: function() {
                    return i.cancel != undefined && i.cancel(),
                    !0
                }
            })
        },
        dialogContent: function(i) {
            var i = n.extend({
                id: null,
                title: "系统窗口",
                width: "100px",
                height: "100px",
                content: "",
                btn: ["确认", "关闭"],
                callBack: null
            },
            i);
            top.layer.open({
                id: i.id,
                type: 1,
                title: i.title,
                fix: !1,
                area: [i.width, i.height],
                success: function() {
                    t.loading({
                        isShow: !1
                    })
                },
                content: i.content,
                btn: i.btn,
                yes: function() {
                    i.callBack(i.id)
                }
            })
        },
        dialogAlert: function(n) {
            n.type == -1 && (n.type = 2);
            top.layer.alert(n.msg, {
                icon: n.type,
                title: "聚久提示",
                success: function() {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        dialogConfirm: function(n) {
            top.layer.confirm(n.msg, {
                icon: 7,
                title: "聚久提示",
                btn: ["确认", "取消"],
                success: function() {
                    t.loading({
                        isShow: !1
                    })
                }
            },
            function() {
                n.callBack(!0)
            },
            function() {
                n.callBack(!1)
            })
        },
        dialogMsg: function(n) {
            n.type == -1 && (n.type = 2);
            top.layer.msg(n.msg, {
                icon: n.type,
                time: 4e3,
                shift: 5
            })
        },
        dialogClose: function() {
            try {
                var n = top.layer.getFrameIndex(window.name),
                t = top.$("#layui-layer" + n).find(".layui-layer-btn").find("#IsdialogClose"),
                i = t.is(":checked");
                t.length == 0 && (i = !0);
                i ? top.layer.close(n) : location.reload()
            } catch(r) {
                alert(r)
            }
        },
        downFile: function(t) {
            if (t.url && t.data) {
                t.data = typeof t.data == "string" ? t.data: jQuery.param(t.data);
                var i = "";
                n.each(t.data.split("&"),
                function() {
                    var n = this.split("=");
                    i += '<input type="hidden" name="' + n[0] + '" value="' + n[1] + '" />'
                });
                n('<form action="' + t.url + '" method="' + (t.method || "post") + '">' + i + "<\/form>").appendTo("body").submit().remove()
            }
        },
        request: function(n) {
            for (var u = location.search.slice(1), r = u.split("&"), i, t = 0; t < r.length; t++) if (i = r[t].split("="), i[0] == n) return unescape(i[1]) == "undefined" ? "": unescape(i[1]);
            return ""
        },
        changeUrlParam: function(url, key, value) {
            var reg = new RegExp("(^|)" + key + "=([^&]*)(|$)"),
            tmp = key + "=" + value;
            return url.match(reg) != null ? url.replace(eval(reg), tmp) : url.match("[?]") ? url + "&" + tmp: url + "?" + tmp
        },
        getBrowserName: function() {
            var n = navigator.userAgent,
            t = n.indexOf("Opera") > -1;
            return t ? "Opera": n.indexOf("Firefox") > -1 ? "FF": n.indexOf("Chrome") > -1 ? window.navigator.webkitPersistentStorage == undefined ? "Edge": window.navigator.webkitPersistentStorage.toString().indexOf("DeprecatedStorageQuota") > -1 ? "Chrome": "360": n.indexOf("Safari") > -1 ? "Safari": n.indexOf("compatible") > -1 && n.indexOf("MSIE") > -1 && !t ? "IE": void 0
        },
        changeStandTab: function(t) {
            n(".standtabactived").removeClass("standtabactived");
            n(t.obj).addClass("standtabactived");
            n(".standtab-pane").css("display", "none");
            n("#" + t.id).css("display", "block")
        },
        windowWidth: function() {
            return n(window).width()
        },
        windowHeight: function() {
            return n(window).height()
        },
        ajax: {
            asyncGet: function(t) {
                var i = null,
                t = n.extend({
                    type: "GET",
                    dataType: "json",
                    async: !1,
                    cache: !1,
                    success: function(n) {
                        i = n
                    }
                },
                t);
                return n.ajax(t),
                i
            }
        },
        createGuid: function() {
            for (var t = "",
            i, n = 1; n <= 32; n++) i = Math.floor(Math.random() * 16).toString(16),
            t += i,
            (n == 8 || n == 12 || n == 16 || n == 20) && (t += "-");
            return t
        },
        isNullOrEmpty: function(n) {
            return typeof n == "string" && n == "" || n == null || n == undefined ? !0 : !1
        },
        isNumber: function(t) {
            n("#" + t).bind("contextmenu",
            function() {
                return ! 1
            });
            n("#" + t).css("ime-mode", "disabled");
            n("#" + t).keypress(function(n) {
                if (n.which != 8 && n.which != 0 && (n.which < 48 || n.which > 57)) return ! 1
            })
        },
        isMoney: function(t) {
            function i(n) {
                return n >= 48 && n <= 57
            }
            function r(n) {
                return n == 8 || n == 46 || n >= 37 && n <= 40 || n == 35 || n == 36 || n == 9 || n == 13
            }
            function u(n) {
                return n == 190 || n == 110
            }
            n("#" + t).bind("contextmenu",
            function() {
                return ! 1
            });
            n("#" + t).css("ime-mode", "disabled");
            n("#" + t).bind("keydown",
            function(t) {
                var f = window.event ? t.keyCode: t.which;
                return u(f) ? n(this).val().indexOf(".") < 0 : r(f) || i(f) && !t.shiftKey
            })
        },
        isHasImg: function(n) {
            var t = new Image;
            return t.src = n,
            t.fileSize > 0 || t.width > 0 && t.height > 0 ? !0 : !1
        },
        formatDate: function(n, t) {
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
        },
        toDecimal: function(n) {
            var r, i, t;
            for (n == null && (n = "0"), n = n.toString().replace(/\$|\,/g, ""), isNaN(n) && (n = "0"), r = n == (n = Math.abs(n)), n = Math.floor(n * 100 + .50000000001), i = n % 100, n = Math.floor(n / 100).toString(), i < 10 && (i = "0" + i), t = 0; t < Math.floor((n.length - (1 + t)) / 3); t++) n = n.substring(0, n.length - (4 * t + 3)) + "" + n.substring(n.length - (4 * t + 3));
            return (r ? "": "-") + n + "." + i
        },
        countFileSize: function(n) {
            return n < 1024 ? t.toDecimal(n) + " 字节": n >= 1024 && n < 1048576 ? t.toDecimal(n / 1024) + " KB": n >= 1048576 && n < 1073741824 ? t.toDecimal(n / 1048576) + " MB": n >= 1073741824 ? t.toDecimal(n / 1073741824) + " GB": void 0
        },
        arrayCopy: function(t) {
            return n.map(t,
            function(t) {
                return n.extend(!0, {},
                t)
            })
        },
        stringArray: function(n, t) {
            var i = n.split(",");
            return i.splice(i.indexOf(t), 1),
            String(i)
        },
        checkedRow: function(n) {
            var i = !0;
            return n == undefined || n == "" || n == "null" || n == "undefined" ? (i = !1, t.dialogMsg({
                msg: "您没有选中任何数据项,请选中后再操作！",
                type: 0
            })) : n.split(",").length > 1 && (i = !1, t.dialogMsg({
                msg: "很抱歉,一次只能选择一条记录！",
                type: 0
            })),
            i
        },
        saveForm: function(i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在处理数据...",
                success: null,
                close: !0
            },
            i);
            t.loading({
                isShow: !0,
                text: i.loading
            });
            n("[name=__RequestVerificationToken]").length > 0 && (i.param.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
            window.setTimeout(function() {
                n.ajax({
                    url: i.url,
                    data: i.param,
                    type: i.type,
                    dataType: i.dataType,
                    success: function(n) {
                        n.type == "3" ? t.dialogAlert({
                            msg: n.message,
                            type: -1
                        }) : (t.loading({
                            isShow: !1
                        }), t.dialogMsg({
                            msg: n.message,
                            type: 1
                        }), i.success(n), i.close == !0 && t.dialogClose())
                    },
                    error: function(n, i, r) {
                        t.loading({
                            isShow: !1
                        });
                        t.dialogMsg({
                            msg: r,
                            type: -1
                        })
                    },
                    beforeSend: function() {
                        t.loading({
                            isShow: !0,
                            text: i.loading
                        })
                    },
                    complete: function() {
                        t.loading({
                            isShow: !1
                        })
                    }
                })
            },
            500)
        },
        setForm: function(i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "get",
                dataType: "json",
                success: null,
                async: !1,
                cache: !1
            },
            i);
            n.ajax({
                url: i.url,
                data: i.param,
                type: i.type,
                dataType: i.dataType,
                async: i.async,
                success: function(n) {
                    n != null && n.type == "3" ? t.dialogAlert({
                        msg: n.message,
                        type: -1
                    }) : i.success(n)
                },
                error: function(n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                },
                beforeSend: function() {
                    t.loading({
                        isShow: !0
                    })
                },
                complete: function() {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        removeForm: function(i) {
            var i = n.extend({
                msg: "注：您确定要删除吗？该操作将无法恢复",
                loading: "正在删除数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            },
            i);
            t.dialogConfirm({
                msg: i.msg,
                callBack: function(r) {
                    r && (t.loading({
                        isShow: !0,
                        text: i.loading
                    }), window.setTimeout(function() {
                        var r = i.param;
                        n("[name=__RequestVerificationToken]").length > 0 && (r.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
                        n.ajax({
                            url: i.url,
                            data: r,
                            type: i.type,
                            dataType: i.dataType,
                            success: function(n) {
                                n.type == "3" ? t.dialogAlert({
                                    msg: n.message,
                                    type: -1
                                }) : (t.dialogMsg({
                                    msg: n.message,
                                    type: 1
                                }), i.success(n))
                            },
                            error: function(n, i, r) {
                                t.loading({
                                    isShow: !1
                                });
                                t.dialogMsg({
                                    msg: r,
                                    type: -1
                                })
                            },
                            beforeSend: function() {
                                t.loading({
                                    isShow: !0,
                                    text: i.loading
                                })
                            },
                            complete: function() {
                                t.loading({
                                    isShow: !1
                                })
                            }
                        })
                    },
                    500))
                }
            })
        },
        confirmAjax: function(i) {
            var i = n.extend({
                msg: "提示信息",
                loading: "正在处理数据...",
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                success: null
            },
            i);
            t.dialogConfirm({
                msg: i.msg,
                callBack: function(r) {
                    r && (t.loading({
                        isShow: !0,
                        text: i.loading
                    }), window.setTimeout(function() {
                        var r = i.param;
                        n("[name=__RequestVerificationToken]").length > 0 && (r.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
                        n.ajax({
                            url: i.url,
                            data: r,
                            type: i.type,
                            dataType: i.dataType,
                            success: function(n) {
                                t.loading({
                                    isShow: !1
                                });
                                n.type == "3" ? t.dialogAlert({
                                    msg: n.message,
                                    type: -1
                                }) : (t.dialogMsg({
                                    msg: n.message,
                                    type: 1
                                }), i.success(n))
                            },
                            error: function(n, i, r) {
                                t.loading({
                                    isShow: !1
                                });
                                t.dialogMsg({
                                    msg: r,
                                    type: -1
                                })
                            },
                            beforeSend: function() {
                                t.loading({
                                    isShow: !0,
                                    text: i.loading
                                })
                            },
                            complete: function() {
                                t.loading({
                                    isShow: !1
                                })
                            }
                        })
                    },
                    200))
                }
            })
        },
        existField: function(i, r, u) {
            var f = n("#" + i),
            e,
            o;
            if (!f.val()) return ! 1;
            e = {
                keyValue: t.request("keyValue")
            };
            e[i] = f.val();
            o = n.extend(e, u);
            n.ajax({
                url: r,
                data: o,
                type: "get",
                dataType: "text",
                async: !1,
                success: function(n) {
                    n.toLocaleLowerCase() == "false" ? (ValidationMessage(f, "已存在,请重新输入"), f.attr("fieldexist", "yes")) : f.attr("fieldexist", "no")
                },
                error: function(n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                }
            })
        },
        getDataForm: function(i) {
            var i = n.extend({
                url: "",
                param: [],
                type: "post",
                dataType: "json",
                loading: "正在获取数据...",
                success: null,
                async: !1,
                cache: !1
            },
            i);
            t.loading({
                isShow: !0,
                text: i.loading
            });
            n("[name=__RequestVerificationToken]").length > 0 && (i.param.__RequestVerificationToken = n("[name=__RequestVerificationToken]").val());
            n.ajax({
                url: i.url,
                data: i.param,
                type: i.type,
                dataType: i.dataType,
                async: i.async,
                success: function(n) {
                    n != null && n.type == "3" ? t.dialogAlert({
                        msg: n.message,
                        type: -1
                    }) : i.success(n)
                },
                error: function(n, i, r) {
                    t.dialogMsg({
                        msg: r,
                        type: -1
                    })
                },
                beforeSend: function() {
                    t.loading({
                        isShow: !0
                    })
                },
                complete: function() {
                    t.loading({
                        isShow: !1
                    })
                }
            })
        },
        getSystemFormFields: function(n) {
            var i = t.getIframe(n);
            return i.$ ? (i.$("body").find("[data-systemHideField]").hide(), i.getSystemFields ? i.getSystemFields() : []) : !1
        },
        loadSystemForm: function(i, r) {
            var u = document.getElementById(i),
            f = function() {
                var n = t.getIframe(i); ! n.$ || n.$("body").find("[data-systemHideField]").hide();
                t.loading({
                    isShow: !1
                })
            };
            u.attachEvent ? u.attachEvent("onload", f) : u.onload = f;
            n("#" + i).attr("src", r)
        },
        getSystemFormData: function(n) {
            var i = t.getIframe(n);
            return ! i || !i.$ ? [] : i.getSystemData ? i.getSystemData() : []
        },
        saveSystemFormData: function(n, i) {
            var r = t.getIframe(n); ! r.$ || !r.AcceptClick || r.AcceptClick(i)
        },
        setSystemFormFieldsAuthrize: function(n, i) {
            var r = t.getIframe(n); ! r.$ || !r.setSystemFieldsAuthorize || r.setSystemFieldsAuthorize(i)
        },
        createProcess: function(n, i) {
            n.processId = t.createGuid();
            n.moduleId = top.$.cookie("currentmoduleId");
            t.getDataForm({
                url: "../../FlowManage/FlowLaunch/CreateProcess",
                param: n,
                loading: "正在创建流程",
                success: function() {
                    i(n.processId)
                }
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
        }
    })
} (window.jQuery, window.learun),
function(n, t) {
    "use strict";
    n.fn.comboBox = function(i) {
        var u = n(this),
        e = u.attr("id"),
        i,
        f,
        r;
        return e ? (i = n.extend({
            description: "==请选择==",
            id: "id",
            text: "text",
            title: "title",
            maxHeight: null,
            width: null,
            allowSearch: !1,
            url: null,
            param: null,
            data: null,
            dataName: !1,
            selectOne: !1,
            method: "GET"
        },
        i), f = {
            rendering: function() {
                var r, f, t;
                return u.find(".ui-select-text").length == 0 && u.html("<div class=\"ui-select-text\" style='color:#999;'>" + i.description + "<\/div>"),
                r = '<div class="ui-select-option">',
                r += '<div class="ui-select-option-content" style="max-height: ' + i.maxHeight + '"><\/div>',
                i.allowSearch && (r += '<div class="ui-select-option-search"><input type="text" class="form-control" placeholder="搜索关键字" /><span class="input-query" title="Search"><i class="fa fa-search"><\/i><\/span><\/div>'),
                r += "<\/div>",
                f = n(r),
                f.attr("id", e + "-option"),
                t = n("#" + e + "-option"),
                t.length != undefined && t.length == 1 ? (i.maxHeight != null && t.find(".ui-select-option-content").css("max-height", i.maxHeight), i.allowSearch && t.find(".ui-select-option-search").length != 1 && t.append('<div class="ui-select-option-search"><input type="text" class="form-control" placeholder="搜索关键字" /><span class="input-query" title="Search"><i class="fa fa-search"><\/i><\/span><\/div>')) : n("body").prepend(f),
                n("#" + e + "-option")
            },
            renderingData: function(t, i, r) {
                if (i.data != undefined && i.data.length >= 0) {
                    var f = n("<ul><\/ul>");
                    i.description && f.append('<li data-value="">' + i.description + "<\/li>");
                    n.each(i.data,
                    function(n, t) {
                        var u = t[i.title];
                        u == undefined && (u = "");
                        r != undefined ? t[i.text].indexOf(r) != -1 && f.append('<li data-value="' + t[i.id] + '" title="' + u + '">' + t[i.text] + "<\/li>") : f.append('<li data-value="' + t[i.id] + '" title="' + u + '">' + t[i.text] + "<\/li>")
                    });
                    t.find(".ui-select-option-content").html(f);
                    t.find("li").css("padding", "0 5px");
                    t.find("li").unbind();
                    t.find("li").click(function(i) {
                        var r = n(this);
                        u.attr("data-value", r.attr("data-value")).attr("data-text", r.text());
                        u.find(".ui-select-text").html(r.text()).css("color", "#000");
                        t.slideUp(150);
                        u.trigger("change");
                        i.stopPropagation()
                    }).hover(function(t) {
                        n(this).hasClass("liactive") || n(this).toggleClass("on");
                        t.stopPropagation()
                    })
                }
            },
            loadData: function() {
                if (!i.url) {
                    var r = u.find("li");
                    r.length > 0 && (i.data = [], r.each(function() {
                        var r = n(this),
                        t = {};
                        t[i.id] = r.attr("data-value");
                        t[i.title] = r.attr("title");
                        t[i.text] = r.html();
                        i.data.push(t)
                    }))
                } else i.data = t.ajax.asyncGet({
                    url: i.url,
                    data: i.param,
                    type: i.method
                }),
                !i.dataName || (i.data = i.data[i.dataName])
            }
        },
        f.loadData(), r = f.rendering(), f.renderingData(r, i), i.allowSearch && (r.find(".ui-select-option-search").find("input").bind("keypress",
        function() {
            if (event.keyCode == "13") {
                var t = n(this);
                f.renderingData(r, t[0].opt, t.val())
            }
        }).focus(function() {
            n(this).select()
        })[0].opt = i), u.unbind("click"), u.bind("click",
        function(t) {
            var s;
            if (u.attr("readonly") == "readonly" || u.attr("disabled") == "disabled") return ! 1;
            if (n(this).addClass("ui-select-focus"), r.is(":hidden")) {
                u.find(".ui-select-option").hide();
                n(".ui-select-option").hide();
                var o = u.offset().left,
                f = u.offset().top + 29,
                e = u.width();
                i.width && (e = i.width);
                r.height() + f < n(document).height() ? r.slideDown(150).css({
                    top: f,
                    left: o,
                    width: e
                }) : (s = f - r.height() - 32, r.show().css({
                    top: s,
                    left: o,
                    width: e
                }), r.attr("data-show", !0));
                r.css("border-top", "1px solid #ccc");
                r.find("li").removeClass("liactive");
                r.find("[data-value=" + u.attr("data-value") + "]").addClass("liactive");
                r.find(".ui-select-option-search").find("input").select()
            } else r.attr("data-show") ? r.hide() : r.slideUp(150);
            t.stopPropagation()
        }), n(document).click(function(t) {
            var t = t ? t: window.event,
            i = t.srcElement || t.target;
            n(i).hasClass("form-control") || (r.attr("data-show") ? r.hide() : r.slideUp(150), u.removeClass("ui-select-focus"), t.stopPropagation())
        }), i.selectOne && (!i.data || u.comboBoxSetValue(i.data[0][i.id])), u) : !1
    };
    n.fn.comboBoxSetValue = function(i) {
        var r, f, u;
        if (!t.isNullOrEmpty(i)) return r = n(this),
        f = n("#" + r.attr("id") + "-option"),
        r.attr("data-value", i),
        u = f.find("ul").find("[data-value=" + i + "]").html(),
        u && (r.attr("data-text", u), r.find(".ui-select-text").html(u).css("color", "#000"), f.find("ul").find("[data-value=" + i + "]").addClass("liactive")),
        r.trigger("change"),
        r
    };
    n.fn.comboBoxTree = function(i) {
        var u = n(this),
        e = u.attr("id");
        if (!e) return ! 1;
        var i = n.extend({
            description: "==请选择==",
            id: "id",
            text: "text",
            title: "title",
            maxHeight: null,
            width: null,
            allowSearch: !1,
            url: !1,
            param: null,
            method: "GET",
            appendTo: null,
            click: null,
            icon: !1,
            data: null,
            dataItemName: !1
        },
        i),
        f = {
            rendering: function() {
                var t, r;
                return u.find(".ui-select-text").length == 0 && u.html("<div class=\"ui-select-text\" style='color:#999;'>" + i.description + "<\/div>"),
                t = '<div class="ui-select-option">',
                t += '<div class="ui-select-option-content" style="max-height: ' + i.maxHeight + '"><\/div>',
                i.allowSearch && (t += '<div class="ui-select-option-search"><input type="text" class="form-control" placeholder="搜索关键字" /><span class="input-query" title="Search"><i class="fa fa-search"><\/i><\/span><\/div>'),
                t += "<\/div>",
                r = n(t),
                r.attr("id", e + "-option"),
                i.appendTo ? n(i.appendTo).prepend(r) : n("body").prepend(r),
                n("#" + e + "-option")
            },
            loadtreeview: function(n, t) {
                o.treeview({
                    onnodeclick: function(t) {
                        if (n.click) {
                            var i = "ok";
                            if (i = n.click(t), i == "false") return ! 1
                        }
                        u.attr("data-value", t.id).attr("data-text", t.text);
                        u.find(".ui-select-text").html(t.text).css("color", "#000");
                        u.trigger("change")
                    },
                    height: n.maxHeight,
                    data: t,
                    description: n.description
                })
            },
            loadData: function(i) {
                var r = [];
                r = i.data ? i.data: t.ajax.asyncGet({
                    url: i.url,
                    data: i.param,
                    type: i.method
                });
                i.dataItemName ? (i.data = [], n.each(r,
                function(n, t) {
                    var r = top.learun.data.get(["dataItem", i.dataItemName, t[i.text]]);
                    r != "" && (t[i.text] = r);
                    i.data.push(t)
                })) : i.data = r
            },
            searchData: function(t, i) {
                var u = !1,
                r = [];
                return n.each(t,
                function(n, t) {
                    var e = {},
                    o, s;
                    for (o in t) o != "ChildNodes" && (e[o] = t[o]);
                    s = !1;
                    e.text.indexOf(i) != -1 && (s = !0);
                    e.hasChildren && (e.ChildNodes = f.searchData(t.ChildNodes, i), e.ChildNodes.length > 0 ? s = !0 : e.hasChildren = !1);
                    s && (u = !0, r.push(e))
                }),
                r
            }
        },
        r = f.rendering(),
        o = n("#" + e + "-option").find(".ui-select-option-content");
        return f.loadData(i),
        f.loadtreeview(i, i.data),
        i.allowSearch && (r.find(".ui-select-option-search").find("input").bind("keypress",
        function() {
            if (event.keyCode == "13") {
                var t = n(this),
                i = n(this).val(),
                r = f.searchData(t[0].opt.data, i);
                f.loadtreeview(t[0].opt, r)
            }
        }).focus(function() {
            n(this).select()
        })[0].opt = i),
        i.icon && (r.find("i").remove(), r.find("img").remove()),
        u.find(".ui-select-text").unbind("click"),
        u.find(".ui-select-text").bind("click",
        function(t) {
            var s;
            if (u.attr("readonly") == "readonly" || u.attr("disabled") == "disabled") return ! 1;
            if (n(this).parent().addClass("ui-select-focus"), r.is(":hidden")) {
                u.find(".ui-select-option").hide();
                n(".ui-select-option").hide();
                var o = u.offset().left,
                f = u.offset().top + 29,
                e = u.width();
                i.width && (e = i.width);
                r.height() + f < n(window).height() ? r.slideDown(150).css({
                    top: f,
                    left: o,
                    width: e
                }) : (s = f - r.height() - 32, r.show().css({
                    top: s,
                    left: o,
                    width: e
                }), r.attr("data-show", !0));
                r.css("border-top", "1px solid #ccc");
                i.appendTo && r.css("position", "inherit");
                r.find(".ui-select-option-search").find("input").select()
            } else r.attr("data-show") ? r.hide() : r.slideUp(150);
            t.stopPropagation()
        }),
        u.find("li div").click(function(t) {
            var t = t ? t: window.event,
            i = t.srcElement || t.target;
            n(i).hasClass("bbit-tree-ec-icon") || (r.slideUp(150), t.stopPropagation())
        }),
        n(document).click(function(t) {
            var t = t ? t: window.event,
            i = t.srcElement || t.target;
            n(i).hasClass("bbit-tree-ec-icon") || n(i).hasClass("form-control") || (r.attr("data-show") ? r.hide() : r.slideUp(150), u.removeClass("ui-select-focus"), t.stopPropagation())
        }),
        u
    };
    n.fn.comboBoxTreeSetValue = function(i) {
        if (!t.isNullOrEmpty(i)) {
            var r = n(this),
            u = n("#" + r.attr("id") + "-option").find(".ui-select-option-content");
            return u.find("ul").find("[data-value=" + i + "]").trigger("click"),
            r
        }
    };
    n.fn.getWebControls = function(t) {
        var i = "";
        return n(this).find("input,select,textarea,.ui-select,.uploadify,.webUploader").each(function() {
            var r = n(this).attr("id"),
            u = n(this).attr("type"),
            t;
            switch (u) {
            case "checkbox":
                i += n("#" + r).is(":checked") ? '"' + r + '":"1",': '"' + r + '":"0",';
                break;
            case "select":
                t = n("#" + r).attr("data-value");
                t == "" && (t = "&nbsp;");
                i += '"' + r + '":"' + n.trim(t) + '",';
                break;
            case "selectTree":
                t = n("#" + r).attr("data-value");
                t == "" && (t = "&nbsp;");
                i += '"' + r + '":"' + n.trim(t) + '",';
                break;
            case "webUploader":
            case "uploadify":
                t = n("#" + r).attr("data-value"); (t == "" || t == undefined) && (t = "&nbsp;");
                i += '"' + r + '":"' + n.trim(t) + '",';
                break;
            default:
                t = n("#" + r).val();
                t == "" && (t = "&nbsp;");
                i += '"' + r + '":"' + n.trim(t) + '",'
            }
        }),
        i = i.substr(0, i.length - 1),
        t || (i = i.replace(/&nbsp;/g, "")),
        i = i.replace(/\\/g, "\\\\"),
        i = i.replace(/\n/g, "\\n"),
        jQuery.parseJSON("{" + i + "}")
    };
    n.fn.setWebControls = function(t) {
        var e = n(this),
        u,
        i,
        f,
        r;
        for (u in t) if (i = e.find("#" + u), i.attr("id")) {
            f = i.attr("type");
            i.hasClass("input-datepicker") && (f = "datepicker");
            r = n.trim(t[u]).replace(/&nbsp;/g, "");
            switch (f) {
            case "checkbox":
                r == 1 ? i.attr("checked", "checked") : i.removeAttr("checked");
                break;
            case "select":
                i.comboBoxSetValue(r);
                break;
            case "selectTree":
                i.comboBoxTreeSetValue(r);
                break;
            case "datepicker":
                i.val(formatDate(r, "yyyy-MM-dd"));
                break;
            case "uploadify":
            case "webUploader":
                i.uploadifyExSet(r);
            default:
                i.val(r)
            }
        }
    };
    n.fn.getSysFormControls = function() {
        var t = [];
        return n(this).find("[data-wfname]").each(function() {
            var i = n(this),
            f = i.attr("data-wfname"),
            r = i.attr("id"),
            e = i.attr("type"),
            u;
            r == undefined && (r = i.attr("data-id"));
            u = i.attr("data-girdid");
            t.push({
                field: r,
                label: f,
                type: e,
                girdId: u
            })
        }),
        t
    };
    n.fn.conTextMenu = function() {
        var e = n(this),
        i = n(".contextmenu");
        n(document).click(function() {
            i.hide()
        });
        n(document).mousedown(function(n) {
            3 == n.which && i.hide()
        });
        var s = i.find("ul"),
        r = i.find("li"),
        f = null,
        o = null,
        t = 0,
        u = [document.documentElement.offsetWidth, document.documentElement.offsetHeight];
        for (i.hide(), t = 0; t < r.length; t++) r[t].getElementsByTagName("ul")[0] && (r[t].className = "sub"),
        r[t].onmouseover = function() {
            var i = this,
            n = i.getElementsByTagName("ul");
            i.className += " active";
            n[0] && (clearTimeout(o), f = setTimeout(function() {
                for (t = 0; t < i.parentNode.children.length; t++) i.parentNode.children[t].getElementsByTagName("ul")[0] && (i.parentNode.children[t].getElementsByTagName("ul")[0].style.display = "none");
                n[0].style.display = "block";
                n[0].style.top = i.offsetTop + "px";
                n[0].style.left = i.offsetWidth + "px";
                var r = u[0] - n[0].offsetWidth,
                f = u[1] - n[0].offsetHeight;
                r < getOffset.left(n[0]) && (n[0].style.left = -n[0].clientWidth + "px");
                f < getOffset.top(n[0]) && (n[0].style.top = -n[0].clientHeight + i.offsetTop + i.clientHeight + "px")
            },
            300))
        },
        r[t].onmouseout = function() {
            var n = this,
            r = n.getElementsByTagName("ul"),
            i;
            n.className = n.className.replace(/\s?active/, "");
            clearTimeout(f);
            i = setTimeout(function() {
                for (t = 0; t < n.parentNode.children.length; t++) n.parentNode.children[t].getElementsByTagName("ul")[0] && (n.parentNode.children[t].getElementsByTagName("ul")[0].style.display = "none")
            },
            300)
        };
        n(e).bind("contextmenu",
        function() {
            var n = n || window.event,
            t, r;
            return i.show(),
            i.css("top", n.clientY + "px"),
            i.css("left", n.clientX + "px"),
            t = u[0] - i.width(),
            r = u[1] - i.height(),
            i.offset().top > r && i.css("top", r + "px"),
            i.offset().left > t && i.css("left", t + "px"),
            !1
        }).bind("click",
        function() {
            i.hide()
        })
    };
    n.fn.panginationEx = function(t) {
        var i = n(this),
        t,
        r;
        if (!i.attr("id")) return ! 1;
        t = n.extend({
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
        t);
        r = n.extend({
            sidx: t.sortname,
            sord: "asc"
        },
        t.params);
        t.remote = {
            url: t.url,
            params: r,
            beforeSend: function(n) {
                t.beforeSend != null && t.beforeSend(n)
            },
            success: function(n, i) {
                t.success != null && t.success(n.rows, i)
            },
            complete: function(n, i) {
                t.complete != null && t.complete(n, i)
            },
            pageIndexName: "page",
            pageSizeName: "rows",
            totalName: "records"
        };
        i.page(t)
    };
    n.fn.leftListShowOfEmail = function(t) {
        var i = n(this),
        t;
        if (!i.attr("id")) return ! 1;
        i.append('<ul  style="padding-top: 10px;"><\/ul>');
        t = n.extend({
            id: "id",
            name: "text",
            img: "fa fa-file-o"
        },
        t);
        i.height(t.height);
        n.ajax({
            url: t.url,
            data: t.param,
            type: "GET",
            dataType: "json",
            async: !1,
            success: function(r) {
                n.each(r,
                function(r, u) {
                    var f = n('<li class="" data-value="' + u[t.id] + '"  data-text="' + u[t.name] + '" ><i class="' + t.img + '" style="vertical-align: middle; margin-top: -2px; margin-right: 8px; font-size: 14px; color: #666666; opacity: 0.9;"><\/i>' + u[t.name] + "<\/li>");
                    r == 0 && f.addClass("active");
                    i.find("ul").append(f)
                });
                i.find("li").click(function() {
                    var r = n(this).attr("data-value"),
                    u = n(this).attr("data-text");
                    i.find("li").removeClass("active");
                    n(this).addClass("active");
                    t.onnodeclick({
                        id: r,
                        name: u
                    })
                })
            },
            error: function(n, t, i) {
                dialogMsg(i, -1)
            }
        })
    };
    n.fn.authorizeButton = function() {
        var i = n(this),
        u,
        r;
        i.find("a.btn").attr("authorize", "no");
        i.find("ul.dropdown-menu").find("li").attr("authorize", "no");
        u = t.tabiframeId().substr(6);
        r = top.learun.data.get(["authorizeButton", u]);
        r != undefined && n.each(r,
        function(n) {
            i.find("#" + r[n].F_EnCode).attr("authorize", "yes")
        });
        i.find("[authorize=no]").remove()
    };
    n.fn.authorizeColModel = function() {
        var i = n(this),
        r = i.jqGrid("getGridParam", "colModel"),
        u,
        t;
        n.each(r,
        function(n) {
            r[n].name != "rn" && i.hideCol(r[n].name)
        });
        u = tabiframeId().substr(6);
        t = top.learun.data.get(["authorizeColumn", u]);
        t != undefined && n.each(t,
        function(n) {
            i.showCol(t[n].F_EnCode)
        })
    };
    n.fn.jqGridEx = function(t) {
        var i = n(this),
        r,
        t;
        if (!i.attr("id")) return ! 1;
        t = n.extend({
            url: "",
            datatype: "json",
            height: n(window).height() - 139.5,
            autowidth: !0,
            colModel: [],
            viewrecords: !0,
            rowNum: 30,
            rowList: [30, 50, 100],
            pager: "#gridPager",
            sortname: "F_CreateDate desc",
            rownumbers: !0,
            shrinkToFit: !1,
            gridview: !0,
            onSelectRow: function() {
                r = n("#" + this.id).getGridParam("selrow")
            },
            gridComplete: function() {
                n("#" + this.id).setSelection(r, !1)
            }
        },
        t);
        i.jqGrid(t)
    };
    n.fn.jqGridRowValue = function(t) {
        var r = n(this),
        e = [],
        u = r.jqGrid("getGridParam", "selarrrow"),
        o,
        f,
        i;
        if (u != undefined && u != "") for (o = u.length, f = 0; f < o; f++) i = r.jqGrid("getRowData", u[f]),
        e.push(i[t]);
        else i = r.jqGrid("getRowData", r.jqGrid("getGridParam", "selrow")),
        e.push(i[t]);
        return String(e)
    };
    n.fn.jqGridRow = function() {
        var i = n(this),
        u = [],
        f = i.jqGrid("getGridParam", "selarrrow"),
        e,
        r,
        t;
        if (f != "") for (e = f.length, r = 0; r < e; r++) t = i.jqGrid("getRowData", f[r]),
        u.push(t);
        else t = i.jqGrid("getRowData", i.jqGrid("getGridParam", "selrow")),
        u.push(t);
        return u
    };
    n.fn.uploadifyEx = function(i) {
        var u = n(this),
        r = u.attr("id"),
        i,
        f;
        if (!r) return ! 1;
        if (i = n.extend({
            btnName: "上传附件",
            url: "",
            onUploadSuccess: !1,
            cancel: !1,
            height: 30,
            width: 90,
            type: "webUploader",
            fileTypeExts: "",
            oneFile: !1
        },
        i), i.type == "uploadify") u.removeAttr("id"),
        u.html('<input id="' + r + '" type="file" />'),
        u = n("#" + r),
        i.fileTypeExts = i.fileTypeExts == "" ? "*.avi;*.mp3;*.mp4;*.bmp;*.ico;*.gif;*.jpeg;*.jpg;*.png;*.psd; *.rar;*.zip;*.swf;*.log;*.pdf;*.doc;*.docx;*.ppt;*.pptx;*.txt; *.xls; *.xlsx;": "*." + i.fileTypeExts.replace(/,/g, ";*.") + ";",
        u.uploadify({
            method: "post",
            uploader: i.url,
            swf: top.contentPath + "/Content/scripts/plugins/uploadify/uploadify.swf",
            buttonText: i.btnName,
            height: i.height,
            width: i.width,
            fileTypeExts: i.fileTypeExts,
            removeCompleted: !1,
            onSelect: function(u) {
                i.oneFile && n("#" + r + "-queue").find(".uploadify-queue-item").each(function() {
                    n(this).attr("id") != u.id && n(this).remove()
                });
                var f = n("#" + u.id);
                f.prepend('<div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + u.type.replace(".", "") + '.png" style="width:40px;height:40px;" /><\/div>');
                f.find(".cancel").find("a").html('<i class="fa fa-trash-o "><\/i>');
                f.find(".cancel").find("a").attr("title", "删除");
                f.hover(function() {
                    n(this).find(".cancel").find("a").show()
                },
                function() {
                    n(this).find(".cancel").find("a").hide()
                });
                f.find(".cancel").unbind();
                f.find(".cancel").on("click",
                function() {
                    var i = f.attr("data-fileId");
                    t.setForm({
                        url: "/Utility/RemoveFile?fileId=" + i,
                        success: function(u) {
                            if (u.code == 1) {
                                f.remove();
                                n("#" + r + "-queue").find(".uploadify-queue-item").length == 0 && n("#" + r + "-queue").hide();
                                var e = n("#" + r).attr("data-value");
                                e = t.stringArray(e, i);
                                n("#" + r).attr("data-value", e)
                            }
                        }
                    });
                    n("#" + r + "-queue").find(".uploadify-queue-item").length == 0 && n("#" + r + "-queue").hide()
                })
            },
            onUploadSuccess: function(t, u) {
                var e, f;
                if (n("#" + t.id).find(".uploadify-progress").remove(), n("#" + t.id).find(".data").html(" 恭喜您，上传成功！"), n("#" + t.id).prepend('<a class="succeed" title="成功"><i class="fa fa-check-circle"><\/i><\/a>'), e = JSON.parse(u), n("#" + t.id).attr("data-fileId", e.fileId), f = n("#" + r).attr("data-value"), f != undefined && f != "" && f != "undefined" ? f += ",": f = "", n("#" + r).attr("data-value", f + e.fileId), i.onUploadSuccess) i.onUploadSuccess(e)
            },
            onUploadError: function(t) {
                n("#" + t.id).removeClass("uploadify-error");
                n("#" + t.id).find(".uploadify-progress").remove();
                n("#" + t.id).find(".data").html(" 很抱歉，上传失败！");
                n("#" + t.id).prepend('<span class="error" title="失败"><i class="fa fa-exclamation-circle"><\/i><\/span>')
            },
            onUploadStart: function() {
                n("#" + r + "-queue").show()
            },
            onCancel: function() {}
        }),
        n("#" + r + "-button").prepend('<i style="opacity: 0.6;" class="fa fa-cloud-upload"><\/i>&nbsp;'),
        n("#" + r + "-queue").hide(),
        n("#" + r).attr("type", "uploadify");
        else {
            u.attr("type", "webUploader");
            u.addClass("webUploader");
            u.html('<div class="btns"><div id="' + r + '-btn" class="btnSelect" style="line-height:' + i.height + "px;height:" + (i.height + 2) + 'px;" ><i style="opacity: 0.6;" class="fa fa-cloud-upload"><\/i>&nbsp;' + i.btnName + '<\/div><\/div><div id="' + r + '-queue" class="uploadify-queue" style="display:none;"><\/div><\/div>');
            f = WebUploader.create({
                auto: !0,
                swf: "/Content/scripts/plugins/webuploader/Uploader.swf",
                server: i.url,
                pick: "#" + r + "-btn",
                accept: {
                    extensions: i.fileTypeExts
                },
                multiple: !0,
                resize: !1
            });
            u.find(".webuploader-pick").height(i.height);
            u.find(".webuploader-pick").width(i.width);
            f.on("startUpload",
            function() {
                var n = u.find(".uploadify-queue");
                n.show()
            });
            f.on("uploadStart",
            function(r) {
                var e = u.find(".uploadify-queue"),
                f;
                i.oneFile && e.html("");
                f = n('<div id="' + r.id + '" class="uploadify-queue-item"><\/div>');
                f.append('<span class="fileName">' + r.name + " (" + t.countFileSize(r.size) + ')<\/span><span class="data"><\/span>');
                f.append('<div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + r.ext + '.png" style="width:40px;height:40px;" /><\/div>');
                e.append(f)
            });
            f.on("uploadStart",
            function(t) {
                var i = n("#" + t.id);
                i.find(".data").html(" - 0%");
                i.append('<div class="uploadify-progress"><div class="uploadify-progress-bar" style="width:0%;"><\/div><\/div>')
            });
            f.on("uploadProgress",
            function(t, i) {
                var r = n("#" + t.id),
                i = i * 100 + "%";
                r.find(".data").html(" - " + i);
                r.find(".uploadify-progress-bar").css("width", i)
            });
            f.on("uploadSuccess",
            function(t, u) {
                var e = n("#" + t.id),
                f;
                if (e.find(".uploadify-progress").remove(), e.find(".data").html(" 恭喜您，上传成功！"), e.attr("data-fileId", u.fileId), e.prepend('<div class="cancel"><a title="删除" style="display: none;"><i class="fa fa-trash-o "><\/i><\/a><\/div>'), e.prepend('<a class="succeed" title="成功"><i class="fa fa-check-circle"><\/i><\/a>'), f = n("#" + r).attr("data-value"), f != undefined && f != "" && f != "undefined" ? f += ",": f = "", n("#" + r).attr("data-value", f + u.fileId), i.onUploadSuccess) i.onUploadSuccess(u)
            });
            f.on("uploadError",
            function(t) {
                var i = n("#" + t.id);
                i.removeClass("uploadify-error");
                i.find(".uploadify-progress").remove();
                i.find(".data").html(" 很抱歉，上传失败！");
                i.append('<div class="cancel"><a title="删除" style="display: none;"><i class="fa fa-trash-o "><\/i><\/a><\/div>');
                i.append('<span class="error" title="失败"><i class="fa fa-exclamation-circle"><\/i><\/span>')
            });
            f.on("uploadComplete",
            function(i) {
                var o = u.find(".uploadify-queue"),
                e = n("#" + i.id);
                e.hover(function() {
                    n(this).find(".cancel").find("a").show()
                },
                function() {
                    n(this).find(".cancel").find("a").hide()
                });
                e.find(".cancel").unbind();
                e.find(".cancel").on("click",
                function() {
                    var u = e.attr("data-fileId");
                    t.setForm({
                        url: "/Utility/RemoveFile?fileId=" + u,
                        success: function(s) {
                            if (s.code == 1) {
                                f.removeFile(i);
                                e.remove();
                                o.find(".uploadify-queue-item").length == 0 && o.hide();
                                var h = n("#" + r).attr("data-value");
                                h = t.stringArray(h, u);
                                n("#" + r).attr("data-value", h)
                            }
                        }
                    });
                    o.find(".uploadify-queue-item").length == 0 && o.hide()
                })
            })
        }
    };
    n.fn.uploadifyExSet = function(i, r) {
        var e = n(this),
        u = e.attr("id"),
        f;
        if (!u) return ! 1;
        f = n("#" + u + "-queue");
        f.length < 1 && (f = n('<div id="' + u + '-queue" class="uploadify-queue" style="display:none;"><\/div>'), e.append(f));
        t.setForm({
            url: "/Utility/GetFiles?fileIdList=" + i,
            success: function(t) {
                n.each(t,
                function (n, t) {
                    console.log(t);
                    f.show();
                    var i = '<div id="' + t.FileId + '"  class="uploadify-queue-item olduploadify-queue-item" ><a class="succeed" title="成功"><i class="fa fa-check-circle"><\/i><\/a><div style="float:left;width:50px;margin-right:2px;"><img src="/Content/images/filetype/' + t.FileType + '.png" style="width:40px;height:40px;"><\/div>'; (r == undefined || r.isRemove) && (i += '<div class="cancel remove" data-fileId="' + t.FileId + '"><a title="删除" style="display: none;"><i class="fa fa-trash-o "><\/i><\/a><\/div>'); (r == undefined || r.isDown) && (i += '<div class="cancel down" data-fileId="' + t.FileId + '"><a title="下载" style="display: none;margin-right:10px;"><i class="fa fa-download"><\/i><\/a><\/div>');
                    i += '<span class="fileName">' + t.FileName + '<\/span><span class="data"><\/span><\/div>';
                    f.append(i)
                })
            }
        });
        e.attr("data-value", i);
        f.find(".uploadify-queue-item").hover(function() {
            n(this).find(".cancel").find("a").show()
        },
        function() {
            n(this).find(".cancel").find("a").hide()
        });
        f.find(".olduploadify-queue-item").find(".remove").on("click",
        function() {
            var i = n(this).attr("data-fileId");
            t.setForm({
                url: "/Utility/RemoveFile?fileId=" + i,
                success: function(r) {
                    if (r.code == 1) {
                        n("#" + i).remove();
                        var f = n("#" + u).attr("data-value");
                        f = t.stringArray(f, i);
                        n("#" + u).attr("data-value", f);
                        n("#" + u + "-queue").find(".uploadify-queue-item").length == 0 && n("#" + u + "-queue").hide()
                    }
                }
            });
            n("#" + u + "-queue").find(".uploadify-queue-item").length == 0 && n("#" + u + "-queue").hide()
        });
        f.find(".olduploadify-queue-item").find(".down").on("click",
        function() {
            var i = n(this).attr("data-fileId");
            t.downFile({
                url: "/Utility/DownFile",
                data: "fileId=" + i,
                method: "post"
            })
        })
    };
    Date.prototype.DateAdd = function(n, t) {
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
    n.fn.userSelectBox = function() {
        var i = n(this),
        u = i.attr("id"),
        r;
        if (!u) return ! 1;
        r = n.extend({
            description: "==请选择=="
        },
        r);
        i.on("click",
        function() {
            var r = i.attr("data-value");
            t.dialogOpen({
                id: "SelectUserIndex",
                title: "用户选择",
                url: "/BaseManage/User/SelectUserIndex?data=" + r,
                width: "800px",
                height: "520px",
                callBack: function(t) {
                    top.frames[t].AcceptClick(function(t) {
                        var u = [],
                        r = [];
                        n.each(t,
                        function(n, t) {
                            u.push(t.id);
                            r.push(t.name)
                        });
                        i.attr("data-value", String(u)).attr("data-text", String(r));
                        i.find(".ui-select-text").html(String(r)).css("color", "#000")
                    })
                }
            })
        })
    }
} (window.jQuery, window.learun),
function(n) {
    n.fn.ComboBox = function(t) {
        return t.maxHeight = t.height,
        n(this).comboBox(t)
    };
    n.fn.ComboBoxSetValue = function(t) {
        return n(this).comboBoxSetValue(t)
    };
    n.fn.ComboBoxTree = function(t) {
        return t.maxHeight = t.height,
        n(this).comboBoxTree(t)
    };
    n.fn.ComboBoxTreeSetValue = function(t) {
        return n(this).comboBoxTreeSetValue(t)
    };
    n.fn.GetWebControls = function(t) {
        return n(this).getWebControls(t)
    };
    n.fn.SetWebControls = function(t) {
        n(this).setWebControls(t)
    };
    n.fn.Contextmenu = n.fn.contextmenu;
    n.fn.LeftListShowOfemail = n.fn.leftListShowOfemail;
    n.SaveForm = function(n) {
        learun.saveForm(n)
    };
    n.SetForm = function(n) {
        learun.setForm(n)
    };
    n.RemoveForm = function(n) {
        learun.removeForm(n)
    };
    n.ConfirmAjax = function(n) {
        learun.confirmAjax(n)
    };
    n.ExistField = function(n, t, i) {
        learun.existField(n, t, i)
    };
    n.getDataForm = function(n) {
        learun.getDataForm(n)
    }
} (window.jQuery);
Loading = function(n, t) {
    learun.loading({
        isShow: n,
        text: t
    })
};
tabiframeId = function() {
    return learun.tabiframeId()
};
dialogTop = function(n, t) {
    learun.dialogTop({
        msg: n,
        type: t
    })
};
dialogAlert = function(n, t) {
    learun.dialogAlert({
        msg: n,
        type: t
    })
};
dialogMsg = function(n, t) {
    learun.dialogMsg({
        msg: n,
        type: t
    })
};
dialogOpen = function(n) {
    learun.dialogOpen(n)
};
dialogContent = function(n) {
    learun.dialogContent(n)
};
dialogConfirm = function(n, t) {
    learun.dialogConfirm({
        msg: n,
        callBack: t
    })
};
dialogClose = function() {
    learun.dialogClose()
};
reload = function() {
    return location.reload(),
    !1
};
newGuid = function() {
    return learun.newGuid()
};
formatDate = function(n, t) {
    return learun.formatDate(n, t)
};
toDecimal = function(n) {
    return learun.toDecimal(n)
};
request = function(n) {
    return learun.request(n)
};
changeUrlParam = function(n, t, i) {
    return learun.changeUrlParam(n, t, i)
};
$.currentIframe = function() {
    return learun.currentIframe()
};
$.isbrowsername = function() {
    return learun.isbrowsername()
};
$.download = function(n, t, i) {
    learun.downFile({
        url: n,
        data: t,
        method: i
    })
};
$.standTabchange = function(n, t) {
    learun.changeStandTab({
        obj: n,
        id: t
    })
};
$.isNullOrEmpty = function(n) {
    return learun.isNullOrEmpty(n)
};
IsNumber = function(n) {
    return learun.isNumber(n)
};
IsMoney = function(n) {
    return learun.isMoney(n)
};
$.arrayClone = function(n) {
    return learun.arrayCopy(n)
};
$.windowWidth = function() {
    return $(window).width()
};
$.windowHeight = function() {
    return $(window).height()
};
checkedArray = function(n) {
    return learun.checkedArray(n)
};
checkedRow = function(n) {
    return learun.checkedRow(n)
};
$(function() {
    $(".ui-filter-text").click(function() {
        $(this).next(".ui-filter-list").is(":hidden") ? ($(this).css("border-bottom-color", "#fff"), $(".ui-filter-list").slideDown(10), $(this).addClass("active")) : ($(this).css("border-bottom-color", "#ccc"), $(".ui-filter-list").slideUp(10), $(this).removeClass("active"))
    });
    $(".profile-nav li").click(function() {
        $(".profile-nav li").removeClass("active");
        $(".profile-nav li").removeClass("hover");
        $(this).addClass("active")
    }).hover(function() {
        $(this).hasClass("active") || $(this).addClass("hover")
    },
    function() {
        $(this).removeClass("hover")
    })
})