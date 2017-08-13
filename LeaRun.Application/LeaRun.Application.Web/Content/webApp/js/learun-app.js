// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var app = angular.module('starter', ['ionic', 'starter.uirouter', 'starter.directive', 'starter.component'])
.run(['$ionicPlatform', '$rootScope', '$state', function ($ionicPlatform, $rootScope, $state) {
    $ionicPlatform.ready(function () {
        // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
        // for form inputs)
        if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
            cordova.plugins.Keyboard.hideKeyboardAccessoryBar(false);
            cordova.plugins.Keyboard.disableScroll(true);
        }
        if (window.StatusBar) {
            StatusBar.styleDefault();
        }
    });
}])
.config(['$ionicConfigProvider', function ($ionicConfigProvider) {
    $ionicConfigProvider.backButton.previousTitleText(false);
    $ionicConfigProvider.backButton.text('').icon('ion-ios-arrow-left');
    $ionicConfigProvider.scrolling.jsScrolling(true);
    $ionicConfigProvider.platform.ios.tabs.style('standard');
    $ionicConfigProvider.platform.ios.tabs.position('bottom');
    $ionicConfigProvider.platform.android.tabs.style('standard');
    $ionicConfigProvider.platform.android.tabs.position('bottom');
    $ionicConfigProvider.platform.ios.navBar.alignTitle('center');
    $ionicConfigProvider.platform.android.navBar.alignTitle('center');
    $ionicConfigProvider.platform.ios.views.transition('ios');
    $ionicConfigProvider.platform.android.views.transition('android');
}])
.factory('$lrfnGuid', function () {
    return function () {
        var guid = "";
        for (var i = 1; i <= 32; i++) {
            var n = Math.floor(Math.random() * 16.0).toString(16);
            guid += n;
            if ((i == 8) || (i == 12) || (i == 16) || (i == 20)) guid += "-";
        }
        return guid;
    };
})
.factory('$lrDoBroadcastToparent', function () {
    return function (name, data) {
        parent.$.webappDesigner.doBroadcast(name, data);//告诉设计页已经加载完成
    }
})
.controller('modelCtrl', ['$rootScope', '$scope', '$state', '$ionicScrollDelegate', '$lrComponent', '$lrDoBroadcastToparent', function ($rootScope, $scope, $state, $ionicScrollDelegate, $lrComponent, $lrDoBroadcastToparent) {

    if ($rootScope.iframeLoaded != true) {
        $rootScope.$on('init', function (event, data) {
            var contentDiv = angular.element(document.querySelector('ion-content .scroll'));
            $scope.modelTitle = "页面";
            $scope.modelbgColor = "#ffffff";
            $scope.modelPadding = true;
            $scope.modelHeadHide = false;

            //初始化设计器
            parent.$('.lr-app-componentContent .lr-component').draggable({
                connectToSortable: 'ion-content .scroll',
                helper: "clone",
                revert: "invalid",
                iframeFix: true,
                iframeObj: parent.frames["phoneDIndex"].contentWindow
            });
            $('ion-content .scroll').sortable({
                opacity: 0.6,
                cursor: 'move',
                placeholder: "lr-component-state",
                start: function (event, ui) {
                    var $this_place = $(ui.item[0]);
                    var componentType = $this_place.attr('data-value');
                    $(".lr-component-state").addClass('lr-component-item');
                    $(".lr-component-state").html(sortableStartComponent(componentType, $this_place.children()));

                    $('.ic-select-box').hide();
                },
                stop: function (event, ui) {
                    var $this_place = $(ui.item[0]).removeAttr('style');
                    $this_place.removeAttr('class');
                    $this_place.addClass('lr-component-item');
                    var componentType = $this_place.attr('data-value');
                    if ($lrComponent[componentType] != null) {
                        $lrComponent[componentType]($this_place, true);
                    }
                    $('.ic-select-box').show();
                }
            });
            if (data != undefined) {
                doBroadcast(appBroadcastCode.cgSelectTemplatePage, { cmd: "initPage", object: data });
            }
        });
        //模板页切换
        $rootScope.$on(appBroadcastCode.cgSelectTemplatePage, function (event, data) {
            switch (data.cmd) {
                case "selectPage":
                    $state.go("model");
                case "initPage":
                case "removePage":
                    $('.ic-select-box').hide();
                    $scope.modelTitle = data.object.text;
                    $scope.modelbgColor = data.object.attr.bgColor;
                    $scope.modelPadding = (data.object.attr.isPadding == "true" ? true : false);
                    $scope.modelHeadHide = (data.object.attr.isHeadHide == "true" ? true : false);
                    $scope.$apply();
                    $('ion-content .scroll').html("");
                    for (var i in data.object.ChildNodes) {
                        var _node = data.object.ChildNodes[i];
                        var $node = $('<div class="lr-component-item" data-value="' + _node.attr.value + '" ></div>');
                        if ($lrComponent[_node.attr.value] != undefined) {
                            $lrComponent[_node.attr.value]($node, false, _node.attr);
                            $('ion-content .scroll').append($node);
                        }
                    }
                    break;
                case "addPage":
                    $scope.modelTitle = data.object.text;
                    $scope.modelbgColor = data.object.attr.bgColor;
                    $scope.modelPadding = (data.object.attr.isPadding == "true" ? true : false);
                    $scope.modelHeadHide = (data.object.attr.isHeadHide == "true" ? true : false);
                    $('ion-content .scroll').html("");
                    $scope.$apply();
                    break
                case "selectComponent":
                    if ($state.current.name != "model") {
                        $('.ic-select-box').hide();
                        $state.go("model");
                        $scope.modelTitle = data.currentPage.text;
                        $scope.modelbgColor = data.currentPage.attr.bgColor;
                        $scope.modelPadding = (data.currentPage.attr.isPadding == "true" ? true : false);
                        $scope.modelHeadHide = (data.currentPage.attr.isHeadHide == "true" ? true : false);

                        $('ion-content .scroll').html("");
                        for (var i in data.currentPage.ChildNodes) {
                            var _node = data.currentPage.ChildNodes[i];
                            var $node = $('<div class="lr-component-item" data-value="' + _node.attr.value + '"></div>');
                            if ($lrComponent[_node.attr.value] != undefined) {
                                $lrComponent[_node.attr.value]($node, false, _node.attr);
                                $('ion-content .scroll').append($node);
                            }
                        }
                        setTimeout(function () {
                            $('.ic-select-box').show();
                            $('ion-content .scroll').find('[data-value="' + data.object.id + '"]').trigger('click');
                        }, 600);

                        $scope.$apply();
                    }
                    else {
                        $('ion-content .scroll').find('[data-value="' + data.object.id + '"]').trigger('click');
                    }
                    break;
                case "removeComponent":
                    $('.ic-select-box').hide();
                    $('ion-content .scroll').html("");
                    for (var i in data.object.ChildNodes) {
                        var _node = data.object.ChildNodes[i];
                        var $node = $('<div class="lr-component-item" data-value="' + _node.attr.value + '" ></div>');
                        if ($lrComponent[_node.attr.value] != undefined) {
                            $lrComponent[_node.attr.value]($node, false, _node.attr);
                            $('ion-content .scroll').append($node);
                        }
                    }
                    break;
                    break;
                case "lrTabs":
                    $('.ic-select-box').hide();
                    $rootScope.tabsData = data.data;
                    $rootScope.focusTabId = data.focus.id;
                    if ($state.current.name != "tabs") {
                        $state.go("tabs");
                    }
                    else {
                        var id = data.focus.id;
                        if (id != 'lrTabs') {
                            $('.tab-item[data-value="' + id + '"]').trigger('click');
                        }
                    }
                    break;
            }
        });
        //属性设置
        $rootScope.$on(appBroadcastCode.cgAttr, function (event, data) {
            switch (data.cmd) {
                case "pageText"://页面标题改变
                    $scope.modelTitle = data.value;
                    break;
                case "pageBgColor":
                    $scope.modelbgColor = data.value;
                    break;
                case "pageIsPadding":
                    $scope.modelPadding = (data.value == "true" ? true : false);

                    if ($selectBoxCurrent != null) {
                        setTimeout(function () {
                            $selectBoxCurrent.trigger('click');
                        }, 10);
                    }
                    break;
                case "pageIsHeadHide":
                    $scope.modelHeadHide = (data.value == "true" ? true : false);

                    if ($selectBoxCurrent != null) {
                        setTimeout(function () {
                            $selectBoxCurrent.trigger('click');
                        }, 10);
                    }
                    break;

                case "tabsBgColor"://页面标题改变
                    var _class = 'tabs-background-' + data.value;
                    setTabsClass(_class, "data-bg");
                    break;
                case "tabsIconColor":
                    var _class = 'tabs-color-' + data.value;
                    setTabsClass(_class, "data-icon");
                    break;
                case "tabsIconType":
                    var _class = 'tabs-icon-' + data.value;
                    setTabsClass(_class, "data-type");
                    break;

                case "tabText":
                    var $obj = $('.tab-item-active');
                    $obj.attr('title', data.value);
                    $obj.find('span').html(data.value);
                    break;
                case "tabIconOn":
                    var $obj = $('.tab-item-active');
                    var ion = $obj.attr('icon-on');
                    $obj.attr('icon-on', data.value);
                    $obj.find('i').removeClass(ion);
                    $obj.find('i').addClass(data.value);
                    break;
                case "tabIconOff":
                    var $obj = $('.tab-item-active');
                    var ion = $obj.attr('icon-on');
                    $obj.attr('icon-off', data.value);
                    break;
                case "componentAttr":
                    $selectBoxCurrent.setAttrLr(data);
                    break;
            }
            $scope.$apply();
        });

        $rootScope.iframeLoaded = true;
        $lrDoBroadcastToparent('iframeLoaded');
    }
    //选中框按钮操作
    $scope.$on(appBroadcastCode.btnSelectBox, function (event, data) {
        $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { cmd: data.cmd, id: data.id });
    });

    $scope.scroll = function () {
        $rootScope.$broadcast("componentScroll", $ionicScrollDelegate.$getByHandle('componentScroll').getScrollPosition());
    }
    function setTabsClass(_class, type) {
        var $obj = $('ion-tabs');
        var _oldClass = $obj.attr(type);
        $obj.attr(type, _class);
        $obj.removeClass(_oldClass);
        $obj.addClass(_class);
    }
}])
.controller('tabsCtrl', ['$rootScope', '$scope', '$lrDoBroadcastToparent', '$ionicTabsDelegate', '$lrfnGuid', function ($rootScope, $scope, $lrDoBroadcastToparent, $ionicTabsDelegate, $lrfnGuid) {
    $scope.modelTitle = "Tabs控制器";
    $scope.tabs = $rootScope.tabsData;
    function doclick() {
        var $selectBox = $('.ic-select-box');
        $selectBox.show();
        var $obj = $(this);
        var id = $obj.attr('data-value');
        $selectBox.attr('data-value', id)
                        .width($obj.width())
                        .height($obj.height())
                        .offset($obj.offset());
        $('.ic-selected').removeClass("ic-selected");
        $obj.addClass("ic-selected");
        $selectBoxCurrent = $obj;
        for (var i in $scope.tabs) {
            if ($scope.tabs[i].id == id) {
                $ionicTabsDelegate.select(parseInt(i));
                $scope.$apply();
                break;
            }
        }
        $lrDoBroadcastToparent(appBroadcastCode.doTabTemplatePhone, { cmd: "selectTab", id: id });
    }


    $scope.$on('$ionicView.afterEnter', function () {
        //设置下tab项绑定点击事件
        $('.tab-item').on('click', doclick);
        $('.tab-item[data-value="' + $rootScope.focusTabId + '"]').trigger('click');
    });
    //模板增加
    $scope.$on(appBroadcastCode.doTabTemplate, function (event, data) {
        switch (data.cmd) {
            case "addTab":
                $scope.$apply();
                setTimeout(function () {
                    $('.tab-item').on('click', doclick);
                    $('.tab-item[data-value="' + data.id + '"]').trigger('click');
                }, 50);
                break;
            case "removeTab":
                $scope.$apply();
                $('.tab-item[data-value="' + data.id + '"]').trigger('click');
                break;
        }


    });

    $scope.$on(appBroadcastCode.btnSelectBox, function (event, data) {
        var _obj = {};
        for (var i in $scope.tabs) {
            var _item = $scope.tabs[i];
            if (_item.id == data.id) {
                _obj = _item;
                break;
            }
        }
        if (_obj.id != undefined) {
            $lrDoBroadcastToparent(appBroadcastCode.doTabTemplatePhone, { cmd: data.cmd, object: _obj });
        }
    });
}]);

