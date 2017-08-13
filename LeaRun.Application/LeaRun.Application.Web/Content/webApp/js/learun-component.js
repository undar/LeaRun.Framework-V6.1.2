var $selectBoxCurrent = null;
angular.module('starter.component', [])
.factory('$lrComponent', ['$rootScope', '$lrfnGuid', '$lrDoBroadcastToparent', function ($rootScope, $lrfnGuid, $lrDoBroadcastToparent) {
    return {
        lrHeader: function ($div, sortable, componentData) {
            var isNewed = $div.find('.lr-component-heading').length;
            if (isNewed == 0) {
                var _hSize = { "H1": "36px", "H2": "30px", "H3": "24px", "H4": "18px", "H5": "14px" };
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "text":
                            $selectBoxCurrent.text(data.object.text);
                            break;
                        case "size":
                            $selectBoxCurrent.css("font-size", _hSize[data.object.attr.size]);
                            break;
                        case "weightSize":
                            $selectBoxCurrent.css("font-weight", data.object.attr.weight.size);
                            break;
                        case "isItalic":
                            var _style = data.object.attr.weight.isItalic == "true" ? 'italic' : 'normal';
                            $selectBoxCurrent.css("font-style", _style);
                            break;
                        case "color":
                            $selectBoxCurrent.css("color", data.object.attr.color);
                            break;
                        case "align":
                            $selectBoxCurrent.css("text-align", data.object.attr.align);
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),
                    size: "H1",
                    weight: {
                        size: '500',
                        isItalic: 'false'
                    },
                    color: "#000000",
                    align: "left",
                    text: "标题",
                    value: "lrHeader",
                    img: "fa fa-header",
                    type: "component"
                };
                if (componentData != undefined) {
                    component = componentData;
                }
                $div.html('<h1  class="lr-component-heading lr-f ">' + component.text + '</h1>');
                $obj = $div.find('.lr-component-heading');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    $selectBox.attr('data-value', obj.id)
                        .width($(this).width())
                        .height($(this).height())
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        $selectBox.width($selectBoxCurrent.width());
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.lr-component-heading').trigger('click');
            }
        },
        lrParagraph: function ($div, sortable, componentData) {
            var isNewed = $div.find('.lr-component-one').length;
            if (isNewed == 0) {
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "content":
                            $selectBoxCurrent.text(data.object.attr.content);
                            break;
                        case "size":
                            $selectBoxCurrent.css("font-size", data.object.attr.size);
                            break;
                        case "color":
                            $selectBoxCurrent.css("color", data.object.attr.color);
                        case "align":
                            $selectBoxCurrent.css("text-align", data.object.attr.align);
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),
                    size: "14px",
                    color: "#000000",
                    align: "left",
                    text: "段落",
                    content: "请填写内容",
                    value: "lrParagraph",
                    type: "component",
                    img: "fa fa-align-left"
                };
                if (componentData != undefined) {
                    component = componentData;
                }
                $div.html('<p class="lr-component-one lr-f ">' + component.content + '</p>');
                $obj = $div.find('.lr-component-one');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    $selectBox.attr('data-value', obj.id)
                        .width($(this).width())
                        .height($(this).height())
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        $selectBox.width($selectBoxCurrent.width());
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.lr-component-one').trigger('click');
            }
        },
        lrBtn: function ($div, sortable, componentData) {
            var isNewed = $div.find('.button').length;
            if (isNewed == 0) {
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "text":
                            $selectBoxCurrent.text(data.object.text);
                            break;
                        case "size":
                            $selectBoxCurrent.css("font-size", data.object.attr.size);
                            break;
                        case "weightSize":
                            $selectBoxCurrent.css("font-weight", data.object.attr.weight.size);
                            break;
                        case "isItalic":
                            var _style = data.object.attr.weight.isItalic == "true" ? 'italic' : 'normal';
                            $selectBoxCurrent.css("font-style", _style);
                            break;
                        case "color":
                            $selectBoxCurrent.css("color", data.object.attr.color);
                            break;
                        case "align":
                            $selectBoxCurrent.css("text-align", data.object.attr.align);
                            break;
                        case "btnType":
                            $selectBoxCurrent.removeClass('button-default');
                            $selectBoxCurrent.removeClass('button-clear');
                            $selectBoxCurrent.removeClass('button-outline');
                            $selectBoxCurrent.addClass('button-' + data.object.attr.btnType);
                            break;
                        case "btnTheme":
                            var _class = $selectBoxCurrent.attr("data-btnTheme");
                            $selectBoxCurrent.removeClass('button-' + _class);
                            $selectBoxCurrent.attr("data-btnTheme", data.object.attr.btnTheme);
                            $selectBoxCurrent.addClass('button-' + data.object.attr.btnTheme);
                            break;
                        case "btnSize":
                            $selectBoxCurrent.removeClass('button-standard');
                            $selectBoxCurrent.removeClass('button-large');
                            $selectBoxCurrent.removeClass('button-small');
                            $selectBoxCurrent.addClass('button-' + data.object.attr.btnSize);
                            $selectBoxCurrent.trigger('click');
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),
                    link: "NotLinked",

                    text: "按钮",
                    size: "16px",
                    color: "#ffffff",
                    align: "center",
                    weight: {
                        size: '500',
                        isItalic: 'false'
                    },


                    btnType: "default",
                    btnTheme: "positive",
                    btnSize: "standard",

                    value: "lrBtn",
                    type: "component",
                    img: "fa fa-square"
                };
                if (componentData != undefined) {
                    component = componentData;
                }
                $div.html('<a class=" button button-block button-positive "  data-btnTheme= "button-positive">' + component.text + '</a>');
                $obj = $div.find('.button');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    var _width = $(this)[0].offsetWidth;
                    var _height = $(this)[0].offsetHeight - 1;
                    $selectBox.attr('data-value', obj.id)
                        .width(_width - 2)
                        .height(_height)
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        var _width = $selectBoxCurrent[0].offsetWidth;
                        $selectBox.width(_width - 2);
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.button').trigger('click');
            }
        },
        lrInput: function ($div, sortable, componentData) {
            var isNewed = $div.find('.item-input').length;
            if (isNewed == 0) {
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "placeholder":
                            $selectBoxCurrent.html(data.object.attr.placeholder);
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),

                    text: "输入框",
                    placeholder: "输入框",
                    inputType: "text",
                    name: "",

                    value: "lrInput",
                    type: "component",
                    img: "fa fa-italic"
                };
                if (componentData != undefined) {
                    component = componentData;
                }

                $div.html('<div class="item item-input" style="padding:10px;color: #B8B8B8;"  >输入框</div>');
                $obj = $div.find('.item-input');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    var _width = $(this)[0].offsetWidth;
                    var _height = $(this)[0].offsetHeight - 1;
                    $selectBox.attr('data-value', obj.id)
                        .width(_width - 2)
                        .height(_height)
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        var _width = $selectBoxCurrent[0].offsetWidth;
                        $selectBox.width(_width - 2);
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.item-input').trigger('click');
            }
        },
        lrList3: function ($div, sortable, componentData) {

            var isNewed = $div.find('.lr-list-type3').length;
            if (isNewed == 0) {
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "name":
                            $selectBoxCurrent.find('span').html(data.object.attr.name);
                            break;
                        case "icon":
                            var _$i = $selectBoxCurrent.find('i');
                            var _class = _$i.attr("data-icon");
                            _$i.removeClass(_class);
                            _$i.attr("data-icon", data.object.attr.icon);
                            _$i.addClass(data.object.attr.icon);
                            break;
                        case "color":
                            var _$i = $selectBoxCurrent.find('i');
                            var _class = _$i.attr("data-color");
                            _$i.removeClass(_class + "-bg");
                            _$i.attr("data-color", data.object.attr.color);
                            _$i.addClass(data.object.attr.color + "-bg");
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),

                    text: "列表类型一",
                    name: "列表类型一",
                    icon: "ion-ios-barcode-outline",
                    color: "royal",
                    link: "NotLinked",

                    value: "lrList3",
                    type: "component",
                    img: "fa fa-th-list"
                };
                if (componentData != undefined) {
                    component = componentData;
                }

                var _chtml = '<div class="lr-list-type3"><div class="item item-icon-left" >';
                _chtml += '<i class="icon ion-ios-barcode-outline royal-bg" data-icon="ion-ios-barcode-outline" data-color="royal"></i>';
                _chtml += '<span class="ng-binding">' + component.name + '</span></div></div>';

                $div.html(_chtml);
                $obj = $div.find('.lr-list-type3');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    var _width = $(this)[0].offsetWidth;
                    var _height = $(this)[0].offsetHeight - 2;
                    $selectBox.attr('data-value', obj.id)
                        .width(_width - 2)
                        .height(_height)
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        var _width = $selectBoxCurrent[0].offsetWidth;
                        $selectBox.width(_width - 2);
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.lr-list-type3').trigger('click');
            }
        },
        lrList4: function ($div, sortable, componentData) {
            var isNewed = $div.find('.lr-iconitem').length;
            if (isNewed == 0) {
                var _setAttrLr = function (data) {
                    switch (data.attrName) {
                        case "name":
                            $selectBoxCurrent.find('span').html(data.object.attr.name);
                            break;
                        case "icon":
                            var _$i = $selectBoxCurrent.find('i:first-child');
                            var _class = _$i.attr("data-icon");
                            _$i.removeClass(_class);
                            _$i.attr("data-icon", data.object.attr.icon);
                            _$i.addClass(data.object.attr.icon);
                            break;
                        case "color":
                            var _$i = $selectBoxCurrent.find('i:first-child');
                            var _class = _$i.attr("data-color");
                            _$i.removeClass("bgcolor_" + _class);
                            _$i.attr("data-color", data.object.attr.color);
                            _$i.addClass("bgcolor_" + data.object.attr.color);
                            break;
                    }
                };

                var component = {
                    id: $lrfnGuid(),

                    text: "列表类型一",
                    name: "列表类型二",
                    icon: "ion-ios-paper-outline",
                    color: "b",
                    link: "NotLinked",

                    value: "lrList4",
                    type: "component",
                    img: "fa fa-list-ul"
                };
                if (componentData != undefined) {
                    component = componentData;
                }

                var _chtml = ' <ion-item class="item-icon-right lr-iconitem item" >';
                _chtml += '<i class="icon ion-ios-paper-outline bgcolor_b" data-icon="ion-ios-paper-outline" data-color="bgcolor_b"></i>';
                _chtml += '<span class="ng-binding">' + component.name + '</span>';
                _chtml += '<i class="icon ion-chevron-right icon-accessory "></i>';
                _chtml += '</ion-item>';

                $div.html(_chtml);
                $obj = $div.find('.lr-iconitem');
                $obj.attr('data-value', component.id);
                var $selectBox = $('.ic-select-box');
                $rootScope.$on('componentScroll', function (event, data) {
                    if ($selectBoxCurrent != null) {
                        $selectBox.offset($selectBoxCurrent.offset());
                    }
                });
                $obj.on('click', function () {
                    $selectBox.show();
                    var obj = {
                        value: component.value,
                        id: $(this).attr('data-value')
                    };
                    var _width = $(this)[0].offsetWidth;
                    var _height = $(this)[0].offsetHeight - 2;
                    $selectBox.attr('data-value', obj.id)
                        .width(_width - 2)
                        .height(_height)
                        .offset($(this).offset());
                    $('.ic-selected').removeClass("ic-selected");
                    $(this).addClass("ic-selected");
                    $selectBoxCurrent = $(this);
                    $selectBoxCurrent.setAttrLr = _setAttrLr;
                    $lrDoBroadcastToparent(appBroadcastCode.doComponentPhone, { "cmd": "selectComponent", "id": obj.id });
                });
                window.onresize = function () {
                    if ($selectBoxCurrent != null) {
                        var _width = $selectBoxCurrent[0].offsetWidth;
                        $selectBox.width(_width - 2);
                    }
                };
                if (sortable) {
                    setTimeout(function () {
                        $obj.trigger('click');
                    }, 50);
                    $lrDoBroadcastToparent(appBroadcastCode.addComponent, component);
                }

            }
            else {
                $div.find('.lr-iconitem').trigger('click');
            }
        }
    };
}])
;
