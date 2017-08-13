(function (n) {
    "use strict";
    var r, i = {
        success: [],
        fail: []
    },
    u = [],
    t = {
        init: function () {
            //var e = top.$.cookie("currentmoduleId"),
       
            //o = learun.request("btnId"); 
            //if (r = top.learun.data.get(["excelImportTemplate", e, "entitys", o, "data"]), console.log(r), !r) return learun.dialogTop({
            //    msg: "获取不到模板数据",
            //    type: "error"
            //}),
            //!1;
            $.SetForm({
                url: "../../SystemManage/ExcelImport/GetFormJsonByModuleId",
                param: { },
                success: function (data) {
                   
                    r = data;
                    console.log(r);
                }
            });
            var i = n(".nav-tabs"),
            f = "";
            f += '<li class="' + (n == 0 ? "active" : "") + '"   data-value="' + n + '"><a >' + r.templateInfo.F_Name + "<\/a><\/li>"
            //n.each(r,
            //function (n, t) {
            //    console.log(t);
            //    f += '<li class="' + (n == 0 ? "active" : "") + '"   data-value="' + n + '"><a >' + t.templateInfo.F_Name + "<\/a><\/li>"
            //});
            t.initTemplate(0);
            i.html(f);
            i.find("li").on("click", t.clickTabs);
            n("#btn_close").on("click",
            function () {
                learun.dialogClose()
            });
            n("#girdPager .prev").on("click",
            function () {
                var i = parseInt(n(this).attr("data-value")) - 1;
                i >= 0 && t.jgGirdRendering(u, 0, 30)
            });
            n("#girdPager .next").on("click",
            function () {
                var i = parseInt(n(this).attr("data-value")) + 1;
                i >= 0 && t.jgGirdRendering(u, 0, 30)
            })
        },
        clickTabs: function () {
            var i = n(this),
            r;
            i.hasClass("active") || (n(".nav-tabs .active").removeClass("active"), i.addClass("active"), r = i.attr("data-value"), t.initTemplate(r))
        },
        initTemplate: function (i) {
            var u = r[i],
            f = [];
            n.each(u.filedsInfo,
            function (n, t) {
                if (t.F_RelationType != 1 && t.F_RelationType != 4 && t.F_RelationType != 5 && t.F_RelationType != 6 && t.F_RelationType != 7) {
                    var i = {
                        label: t.F_ColName,
                        name: t.F_ColName,
                        index: t.F_ColName,
                        width: 100,
                        align: "left",
                        sortable: !1
                    };
                    f.push(i)
                }
            });
            t.initJqGird(f);
            t.initButton(u)
        },
        initButton: function (i) {
            n("#lr-upfile").uploadifyEx({
                url: "/Utility/ExecuteImportExcel?templateId=" + i.templateInfo.F_Id,
                btnName: "上传文件",
                type: "uploadify",
                height: 31,
                width: 90,
                oneFile: !0,
                fileTypeExts: "xls,xlsx",
                onUploadSuccess: t.loadData
            });
            n("#lr-download").unbind();
            n("#lr-download").on("click",
            function () {
                learun.downFile({
                    url: "/Utility/DownExcelTemplate",
                    data: "templateId=" + i.templateInfo.F_Id,
                    method: "post"
                })
            })
        },
        initJqGird: function (t) {
            t.push({
                label: "状态",
                name: "learunColOk",
                index: "learunColOk",
                width: 60,
                align: "center",
                sortable: !1,
                formatter: function (n) {
                    return n == 1 ? "<span >成功<\/span>" : '<span style="color:red">失败<\/span>'
                }
            });
            t.push({
                label: "描述",
                name: "learunColError",
                index: "learunColError",
                width: 200,
                align: "left",
                sortable: !1
            });
            n(".gridPanel").html(' <table id="gridTable"><\/table>');
            var i = n("#gridTable");
            i.jqGrid({
                unwritten: !1,
                datatype: "local",
                height: n(window).height() - 214,
                autowidth: !0,
                colModel: t,
                pager: !1,
                rownumbers: !0,
                shrinkToFit: !1,
                gridview: !0
            })
        },
        jgGirdRendering: function (t, i, r) {
            var u = t.length,
            o = !1,
            l = i * r,
            s = i * r + 30,
            h = 0,
            e, c, f;
            for (s > u && (s = u), e = l; e < u; e++) o || (n("#gridTable").jqGrid("clearGridData"), o = !0),
            n("#gridTable").jqGrid("addRowData", h, t[e]),
            h++;
            o && (c = parseInt(u / r) + (u % r > 0 ? 1 : 0), f = n("#girdPager"), f.find(".num-total").text(c), f.find(".num-index").text(i + 1), f.find(".prev").attr("data-value", i), f.find(".next").attr("data-value", i))
        },
        loadData: function (r) {
            var s, f, e, o;
            console.log(r);
            try {
                learun.currentIframe().$("#gridTable").trigger("reloadGrid")
            } catch (h) { }
            for (s = r.Rows.length, i.success = [], i.fail = [], f = 0; f < s; f++) e = r.Rows[f],
            e.rownum = f,
            e.learunColOk == "1" ? i.success.push(e) : i.fail.push(e);
            o = n("#girdPager");
            o.find(".num-all").text(s);
            o.find(".num-success").text(i.success.length);
            o.find(".num-fail").text(i.fail.length);
            u = i.fail.concat(i.success);
            t.jgGirdRendering(u, 0, 30)
        }
    };
    n(function () {
        t.init()
    })
})(window.jQuery, window.learun)