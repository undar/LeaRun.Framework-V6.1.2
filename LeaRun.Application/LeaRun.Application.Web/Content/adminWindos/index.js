(function ($) {
    tablist = {
        newTab: function (item) {
            var dataId = item.id;
            var dataUrl = item.url;
            var menuName = item.title;
            var flag = true;
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            $('.menuTab').each(function () {
                if ($(this).data('id') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.learuntab.scrollToTab(this);
                        $('.mainContent .LRADMS_iframe').each(function () {
                            if ($(this).data('id') == dataUrl) {
                                $(this).show().siblings('.LRADMS_iframe').hide();
                                return false;
                            }
                        });
                    }
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-remove"></i></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="LRADMS_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
                $('.mainContent').find('iframe.LRADMS_iframe').hide();
                $('.mainContent').append(str1);
                Loading(true);
                $('.mainContent iframe:visible').load(function () {
                    Loading(false);
                });
                $('.menuTabs .page-tabs-content').append(str);
                $.learuntab.scrollToTab($('.menuTab.active'));
            }
        }
    }
    $.learuntab = {
        requestFullScreen: function () {
            var de = document.documentElement;
            if (de.requestFullscreen) {
                de.requestFullscreen();
            } else if (de.mozRequestFullScreen) {
                de.mozRequestFullScreen();
            } else if (de.webkitRequestFullScreen) {
                de.webkitRequestFullScreen();
            }
        },
        exitFullscreen: function () {
            var de = document;
            if (de.exitFullscreen) {
                de.exitFullscreen();
            } else if (de.mozCancelFullScreen) {
                de.mozCancelFullScreen();
            } else if (de.webkitCancelFullScreen) {
                de.webkitCancelFullScreen();
            }
        },
        refreshTab: function () {
            var currentId = $('.page-tabs-content').find('.active').attr('data-id');
            var target = $('.LRADMS_iframe[data-id="' + currentId + '"]');
            var url = target.attr('src');
            Loading(true);
            target.attr('src', url).load(function () {
                Loading(false);
            });
        },
        activeTab: function () {
            var currentId = $(this).data('id');
            if (!$(this).hasClass('active')) {
                $('.mainContent .LRADMS_iframe').each(function () {
                    if ($(this).data('id') == currentId) {
                        $(this).show().siblings('.LRADMS_iframe').hide();
                        return false;
                    }
                });
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $.learuntab.scrollToTab(this);
            }
        },
        closeOtherTabs: function () {
            $('.page-tabs-content').children("[data-id]").find('.fa-remove').parents('a').not(".active").each(function () {
                $('.LRADMS_iframe[data-id="' + $(this).data('id') + '"]').remove();
                $(this).remove();
            });
            $('.page-tabs-content').css("margin-left", "0");
        },
        closeTab: function () {
            var closeTabId = $(this).parents('.menuTab').data('id');
            var currentWidth = $(this).parents('.menuTab').width();
            if ($(this).parents('.menuTab').hasClass('active')) {
                if ($(this).parents('.menuTab').next('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').next('.menuTab:eq(0)').data('id');
                    $(this).parents('.menuTab').next('.menuTab:eq(0)').addClass('active');

                    $('.mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.LRADMS_iframe').hide();
                            return false;
                        }
                    });
                    var marginLeftVal = parseInt($('.page-tabs-content').css('margin-left'));
                    if (marginLeftVal < 0) {
                        $('.page-tabs-content').animate({
                            marginLeft: (marginLeftVal + currentWidth) + 'px'
                        }, "fast");
                    }
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
                if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                    $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                    $('.mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.LRADMS_iframe').hide();
                            return false;
                        }
                    });
                    $(this).parents('.menuTab').remove();
                    $('.mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
            }
            else {
                $(this).parents('.menuTab').remove();
                $('.mainContent .LRADMS_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
                $.learuntab.scrollToTab($('.menuTab.active'));
            }
            return false;
        },
        addTab: function () {
            $(".navbar-custom-menu>ul>li.open").removeClass("open");
            $(".sidebar-menu-overlay").remove();
            $(".sidebar-menu").find(".sidebar-menu-right").css("overflow", "hidden");
            $(".sidebar-menu").slideUp(300)

            var dataId = $(this).attr('data-id');
            if (dataId != "") {
                top.$.cookie('currentmoduleId', dataId, { path: "/" });
            }
            var dataUrl = $(this).attr('href');
            var menuName = $.trim($(this).text());
            var flag = true;
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            $('.menuTab').each(function () {
                if ($(this).data('id') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.learuntab.scrollToTab(this);
                        $('.mainContent .LRADMS_iframe').each(function () {
                            if ($(this).data('id') == dataUrl) {
                                $(this).show().siblings('.LRADMS_iframe').hide();
                                return false;
                            }
                        });
                    }
                    flag = false;
                    return false;
                }
            });
            if (flag) {
                var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <i class="fa fa-remove"></i></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="LRADMS_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
                $('.mainContent').find('iframe.LRADMS_iframe').hide();
                $('.mainContent').append(str1);
                Loading(true);
                $('.mainContent iframe:visible').load(function () {
                    Loading(false);
                });
                $('.menuTabs .page-tabs-content').append(str);
                $.learuntab.scrollToTab($('.menuTab.active'));
            }
            return false;
        },
        scrollTabRight: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.learuntab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                scrollVal = $.learuntab.calSumWidth($(tabElement).prevAll());
                if (scrollVal > 0) {
                    $('.page-tabs-content').animate({
                        marginLeft: 0 - scrollVal + 'px'
                    }, "fast");
                }
            }
        },
        scrollTabLeft: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.learuntab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").width() < visibleWidth) {
                return false;
            } else {
                var tabElement = $(".menuTab:first");
                var offsetVal = 0;
                while ((offsetVal + $(tabElement).outerWidth(true)) <= marginLeftVal) {
                    offsetVal += $(tabElement).outerWidth(true);
                    tabElement = $(tabElement).next();
                }
                offsetVal = 0;
                if ($.learuntab.calSumWidth($(tabElement).prevAll()) > visibleWidth) {
                    while ((offsetVal + $(tabElement).outerWidth(true)) < (visibleWidth) && tabElement.length > 0) {
                        offsetVal += $(tabElement).outerWidth(true);
                        tabElement = $(tabElement).prev();
                    }
                    scrollVal = $.learuntab.calSumWidth($(tabElement).prevAll());
                }
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        scrollToTab: function (element) {
            var marginLeftVal = $.learuntab.calSumWidth($(element).prevAll()), marginRightVal = $.learuntab.calSumWidth($(element).nextAll());
            var tabOuterWidth = $.learuntab.calSumWidth($(".content-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".content-tabs").outerWidth(true) - tabOuterWidth;
            var scrollVal = 0;
            if ($(".page-tabs-content").outerWidth() < visibleWidth) {
                scrollVal = 0;
            } else if (marginRightVal <= (visibleWidth - $(element).outerWidth(true) - $(element).next().outerWidth(true))) {
                if ((visibleWidth - $(element).next().outerWidth(true)) > marginRightVal) {
                    scrollVal = marginLeftVal;
                    var tabElement = element;
                    while ((scrollVal - $(tabElement).outerWidth()) > ($(".page-tabs-content").outerWidth() - visibleWidth)) {
                        scrollVal -= $(tabElement).prev().outerWidth();
                        tabElement = $(tabElement).prev();
                    }
                }
            } else if (marginLeftVal > (visibleWidth - $(element).outerWidth(true) - $(element).prev().outerWidth(true))) {
                scrollVal = marginLeftVal - $(element).prev().outerWidth(true);
            }
            if (scrollVal > 0) {
                scrollVal += 30;
            }
            $('.page-tabs-content').animate({
                marginLeft: 0 - scrollVal + 'px'
            }, "fast");
        },
        calSumWidth: function (element) {
            var width = 0;
            $(element).each(function () {
                width += $(this).outerWidth(true);
            });
            return width;
        },
        init: function () {
            $('.menuItem').on('click', top.$.learuntab.addTab);
            $('.menuTabs').on('click', '.menuTab i', top.$.learuntab.closeTab);
            $('.menuTabs').on('click', '.menuTab', top.$.learuntab.activeTab);
            $('.tabLeft').on('click', top.$.learuntab.scrollTabLeft);
            $('.tabRight').on('click', top.$.learuntab.scrollTabRight);
            $('.tabReload').on('click', top.$.learuntab.refreshTab);
            $('.tabCloseCurrent').on('click', function () {
                $('.page-tabs-content').find('.active i').trigger("click");
            });
            $('.tabCloseAll').on('click', function () {
                $('.page-tabs-content').children("[data-id]").find('.fa-remove').each(function () {
                    $('.LRADMS_iframe[data-id="' + $(this).data('id') + '"]').remove();
                    $(this).parents('a').remove();
                });
                $('.page-tabs-content').children("[data-id]:first").each(function () {
                    $('.LRADMS_iframe[data-id="' + $(this).data('id') + '"]').show();
                    $(this).addClass("active");
                });
                $('.page-tabs-content').css("margin-left", "0");
            });
            $('.tabCloseOther').on('click', $.learuntab.closeOtherTabs);
            $('.fullscreen').on('click', function () {
                if (!$(this).attr('fullscreen')) {
                    $(this).attr('fullscreen', 'true');
                    $.learuntab.requestFullScreen();
                } else {
                    $(this).removeAttr('fullscreen')
                    $.learuntab.exitFullscreen();
                }
            });
        }
    };
    $.learunindex = {
        load: function () {
            $("#content-wrapper").find('.mainContent').height($(window).height() - 158);
            $(window).resize(function (e) {
                $("#content-wrapper").find('.mainContent').height($(window).height() - 158);
            });
            $(window).load(function () {
                window.setTimeout(function () {
                    $('#ajax-loader').fadeOut();
                    Loading(false);
                }, 300);
            });
        },
        jsonWhere: function (data, action) {
            if (action == null) return;
            var reval = new Array();
            $(data).each(function (i, v) {
                if (action(v)) {
                    reval.push(v);
                }
            })
            return reval;
        },
        loadMenu: function () {
            var data = authorizeMenuData;
            var _html = "";
            $.each(data, function (i) {
                var row = data[i];
                if (row.ParentId == "0") {
                    _html += '<li><a href="#" data-id="' + row.ModuleId + '"><i class="fa ' + row.Icon + '"></i><span>' + row.FullName + '</span></a><em></em></li>';
                }
            });
            var $ul = $(".sidebar-menu-left ul").html(_html);
            $ul.find("li").click(function () {
                $ul.find("li.active").removeClass("active");
                $(this).addClass("active");
                var dataId = $(this).find("a").attr("data-id");
                var childNodes = $.learunindex.jsonWhere(data, function (v) { return v.ParentId == dataId });
                var _childNodeshtml = "";
                $.each(childNodes, function (i) {
                    var row = childNodes[i];
                    var subchildNodes = $.learunindex.jsonWhere(data, function (v) { return v.ParentId == row.ModuleId });
                    if (subchildNodes.length > 0) {
                        _childNodeshtml += '<li>';
                        _childNodeshtml += '<a href="#"><i class="fa ' + row.Icon + '"></i><span>' + row.FullName + '</span><i class="fa fa-angle-left pull-right"></i></a>';
                        _childNodeshtml += '<ul>';
                        $.each(subchildNodes, function (i) {
                            var subrow = subchildNodes[i];
                            _childNodeshtml += '<li><a class="menuItem" data-id="' + subrow.ModuleId + '" href="' + subrow.UrlAddress + '"><i class="fa ' + subrow.Icon + '"></i><span>' + subrow.FullName + '</span></a></li>';
                        });
                        _childNodeshtml += '</ul>';
                        _childNodeshtml += '</li>';

                    } else {
                        _childNodeshtml += '<li><a class="menuItem" data-id="' + row.ModuleId + '" href="' + row.UrlAddress + '"><i class="fa ' + row.Icon + '"></i><span>' + row.FullName + '</span></a></li>';
                    }
                });
                var $subul = $(".sidebar-menu-right>ul").html(_childNodeshtml);
                $subul.find(">li").click(function () {
                    var e = $(this);
                    if (e.find("ul>li").length > 0) {
                        if (e.find("ul").is(":visible")) {
                            e.find("ul").slideUp(500)
                            e.find(">a").removeClass("active")
                        } else {
                            $subul.find("ul").slideUp(500)
                            $subul.find(">li>a").removeClass("active");
                            e.find("ul").slideDown(500);
                            e.find(">a").addClass("active")
                        }
                    }
                });
                $.learuntab.init();
            });
            $ul.find("li:first").trigger("click");
            //开始菜单
            $(".start_menu").click(function () {
                if (!$(".sidebar-menu").is(":visible")) {
                    $(".sidebar-menu").find(".sidebar-menu-right").css("overflow", "auto");
                    $(".sidebar-menu").show();
                    $("body").prepend('<div class="sidebar-menu-overlay" style="display: block;"></div>');
                    $(".sidebar-menu-overlay").click(function () {
                        $(this).remove();
                        $(".sidebar-menu").find(".sidebar-menu-right").css("overflow", "hidden");
                        $(".sidebar-menu").slideUp(300)
                    })
                }
            });
        },
        loadDesktop: function () {
            if ($(".slidebox").length > 0) {
                var data = [{ "ModuleId": "1", "ParentId": "0", "EnCode": "SysManage", "FullName": "系统管理", "Icon": "fa fa-desktop", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2015-11-17 11:22:46", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "8", "ParentId": "2", "EnCode": "OrganizeManage", "FullName": "机构管理", "Icon": "fa fa-sitemap", "UrlAddress": "/BaseManage/Organize/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 11:55:28", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "7ae94059-9aa5-48eb-8330-4e2a6565b193", "ParentId": "1", "EnCode": "AreaManage", "FullName": "行政区域", "Icon": "fa fa-leaf", "UrlAddress": "/SystemManage/Area/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": "行政区域管理", "CreateDate": "2015-11-12 14:38:20", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:05:33", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "34e362f4-c220-4fb7-b3f0-288c83417cb3", "ParentId": "7cec0a0f-7204-4240-b009-312fa0c11cbf", "EnCode": "DataBaseLink", "FullName": "数据库连接", "Icon": "fa fa-plug", "UrlAddress": "/SystemManage/DataBaseLink/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": "动态链接数据库", "CreateDate": "2015-11-24 09:50:22", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:07:45", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "1b642904-d674-495f-a1e1-4814cc543870", "ParentId": "5", "EnCode": "发起流程", "FullName": "发起流程", "Icon": "fa fa-edit", "UrlAddress": "/FlowManage/FlowLaunch/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 22:12:27", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-01-12 17:39:01", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "a977d91e-77b7-4d60-a7ad-dfbc138f7c0a", "ParentId": "b9f9df92-8ac5-46e2-90ac-68c5c2e034c3", "EnCode": "企业号设置", "FullName": "企业号设置", "Icon": "fa fa-plug", "UrlAddress": "/WeChatManage/Token/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-12-22 17:20:21", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-12-29 19:05:02", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "00ae31cf-b340-4c17-9ee7-6dd08943df02", "ParentId": "458113c6-b0be-4d6f-acce-7524f4bc3e88", "EnCode": "FormCategory", "FullName": "表单类别", "Icon": "fa fa-tags", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=FormSort", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-27 10:30:47", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-12-01 09:42:16", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClientBaseData", "FullName": "基础设置", "Icon": "fa fa fa-book", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 1, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": "客户基础资料", "CreateDate": "2016-03-11 11:51:34", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-29 09:41:15", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "92a535c9-4d4b-4500-968d-a142e671c09b", "ParentId": "6", "EnCode": "ReportTemp", "FullName": "报表管理", "Icon": "fa fa-cogs", "UrlAddress": "/ReportManage/Report/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 1, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": "报表模板管理", "CreateDate": "2016-01-13 17:21:17", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:14:56", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "e35d24ce-8a6a-46b9-8b3f-6dc864a8f342", "ParentId": "4", "EnCode": "NewManage", "FullName": "新闻中心", "Icon": "fa fa-feed", "UrlAddress": "/PublicInfoManage/News/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-27 09:47:16", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:17:09", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "3b03806d-98d8-40fe-9895-01633119458c", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_ProductInfo", "FullName": "产品信息", "Icon": "fa fa-shopping-bag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_ProductInfo", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 1, "DeleteMark": 0, "EnabledMark": 1, "Description": "销售产品信息", "CreateDate": "2016-03-11 16:42:57", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-23 16:36:07", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "27b6c487-a2d9-4a3a-a40d-dbba27a53d26", "ParentId": "b5cb98f6-fb41-4a0f-bc11-469ff117a411", "EnCode": "FlowMonitor", "FullName": "流程监控", "Icon": "fa fa-eye", "UrlAddress": "/FlowManage/FlowProcess/MonitoringIndex", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 21:58:17", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-26 12:06:13", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "6252983c-52f5-402c-991b-ad19a9cb1f94", "ParentId": "4", "EnCode": "NoticeManage", "FullName": "通知公告", "Icon": "fa fa-volume-up", "UrlAddress": "/PublicInfoManage/Notice/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-27 09:47:33", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:17:39", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "66f6301c-1789-4525-a7d2-2b83272aafa6", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClientChance", "FullName": "商机管理", "Icon": "fa fa-binoculars", "UrlAddress": "/CustomerManage/Chance/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": "商机管理", "CreateDate": "2016-03-11 11:55:16", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:19:13", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "affacee1-41a3-4c7b-8804-f1c1926babbd", "ParentId": "6", "EnCode": "PurchaseReport", "FullName": "采购报表", "Icon": "fa fa-bar-chart", "UrlAddress": "/ReportManage/ReportDemo/Purchase", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-01-04 16:29:07", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:15:19", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "6be31cc9-4aee-4279-8435-4b266cec33f0", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_Trade", "FullName": "客户行业", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_Trade", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-11 16:45:14", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-23 16:36:23", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "5cc9d2d9-e097-4b51-9b9e-84ca9f1a0ab5", "ParentId": "b9f9df92-8ac5-46e2-90ac-68c5c2e034c3", "EnCode": "企业号部门", "FullName": "企业号部门", "Icon": "fa fa-sitemap", "UrlAddress": "/WeChatManage/Organize/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-12-22 17:20:38", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:10:46", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "08dfd779-92d5-4cd8-9982-a76176af0f7c", "ParentId": "458113c6-b0be-4d6f-acce-7524f4bc3e88", "EnCode": "FlowCategory", "FullName": "流程类别", "Icon": "fa fa-tags", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=FlowSort", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 14:42:18", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-11-27 10:41:42", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "c4d7ce1f-72de-4651-b495-6c466261e9af", "ParentId": "7cec0a0f-7204-4240-b009-312fa0c11cbf", "EnCode": "DataBaseBackup", "FullName": "数据库备份", "Icon": "fa fa-cloud-download", "UrlAddress": "/SystemManage/DataBaseBackup/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": "数据备份、数据还原", "CreateDate": "2015-11-24 09:55:52", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:08:22", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "691f3810-a602-4523-8518-ce5856482d48", "ParentId": "5", "EnCode": "草稿流程", "FullName": "草稿流程", "Icon": "fa fa-file-text-o", "UrlAddress": "/FlowManage/FlowRoughdraft/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 22:13:21", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-19 16:15:15", "ModifyUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "ModifyUserName": "陈彬彬" }, { "ModuleId": "9", "ParentId": "2", "EnCode": "DepartmentManage", "FullName": "部门管理", "Icon": "fa fa-th-list", "UrlAddress": "/BaseManage/Department/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 11:57:20", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "2", "ParentId": "0", "EnCode": "BaseManage", "FullName": "单位组织", "Icon": "fa fa-coffee", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-03-11 11:02:06", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "4efd37bf-e3ef-4ced-8248-58eba046d78b", "ParentId": "1", "EnCode": "DataItemManage", "FullName": "通用字典", "Icon": "fa fa-book", "UrlAddress": "/SystemManage/DataItemDetail/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 2, "DeleteMark": 0, "EnabledMark": 1, "Description": "通用数据字典", "CreateDate": "2015-11-12 14:37:04", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:06:26", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "5", "ParentId": "0", "EnCode": "FlowManage", "FullName": "工作流程", "Icon": "fa fa-share-alt", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-11 10:21:44", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "7adc5a16-54a4-408e-a101-2ddab8117d67", "ParentId": "1", "EnCode": "CodeRule", "FullName": "单据编码", "Icon": "fa fa-barcode", "UrlAddress": "/SystemManage/CodeRule/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": "自动产生号码", "CreateDate": "2015-11-12 14:47:51", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-05-03 15:56:56", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "11", "ParentId": "2", "EnCode": "RoleManage", "FullName": "角色管理", "Icon": "fa fa-paw", "UrlAddress": "/BaseManage/Role/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-05-23 18:12:29", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "a57993fa-5a94-44a8-a330-89196515c1d9", "ParentId": "458113c6-b0be-4d6f-acce-7524f4bc3e88", "EnCode": "FormDesign", "FullName": "表单设计", "Icon": "fa fa-puzzle-piece", "UrlAddress": "/FlowManage/FormDesign/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-27 10:29:53", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-12-01 09:41:58", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "4d0f2e44-f68f-41fd-a55c-40ac67453ef4", "ParentId": "b9f9df92-8ac5-46e2-90ac-68c5c2e034c3", "EnCode": "企业号成员", "FullName": "企业号成员", "Icon": "fa fa-users", "UrlAddress": "/WeChatManage/User/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-12-22 17:20:53", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:11:24", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "c30310a7-d0a5-4bf6-8655-c3834a8cc73d", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_Sort", "FullName": "客户类别", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_Sort", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-11 16:47:39", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-23 16:36:33", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "ff1823b5-a966-4e6c-83de-807854f4f0fb", "ParentId": "6", "EnCode": "SalesReport", "FullName": "销售报表", "Icon": "fa fa-line-chart", "UrlAddress": "/ReportManage/ReportDemo/Sales", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-01-04 16:29:46", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:15:34", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "021a59b0-2589-4f9e-8140-6052177a967c", "ParentId": "5", "EnCode": "我的流程", "FullName": "我的流程", "Icon": "fa fa-file-word-o", "UrlAddress": "/FlowManage/FlowMyProcess/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-19 13:32:54", "CreateUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "CreateUserName": "陈彬彬", "ModifyDate": "2016-03-22 20:02:21", "ModifyUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "ModifyUserName": "陈彬彬" }, { "ModuleId": "1d3797f6-5cd2-41bc-b769-27f2513d61a9", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClientInfoManage", "FullName": "客户管理", "Icon": "fa fa-suitcase", "UrlAddress": "/CustomerManage/Customer/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": "客户管理", "CreateDate": "2016-03-11 11:57:48", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:19:05", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "04b88c96-8d99-45ec-956c-444efa630020", "ParentId": "4", "EnCode": "ResourceFileManage", "FullName": "文件资料", "Icon": "fa fa-jsfiddle", "UrlAddress": "/PublicInfoManage/ResourceFile/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": "文件管理", "CreateDate": "2015-11-27 09:47:48", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-21 15:06:21", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "2f820f6e-ae2e-472f-82cc-0129a2a57597", "ParentId": "7cec0a0f-7204-4240-b009-312fa0c11cbf", "EnCode": "DataBaseTable", "FullName": "数据表管理", "Icon": "fa fa-table", "UrlAddress": "/SystemManage/DataBaseTable/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": "数据库表结构", "CreateDate": "2015-11-24 09:53:42", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:08:55", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "c0544bfd-a557-45fc-a856-a678a1e88bfc", "ParentId": "b5cb98f6-fb41-4a0f-bc11-469ff117a411", "EnCode": "FlowDelegate", "FullName": "流程指派", "Icon": "fa fa-random", "UrlAddress": "/FlowManage/FlowProcess/DesignationIndex", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 3, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 21:58:36", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-26 12:06:40", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "901e6122-985d-4c84-8d8c-56560520f6ed", "ParentId": "6", "EnCode": "StorageReport", "FullName": "仓存报表", "Icon": "fa fa-area-chart", "UrlAddress": "/ReportManage/ReportDemo/Store", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-01-04 16:30:25", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:15:52", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "aed02ee7-322f-47f0-8ad6-ab0a2172628f", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_Degree", "FullName": "客户程度", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_Degree", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-11 16:49:46", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-06 10:23:36", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "5f1fa264-cc9b-4146-b49e-743e4633bb4c", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClientInvoice", "FullName": "客户开票", "Icon": "fa fa-coffee", "UrlAddress": "/CustomerManage/Invoice/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": "开票管理", "CreateDate": "2016-04-01 10:40:18", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:20:23", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "aa844d70-7211-41d9-907a-f9a10f4ac801", "ParentId": "b9f9df92-8ac5-46e2-90ac-68c5c2e034c3", "EnCode": "企业号应用", "FullName": "企业号应用", "Icon": "fa fa-safari", "UrlAddress": "/WeChatManage/App/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-12-22 17:21:25", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-12-25 10:34:44", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "f63a252b-975f-4832-a5be-1ce733bc8ece", "ParentId": "458113c6-b0be-4d6f-acce-7524f4bc3e88", "EnCode": "FlowDesign", "FullName": "流程设计", "Icon": "fa fa-share-alt", "UrlAddress": "/FlowManage/FlowDesign/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 14:42:43", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-11-27 10:41:09", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "0d296398-bc0e-4f38-996a-6e24bc88cc53", "ParentId": "5", "EnCode": "待办流程", "FullName": "待办流程", "Icon": "fa fa-hourglass-half", "UrlAddress": "/FlowManage/FlowBeforeProcessing/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 22:13:39", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-23 18:07:42", "ModifyUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "ModifyUserName": "陈彬彬" }, { "ModuleId": "7cec0a0f-7204-4240-b009-312fa0c11cbf", "ParentId": "1", "EnCode": "DatabaseManage", "FullName": "数据管理", "Icon": "fa fa-database", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-12 15:03:09", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-11 12:10:01", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "6", "ParentId": "0", "EnCode": "ReportManage", "FullName": "报表中心", "Icon": "fa fa-area-chart", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 4, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-11 10:21:54", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "4", "ParentId": "0", "EnCode": "CommonInfo", "FullName": "公共信息", "Icon": "fa fa-globe", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-11 10:21:59", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "13", "ParentId": "2", "EnCode": "PostManage", "FullName": "岗位管理", "Icon": "fa fa-graduation-cap", "UrlAddress": "/BaseManage/Post/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 11:59:17", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "923f7d65-e307-45f7-8f96-73ecbf23b324", "ParentId": "5", "EnCode": "已办流程", "FullName": "已办流程", "Icon": "fa fa-flag", "UrlAddress": "/FlowManage/FlowAferProcessing/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 22:14:03", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-25 11:39:51", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "52fe82f8-41ba-433e-9351-ef67e5b35217", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_Level", "FullName": "客户级别", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_Level", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-11 16:52:08", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-06 10:23:29", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "b352f049-4331-4b19-ac22-e379cb30bd55", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClientOrder", "FullName": "客户订单", "Icon": "fa fa-modx", "UrlAddress": "/CustomerManage/Order/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": "客户订单管理", "CreateDate": "2016-03-11 12:01:30", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:20:16", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "b9f9df92-8ac5-46e2-90ac-68c5c2e034c3", "ParentId": "1", "EnCode": "WeChatManage", "FullName": "微信管理", "Icon": "fa fa-weixin", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-12-22 16:42:12", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2015-12-22 18:20:30", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "9db71a92-2ecb-496c-839f-7a82bc22905d", "ParentId": "6", "EnCode": "MoneyReport", "FullName": "对账报表", "Icon": "fa fa-pie-chart", "UrlAddress": "/ReportManage/ReportDemo/Reconciliation", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 5, "DeleteMark": 0, "EnabledMark": 1, "Description": "现金银行报表", "CreateDate": "2016-01-04 16:31:03", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-29 14:16:09", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "c6b80ed5-b0cb-4844-ba1a-725d2cb4f935", "ParentId": "4", "EnCode": "EmailManage", "FullName": "邮件中心", "Icon": "fa fa-send", "UrlAddress": "/PublicInfoManage/Email/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": "邮件管理", "CreateDate": "2015-11-27 09:48:38", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-21 15:06:31", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "23713d3a-810f-422d-acd5-39bec28ce47e", "ParentId": "4", "EnCode": "ScheduleManage", "FullName": "日程管理", "Icon": "fa fa-calendar", "UrlAddress": "/PublicInfoManage/Schedule/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": "日程管理", "CreateDate": "2016-04-21 14:15:30", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-21 16:08:46", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "ParentId": "0", "EnCode": "CustomerManage", "FullName": "客户关系", "Icon": "fa fa-briefcase", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": "客户关系管理", "CreateDate": "2016-03-11 10:53:05", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-21 16:00:07", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "be9cbe61-265f-4ddd-851e-d5a1cef6011b", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_ChanceSource", "FullName": "商机来源", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_ChanceSource", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-12 11:01:38", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-23 16:36:58", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "535d92e9-e066-406c-b2c2-697150a5bdff", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClienReceivable", "FullName": "收款管理", "Icon": "fa fa-money", "UrlAddress": "/CustomerManage/Receivable/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": "收款管理", "CreateDate": "2016-04-06 16:04:16", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:20:56", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "6a67a67f-ef07-41e7-baa5-00bc5f662a76", "ParentId": "5", "EnCode": "工作委托", "FullName": "工作委托", "Icon": "fa fa-coffee", "UrlAddress": "/FlowManage/FlowDelegate/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 22:14:20", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-28 17:34:24", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "12", "ParentId": "2", "EnCode": "JobManage", "FullName": "职位管理", "Icon": "fa fa-briefcase", "UrlAddress": "/BaseManage/Job/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 12:00:32", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "f21fa3a0-c523-4d02-99ca-fd8dd3ae3d59", "ParentId": "1", "EnCode": "SystemLog", "FullName": "系统日志", "Icon": "fa fa-warning", "UrlAddress": "/SystemManage/Log/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 6, "DeleteMark": 0, "EnabledMark": 1, "Description": "登录日志、操作日志。异常日志", "CreateDate": "2015-11-12 15:04:58", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:12:14", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "14", "ParentId": "2", "EnCode": "UserGroupManage", "FullName": "用户组管理", "Icon": "fa fa-group", "UrlAddress": "/BaseManage/UserGroup/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 7, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 12:01:17", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "21", "ParentId": "1", "EnCode": "SystemModule", "FullName": "系统功能", "Icon": "fa fa-navicon", "UrlAddress": "/AuthorizeManage/Module/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 7, "DeleteMark": 0, "EnabledMark": 1, "Description": "系统导航功能", "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 14:13:00", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "1ef31fba-7f0a-46f7-b533-49dd0c2e51e0", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClienReceivableReport", "FullName": "收款报表", "Icon": "fa fa-bar-chart", "UrlAddress": "/CustomerManage/ReceivableReport/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 7, "DeleteMark": 0, "EnabledMark": 1, "Description": "收款报表", "CreateDate": "2016-04-20 09:41:51", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:21:24", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "80620d6f-55bd-492b-9c21-1b04ca268e75", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_ChancePhase", "FullName": "商机阶段", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_ChancePhase", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 7, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-12 11:02:09", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-03-23 16:37:06", "ModifyUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "ModifyUserName": "undar" }, { "ModuleId": "458113c6-b0be-4d6f-acce-7524f4bc3e88", "ParentId": "5", "EnCode": "流程配置", "FullName": "流程配置", "Icon": "fa fa-wrench", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 7, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-27 10:39:01", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-19 13:34:52", "ModifyUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "ModifyUserName": "陈彬彬" }, { "ModuleId": "b0261df5-7be0-4c8e-829c-15836e200af0", "ParentId": "1", "EnCode": "SystemForm", "FullName": "系统表单", "Icon": "fa fa-paw", "UrlAddress": "/AuthorizeManage/ModuleForm/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": "系统功能自定义表单", "CreateDate": "2016-04-11 11:19:06", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:14:02", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "9fc384f5-efb7-439e-9fe1-3e50807e6399", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClienExpenses", "FullName": "支出管理", "Icon": "fa fa-credit-card-alt", "UrlAddress": "/CustomerManage/Expenses/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": "支出管理", "CreateDate": "2016-04-20 11:31:56", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-29 14:21:50", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "77f13de5-32ad-4226-9e24-f1db507e78cb", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_PaymentMode", "FullName": "收支方式", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_PaymentMode", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-14 19:49:53", "CreateUserId": "0f36148c-719f-41e0-8c8c-16ffbc40d0e0", "CreateUserName": "undar", "ModifyDate": "2016-04-20 09:55:52", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "b5cb98f6-fb41-4a0f-bc11-469ff117a411", "ParentId": "5", "EnCode": "FlowManage", "FullName": "流程管理", "Icon": "fa fa-cogs", "UrlAddress": null, "Target": "expand", "IsMenu": 0, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2015-11-23 10:20:00", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-03-19 13:33:50", "ModifyUserId": "24a055d6-5924-44c5-be52-3715cdd68011", "ModifyUserName": "陈彬彬" }, { "ModuleId": "cfa631fe-e7f8-42b5-911f-7172f178a811", "ParentId": "1", "EnCode": "CodeCreate", "FullName": "快速开发", "Icon": "fa fa-code", "UrlAddress": "/GeneratorManage/Template/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": "自动生成代码、自动生成功能", "CreateDate": "2015-11-12 15:21:38", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-12 10:52:30", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "10", "ParentId": "2", "EnCode": "UserManage", "FullName": "用户管理", "Icon": "fa fa-user", "UrlAddress": "/BaseManage/User/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 8, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": null, "CreateUserId": null, "CreateUserName": null, "ModifyDate": "2016-04-29 11:51:54", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "dec79ca7-3b54-432a-be1e-c96e7a2c7150", "ParentId": "ad147f6d-613f-4d2d-8c84-b749d0754f3b", "EnCode": "ClienCashBalanceReport", "FullName": "现金报表", "Icon": "fa fa-bar-chart", "UrlAddress": "/CustomerManage/CashBalanceReport/Index", "Target": "iframe", "IsMenu": 1, "AllowExpand": 1, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 9, "DeleteMark": 0, "EnabledMark": 1, "Description": "收支报表", "CreateDate": "2016-04-28 15:12:16", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-05-27 16:29:15", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "ddce0dc1-3345-41b7-9716-22641fbbfaed", "ParentId": "6", "EnCode": "rpt001", "FullName": "销售日报表", "Icon": "fa fa-pie-chart", "UrlAddress": "/ReportManage/Report/ReportPreview?keyValue=a9762855-cd45-4815-a8e1-c8b818f79ad5", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 9, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-03-22 16:55:20", "CreateUserId": "eab01522-f4fe-48ce-8db6-76fd7813cdf5", "CreateUserName": "刘晓雷", "ModifyDate": "2016-03-29 16:53:54", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "62af0605-4558-47b1-9530-bc3515036b37", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_PaymentAccount", "FullName": "收支账户", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_PaymentAccount", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 9, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-04-20 09:54:48", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-20 09:55:13", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }, { "ModuleId": "eab4a37f-d976-42b7-9589-489ed0678151", "ParentId": "16d4e2d5-d154-455f-94f7-63bf80ab26aa", "EnCode": "Client_ExpensesType", "FullName": "支出种类", "Icon": "fa fa-tag", "UrlAddress": "/SystemManage/DataItemList/Index?ItemCode=Client_ExpensesType", "Target": "iframe", "IsMenu": 1, "AllowExpand": 0, "IsPublic": 0, "AllowEdit": null, "AllowDelete": null, "SortCode": 10, "DeleteMark": 0, "EnabledMark": 1, "Description": null, "CreateDate": "2016-04-20 15:06:10", "CreateUserId": "System", "CreateUserName": "超级管理员", "ModifyDate": "2016-04-20 15:06:46", "ModifyUserId": "System", "ModifyUserName": "超级管理员" }];
                var color = ["2e99d4", "fe8977 ", "9dd6d7  ", "b5adab", "8ebdd4", "edd46e", "64cfa7", "FFA300", "708FE3", "D972E3", "56BD4E", "1ABC9C", "2e99d4"]
                var _html = "";
                $.each(data, function (i) {
                    var row = data[i];
                    if (row.Target == "iframe") {
                        var colorindex = Math.round(Math.random() * 9 + 1);
                        _html += '<li class="menuItem" data-id="' + row.ModuleId + '" href="' + row.UrlAddress + '">';
                        _html += '    <div class="icon" style="background: #' + color[colorindex] + ';">';
                        _html += '        <i class="fa ' + row.Icon + '"></i>';
                        _html += '     </div>';
                        _html += '     <div class="icon-text">';
                        _html += '         <span>' + row.FullName + '</a>';
                        _html += '     </div>';
                        _html += '</li>';
                    }
                });
                $(".slidebox ul").append(_html);
                var ul = $(".slidebox > ul");
                var lis = ul.children("li");
                lis.each(function (i, dom) {
                    if (i % 28 == 0) {
                        ul.before('<ul>');
                    }
                    ul.prev("ul").append(dom);
                });
                ul.remove();

                var slideboxCount = Math.ceil($(".slidebox>ul>li").length / 28);
                for (var i = 0; i < slideboxCount; i++) {
                    $(".slidebox-slider ul").prepend('<li><i class="fa fa-circle"></i></li>');
                }
                $(".slidebox-slider ul li:first").addClass("active");
                $(".slidebox-slider ul li").click(function () {
                    $(".slidebox-slider ul li").removeClass("active");
                    $(this).addClass("active");
                    var index = $(this).index();
                    $(".slidebox").animate({ "left": -$(".slidebox>ul").width() * index }, 1000);
                });
                $(".slidebox>ul").width($(".slidebox").width());
                $(".slidebox").width($(".slidebox").width() * $(".slidebox>ul").length);
            }
        },
        indexOut: function () {
            dialogConfirm("注：您确定要安全退出本次登录吗？", function (r) {
                if (r) {
                    Loading(true, "正在安全退出...");
                    window.setTimeout(function () {
                        $.ajax({
                            url: contentPath + "/Login/OutLogin",
                            type: "post",
                            dataType: "json",
                            success: function (data) {
                                window.location.href = contentPath + "/Login/Index";
                            }
                        });
                    }, 500);
                }
            });
        }
    };
})(jQuery);