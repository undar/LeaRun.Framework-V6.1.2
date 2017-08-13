var tablist = {
    newTab: function (item) {
        var dataId = item.id;
        var dataUrl = item.url;
        var menuName = '<i class="' + item.icon + '"></i>' + item.title;
        var flag = true;
        if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
            return false;
        }
        $('.menuTab').each(function () {
            if ($(this).data('id') == dataUrl) {
                if (!$(this).hasClass('active')) {
                    $(this).addClass('active').siblings('.menuTab').removeClass('active');
                    $.learuntab.scrollToTab(this);
                    $('#mainContent .LRADMS_iframe').each(function () {
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
            var str = '<a href="javascript:;" class="active menuTab" data-id="' + dataUrl + '">' + menuName + ' <div class="tab_close "></div></a>';
            $('.menuTab').removeClass('active');
            var str1 = '<iframe class="LRADMS_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
            $('#mainContent').find('iframe.LRADMS_iframe').hide();
            $('#mainContent').append(str1);
            Loading(true);
            $('#mainContent iframe:visible').load(function () {
                Loading(false);
            });
            $('.menuTabs .page-tabs-content').append(str);
            $.learuntab.scrollToTab($('.menuTab.active'));
        }

    }
};
(function ($) {
    "use strict";
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
                $('#mainContent .LRADMS_iframe').each(function () {
                    if ($(this).data('id') == currentId) {
                        $(this).show().siblings('.LRADMS_iframe').hide();
                        return false;
                    }
                });
                $(this).addClass('active').siblings('.menuTab').removeClass('active');
                $.learuntab.scrollToTab(this);
            }
            var dataId = $(this).attr('data-value');
            if (dataId != "") {
                top.$.cookie('currentmoduleId', dataId, { path: "/" });
            }
        },
        closeOtherTabs: function () {
            $('.page-tabs-content').children("[data-id]").find('.tab_close').parents('a').not(".active").each(function () {
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

                    $('#mainContent .LRADMS_iframe').each(function () {
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
                    $('#mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
                if ($(this).parents('.menuTab').prev('.menuTab').size()) {
                    var activeId = $(this).parents('.menuTab').prev('.menuTab:last').data('id');
                    $(this).parents('.menuTab').prev('.menuTab:last').addClass('active');
                    $('#mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == activeId) {
                            $(this).show().siblings('.LRADMS_iframe').hide();
                            return false;
                        }
                    });
                    $(this).parents('.menuTab').remove();
                    $('#mainContent .LRADMS_iframe').each(function () {
                        if ($(this).data('id') == closeTabId) {
                            $(this).remove();
                            return false;
                        }
                    });
                }
            }
            else {
                $(this).parents('.menuTab').remove();
                $('#mainContent .LRADMS_iframe').each(function () {
                    if ($(this).data('id') == closeTabId) {
                        $(this).remove();
                        return false;
                    }
                });
                $.learuntab.scrollToTab($('.menuTab.active'));
            }
            var dataId = $('.menuTab.active').attr('data-value');
            if (dataId != "") {
                top.$.cookie('currentmoduleId', dataId, { path: "/" });
            }
            return false;
        },
        addTab: function () {
            var dataId = $(this).attr('data-id');
            if (dataId != "") {
                top.$.cookie('currentmoduleId', dataId, { path: "/" });
            }
            var dataUrl = $(this).attr('href');
            var menuName = $.trim($(this).html());
            if (dataUrl == undefined || $.trim(dataUrl).length == 0) {
                return false;
            }
            var flag = true;
            $('.menuTab').each(function () {
                if ($(this).data('id') == dataUrl) {
                    if (!$(this).hasClass('active')) {
                        $(this).addClass('active').siblings('.menuTab').removeClass('active');
                        $.learuntab.scrollToTab(this);
                        $('#mainContent .LRADMS_iframe').each(function () {
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
                var str = '<a href="javascript:;" class="active menuTab" data-value=' + dataId + ' data-id="' + dataUrl + '">' + menuName + '<div class="tab_close "></div></a>';
                $('.menuTab').removeClass('active');
                var str1 = '<iframe class="LRADMS_iframe" id="iframe' + dataId + '" name="iframe' + dataId + '"  width="100%" height="100%" src="' + dataUrl + '" frameborder="0" data-id="' + dataUrl + '" seamless></iframe>';
                $('#mainContent').find('iframe.LRADMS_iframe').hide();
                $('#mainContent').append(str1);
                Loading(true);
                $('#mainContent iframe:visible').load(function () {
                    Loading(false);
                });
                $('.menuTabs .page-tabs-content').append(str);
                $.learuntab.scrollToTab($('.menuTab.active'));
            }
            $(this).parents('.popover-moreMenu').hide();
            $(this).parents('.popover-menu').hide();
            $(this).parents('.popover-menu-sub').hide();

            return false;
        },
        scrollTabRight: function () {
            var marginLeftVal = Math.abs(parseInt($('.page-tabs-content').css('margin-left')));
            var tabOuterWidth = $.learuntab.calSumWidth($(".lea-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".lea-tabs").outerWidth(true) - tabOuterWidth;
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
            var tabOuterWidth = $.learuntab.calSumWidth($(".lea-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".lea-tabs").outerWidth(true) - tabOuterWidth;
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
            var tabOuterWidth = $.learuntab.calSumWidth($(".lea-tabs").children().not(".menuTabs"));
            var visibleWidth = $(".lea-tabs").outerWidth(true) - tabOuterWidth;
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
            $('.menuTabs').on('click', '.menuTab .tab_close', $.learuntab.closeTab);
            $('.menuTabs').on('click', '.menuTab', $.learuntab.activeTab);

            $('.tabLeft').on('click', $.learuntab.scrollTabLeft);
            $('.tabRight').on('click', $.learuntab.scrollTabRight);
            $('.tabReload').on('click', $.learuntab.refreshTab);
            $('.tabCloseCurrent').on('click', function () {
                $('.page-tabs-content').find('.active .tab_close').trigger("click");
            });
            $('.tabCloseAll').on('click', function () {
                $('.page-tabs-content').children("[data-id]").find('.tab_close').each(function () {
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
                    $(this).removeAttr('fullscreen');
                    $.learuntab.exitFullscreen();
                }
            });


        }
    };
    $.learunindex = {
        load: function () {
            $("#mainContent").height($(window).height() - 128);
            $(window).resize(function (e) {
                $("#mainContent").height($(window).height() - 128);
                $.learunindex.loadMenu(true);
            });
            //个人中心
            $("#UserSetting").click(function () {
                tablist.newTab({ id: "UserSetting", title: "个人中心", closed: true, icon: "fa fa fa-user", url: contentPath + "/PersonCenter/Index" });
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
        loadMenu: function (isInit) {
            var flag = false;
            var topMenuWidth = $('.lea-Head').width() - $.learuntab.calSumWidth($(".lea-Head").children().not(".left-bar"));

            var data = authorizeMenuData;
            var _html = "";
            var menuWidth = 0;
            var _html1 = "", _html2 = "";
            $.each(data, function (i) {
                var row = data[i];
                if (row.ParentId == "0") {
                    var _itemHtml = "";
                    _itemHtml += '<li class="treeview">';
                    _itemHtml += '<a>';
                    _itemHtml += '<i class="' + row.Icon + '"></i><span>' + row.FullName + '</span>';
                    _itemHtml += '</a>';
                    var childNodes = $.learunindex.jsonWhere(data, function (v) { return v.ParentId == row.ModuleId });
                    if (childNodes.length > 0) {
                        _itemHtml += '<div class="popover-menu"><div class="arrow"><em></em><span></span></div><ul class="treeview-menu">';
                        $.each(childNodes, function (i) {
                            var subrow = childNodes[i];
                            var subchildNodes = $.learunindex.jsonWhere(data, function (v) { return v.ParentId == subrow.ModuleId });
                            _itemHtml += '<li>';

                            if (subchildNodes.length > 0) {
                                _itemHtml += '<a class="menuTreeItem menuItem" href="#"><i class="' + subrow.Icon + ' firstIcon"></i>' + subrow.FullName + '';
                                _itemHtml += '<i class="fa fa-angle-right pull-right"></i></a>';

                                _itemHtml += '<div class="popover-menu-sub"><ul class="treeview-menu">';
                                $.each(subchildNodes, function (i) {
                                    var subchildNodesrow = subchildNodes[i];
                                    _itemHtml += '<li><a class="menuItem menuiframe" data-id="' + subchildNodesrow.ModuleId + '" href="' + subchildNodesrow.UrlAddress + '"><i class="' + subchildNodesrow.Icon + ' firstIcon"></i>' + subchildNodesrow.FullName + '</a></li>';
                                });
                                _itemHtml += '</ul></div>';
                            } else {
                                _itemHtml += '<a class="menuItem menuiframe" data-id="' + subrow.ModuleId + '" href="' + subrow.UrlAddress + '"><i class="' + subrow.Icon + ' firstIcon"></i>' + subrow.FullName + '</a>';
                            }
                            _itemHtml += '</li>';
                        });
                        _itemHtml += '</ul></div>';
                    }
                    _itemHtml += '</li>';
                    menuWidth += 88;
                    if (menuWidth > topMenuWidth) {
                        _html2 += _itemHtml;
                    }
                    else if ((menuWidth + 88) > topMenuWidth) {
                        _html1 = _itemHtml;
                    }
                    else {
                        _html += _itemHtml;
                    }
                }
            });
            if (menuWidth > topMenuWidth) {
                _html2 = _html1 + _html2;
                _html += ' <li class="treeview" id="moreMenu"><a ><i class="fa fa-reorder"></i><span>更多应用</span></a></li>';
                flag = true;
            }
            else {
                _html += _html1;
            }

            if (isInit || flag) {
                $("#top-menu").html(_html);
                if (flag) {
                    $('#moreMenu').append('<div class="popover-moreMenu"><div class="arrow"><em></em><span></span></div><div class="title">更多应用</div><div class="moresubmenu"></div></div>');
                    $('.moresubmenu').html(_html2);
                    //更多应用菜单点击事件
                    $('.moresubmenu > .treeview > a').unbind();
                    $('.moresubmenu > .treeview > a').on('click', function () {
                        $('.moresubmenu > .treeview > a.active').parent().find('.popover-menu').hide();
                        $('.moresubmenu > .treeview > a.active').removeClass('active');
                        var $li = $(this);
                        $li.addClass('active');
                        $li.parent().find('.popover-menu').show();
                    });
                }
                $("#top-menu>.treeview").unbind();
                $('.popover-menu>ul>li').unbind();
                $("#top-menu>.treeview").hover(
                    function () {
                        var $li = $(this);

                        var $moreMenuPopover = $li.find('.popover-moreMenu');
                        $li.addClass('active');
                        if ($moreMenuPopover.length > 0) {
                            $moreMenuPopover.slideDown(150);
                            $($moreMenuPopover.find('.treeview>a')[0]).trigger('click');
                        }
                        else {
                            var $popover = $li.find('.popover-menu');
                            $popover.slideDown(150);
                        }
                    },
                    function () {
                        var $li = $(this);
                        var $popover = $li.find('.popover-menu');
                        var $moreMenuPopover = $li.find('.popover-moreMenu');
                        if ($moreMenuPopover.length == 0) {
                            $popover.slideUp(50);
                        }
                        else {
                            $moreMenuPopover.hide();
                        }
                        $li.removeClass('active');
                    });
                $('.popover-menu>ul>li').hover(
                    function () {
                        var $li = $(this);
                        if ($li.parents('.moresubmenu').length == 0) {
                            var windowWidth = $(window).width();
                            var windowHeight = $(window).height();
                            var $popover = $li.find('.popover-menu-sub');
                            var subHeight = $popover.height();
                            if ((windowWidth - $li.offset().left - 154) < 152) {
                                $popover.css("left", "-156px");
                            }
                            if ((subHeight - 10 + $li.offset().top) > windowHeight) {
                                var marginTop = subHeight - 10 + $li.offset().top - windowHeight + 46;
                                $popover.css('margin-top', '-' + marginTop + 'px');
                            }
                            $li.addClass('active');
                            $popover.slideDown(150);
                        }
                    },
                    function () {
                        var $li = $(this);
                        if ($li.parents('.moresubmenu').length == 0) {
                            var $popover = $li.find('.popover-menu-sub');
                            $li.removeClass('active');
                            $popover.css('margin-top', '-46px');
                            $popover.slideUp(50);
                        }
                    });
                $('.menuiframe').unbind();
                $('.menuiframe').on('click', $.learuntab.addTab);

                $('.moresubmenu .menuTreeItem ').unbind();
                $('.menuTreeItem ').on('click', function () {
                    $('.moresubmenu .popover-menu-sub').slideUp(300);
                    var $this = $(this);
                    if (!$this.hasClass('active')) {
                        var $sub = $(this).parent().find('.popover-menu-sub');
                        $this.addClass('active');
                        $sub.slideDown(300);
                    }
                    else {
                        $this.removeClass('active');
                    }
                });

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
    $(function () {
        $.learunindex.loadMenu(true);
        $.learuntab.init();
        $.learunindex.load();
    });
})(jQuery);

//安全退出
function IndexOut() {
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