//广播一个数据
function doBroadcast(name, data) {
    var $rootScope = angular.element(document.querySelector('[ ng-app="starter"]')).scope().$root;
    $rootScope.$broadcast(name, data);
};
//
function sortableStartComponent(componentType, node) {
    var componentHtml = "";

    switch (componentType) {
        case "lrHeader":
            if (node.hasClass("fa")) {
                componentHtml = '<h1 class="lr-component-heading">标题</h1>';
            }
            else {
                componentHtml = '<h1 class="lr-component-heading">' + node.text() + '</h1>';
            }

            break;
        case "lrParagraph":
            if (node.hasClass("fa")) {
                componentHtml = '<p class="lr-component-one">请填写内容</p>';
            }
            else {
                componentHtml = '<p class="lr-component-one">' + node.html() + '</p>';
            }
            break;
        case "lrBtn":
            if (node.hasClass("fa")) {
                componentHtml = '<a class="button button-block" style="border:1px dashed #ccc;">按钮</a>';
            }
            else {
                componentHtml = '<a class="button button-block" style="border:1px dashed #ccc;">' + node.text() + '</a>';
            }
            break;
        case "lrInput":
            if (node.hasClass("fa")) {
                componentHtml = '<div class="item item-input" style="padding:10px;color: #B8B8B8;border:1px dashed #ccc;"  >输入框</div>';
            }
            else {
                componentHtml = '<div class="item item-input" style="padding:10px;color: #B8B8B8;border:1px dashed #ccc;"  >' + node.html() + '</div>';
            }
            break;
        case "lrList3":
            if (node.hasClass("fa")) {
                componentHtml = '<div class="lr-list-type3"><div class="item item-icon-left" style="border:1px dashed #ccc;color:#ccc">';
                componentHtml += '<i class="icon ion-ios-barcode-outline royal-bg" data-icon="ion-ios-barcode-outline" data-color="royal"></i>';
                componentHtml += '<span class="ng-binding">列表类型一</span></div></div>';
            }
            else {
                componentHtml = '<div class="lr-list-type3"  ><div class="item item-icon-left" style="border:1px dashed #ccc;color:#ccc">';
                componentHtml += node.find('div').html();
                componentHtml += '</div></div>';
            }
            break;
        case "lrList4":
            if (node.hasClass("fa")) {
                componentHtml = ' <ion-item class="item-icon-right lr-iconitem item" style="border:1px dashed #ccc;">';
                componentHtml += '<i class="icon ion-ios-paper-outline bgcolor_b" data-icon="ion-ios-paper-outline" data-color="bgcolor_b"></i>';
                componentHtml += '<span class="ng-binding">列表类型二</span>';
                componentHtml += '<i class="icon ion-chevron-right icon-accessory "></i>';
                componentHtml += '</ion-item>';
            }
            else {
                componentHtml = ' <ion-item class="item-icon-right lr-iconitem item"  style="border:1px dashed #ccc;">' + node.html() + '</ion-item>';
            }
            break;
    }
    return componentHtml;
}
function sortableStopComponent(componentType) {
    var componentHtml = "";
    switch (componentType) {
        case "lrHeader":
            componentHtml = '<lr-component-header></lr-component-header>';
            break;
    }
    return componentHtml;
}


