$.Report = function (options) {
    var defaults = {
        picDiv: "",
        reportId: "",
        listDiv: ""
    };
    var options = $.extend(defaults, options);
    $.ajax({
        url: "/ReportManage/Report/GetReportJson",
        data: { reportId: options.reportId },
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            console.log(data);
            if (data) {
                switch (data.tempType) {
                    case 'pie':
                        DrawPie(data.picData, options.picDiv);
                        break;
                    case 'bar':
                        DrawBar(data.picData, options.picDiv);
                        break;
                    case 'line':
                        DrawLine(data.picData, options.picDiv);
                        break;
                    case 'pie':
                        DrawPie(data.picData, options.picDiv);
                        break;
                    default:
                }
                if (options.listDiv)
                    if (data.listData != null) {
                        DrawList(data, options.listDiv);
                    }
            }
        },
        error: function (msg) {
            alert("系统发生错误");
        }
    });
    function DrawPie(data, id) {
        var myChart = echarts.init(document.getElementById(id));
        var option = ECharts.ChartOptionTemplates.Pie(data);
        myChart.setOption(option);
    }
    function DrawBar(data, id) {
        var myChart = echarts.init(document.getElementById(id));
        var option = ECharts.ChartOptionTemplates.Bars(data, 'bar', true);
        myChart.setOption(option);
    }
    function DrawLine(data, id) {
        var myChart = echarts.init(document.getElementById(id));
        var option = ECharts.ChartOptionTemplates.Lines(data, 'line', true);
        myChart.setOption(option);
    }
    function DrawMap(data, id) {
        var myChart = echarts.init(document.getElementById(id));
        var option = ECharts.ChartOptionTemplates.Maps(data);
        myChart.setOption(option);
    }
    function DrawList(data, id) {
        var colModelData = [];
        $.each(data.listField, function (i) {
            var row = data.listField[i];
            colModelData.push({ label: row.Field, name: row.Field, align: "left", index: row.Field });
        });
        var $gridTable = $("#" + id);
        $gridTable.jqGrid({
            datatype: "local",
            data: data.listData,
            height: '100%',
            autowidth: true,
            colModel: colModelData,
            rowNum: 30,
            rowList: [30, 50, 100],
            sortorder: 'desc',
            rownumbers: true,
            shrinkToFit: false,
            footerrow: true,
            gridComplete: function () {
                var totalMoney = $(this).getCol("销售额", false, "sum");
                //合计
                $(this).footerData("set", {
                    "客户名称": "合计：",
                    "销售额": totalMoney,
                });
                $('table .ui-jqgrid-btable tr').prevUntil().css("border-right-color", "#fff");
            }
        });
    }
}
