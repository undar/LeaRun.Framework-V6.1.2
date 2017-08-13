function GooFlow(n, t) {
    var r, i, o, u, e, s, f;
    if (GooFlow.prototype.useSVG = navigator.userAgent.indexOf("MSIE 8.0") > 0 || navigator.userAgent.indexOf("MSIE 7.0") > 0 || navigator.userAgent.indexOf("MSIE 6.0") > 0 ? "" : "1", this.$id = n.attr("id"), this.$bgDiv = n, this.$bgDiv.addClass("GooFlow"), r = (t.width || 800) - 2, i = (t.height || 500) - 2, this.$bgDiv.css({
        width: r + "px",
        height: i + "px"
    }), this.$tool = null, this.$head = null, this.$title = "newFlow_1", this.$nodeRemark = {},
    this.$nowType = "cursor", this.$lineData = {},
    this.$lineCount = 0, this.$nodeData = {},
    this.$nodeCount = 0, this.$areaData = {},
    this.$areaCount = 0, this.$lineDom = {},
    this.$nodeDom = {},
    this.$areaDom = {},
    this.$max = t.initNum || 1, this.$focus = "", this.$cursor = "default", this.$editable = !1, this.$deletedItem = {},
    o = 0, u = "", t.haveHead) {
        for (u = "<div class='GooFlow_head'><label title='" + (t.initLabelText || "newFlow_1") + "'>" + (t.initLabelText || "newFlow_1") + "<\/label>", e = 0; e < t.headBtns.length; ++e) u += "<a  class='GooFlow_head_btn'><b class='ico_" + t.headBtns[e] + "'><\/b><\/a>";
        if (u += "<\/div>", this.$head = $(u), this.$bgDiv.append(this.$head), o = 24, this.onBtnNewClick = null, this.onBtnOpenClick = null, this.onBtnSaveClick = null, this.onFreshClick = null, t.headBtns) this.$head.on("click", {
            inthis: this
        },
        function (n) {
            var i, t;
            if (n || (n = window.event), i = n.target, i.tagName != "DIV" && i.tagName != "SPAN") {
                i.tagName == "a" && (i = i.childNode[0]);
                t = n.data.inthis;
                switch ($(i).attr("class")) {
                    case "ico_new":
                        t.onBtnNewClick != null && t.onBtnNewClick();
                        break;
                    case "ico_open":
                        t.onBtnOpenClick != null && t.onBtnOpenClick();
                        break;
                    case "ico_save":
                        t.onBtnSaveClick != null && t.onBtnSaveClick();
                        break;
                    case "ico_undo":
                        t.undo();
                        break;
                    case "ico_redo":
                        t.redo();
                        break;
                    case "ico_reload":
                        t.onFreshClick != null && t.onFreshClick()
                }
            }
        })
    }
    if (s = 0, t.haveTool) {
        if (this.$bgDiv.append("<div class='GooFlow_tool'" + (t.haveHead ? "" : " style='margin-top:1px'") + "><div style='height:" + (i - o - (t.haveHead ? 7 : 10)) + "px' class='GooFlow_tool_div'><\/div><\/div>"), this.$tool = this.$bgDiv.find(".GooFlow_tool div"), this.$tool.append("<a  type='cursor' class='GooFlow_tool_btndown' id='" + this.$id + "_btn_cursor'><b class='ico_cursor'/><\/a><a  type='direct' class='GooFlow_tool_btn' id='" + this.$id + "_btn_direct'><b class='ico_direct'/><\/a>"), t.toolBtns && t.toolBtns.length > 0) {
            for (u = "<span/>", f = 0; f < t.toolBtns.length; ++f) u += "<a  type='" + t.toolBtns[f] + "' id='" + this.$id + "_btn_" + t.toolBtns[f].split(" ")[0] + "' class='GooFlow_tool_btn'><b class='ico_" + t.toolBtns[f] + "'/><\/a>";
            this.$tool.append(u)
        }
        t.haveGroup && this.$tool.append("<span/><a  type='group' class='GooFlow_tool_btn' id='" + this.$id + "_btn_group'><b class='ico_group'/><\/a>");
        s = 31;
        this.$nowType = "cursor";
        this.$tool.on("click", {
            inthis: this
        },
        function (n) {
            var t, i;
            n || (n = window.event);
            switch (n.target.tagName) {
                case "SPAN":
                    return !1;
                case "DIV":
                    return !1;
                case "B":
                    t = n.target.parentNode;
                    break;
                case "A":
                    t = n.target
            }
            return i = $(t).attr("type"),
            n.data.inthis.switchToolBtn(i),
            !1
        });
        this.$editable = !0
    }
    if (r = r - s - 8, i = i, this.$bgDiv.append("<div class='GooFlow_work' style='width:" + (r - (t.haveTool == !0 ? 20 : -8)) + "px;height:" + i + "px;" + (t.haveHead ? "" : "margin-top:0px") + "'><\/div>"), this.$workArea = $("<div class='GooFlow_work_inner' style='width:" + r * 3 + "px;height:" + i * 3 + "px'><\/div>").attr({
        unselectable: "on",
        onselectstart: "return false",
        onselect: "document.selection.empty()"
    }), this.$bgDiv.children(".GooFlow_work").append(this.$workArea), this.$draw = null, this.initDraw("draw_" + this.$id, r, i), this.$group = null, t.haveGroup && this.initGroup(r, i), this.$editable) {
        this.$workArea.on("click", {
            inthis: this
        },
        function (n) {
            var u, s, h, f, i, r, t, e, o;
            if (n || (n = window.event), n.data.inthis.$editable) {
                if (t = n.data.inthis.$nowType, t == "cursor") {
                    i = $(n.target);
                    u = i.prop("tagName"); (u == "svg" || u == "DIV" && i.prop("class").indexOf("GooFlow_work") > -1 || u == "LABEL") && n.data.inthis.blurItem();
                    return
                }
                t != "direct" && t != "group" && (f = mousePosition(n), i = getElCoordinate(this), s = f.x - i.left + this.parentNode.scrollLeft - 1, h = f.y - i.top + this.parentNode.scrollTop - 1, r = "新建节点" + n.data.inthis.$max, t = n.data.inthis.$nowType, t == "startround" && (r = "开始"), t == "endround" && (r = "结束"), e = !0, o = n.data.inthis.$nodeData, $.each(o,
                function (n) {
                    if (o[n].name == r) return alert(r + "节点不能重复"),
                    e = !1,
                    !1
                }), e && (n.data.inthis.addNode(n.data.inthis.$id + "_node_" + n.data.inthis.$max, {
                    name: r,
                    left: s,
                    top: h,
                    type: n.data.inthis.$nowType,
                    css: "",
                    img: ""
                }), n.data.inthis.$max++))
            }
        });
        this.$workArea.mousemove({
            inthis: this
        },
        function (n) {
            var t, f, e, r, u, i;
            n.data.inthis.$nowType == "direct" && (t = $(this).data("lineStart"), t) && (f = mousePosition(n), e = getElCoordinate(this), r = f.x - e.left + this.parentNode.scrollLeft, u = f.y - e.top + this.parentNode.scrollTop, i = document.getElementById("GooFlow_tmp_line"), GooFlow.prototype.useSVG != "" ? (i.childNodes[0].setAttribute("d", "M " + t.x + " " + t.y + " L " + r + " " + u), i.childNodes[1].setAttribute("d", "M " + t.x + " " + t.y + " L " + r + " " + u), i.childNodes[1].getAttribute("marker-end") == 'url("#arrow2")' ? i.childNodes[1].setAttribute("marker-end", "url(#arrow3)") : i.childNodes[1].setAttribute("marker-end", "url(#arrow2)")) : i.points.value = t.x + "," + t.y + " " + r + "," + u)
        });
        this.$workArea.mouseup({
            inthis: this
        },
        function (n) {
            if (n.data.inthis.$nowType == "direct") {
                $(this).css("cursor", "auto").removeData("lineStart");
                var t = document.getElementById("GooFlow_tmp_line");
                t && n.data.inthis.$draw.removeChild(t)
            }
        });
        this.initWorkForNode();
        this.$ghost = $("<div class='rs_ghost'><\/div>").attr({
            unselectable: "on",
            onselectstart: "return false",
            onselect: "document.selection.empty()"
        });
        this.$bgDiv.append(this.$ghost);
        this.$textArea = $("<textarea><\/textarea>");
        this.$bgDiv.append(this.$textArea);
        this.$lineMove = $("<div class='GooFlow_line_move' style='display:none'><\/div>");
        this.$workArea.append(this.$lineMove);
        this.$lineMove.on("mousedown", {
            inthis: this
        },
        function (n) {
            var f;
            if (n.button == 2) return !1;
            f = $(this);
            f.css({
                "background-color": "#333"
            });
            var t = n.data.inthis,
            e = mousePosition(n),
            u = getElCoordinate(t.$workArea[0]),
            i,
            r;
            i = e.x - u.left + t.$workArea[0].parentNode.scrollLeft;
            r = e.y - u.top + t.$workArea[0].parentNode.scrollTop;
            var o = t.$lineMove.position(),
            h = i - o.left,
            c = r - o.top,
            s = !1;
            document.onmousemove = function (n) {
                n || (n = window.event);
                var f = mousePosition(n),
                e = t.$lineMove.position();
                i = f.x - u.left + t.$workArea[0].parentNode.scrollLeft;
                r = f.y - u.top + t.$workArea[0].parentNode.scrollTop;
                t.$lineMove.data("type") == "lr" ? (i = i - h, i < 0 ? i = 0 : i > t.$workArea.width() && (i = t.$workArea.width()), t.$lineMove.css({
                    left: i + "px"
                })) : t.$lineMove.data("type") == "tb" && (r = r - c, r < 0 ? r = 0 : r > t.$workArea.height() && (r = t.$workArea.height()), t.$lineMove.css({
                    top: r + "px"
                }));
                s = !0
            };
            document.onmouseup = function () {
                if (s) {
                    var n = t.$lineMove.position();
                    t.$lineMove.data("type") == "lr" ? t.setLineM(t.$lineMove.data("tid"), n.left + 3) : t.$lineMove.data("type") == "tb" && t.setLineM(t.$lineMove.data("tid"), n.top + 3)
                }
                t.$lineMove.css({
                    "background-color": "transparent"
                });
                t.$focus == t.$lineMove.data("tid") && t.focusItem(t.$lineMove.data("tid"));
                document.onmousemove = null;
                document.onmouseup = null
            }
        });
        this.$lineOper = $("<div class='GooFlow_line_oper' style='display:none'><b class='b_l1'><\/b><b class='b_l2'><\/b><b class='b_l3'><\/b><b class='b_x'><\/b><\/div>");
        this.$workArea.append(this.$lineOper);
        this.$lineOper.on("click", {
            inthis: this
        },
        function (n) {
            if (n || (n = window.event), n.target.tagName == "A" || n.target.tagName == "B") {
                var t = n.data.inthis,
                i = $(this).data("tid");
                switch ($(n.target).attr("class")) {
                    case "b_x":
                        t.delLine(i);
                        this.style.display = "none";
                        break;
                    case "b_l1":
                        t.setLineType(i, "lr");
                        break;
                    case "b_l2":
                        t.setLineType(i, "tb");
                        break;
                    case "b_l3":
                        t.setLineType(i, "sl")
                }
            }
        });
        this.onItemAdd = null;
        this.onItemDel = null;
        this.onItemMove = null;
        this.onItemRename = null;
        this.onItemFocus = null;
        this.onItemBlur = null;
        this.onItemResize = null;
        this.onLineMove = null;
        this.onLineSetType = null;
        this.onItemMark = null;
        t.useOperStack && this.$editable && (this.$undoStack = [], this.$redoStack = [], this.$isUndo = 0, this.pushOper = function (n, t) {
            var i = this.$undoStack.length;
            this.$isUndo == 1 ? (this.$redoStack.push([n, t]), this.$isUndo = !1, this.$redoStack.length > 40 && this.$redoStack.shift()) : (this.$undoStack.push([n, t]), this.$undoStack.length > 40 && this.$undoStack.shift(), this.$isUndo == 0 && this.$redoStack.splice(0, this.$redoStack.length), this.$isUndo = 0)
        },
        this.pushExternalOper = function (n, t) {
            this.pushOper("externalFunc", [n, t])
        },
        this.undo = function () {
            if (this.$undoStack.length != 0) {
                var n = this.$undoStack.pop();
                if (this.$isUndo = 1, n[0] == "externalFunc") n[1][0](n[1][1]);
                else switch (n[1].length) {
                    case 0:
                        this[n[0]]();
                        break;
                    case 1:
                        this[n[0]](n[1][0]);
                        break;
                    case 2:
                        this[n[0]](n[1][0], n[1][1]);
                        break;
                    case 3:
                        this[n[0]](n[1][0], n[1][1], n[1][2]);
                        break;
                    case 4:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3]);
                        break;
                    case 5:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3], n[1][4]);
                        break;
                    case 6:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3], n[1][4], n[1][5])
                }
            }
        },
        this.redo = function () {
            if (this.$redoStack.length != 0) {
                var n = this.$redoStack.pop();
                if (this.$isUndo = 2, n[0] == "externalFunc") n[1][0](n[1][1]);
                else switch (n[1].length) {
                    case 0:
                        this[n[0]]();
                        break;
                    case 1:
                        this[n[0]](n[1][0]);
                        break;
                    case 2:
                        this[n[0]](n[1][0], n[1][1]);
                        break;
                    case 3:
                        this[n[0]](n[1][0], n[1][1], n[1][2]);
                        break;
                    case 4:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3]);
                        break;
                    case 5:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3], n[1][4]);
                        break;
                    case 6:
                        this[n[0]](n[1][0], n[1][1], n[1][2], n[1][3], n[1][4], n[1][5])
                }
            }
        });
        $(document).keydown({
            inthis: this
        },
        function (n) {
            var t = n.data.inthis;
            if (t.$focus != "") switch (n.keyCode) {
                case 46:
                    t.delNode(t.$focus, !0);
                    t.delLine(t.$focus)
            }
        })
    }
}
function getElCoordinate(n) {
    var t = n.offsetTop,
    i = n.offsetLeft;
    for (n = n.offsetParent; n;) t += n.offsetTop,
    i += n.offsetLeft,
    n = n.offsetParent;
    return {
        top: t,
        left: i
    }
}
function mousePosition(n) {
    return (n || (n = window.event), n.pageX || n.pageY) ? {
        x: n.pageX,
        y: n.pageY
    } : {
        x: n.clientX + document.documentElement.scrollLeft - document.body.clientLeft,
        y: n.clientY + document.documentElement.scrollTop - document.body.clientTop
    }
}
GooFlow.prototype = {
    useSVG: "",
    getSvgMarker: function (n, t) {
        var i = document.createElementNS("http://www.w3.org/2000/svg", "marker"),
        r;
        return i.setAttribute("id", n),
        i.setAttribute("viewBox", "0 0 6 6"),
        i.setAttribute("refX", 5),
        i.setAttribute("refY", 3),
        i.setAttribute("markerUnits", "strokeWidth"),
        i.setAttribute("markerWidth", 6),
        i.setAttribute("markerHeight", 6),
        i.setAttribute("orient", "auto"),
        r = document.createElementNS("http://www.w3.org/2000/svg", "path"),
        r.setAttribute("d", "M 0 0 L 6 3 L 0 6 z"),
        r.setAttribute("fill", t),
        r.setAttribute("stroke-width", 0),
        i.appendChild(r),
        i
    },
    initDraw: function (n, t, i) {
        var r, u;
        GooFlow.prototype.useSVG != "" ? (this.$draw = document.createElementNS("http://www.w3.org/2000/svg", "svg"), this.$workArea.prepend(this.$draw), r = document.createElementNS("http://www.w3.org/2000/svg", "defs"), this.$draw.appendChild(r), r.appendChild(GooFlow.prototype.getSvgMarker("arrow1", "gray")), r.appendChild(GooFlow.prototype.getSvgMarker("arrow2", "#ff3300")), r.appendChild(GooFlow.prototype.getSvgMarker("arrow3", "#ff3300"))) : (this.$draw = document.createElement("v:group"), this.$draw.coordsize = t * 3 + "," + i * 3, this.$workArea.prepend("<div class='GooFlow_work_vml' style='position:relative;width:" + t * 3 + "px;height:" + i * 3 + "px'><\/div>"), this.$workArea.children("div")[0].insertBefore(this.$draw, null));
        this.$draw.id = n;
        this.$draw.style.width = t * 3 + "px";
        this.$draw.style.height = +i * 3 + "px";
        u = null;
        u = GooFlow.prototype.useSVG != "" ? "g" : "PolyLine";
        this.$editable && ($(this.$draw).delegate(u, "click", {
            inthis: this
        },
        function (n) {
            n.data.inthis.focusItem(this.id, !0)
        }), $(this.$draw).delegate(u, "dblclick", {
            inthis: this
        },
        function (n) {
            var t = n.data.inthis;
            OpenLine(this.id, t)
        }))
    },
    initGroup: function (n, t) {
        if (this.$group = $("<div class='GooFlow_work_group' style='width:" + n * 3 + "px;height:" + t * 3 + "px'><\/div>"), this.$workArea.prepend(this.$group), this.$editable) {
            this.$group.on("mousedown", {
                inthis: this
            },
            function (n) {
                var t, e, i, o, c, r, u, f, s, h, l;
                if (n.button == 2) return !1;
                if (t = n.data.inthis, t.$nowType == "group") {
                    if (t.$textArea.css("display") == "block") return t.setName(t.$textArea.data("id"), t.$textArea.val(), "area"),
                    t.$textArea.val("").removeData("id").hide(),
                    !1;
                    n || (n = window.event);
                    e = $(n.target).css("cursor");
                    i = n.target.parentNode;
                    switch (e) {
                        case "nw-resize":
                            i = i.parentNode;
                            break;
                        case "w-resize":
                            i = i.parentNode;
                            break;
                        case "n-resize":
                            i = i.parentNode;
                            break;
                        case "move":
                            break;
                        default:
                            return
                    }
                    i = i.id;
                    o = 1;
                    navigator.userAgent.indexOf("8.0") != -1 && (o = 0);
                    c = mousePosition(n);
                    r = getElCoordinate(t.$workArea[0]);
                    u = c.x - r.left + t.$workArea[0].parentNode.scrollLeft;
                    f = c.y - r.top + t.$workArea[0].parentNode.scrollTop;
                    e != "move" ? (t.$ghost.css({
                        display: "block",
                        width: t.$areaData[i].width - 2 + "px",
                        height: t.$areaData[i].height - 2 + "px",
                        top: t.$areaData[i].top + r.top - t.$workArea[0].parentNode.scrollTop + o + "px",
                        left: t.$areaData[i].left + r.left - t.$workArea[0].parentNode.scrollLeft + o + "px",
                        cursor: e
                    }), s = t.$areaData[i].left + t.$areaData[i].width - u, h = t.$areaData[i].top + t.$areaData[i].height - f) : (s = u - t.$areaData[i].left, h = f - t.$areaData[i].top);
                    l = !1;
                    t.$ghost.css("cursor", e);
                    document.onmousemove = function (n) {
                        n || (n = window.event);
                        var c = mousePosition(n);
                        if (e != "move") {
                            u = c.x - r.left + t.$workArea[0].parentNode.scrollLeft - t.$areaData[i].left + s;
                            f = c.y - r.top + t.$workArea[0].parentNode.scrollTop - t.$areaData[i].top + h;
                            u < 200 && (u = 200);
                            f < 100 && (f = 100);
                            switch (e) {
                                case "nw-resize":
                                    t.$ghost.css({
                                        width:
                                        u - 2 + "px",
                                        height: f - 2 + "px"
                                    });
                                    break;
                                case "w-resize":
                                    t.$ghost.css({
                                        width:
                                        u - 2 + "px"
                                    });
                                    break;
                                case "n-resize":
                                    t.$ghost.css({
                                        height:
                                        f - 2 + "px"
                                    })
                            }
                        } else t.$ghost.css("display") == "none" && t.$ghost.css({
                            display: "block",
                            width: t.$areaData[i].width - 2 + "px",
                            height: t.$areaData[i].height - 2 + "px",
                            top: t.$areaData[i].top + r.top - t.$workArea[0].parentNode.scrollTop + o + "px",
                            left: t.$areaData[i].left + r.left - t.$workArea[0].parentNode.scrollLeft + o + "px",
                            cursor: e
                        }),
                        u = c.x - s,
                        f = c.y - h,
                        u < r.left - t.$workArea[0].parentNode.scrollLeft ? u = r.left - t.$workArea[0].parentNode.scrollLeft : u + t.$workArea[0].parentNode.scrollLeft + t.$areaData[i].width > r.left + t.$workArea.width() && (u = r.left + t.$workArea.width() - t.$workArea[0].parentNode.scrollLeft - t.$areaData[i].width),
                        f < r.top - t.$workArea[0].parentNode.scrollTop ? f = r.top - t.$workArea[0].parentNode.scrollTop : f + t.$workArea[0].parentNode.scrollTop + t.$areaData[i].height > r.top + t.$workArea.height() && (f = r.top + t.$workArea.height() - t.$workArea[0].parentNode.scrollTop - t.$areaData[i].height),
                        t.$ghost.css({
                            left: u + o + "px",
                            top: f + o + "px"
                        });
                        l = !0
                    };
                    document.onmouseup = function () {
                        if (t.$ghost.empty().hide(), document.onmousemove = null, document.onmouseup = null, l) return e != "move" ? t.resizeArea(i, t.$ghost.outerWidth(), t.$ghost.outerHeight()) : t.moveArea(i, u + t.$workArea[0].parentNode.scrollLeft - r.left, f + t.$workArea[0].parentNode.scrollTop - r.top),
                        !1
                    }
                }
            });
            this.$group.on("dblclick", {
                inthis: this
            },
            function (n) {
                var t = n.data.inthis;
                if (t.$nowType == "group") {
                    if (n || (n = window.event), n.target.tagName != "LABEL") return !1;
                    var u = n.target.innerHTML,
                    i = n.target.parentNode,
                    f = parseInt(i.style.left, 10) + 18,
                    e = parseInt(i.style.top, 10) + 1,
                    r = getElCoordinate(t.$workArea[0]);
                    t.$textArea.val(u).css({
                        display: "block",
                        width: 100,
                        height: 14,
                        left: r.left + f - t.$workArea[0].parentNode.scrollLeft,
                        top: r.top + e - t.$workArea[0].parentNode.scrollTop
                    }).data("id", i.id).focus();
                    t.$workArea.parent().one("mousedown",
                    function (n) {
                        if (n.button == 2) return !1;
                        t.$textArea.css("display") == "block" && (t.setName(t.$textArea.data("id"), t.$textArea.val(), "area"), t.$textArea.val("").removeData("id").hide())
                    });
                    return !1
                }
            });
            this.$group.mouseup({
                inthis: this
            },
            function (n) {
                var t = n.data.inthis,
                i, f, e, r, u, o;
                if (t.$nowType == "group") {
                    n || (n = window.event);
                    switch ($(n.target).attr("class")) {
                        case "rs_close":
                            return t.delArea(n.target.parentNode.parentNode.id),
                            !1;
                        case "bg":
                            return
                    }
                    switch (n.target.tagName) {
                        case "LABEL":
                            return !1;
                        case "B":
                            i = n.target.parentNode.id;
                            switch (t.$areaData[i].color) {
                                case "red":
                                    t.setAreaColor(i, "yellow");
                                    break;
                                case "yellow":
                                    t.setAreaColor(i, "blue");
                                    break;
                                case "blue":
                                    t.setAreaColor(i, "green");
                                    break;
                                case "green":
                                    t.setAreaColor(i, "red")
                            }
                            return !1
                    }
                    if (n.data.inthis.$ghost.css("display") == "none") return r = mousePosition(n),
                    u = getElCoordinate(this),
                    f = r.x - u.left + this.parentNode.parentNode.scrollLeft - 1,
                    e = r.y - u.top + this.parentNode.parentNode.scrollTop - 1,
                    o = ["red", "yellow", "blue", "green"],
                    n.data.inthis.addArea(n.data.inthis.$id + "_area_" + n.data.inthis.$max, {
                        name: "area_" + n.data.inthis.$max,
                        left: f,
                        top: e,
                        color: o[n.data.inthis.$max % 4],
                        width: 200,
                        height: 100
                    }),
                    n.data.inthis.$max++,
                    !1
                }
            })
        }
    },
    setNodeRemarks: function (n) {
        this.$tool != null && (this.$tool.children("a").each(function () {
            this.title = n[$(this).attr("id").split("btn_")[1]]
        }), this.$nodeRemark = n)
    },
    switchToolBtn: function (n) {
        var t;
        if (this.$tool.children("#" + this.$id + "_btn_" + this.$nowType.split(" ")[0]).attr("class", "GooFlow_tool_btn"), this.$nowType == "group") {
            this.$workArea.prepend(this.$group);
            for (t in this.$areaDom) this.$areaDom[t].addClass("lock").children("div:eq(1)").css("display", "none")
        }
        if (this.$nowType = n, this.$tool.children("#" + this.$id + "_btn_" + n.split(" ")[0]).attr("class", "GooFlow_tool_btndown"), this.$nowType == "group") {
            this.blurItem();
            this.$workArea.append(this.$group);
            for (t in this.$areaDom) this.$areaDom[t].removeClass("lock").children("div:eq(1)").css("display", "")
        }
        this.$textArea.css("display") == "none" && this.$textArea.removeData("id").val("").hide()
    },
    addNode: function (n, t) {
        var r, f, i, u; (this.onItemAdd == null || this.onItemAdd(n, "node", t)) && (this.$undoStack && this.$editable && this.pushOper("delNode", [n]), r = t.type, t.type != "startround" && t.type != "endround" ? ((!t.width || t.width < 86) && (t.width = 150), (!t.height || t.height < 24) && (t.height = 65), (!t.top || t.top < 0) && (t.top = 0), (!t.left || t.left < 0) && (t.left = 0), f = 0, navigator.userAgent.indexOf("8.0") != -1 && (f = 2), this.$nodeDom[n] = $("<div class='GooFlow_item " + r + "' id='" + n + "' style='top:" + t.top + "px;left:" + t.left + "px'><table cellspacing='1' style='width:" + t.width + "px;height:" + t.height + "px;'><tr><td class='ico'><b class='ico_" + t.type + "'><\/b><\/td><td>" + t.name + "<\/td><\/tr><\/table><div style='display:none'><div class='rs_bottom'><\/div><div class='rs_right'><\/div><div class='rs_rb'><\/div><div class='rs_close'><\/div><\/div><\/div>"), t.type.indexOf(" mix") > -1 && this.$nodeDom[n].addClass(r)) : (t.width = 24, t.height = 24, i = t.name, t.type == "startround" && (i = "开始"), t.type == "endround" && (i = "结束"), this.$nodeDom[n] = $("<div class='GooFlow_item item_" + t.type + "' id='" + n + "' style='top:" + t.top + "px;left:" + t.left + "px'><table cellspacing='0'><tr><td class='ico'><\/td><\/tr><\/table><div  style='display:none'><div class='rs_close'><\/div><\/div><div class='span'>" + i + "<\/div><\/div>")), u = navigator.userAgent.toLowerCase(), u.indexOf("msie") != -1 && u.indexOf("8.0") != -1 && this.$nodeDom[n].css("filter", "progid:DXImageTransform.Microsoft.Shadow(color=#94AAC2,direction=135,strength=2)"), this.$workArea.append(this.$nodeDom[n]), this.$nodeData[n] = t, ++this.$nodeCount, this.$editable && (this.$nodeData[n].alt = !0, this.$deletedItem[n] && delete this.$deletedItem[n]))
    },
    initWorkForNode: function () {
        (this.$workArea.delegate(".GooFlow_item", "click", {
            inthis: this
        },
            function (n) {
                n.data.inthis.focusItem(this.id, !0);
                $(this).removeClass("item_mark")
            }), this.$workArea.delegate(".GooFlow_item", "contextmenu", {
                inthis: this
            },
            function (n) {
                return n.data.inthis.focusItem(this.id, !0),
                $(this).removeClass("item_mark"),
                !1
            }), this.$workArea.delegate(".ico", "mousedown", {
                inthis: this
            },
            function (n) {
                var t, o, r, e, s, i, u, f;
                if (n || (n = window.event), n.button == 2) return !1;
                if (t = n.data.inthis, t.$nowType != "direct") {
                    o = $(this).parents(".GooFlow_item");
                    r = o.attr("id");
                    t.focusItem(r, !0);
                    e = 1;
                    navigator.userAgent.indexOf("8.0") != -1 && (e = 0);
                    s = mousePosition(n);
                    i = getElCoordinate(t.$workArea[0]);
                    o.children("table").clone().prependTo(t.$ghost);
                    u = s.x - i.left + t.$workArea[0].parentNode.scrollLeft;
                    f = s.y - i.top + t.$workArea[0].parentNode.scrollTop;
                    var c = u - t.$nodeData[r].left,
                    l = f - t.$nodeData[r].top,
                    h = !1;
                    document.onmousemove = function (n) {
                        n || (n = window.event);
                        var o = mousePosition(n);
                        if (u == o.x - c && f == o.y - l) return !1;
                        u = o.x - c;
                        f = o.y - l;
                        h && t.$ghost.css("display") == "none" && t.$ghost.css({
                            display: "block",
                            width: $("#" + r).width() - 2 + "px",
                            height: $("#" + r).height() - 2 + "px",
                            top: t.$nodeData[r].top + i.top - t.$workArea[0].parentNode.scrollTop + e + "px",
                            left: t.$nodeData[r].left + i.left - t.$workArea[0].parentNode.scrollLeft + e + "px",
                            cursor: "move"
                        });
                        u < i.left - t.$workArea[0].parentNode.scrollLeft ? u = i.left - t.$workArea[0].parentNode.scrollLeft : u + t.$workArea[0].parentNode.scrollLeft + t.$nodeData[r].width > i.left + t.$workArea.width() && (u = i.left + t.$workArea.width() - t.$workArea[0].parentNode.scrollLeft - t.$nodeData[r].width);
                        f < i.top - t.$workArea[0].parentNode.scrollTop ? f = i.top - t.$workArea[0].parentNode.scrollTop : f + t.$workArea[0].parentNode.scrollTop + t.$nodeData[r].height > i.top + t.$workArea.height() && (f = i.top + t.$workArea.height() - t.$workArea[0].parentNode.scrollTop - t.$nodeData[r].height);
                        t.$ghost.css({
                            left: u + e + "px",
                            top: f + e + "px"
                        });
                        h = !0
                    };
                    document.onmouseup = function () {
                        h && t.moveNode(r, u + t.$workArea[0].parentNode.scrollLeft - i.left, f + t.$workArea[0].parentNode.scrollTop - i.top);
                        t.$ghost.empty().hide();
                        document.onmousemove = null;
                        document.onmouseup = null
                    }
                }
            }), this.$editable) && (this.$workArea.delegate(".GooFlow_item", "mouseenter", {
                inthis: this
            },
            function (n) {
                n.data.inthis.$nowType == "direct" && $(this).addClass("item_mark")
            }), this.$workArea.delegate(".GooFlow_item", "mouseleave", {
                inthis: this
            },
            function (n) {
                n.data.inthis.$nowType == "direct" && $(this).removeClass("item_mark")
            }), this.$workArea.delegate(".GooFlow_item", "mousedown", {
                inthis: this
            },
            function (n) {
                var t, u, f, i, r, e;
                if (n.button == 2) return !1; (t = n.data.inthis, t.$nowType == "direct") && (u = mousePosition(n), f = getElCoordinate(t.$workArea[0]), i = u.x - f.left + t.$workArea[0].parentNode.scrollLeft, r = u.y - f.top + t.$workArea[0].parentNode.scrollTop, t.$workArea.data("lineStart", {
                    x: i,
                    y: r,
                    id: this.id
                }).css("cursor", "crosshair"), e = GooFlow.prototype.drawLine("GooFlow_tmp_line", [i, r], [i, r], !0, !0), t.$draw.appendChild(e))
            }), this.$workArea.delegate(".GooFlow_item", "mouseup", {
                inthis: this
            },
            function (n) {
                var t = n.data.inthis,
                i;
                t.$nowType == "direct" && (i = t.$workArea.data("lineStart"), i && t.addLine(t.$id + "_line_" + t.$max, {
                    from: i.id,
                    to: this.id,
                    name: ""
                }), t.$max++)
            }), this.$workArea.delegate(".GooFlow_item > .span", "dblclick", {
                inthis: this
            },
            function (n) {
                var t = n.data.inthis,
                i = $(".item_focus").hasClass("item_startround");
                i && OpenNode(t)
            }), this.$workArea.delegate(".ico + td", "dblclick", {
                inthis: this
            },
            function (n) {
                var t = n.data.inthis;
                OpenNode(t)
            }), this.$workArea.delegate(".rs_close", "click", {
                inthis: this
            },
            function (n) {
                return n || (n = window.event),
                n.data.inthis.delNode(n.data.inthis.$focus),
                !1
            }), this.$workArea.delegate(".GooFlow_item > div > div[class!=rs_close]", "mousedown", {
                inthis: this
            },
            function (n) {
                var e, t, i, o, s, r, u, f;
                if (n || (n = window.event), n.button == 2) return !1;
                if (e = $(this).css("cursor"), e != "pointer") {
                    t = n.data.inthis;
                    i = t.$focus;
                    t.switchToolBtn("cursor");
                    n.cancelBubble = !0;
                    n.stopPropagation();
                    o = 1;
                    navigator.userAgent.indexOf("8.0") != -1 && (o = 0);
                    s = mousePosition(n);
                    r = getElCoordinate(t.$workArea[0]);
                    t.$ghost.css({
                        display: "block",
                        width: t.$nodeData[i].width - 2 + "px",
                        height: t.$nodeData[i].height - 2 + "px",
                        top: t.$nodeData[i].top + r.top - t.$workArea[0].parentNode.scrollTop + o + "px",
                        left: t.$nodeData[i].left + r.left - t.$workArea[0].parentNode.scrollLeft + o + "px",
                        cursor: e
                    });
                    u = s.x - r.left + t.$workArea[0].parentNode.scrollLeft;
                    f = s.y - r.top + t.$workArea[0].parentNode.scrollTop;
                    var c = t.$nodeData[i].left + t.$nodeData[i].width - u,
                    l = t.$nodeData[i].top + t.$nodeData[i].height - f,
                    h = !1;
                    t.$ghost.css("cursor", e);
                    document.onmousemove = function (n) {
                        n || (n = window.event);
                        var o = mousePosition(n);
                        u = o.x - r.left + t.$workArea[0].parentNode.scrollLeft - t.$nodeData[i].left + c;
                        f = o.y - r.top + t.$workArea[0].parentNode.scrollTop - t.$nodeData[i].top + l;
                        u < 86 && (u = 86);
                        f < 24 && (f = 24);
                        h = !0;
                        switch (e) {
                            case "nw-resize":
                                t.$ghost.css({
                                    width:
                                    u - 2 + "px",
                                    height: f - 2 + "px"
                                });
                                break;
                            case "w-resize":
                                t.$ghost.css({
                                    width:
                                    u - 2 + "px"
                                });
                                break;
                            case "n-resize":
                                t.$ghost.css({
                                    height:
                                    f - 2 + "px"
                                })
                        }
                    };
                    document.onmouseup = function (n) {
                        (t.$ghost.hide(), h) && (n || (n = window.event), t.resizeNode(i, t.$ghost.outerWidth(), t.$ghost.outerHeight()), document.onmousemove = null, document.onmouseup = null)
                    }
                }
            }))
    },
    getItemInfo: function (n, t) {
        switch (t) {
            case "node":
                return this.$nodeData[n] || null;
            case "line":
                return this.$lineData[n] || null;
            case "area":
                return this.$areaData[n] || null
        }
    },
    blurItem: function () {
        if (this.$focus != "") {
            var n = $("#" + this.$focus);
            if (n.prop("tagName") == "DIV") {
                if (this.onItemBlur != null && !this.onItemBlur(id, "node")) return !1;
                n.removeClass("item_focus").children("div:eq(0)").css("display", "none")
            } else {
                if (this.onItemBlur != null && !this.onItemBlur(id, "line")) return !1;
                GooFlow.prototype.useSVG != "" ? this.$lineData[this.$focus].marked || (n[0].childNodes[1].setAttribute("stroke", "gray"), n[0].childNodes[1].setAttribute("marker-end", "url(#arrow1)")) : this.$lineData[this.$focus].marked || (n[0].strokeColor = "gray");
                this.$lineMove.hide().removeData("type").removeData("tid");
                this.$editable && this.$lineOper.hide().removeData("tid")
            }
        }
        return this.$focus = "",
        !0
    },
    focusItem: function (n, t) {
        var u = $("#" + n),
        e,
        o,
        i,
        r,
        f;
        if (u.length != 0 && this.blurItem()) {
            if (u.prop("tagName") == "DIV") {
                if (t && this.onItemFocus != null && !this.onItemFocus(n, "node")) return;
                u.addClass("item_focus");
                this.$editable && u.children("div:eq(0)").css("display", "block");
                this.$workArea.append(u)
            } else {
                if (this.onItemFocus != null && !this.onItemFocus(n, "line")) return;
                if (GooFlow.prototype.useSVG != "" ? (u[0].childNodes[1].setAttribute("stroke", "#ff3300"), u[0].childNodes[1].setAttribute("marker-end", "url(#arrow2)")) : u[0].strokeColor = "#ff3300", !this.$editable) return;
                GooFlow.prototype.useSVG != "" ? (i = u.attr("from").split(","), r = u.attr("to").split(",")) : (f = u[0].getAttribute("fromTo").split(","), i = [f[0], f[1]], r = [f[2], f[3]]);
                i[0] = parseInt(i[0], 10);
                i[1] = parseInt(i[1], 10);
                r[0] = parseInt(r[0], 10);
                r[1] = parseInt(r[1], 10);
                this.$lineData[n].type == "lr" ? (i[0] = this.$lineData[n].M, r[0] = i[0], this.$lineMove.css({
                    width: "5px",
                    height: (r[1] - i[1]) * (r[1] > i[1] ? 1 : -1) + "px",
                    left: i[0] - 3 + "px",
                    top: (r[1] > i[1] ? i[1] : r[1]) + 1 + "px",
                    cursor: "e-resize",
                    display: "block"
                }).data({
                    type: "lr",
                    tid: n
                })) : this.$lineData[n].type == "tb" && (i[1] = this.$lineData[n].M, r[1] = i[1], this.$lineMove.css({
                    width: (r[0] - i[0]) * (r[0] > i[0] ? 1 : -1) + "px",
                    height: "5px",
                    left: (r[0] > i[0] ? i[0] : r[0]) + 1 + "px",
                    top: i[1] - 3 + "px",
                    cursor: "s-resize",
                    display: "block"
                }).data({
                    type: "tb",
                    tid: n
                }));
                e = (i[0] + r[0]) / 2 - 35;
                o = (i[1] + r[1]) / 2 + 6;
                this.$lineOper.css({
                    display: "block",
                    left: e + "px",
                    top: o + "px"
                }).data("tid", n)
            }
            this.$focus = n;
            this.switchToolBtn("cursor")
        }
    },
    moveNode: function (n, t, i) {
        if (this.$nodeData[n] && (this.onItemMove == null || this.onItemMove(n, "node", t, i))) {
            if (this.$undoStack) {
                var r = [n, this.$nodeData[n].left, this.$nodeData[n].top];
                this.pushOper("moveNode", r)
            }
            t < 0 && (t = 0);
            i < 0 && (i = 0);
            $("#" + n).css({
                left: t + "px",
                top: i + "px"
            });
            this.$nodeData[n].left = t;
            this.$nodeData[n].top = i;
            this.resetLines(n, this.$nodeData[n]);
            this.$editable && (this.$nodeData[n].alt = !0)
        }
    },
    setName: function (n, t, i, r) {
        var e, c, s, h, u, f, o, l;
        if (i == "node") this.$nodeData[n].setInfo = r,
        this.$nodeData[n] || this.$nodeData[n].name == t && (this.onItemRename == null || this.onItemRename(n, t, "node") || (e = this.$nodeData[n].name)),
        this.$nodeData[n].name = t,
        this.$nodeData[n].type.indexOf("round") > 1 ? this.$nodeDom[n].children(".span").text(t) : (this.$nodeDom[n].find("td:eq(1)").text(t), c = 0, navigator.userAgent.indexOf("8.0") != -1 && (c = 2), s = this.$nodeDom[n].outerWidth(), h = this.$nodeDom[n].outerHeight(), this.$nodeDom[n].children("table").css({
            width: s - 2 + "px",
            height: h - 2 + "px"
        }), this.$nodeData[n].width = s, this.$nodeData[n].height = h),
        this.$editable && (this.$nodeData[n].alt = !0),
        this.resetLines(n, this.$nodeData[n]);
        else if (i == "line") this.$lineData[n].setInfo = r,
        this.$lineData[n] || this.$lineData[n].name == t && (this.onItemRename == null || this.onItemRename(n, t, "line") || (e = this.$lineData[n].name)),
        this.$lineData[n].name = t,
        GooFlow.prototype.useSVG != "" ? this.$lineDom[n].childNodes[2].textContent = t : (this.$lineDom[n].childNodes[1].innerHTML = t, u = this.$lineDom[n].getAttribute("fromTo").split(","), this.$lineData[n].type != "lr" ? f = (u[2] - u[0]) / 2 : (o = u[2] > u[0] ? u[0] : u[2], o > this.$lineData[n].M && (o = this.$lineData[n].M), f = this.$lineData[n].M - o), f < 0 && (f = f * -1), this.$lineDom[n].childNodes[1].style.left = f - this.$lineDom[n].childNodes[1].offsetWidth / 2 + 4 + "px"),
        this.$editable && (this.$lineData[n].alt = !0);
        else if (i == "area") {
            if (!this.$areaData[n]) return;
            if (this.$areaData[n].name == t) return;
            if (this.onItemRename != null && !this.onItemRename(n, t, "area")) return;
            e = this.$areaData[n].name;
            this.$areaData[n].name = t;
            this.$areaDom[n].children("label").text(t);
            this.$editable && (this.$areaData[n].alt = !0)
        }
        this.$undoStack && (l = [n, e, i], this.pushOper("setName", l))
    },
    resizeNode: function (n, t, i) {
        var u, r;
        this.$nodeData[n] && (this.onItemResize == null || this.onItemResize(n, "node", t, i)) && this.$nodeData[n].type != "start" && this.$nodeData[n].type != "end" && (this.$undoStack && (u = [n, this.$nodeData[n].width, this.$nodeData[n].height], this.pushOper("resizeNode", u)), r = 0, navigator.userAgent.indexOf("8.0") != -1 && (r = 2), this.$nodeDom[n].children("table").css({
            width: t - 2 + "px",
            height: i - 2 + "px"
        }), t = this.$nodeDom[n].outerWidth() - r, i = this.$nodeDom[n].outerHeight() - r, this.$nodeDom[n].children("table").css({
            width: t - 2 + "px",
            height: i - 2 + "px"
        }), this.$nodeData[n].width = t, this.$nodeData[n].height = i, this.$editable && (this.$nodeData[n].alt = !0), this.resetLines(n, this.$nodeData[n]))
    },
    delNode: function (n) {
        var t, i;
        if (this.$nodeData[n] && (this.onItemDel == null || this.onItemDel(n, "node"))) {
            for (t in this.$lineData) (this.$lineData[t].from == n || this.$lineData[t].to == n) && this.delLine(t);
            this.$undoStack && (i = [n, this.$nodeData[n]], this.pushOper("addNode", i));
            delete this.$nodeData[n];
            this.$nodeDom[n].remove();
            delete this.$nodeDom[n]; --this.$nodeCount;
            this.$focus == n && (this.$focus = "");
            this.$editable && n.indexOf(this.$id + "_node_") < 0 && (this.$deletedItem[n] = "node")
        }
    },
    setTitle: function (n) {
        this.$title = n;
        this.$head && this.$head.children("label").attr("title", n).text(n)
    },
    loadData: function (n) {
        var u, t, i, r;
        n == undefined && (n = "");
        u = this.$editable;
        this.$editable = !1;
        n.title && this.setTitle(n.title);
        n.initNum && (this.$max = n.initNum);
        for (t in n.nodes) this.addNode(n.nodes[t].id, n.nodes[t]);
        for (i in n.lines) this.addLine(n.lines[i].id, n.lines[i]);
        for (r in n.areas) this.addArea(n.areas[r].id, n.areas[r]);
        this.$editable = u;
        this.$deletedItem = {}
    },
    loadDataAjax: function (n) {
        var t = this;
        $.ajax({
            type: n.type,
            url: n.url,
            dataType: "json",
            data: n.data,
            success: function (i) {
                n.dataFilter && n.dataFilter(i, "json");
                t.loadData(i);
                n.success && n.success(i)
            },
            error: function (t, i, r) {
                n.error && n.error(i, r)
            }
        })
    },
    exportData: function () {
        var n = {
            title: this.$title,
            nodes: this.$nodeData,
            lines: this.$lineData,
            areas: this.$areaData,
            initNum: this.$max
        },
        r = [],
        u = [],
        t,
        i;
        for (t in n.nodes) n.nodes[t].marked || delete n.nodes[t].marked,
        n.nodes[t].id = t,
        r.push(n.nodes[t]);
        n.nodes = r;
        for (i in n.lines) n.lines[i].marked || delete n.lines[i].marked,
        n.lines[i].id = i,
        u.push(n.lines[i]);
        return n.lines = u,
        n
    },
    exportAlter: function () {
        var n = {
            nodes: {},
            lines: {},
            areas: {}
        },
        t,
        i,
        r;
        for (t in this.$nodeData) this.$nodeData[t].alt && (n.nodes[t] = this.$nodeData[t]);
        for (i in this.$lineData) this.$lineData[i].alt && (n.lines[i] = this.$lineData[i]);
        for (r in this.$areaData) this.$areaData[r].alt && (n.areas[r] = this.$areaData[r]);
        return n.deletedItem = this.$deletedItem,
        n
    },
    transNewId: function (n, t, i) {
        var r;
        switch (i) {
            case "node":
                this.$nodeData[n] && (r = this.$nodeData[n], delete this.$nodeData[n], this.$nodeData[t] = r);
                break;
            case "line":
                this.$lineData[n] && (r = this.$lineData[n], delete this.$lineData[n], this.$lineData[t] = r);
                break;
            case "area":
                this.$areaData[n] && (r = this.$areaData[n], delete this.$areaData[n], this.$areaData[t] = r)
        }
    },
    clearData: function () {
        var n;
        for (n in this.$nodeData) this.delNode(n);
        for (n in this.$lineData) this.delLine(n);
        for (n in this.$areaData) this.delArea(n);
        this.$deletedItem = {}
    },
    destrory: function () {
        this.$bgDiv.empty();
        this.$lineData = null;
        this.$nodeData = null;
        this.$lineDom = null;
        this.$nodeDom = null;
        this.$areaDom = null;
        this.$areaData = null;
        this.$nodeCount = 0;
        this.$areaCount = 0;
        this.$areaCount = 0;
        this.$deletedItem = {}
    },
    drawLine: function (n, t, i, r, u) {
        var f, s, e, o, h, c;
        return GooFlow.prototype.useSVG != "" ? (f = document.createElementNS("http://www.w3.org/2000/svg", "g"), s = document.createElementNS("http://www.w3.org/2000/svg", "path"), e = document.createElementNS("http://www.w3.org/2000/svg", "path"), n != "" && f.setAttribute("id", n), f.setAttribute("from", t[0] + "," + t[1]), f.setAttribute("to", i[0] + "," + i[1]), s.setAttribute("visibility", "hidden"), s.setAttribute("stroke-width", 9), s.setAttribute("fill", "none"), s.setAttribute("stroke", "white"), s.setAttribute("d", "M " + t[0] + " " + t[1] + " L " + i[0] + " " + i[1]), s.setAttribute("pointer-events", "stroke"), e.setAttribute("d", "M " + t[0] + " " + t[1] + " L " + i[0] + " " + i[1]), e.setAttribute("stroke-width", 2), e.setAttribute("stroke-linecap", "round"), e.setAttribute("fill", "none"), u && e.setAttribute("style", "stroke-dasharray:6,5"), r ? (e.setAttribute("stroke", "#ff3300"), e.setAttribute("marker-end", "url(#arrow2)")) : (e.setAttribute("stroke", "gray"), e.setAttribute("marker-end", "url(#arrow1)")), f.appendChild(s), f.appendChild(e), f.style.cursor = "crosshair", n != "" && n != "GooFlow_tmp_line" && (o = document.createElementNS("http://www.w3.org/2000/svg", "text"), f.appendChild(o), h = (i[0] + t[0]) / 2, c = (i[1] + t[1]) / 2, o.setAttribute("text-anchor", "middle"), o.setAttribute("x", h), o.setAttribute("y", c - 5), f.style.cursor = "pointer", o.style.cursor = "text")) : (f = document.createElement("v:polyline"), n != "" && (f.id = n), f.points.value = t[0] + "," + t[1] + " " + i[0] + "," + i[1], f.setAttribute("fromTo", t[0] + "," + t[1] + "," + i[0] + "," + i[1]), f.strokeWeight = "1.2", f.stroke.EndArrow = "Block", f.style.cursor = "crosshair", n != "" && n != "GooFlow_tmp_line" && (o = document.createElement("div"), f.appendChild(o), h = (i[0] - t[0]) / 2, c = (i[1] - t[1]) / 2, h < 0 && (h = h * -1), c < 0 && (c = c * -1), o.style.left = h + "px", o.style.top = c - 6 + "px", f.style.cursor = "pointer"), u && (f.stroke.dashstyle = "Dash"), f.strokeColor = r ? "#ff3300" : "gray"),
        f
    },
    drawPoly: function (n, t, i, r, u, f) {
        var e, o, c, h, s, l, a;
        return GooFlow.prototype.useSVG != "" ? (e = document.createElementNS("http://www.w3.org/2000/svg", "g"), c = document.createElementNS("http://www.w3.org/2000/svg", "path"), h = document.createElementNS("http://www.w3.org/2000/svg", "path"), n != "" && e.setAttribute("id", n), e.setAttribute("from", t[0] + "," + t[1]), e.setAttribute("to", u[0] + "," + u[1]), c.setAttribute("visibility", "hidden"), c.setAttribute("stroke-width", 9), c.setAttribute("fill", "none"), c.setAttribute("stroke", "white"), o = "M " + t[0] + " " + t[1], (i[0] != t[0] || i[1] != t[1]) && (o += " L " + i[0] + " " + i[1]), (r[0] != u[0] || r[1] != u[1]) && (o += " L " + r[0] + " " + r[1]), o += " L " + u[0] + " " + u[1], c.setAttribute("d", o), c.setAttribute("pointer-events", "stroke"), h.setAttribute("d", o), h.setAttribute("stroke-width", 2), h.setAttribute("stroke-linecap", "round"), h.setAttribute("fill", "none"), f ? (h.setAttribute("stroke", "#ff3300"), h.setAttribute("marker-end", "url(#arrow2)")) : (h.setAttribute("stroke", "gray"), h.setAttribute("marker-end", "url(#arrow1)")), e.appendChild(c), e.appendChild(h), s = document.createElementNS("http://www.w3.org/2000/svg", "text"), e.appendChild(s), l = (r[0] + i[0]) / 2, a = (r[1] + i[1]) / 2, s.setAttribute("text-anchor", "middle"), s.setAttribute("x", l), s.setAttribute("y", a - 5), s.style.cursor = "text", e.style.cursor = "pointer") : (e = document.createElement("v:Polyline"), n != "" && (e.id = n), e.filled = "false", o = t[0] + "," + t[1], (i[0] != t[0] || i[1] != t[1]) && (o += " " + i[0] + "," + i[1]), (r[0] != u[0] || r[1] != u[1]) && (o += " " + r[0] + "," + r[1]), o += " " + u[0] + "," + u[1], e.points.value = o, e.setAttribute("fromTo", t[0] + "," + t[1] + "," + u[0] + "," + u[1]), e.strokeWeight = "1.2", e.stroke.EndArrow = "Block", s = document.createElement("div"), e.appendChild(s), l = (r[0] - i[0]) / 2, a = (r[1] - i[1]) / 2, l < 0 && (l = l * -1), a < 0 && (a = a * -1), s.style.left = l + "px", s.style.top = a - 4 + "px", e.style.cursor = "pointer", e.strokeColor = f ? "#ff3300" : "gray"),
        e
    },
    calcStartEnd: function (n, t) {
        var i, r, a, v, s = n.left,
        u = n.left + n.width,
        h = t.left,
        f = t.left + t.width;
        s >= f ? (i = s, a = f) : u <= h ? (i = u, a = h) : s <= h && u >= h && u <= f ? (i = (u + h) / 2, a = i) : s >= h && u <= f ? (i = (s + u) / 2, a = i) : h >= s && f <= u ? (i = (h + f) / 2, a = i) : s <= f && u >= f && (i = (s + f) / 2, a = i);
        var c = n.top,
        e = n.top + n.height,
        l = t.top,
        o = t.top + t.height;
        return c >= o ? (r = c, v = o) : e <= l ? (r = e, v = l) : c <= l && e >= l && e <= o ? (r = (e + l) / 2, v = r) : c >= l && e <= o ? (r = (c + e) / 2, v = r) : l >= c && o <= e ? (r = (l + o) / 2, v = r) : c <= o && e >= o && (r = (c + o) / 2, v = r),
        {
            start: [i, r],
            end: [a, v]
        }
    },
    calcPolyPoints: function (n, t, i, r) {
        var e = {
            x: n.left + n.width / 2,
            y: n.top + n.height / 2
        },
        o = {
            x: t.left + t.width / 2,
            y: t.top + t.height / 2
        },
        s = [],
        u = [],
        f = [],
        h = [];
        return s = [e.x, e.y],
        h = [o.x, o.y],
        i == "lr" ? (u = [r, e.y], f = [r, o.y], u[0] > n.left && u[0] < n.left + n.width ? (u[1] = e.y > o.y ? n.top : n.top + n.height, s[0] = u[0], s[1] = u[1]) : s[0] = u[0] < n.left ? n.left : n.left + n.width, f[0] > t.left && f[0] < t.left + t.width ? (f[1] = e.y > o.y ? t.top + t.height : t.top, h[0] = f[0], h[1] = f[1]) : h[0] = f[0] < t.left ? t.left : t.left + t.width) : i == "tb" && (u = [e.x, r], f = [o.x, r], u[1] > n.top && u[1] < n.top + n.height ? (u[0] = e.x > o.x ? n.left : n.left + n.width, s[0] = u[0], s[1] = u[1]) : s[1] = u[1] < n.top ? n.top : n.top + n.height, f[1] > t.top && f[1] < t.top + t.height ? (f[0] = e.x > o.x ? t.left + t.width : t.left, h[0] = f[0], h[1] = f[1]) : h[1] = f[1] < t.top ? t.top : t.top + t.height),
        {
            start: s,
            m1: u,
            m2: f,
            end: h
        }
    },
    getMValue: function (n, t, i) {
        return i == "lr" ? (n.left + n.width / 2 + t.left + t.width / 2) / 2 : i == "tb" ? (n.top + n.height / 2 + t.top + t.height / 2) / 2 : void 0
    },
    addLine: function (n, t) {
        var e, u, f, i, r;
        if ((this.onItemAdd == null || this.onItemAdd(n, "line", t)) && (this.$undoStack && this.$editable && this.pushOper("delLine", [n]), u = null, f = null, t.from != t.to)) {
            for (e in this.$lineData) if (t.from == this.$lineData[e].from && t.to == this.$lineData[e].to) return; (u = this.$nodeData[t.from], f = this.$nodeData[t.to], u && f) && (i = t.type && t.type != "sl" ? GooFlow.prototype.calcPolyPoints(u, f, t.type, t.M) : GooFlow.prototype.calcStartEnd(u, f), i) && (this.$lineData[n] = {},
            this.$lineData[n].setInfo = t.setInfo, this.$lineData[n].id = t.id, t.type ? (this.$lineData[n].type = t.type, this.$lineData[n].M = t.M) : this.$lineData[n].type = "sl", this.$lineData[n].from = t.from, this.$lineData[n].to = t.to, this.$lineData[n].name = t.name, this.$lineData[n].marked = t.mark ? t.mark : !1, this.$lineDom[n] = this.$lineData[n].type == "sl" ? GooFlow.prototype.drawLine(n, i.start, i.end, t.mark) : GooFlow.prototype.drawPoly(n, i.start, i.m1, i.m2, i.end, t.mark), this.$draw.appendChild(this.$lineDom[n]), GooFlow.prototype.useSVG == "" ? (this.$lineDom[n].childNodes[1].innerHTML = t.name, this.$lineData[n].type != "sl" ? (r = i.start[0] > i.end[0] ? i.end[0] : i.start[0], r > i.m2[0] && (r = i.m2[0]), r > i.m1[0] && (r = i.m1[0]), this.$lineDom[n].childNodes[1].style.left = (i.m2[0] + i.m1[0]) / 2 - r - this.$lineDom[n].childNodes[1].offsetWidth / 2 + 4, r = i.start[1] > i.end[1] ? i.end[1] : i.start[1], r > i.m2[1] && (r = i.m2[1]), r > i.m1[1] && (r = i.m1[1]), this.$lineDom[n].childNodes[1].style.top = (i.m2[1] + i.m1[1]) / 2 - r - this.$lineDom[n].childNodes[1].offsetHeight / 2) : this.$lineDom[n].childNodes[1].style.left = ((i.end[0] - i.start[0]) * (i.end[0] > i.start[0] ? 1 : -1) - this.$lineDom[n].childNodes[1].offsetWidth) / 2 + 4) : this.$lineDom[n].childNodes[2].textContent = t.name, ++this.$lineCount, this.$editable && (this.$lineData[n].alt = !0, this.$deletedItem[n] && delete this.$deletedItem[n]))
        }
    },
    resetLines: function (n, t) {
        var r, f, i, u;
        for (r in this.$lineData) {
            if (f = null, this.$lineData[r].from == n) {
                if (f = this.$nodeData[this.$lineData[r].to] || null, f == null) continue;
                if (i = this.$lineData[r].type == "sl" ? GooFlow.prototype.calcStartEnd(t, f) : GooFlow.prototype.calcPolyPoints(t, f, this.$lineData[r].type, this.$lineData[r].M), !i) break
            } else if (this.$lineData[r].to == n) {
                if (f = this.$nodeData[this.$lineData[r].from] || null, f == null) continue;
                if (i = this.$lineData[r].type == "sl" ? GooFlow.prototype.calcStartEnd(f, t) : GooFlow.prototype.calcPolyPoints(f, t, this.$lineData[r].type, this.$lineData[r].M), !i) break
            }
            f != null && (this.$draw.removeChild(this.$lineDom[r]), this.$lineDom[r] = this.$lineData[r].type == "sl" ? GooFlow.prototype.drawLine(r, i.start, i.end, this.$lineData[r].marked) : GooFlow.prototype.drawPoly(r, i.start, i.m1, i.m2, i.end, this.$lineData[r].marked), this.$draw.appendChild(this.$lineDom[r]), GooFlow.prototype.useSVG == "" ? (this.$lineDom[r].childNodes[1].innerHTML = this.$lineData[r].name, this.$lineData[r].type != "sl" ? (u = i.start[0] > i.end[0] ? i.end[0] : i.start[0], u > i.m2[0] && (u = i.m2[0]), u > i.m1[0] && (u = i.m1[0]), this.$lineDom[r].childNodes[1].style.left = (i.m2[0] + i.m1[0]) / 2 - u - this.$lineDom[r].childNodes[1].offsetWidth / 2 + 4, u = i.start[1] > i.end[1] ? i.end[1] : i.start[1], u > i.m2[1] && (u = i.m2[1]), u > i.m1[1] && (u = i.m1[1]), this.$lineDom[r].childNodes[1].style.top = (i.m2[1] + i.m1[1]) / 2 - u - this.$lineDom[r].childNodes[1].offsetHeight / 2 - 4) : this.$lineDom[r].childNodes[1].style.left = ((i.end[0] - i.start[0]) * (i.end[0] > i.start[0] ? 1 : -1) - this.$lineDom[r].childNodes[1].offsetWidth) / 2 + 4) : this.$lineDom[r].childNodes[2].textContent = this.$lineData[r].name)
        }
    },
    setLineType: function (n, t) {
        var f, e, r, u, i;
        if (!t || t == null || t == "" || t == this.$lineData[n].type) return !1;
        if (this.onLineSetType == null || this.onLineSetType(n, t)) {
            if (this.$undoStack && (f = [n, this.$lineData[n].type], this.pushOper("setLineType", f), this.$lineData[n].type != "sl" && (e = [n, this.$lineData[n].M], this.pushOper("setLineM", e))), r = this.$lineData[n].from, u = this.$lineData[n].to, this.$lineData[n].type = t, t != "sl") i = GooFlow.prototype.calcPolyPoints(this.$nodeData[r], this.$nodeData[u], this.$lineData[n].type, this.$lineData[n].M),
            this.setLineM(n, this.getMValue(this.$nodeData[r], this.$nodeData[u], t), !0);
            else {
                if (delete this.$lineData[n].M, this.$lineMove.hide().removeData("type").removeData("tid"), i = GooFlow.prototype.calcStartEnd(this.$nodeData[r], this.$nodeData[u]), !i) return;
                this.$draw.removeChild(this.$lineDom[n]);
                this.$lineDom[n] = GooFlow.prototype.drawLine(n, i.start, i.end, this.$lineData[n].marked || this.$focus == n);
                this.$draw.appendChild(this.$lineDom[n]);
                GooFlow.prototype.useSVG == "" ? (this.$lineDom[n].childNodes[1].innerHTML = this.$lineData[n].name, this.$lineDom[n].childNodes[1].style.left = ((i.end[0] - i.start[0]) * (i.end[0] > i.start[0] ? 1 : -1) - this.$lineDom[n].childNodes[1].offsetWidth) / 2 + 4) : this.$lineDom[n].childNodes[2].textContent = this.$lineData[n].name
            }
            this.$focus == n && this.focusItem(n);
            this.$editable && (this.$lineData[n].alt = !0)
        }
    },
    setLineM: function (n, t, i) {
        var f, e, o, r, u;
        if (!this.$lineData[n] || t < 0 || !this.$lineData[n].type || this.$lineData[n].type == "sl" || this.onLineMove != null && !this.onLineMove(n, t)) return !1;
        this.$undoStack && !i && (f = [n, this.$lineData[n].M], this.pushOper("setLineM", f));
        e = this.$lineData[n].from;
        o = this.$lineData[n].to;
        this.$lineData[n].M = t;
        r = GooFlow.prototype.calcPolyPoints(this.$nodeData[e], this.$nodeData[o], this.$lineData[n].type, this.$lineData[n].M);
        this.$draw.removeChild(this.$lineDom[n]);
        this.$lineDom[n] = GooFlow.prototype.drawPoly(n, r.start, r.m1, r.m2, r.end, this.$lineData[n].marked || this.$focus == n);
        this.$draw.appendChild(this.$lineDom[n]);
        GooFlow.prototype.useSVG == "" ? (this.$lineDom[n].childNodes[1].innerHTML = this.$lineData[n].name, u = r.start[0] > r.end[0] ? r.end[0] : r.start[0], u > r.m2[0] && (u = r.m2[0]), u > r.m1[0] && (u = r.m1[0]), this.$lineDom[n].childNodes[1].style.left = (r.m2[0] + r.m1[0]) / 2 - u - this.$lineDom[n].childNodes[1].offsetWidth / 2 + 4, u = r.start[1] > r.end[1] ? r.end[1] : r.start[1], u > r.m2[1] && (u = r.m2[1]), u > r.m1[1] && (u = r.m1[1]), this.$lineDom[n].childNodes[1].style.top = (r.m2[1] + r.m1[1]) / 2 - u - this.$lineDom[n].childNodes[1].offsetHeight / 2 - 4) : this.$lineDom[n].childNodes[2].textContent = this.$lineData[n].name;
        this.$editable && (this.$lineData[n].alt = !0)
    },
    delLine: function (n) {
        if (this.$lineData[n] && (this.onItemDel == null || this.onItemDel(n, "node"))) {
            if (this.$undoStack) {
                var t = [n, this.$lineData[n]];
                this.pushOper("addLine", t)
            }
            this.$draw.removeChild(this.$lineDom[n]);
            delete this.$lineData[n];
            delete this.$lineDom[n];
            this.$focus == n && (this.$focus = ""); --this.$lineCount;
            this.$editable && n.indexOf(this.$id + "_line_") < 0 && (this.$deletedItem[n] = "line");
            this.$lineOper.hide()
        }
    },
    markItem: function (n, t, i) {
        if (t == "node") {
            if (!this.$nodeData[n]) return;
            if (this.onItemMark != null && !this.onItemMark(n, "node", i)) return;
            this.$nodeData[n].marked = i || !1;
            i ? this.$nodeDom[n].addClass("item_mark") : this.$nodeDom[n].removeClass("item_mark")
        } else if (t == "line") {
            if (!this.$lineData[n]) return;
            if (this.onItemMark != null && !this.onItemMark(n, "line", i)) return;
            this.$lineData[n].marked = i || !1;
            GooFlow.prototype.useSVG != "" ? i ? (this.$nodeDom[n].childNodes[1].setAttribute("stroke", "#ff3300"), this.$nodeDom[n].childNodes[1].setAttribute("marker-end", "url(#arrow2)")) : (this.$nodeDom[n].childNodes[1].setAttribute("stroke", "gray"), this.$nodeDom[n].childNodes[1].setAttribute("marker-end", "url(#arrow1)")) : this.$nodeDom[n].strokeColor = i ? "#ff3300" : "gray"
        }
        if (this.$undoStatck) {
            var r = [n, t, !i];
            this.pushOper("markItem", r)
        }
    },
    moveArea: function (n, t, i) {
        if (this.$areaData[n] && (this.onItemMove == null || this.onItemMove(n, "area", t, i))) {
            if (this.$undoStack) {
                var r = [n, this.$areaData[n].left, this.$areaData[n].top];
                this.pushOper("moveNode", r)
            }
            t < 0 && (t = 0);
            i < 0 && (i = 0);
            $("#" + n).css({
                left: t + "px",
                top: i + "px"
            });
            this.$areaData[n].left = t;
            this.$areaData[n].top = i;
            this.$editable && (this.$areaData[n].alt = !0)
        }
    },
    delArea: function (n) {
        if (this.$areaData[n]) {
            if (this.$undoStack) {
                var t = [n, this.$areaData[n]];
                this.pushOper("addArea", t)
            } (this.onItemDel == null || this.onItemDel(n, "node")) && (delete this.$areaData[n], this.$areaDom[n].remove(), delete this.$areaDom[n], --this.$areaCount, this.$editable && n.indexOf(this.$id + "_area_") < 0 && (this.$deletedItem[n] = "area"))
        }
    },
    setAreaColor: function (n, t) {
        if (this.$areaData[n]) {
            if (this.$undoStack) {
                var i = [n, this.$areaData[n].color];
                this.pushOper("setAreaColor", i)
            } (t == "red" || t == "yellow" || t == "blue" || t == "green") && (this.$areaDom[n].removeClass("area_" + this.$areaData[n].color).addClass("area_" + t), this.$areaData[n].color = t);
            this.$editable && (this.$areaData[n].alt = !0)
        }
    },
    resizeArea: function (n, t, i) {
        var r, u;
        this.$areaData[n] && (this.onItemResize == null || this.onItemResize(n, "area", t, i)) && (this.$undoStack && (r = [n, this.$areaData[n].width, this.$areaData[n].height], this.pushOper("resizeArea", r)), u = 0, navigator.userAgent.indexOf("8.0") != -1 && (u = 2), this.$areaDom[n].children(".bg").css({
            width: t - 2 + "px",
            height: i - 2 + "px"
        }), t = this.$areaDom[n].outerWidth(), i = this.$areaDom[n].outerHeight(), this.$areaDom[n].children("bg").css({
            width: t - 2 + "px",
            height: i - 2 + "px"
        }), this.$areaData[n].width = t, this.$areaData[n].height = i, this.$editable && (this.$areaData[n].alt = !0))
    },
    addArea: function (n, t) {
        (this.onItemAdd == null || this.onItemAdd(n, "area", t)) && (this.$undoStack && this.$editable && this.pushOper("delArea", [n]), this.$areaDom[n] = $("<div id='" + n + "' class='GooFlow_area area_" + t.color + "' style='top:" + t.top + "px;left:" + t.left + "px'><div class='bg' style='width:" + (t.width - 2) + "px;height:" + (t.height - 2) + "px'><\/div><label>" + t.name + "<\/label><b><\/b><div><div class='rs_bottom'><\/div><div class='rs_right'><\/div><div class='rs_rb'><\/div><div class='rs_close'><\/div><\/div><\/div>"), this.$areaData[n] = t, this.$group.append(this.$areaDom[n]), this.$nowType != "group" && this.$areaDom[n].children("div:eq(1)").css("display", "none"), ++this.$areaCount, this.$editable && (this.$areaData[n].alt = !0, this.$deletedItem[n] && delete this.$deletedItem[n]))
    },
    reinitSize: function (n, t) {
        var r = (n || 800) - 2,
        i = (t || 500) - 2,
        u,
        f;
        this.$bgDiv.css({
            height: i + "px",
            width: r + "px"
        });
        u = 0;
        f = 10;
        this.$head != null && (u = 24, f = 7);
        this.$tool != null && this.$tool.css({
            height: i - u - f + "px"
        });
        r -= 39;
        i = i - u - (this.$head != null ? 5 : 8);
        this.$workArea.parent().css({
            height: i + "px",
            width: r + "px"
        });
        this.$workArea.css({
            height: i * 3 + "px",
            width: r * 3 + "px"
        });
        GooFlow.prototype.useSVG == "" && (this.$draw.coordsize = r * 3 + "," + i * 3);
        this.$draw.style.width = r * 3 + "px";
        this.$draw.style.height = +i * 3 + "px";
        this.$group == null && this.$group.css({
            height: i * 3 + "px",
            width: r * 3 + "px"
        })
    }
};
jQuery.extend({
    createGooFlow: function (n, t) {
        return new GooFlow(n, t)
    }
}),
function (n) {
    n.fn.flowDesigner = function (t) {
        function e() {
            var f = {},
            e, r, u;
           
            var opt = t.frmData;
            var data = [];
            //console.log(opt);
            if (opt.type == 2) {
                data = opt.data.fields;
            }
            else {
                for(a in opt.data)
                {
                    s = opt.data[a];
                    for (b in s.fields)
                    {
                        data.push(s.fields[b]);
                    }
                    
                }
            }
            //console.log(data);
            for (e in data) r = data[e],
            f[r.field] = r.label;
            //console.log(f);
            u = {};
            n.ajax({
                url: "../../SystemManage/DataBaseLink/GetListJson",
                type: "get",
                dataType: "json",
                async: !1,
                success: function (n) {
                    for (var t in n) u[n[t].DatabaseLinkId] = n[t].DBAlias
                }
            });
            var o = {
                "0": "前一步",
                "1": "第一步",
                "2": "某一步",
                "3": "用户指定",
                "4": "不处理"
            },
            s= { "NodeDesignateType1": "所有成员", "NodeDesignateType2": "指定成员", "NodeDesignateType3": "发起者领导", "NodeDesignateType4": "前一步骤领导", "NodeDesignateType5": "发起者部门领导", "NodeDesignateType6": "发起者公司领导" },
            h = {
                "0": "所有步骤通过",
                "1": "一个步骤通过即可",
                "2": "按百分比计算"
            };
            n.each(t.schemeContent.nodes,
            function (n, t) {
                var r, e, c, v, n, l, a;
                if ( t.type != "startround" && t.type != "endround" && t.setInfo != undefined) {
                    if (r = "", r += '<div class="flow-portal-panel-title"><i class="fa fa-navicon"><\/i>&nbsp;&nbsp;基本信息<\/div>', r += "<ul>", r += "<li>节点标识:" + t.setInfo.NodeCode + "<\/li>", r += "<li>驳回类型:" + o[t.setInfo.NodeRejectType] + "<\/li>", t.setInfo.Description != "" && (r += "<li>备注:" + t.setInfo.Description + "<\/li>"), t.setInfo.NodeConfluenceType != undefined && t.setInfo.NodeConfluenceType != "" && (r += "<li>会签策略:" + h[t.setInfo.NodeConfluenceType] + "<\/li>", t.setInfo.NodeConfluenceType == 2 && (r += "<li>会签比例:" + t.setInfo.nodeConfluenceRate + "<\/li>")), r += "<\/ul>", r += '<div class="flow-portal-panel-title"><i class="fa fa-navicon"><\/i>&nbsp;&nbsp;审核者<\/div>', r += "<ul>", r += "<li>类型:" + s[t.setInfo.NodeDesignate] + "<\/li>", t.setInfo.NodeDesignateData != undefined) {
                        e = "";
                        for (n in t.setInfo.NodeDesignateData.role) c = t.setInfo.NodeDesignateData.role[n],
                        e += ' <span class="label label-success">' + top.clientroleData[c].FullName + "<\/span>",
                        n == t.setInfo.NodeDesignateData.role.length - 1 && (r += "<li>角色:" + e + "<\/li>");
                        e = "";
                        for (n in t.setInfo.NodeDesignateData.post) c = t.setInfo.NodeDesignateData.post[n],
                        e += ' <span class="label label-info">' + top.clientpostData[c].FullName + "<\/span>",
                        n == t.setInfo.NodeDesignateData.post.length - 1 && (r += "<li>岗位:" + e + "<\/li>");
                        e = "";
                        for (n in t.setInfo.NodeDesignateData.usergroup) c = t.setInfo.NodeDesignateData.usergroup[n],
                        e += ' <span class="label label-warning">' + top.clientuserGroup[c].FullName + "<\/span>",
                        n == t.setInfo.NodeDesignateData.usergroup.length - 1 && (r += "<li>用户组:" + e + "<\/li>");
                        e = "";
                        for (n in t.setInfo.NodeDesignateData.user) c = t.setInfo.NodeDesignateData.user[n],
                        e += ' <span class="label label-danger">' + top.clientuserData[c].RealName + "<\/span>",
                        n == t.setInfo.NodeDesignateData.user.length - 1 && (r += "<li>用户:" + e + "<\/li>")
                    }
                    r += "<\/ul>";
                    v = "";
                    for (n in t.setInfo.frmPermissionInfo) l = t.setInfo.frmPermissionInfo[n],
                    a = "",
                    l.down ? a = " | 可下载" : l.down != undefined && (a = " | 不可下载"),
                    v += "<li>" + f[l.fieldid] + ": " + (l.look ? "可查看" : "不可查看") + a + "<\/li>",
                    n == t.setInfo.frmPermissionInfo.length - 1 && (r += '<div class="flow-portal-panel-title"><i class="fa fa-navicon"><\/i>&nbsp;&nbsp;权限分配<\/div>', r += "<ul>", r += v, r += "<\/ul>"); (t.setInfo.NodeDataBaseToSQL != "" || t.setInfo.NodeSQL != "") && (r += '<div class="flow-portal-panel-title"><i class="fa fa-navicon"><\/i>&nbsp;&nbsp;执行SQL<\/div>', r += "<ul>", r += "<li>数据库:" + u[t.setInfo.NodeDataBaseToSQL] + "<\/li>", r += "<li>SQL语句:" + t.setInfo.NodeSQL + "<\/li>", r += "<\/ul>");
                    i.find("#" + t.id).attr("title", t.name);
                    i.find("#" + t.id).attr("data-toggle", "popover");
                    i.find("#" + t.id).attr("data-placement", "bottom");
                    i.find("#" + t.id).attr("data-content", r)
                }
            });
            i.find(".GooFlow_item").popover({
                html: !0
            })
        }
        function u(n) {
            i.find(".flow-labellingnode-red").removeClass("flow-labellingnode-red");
            i.find("#" + n).addClass("flow-labellingnode-red")
        }
        var i = n(this),
        t,
        r,
        f;
        return i.attr("id") ? (i.html(""), t = n.extend({
            schemeContent: "",
            frmType: 0,
            frmData: "",
            width: n(window).width(),
            height: n(window).height() + 2,
            OpenNode: function () {
                return !1
            },
            OpenLine: function () {
                return !1
            },
            NodeRemarks: {
                cursor: "选择指针",
                direct: "步骤连线",
                startround: "开始节点",
                endround: "结束节点",
                stepnode: "普通节点",
                shuntnode: "分流节点",
                confluencenode: "合流节点",
                group: "区域规划"
            },
            haveTool: !0,
            toolBtns: ["startround", "endround", "stepnode", "shuntnode", "confluencenode"],
            isprocessing: !1,
            nodeData: null,
            activityId: "",
            preview: 0
        },
        t), r = n.createGooFlow(i, {
            width: t.width,
            height: t.height,
            haveHead: !0,
            headBtns: ["undo", "redo"],
            haveTool: t.haveTool,
            toolBtns: t.toolBtns,
            haveGroup: !0,
            useOperStack: !0
        }), r.setNodeRemarks(t.NodeRemarks), r.loadData(t.schemeContent), OpenNode = t.OpenNode, OpenLine = t.OpenLine, r.exportDataEx = function () {
            var n = r.exportData(),
            i = {},
            s = {},
            e = {},
            l = [],
            v = [],
            y = 0,
            p = 0,
            f,
            w,
            t,
            c,
            b,
            o,
            a,
            k,
            h,
            d;
            for (f in n.lines) i[n.lines[f].from] == undefined && (i[n.lines[f].from] = []),
            i[n.lines[f].from].push(n.lines[f].to),
            s[n.lines[f].to] == undefined && (s[n.lines[f].to] = []),
            s[n.lines[f].to].push(n.lines[f].from);
            for (w in n.nodes) {
                t = n.nodes[w];
                c = !1;
                switch (t.type) {
                    case "startround":
                        if (y++, i[t.id] == undefined) return dialogTop("开始节点无法流转到下一个节点", "error"),
                        -1;
                        break;
                    case "endround":
                        if (p++, s[t.id] == undefined) return dialogTop("无法流转到结束节点", "error"),
                        -1;
                        break;
                    case "stepnode":
                        c = !0;
                        break;
                    case "shuntnode":
                        c = !0;
                        l.push(t.id);
                        break;
                    case "confluencenode":
                        v.push(t.id);
                        c = !0;
                        break;
                    default:
                        return dialogTop("节点数据异常,请重新登录下系统！", "error"),
                        -1
                }
                if (c) {
                    if (s[t.id] == undefined) return u(t.id),
                    dialogTop("标注红色的节点没有【进来】的连接线段", "error"),
                    -1;
                    if (i[t.id] == undefined) return u(t.id),
                    dialogTop("标注红色的节点没有【出去】的连接线段", "error"),
                    -1
                }
                e[t.id] = t
            }
            if (y == 0) return dialogTop("必须有开始节点", "error"),
            -1;
            if (p == 0) return dialogTop("必须有结束节点", "error"),
            -1;
            if (l.length != v.length) return dialogTop("分流节点必须等于合流节点", "error"),
            -1;
            for (b in l) {
                if (o = l[b], i[o].length == 1) return u(o),
                dialogTop("标注红色的分流节点不允许只有一条【出去】的线段", "error"),
                -1;
                a = {};
                for (k in i[o]) if (btoNode = i[o][k], e[btoNode].type == "stepnode") {
                    if (h = i[e[btoNode].id], d = e[h[0]], d.type != "confluencenode") return u(e[btoNode].id),
                    dialogTop("标注红色的普通节点下一个节点必须是合流节点", "error"),
                    -1;
                    if (a[h[0]] = 0, a.length > 1) return u(o),
                    dialogTop("标注红色的分流节点与之对应的合流节点只能有一个", "error"),
                    -1;
                    if (s[h[0]].length != i[o].length) return u(h[0]),
                    dialogTop("标注红色的合流节点与之对应的分流节点只能有一个", "error"),
                    -1;
                    if (h.length > 1) return u(e[btoNode].id),
                    dialogTop("标注红色的节点只能有一条出去的线条【分流合流之间】", "error"),
                    -1;
                    if (s[e[btoNode].id], length > 1) return u(e[btoNode].id),
                    dialogTop("标注红色的节点只能有一条进来的线条【分流合流之间】", "error"),
                    -1
                } else return u(o),
                dialogTop("标注红色的分流节点必须经过一个普通节点到合流节点", "error"),
                -1
            }
            return n
        },
        r.SetNodeEx = function (n, t) {
            r.setName(n, t.nodeMyName, "node", t)
        },
        r.SetLineEx = function (n, t) {
            r.setName(n, t.lineMyName, "line", t)
        },
        t.isprocessing && (f = '<div style="position:absolute;left:10px;margin-top: 10px;padding:10px;border-radius:5px;background:rgba(0,0,0,0.05);z-index:1000;display:inline-block;">', f += '<div style="display: inline-block;"><i style="padding-right:5px;color:#5cb85c;" class="fa fa-flag"><\/i><span>已处理<\/span><\/div>', f += '<div style="display: inline-block;margin-left: 10px;"><i style="padding-right:5px;color:#5bc0de;" class="fa fa-flag"><\/i><span>正在处理<\/span><\/div>', f += '<div style="display: inline-block;margin-left: 10px;"><i style="padding-right:5px;color:#d9534f;" class="fa fa-flag"><\/i><span>不通过<\/span><\/div>', f += '<div style="display: inline-block;margin-left: 10px;"><i style="padding-right:5px;color:#f0ad4e;" class="fa fa-flag"><\/i><span>驳回<\/span><\/div>', f += '<div style="display: inline-block;margin-left: 10px;"><i style="padding-right:5px;color:#999;" class="fa fa-flag"><\/i><span>未处理<\/span><\/div><\/div>', i.find(".GooFlow_work .GooFlow_work_inner").css("background-image", "none"), i.find("td").css("color", "#fff"), i.css("background", "#fff"), i.find(".ico").remove(), i.find(".GooFlow_item").css("border", "0px"), i.append(f), i.find(".GooFlow_item.stepnode").css("background", "#999"), n.each(t.nodeData,
        function (n, item) {
            i.find("#" + item.id).css("background", "#999");
            if (item.type == "startround") {
                i.find("#" + item.id).css("background", "#5cb85c");
            }
            else {
                if (item.id == t.activityId) {
                    i.find("#" + item.id).css("background", "#5bc0de");//正在处理
                }
                if (item.setInfo != undefined && item.setInfo.Taged != undefined) {
                    if (item.setInfo.Taged == -1) {
                        i.find("#" + item.id).css("background", "#d9534f");//不通过
                    }
                    else if (item.setInfo.Taged == 1) {
                        i.find("#" + item.id).css("background", "#5cb85c");//通过
                    }
                    else {
                        i.find("#" + item.id).css("background", "#f0ad4e");//驳回
                    }
                }
            }
            if (item.setInfo != undefined && item.setInfo.Taged != undefined) {
                var _one = top.clientuserData[item.setInfo.UserId];
                var _row = '<div style="text-align:left">';
                var tagname = { "-1": "不通过", "1": "通过", "0": "驳回" };
                _row += "<p>处理人：" + (_one == undefined ? item.setInfo.UserId : _one.RealName) + "</p>";
                _row += "<p>结果：" + tagname[item.setInfo.Taged] + "</p>";
                _row += "<p>处理时间：" + item.setInfo.TagedTime + "</p>";
                _row += "<p>备注：" + item.setInfo.description + "</p></div>";

                i.find('#' + item.id).attr('data-toggle', 'tooltip');
                i.find('#' + item.id).attr('data-placement', 'bottom');
                i.find('#' + item.id).attr('title', _row);
            }
        }), n('[data-toggle="tooltip"]').tooltip({
            html: !0
        })), t.preview == 1 && e(), r) : !1
    }
}(window.jQuery)