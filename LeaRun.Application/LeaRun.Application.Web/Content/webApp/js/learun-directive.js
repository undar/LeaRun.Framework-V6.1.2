
/**
 * Created by cbb on 16/6/5.
 */
angular.module('starter.directive', [])
    //标题组件
  .directive('lrComponentHeader', ['$rootScope', '$lrfnGuid', function ($rootScope, $lrfnGuid) {
      return {
          restrict: 'E',
          replace: true,
          template: '<h1  class="lr-component-heading lr-f ic-selected">标题</h1>',
          link: function (scope, element, attrs) {
              $(element).attr('data-value', $lrfnGuid());
              $rootScope.$on('componentScroll', function (event, data) {
                  var $obj = $(element);
                  var $selectBox = $('.ic-select-box');
                  if ($selectBox.attr('data-value') == $obj.attr('data-value')) {
                      $selectBox.offset($obj.offset());
                  }
              });
              element.on('click', function () {
                  var $obj = $(element);
                  $('.ic-select-box')
                      .attr('data-value', $obj.attr('data-value'))
                      .width($obj.width())
                      .height($obj.height())
                      .offset($obj.offset());
              });

              window.onresize = function () {
                  var $obj = $(element);
                  var $selectBox = $('.ic-select-box');
                  if ($selectBox.attr('data-value') == $obj.attr('data-value')) {
                      $selectBox.width($obj.width());
                  }
              }
          }
      };
  }])
.directive('lrSelectBox', function () {
    return {
        restrict: "E",
        replace: true,
        template: '<div class="ic-select-box ic-select-box-can-delete" style="width: 300px; height: 43px;display:none;">' +
                '<div class="control-container">' +
                '<a class="select-duplicate" ><span class="fa fa-clone"></span></a>' +
                '<a class="select-remove" ><span class="fa fa-trash-o"></span></a>' +
                '</div>' +
                '</div>',
        link: function (scope, element, attr) {
            element.find('a').on('mouseover', function () {
                element.removeClass('duplicatebox');
                element.removeClass('removebox');
                if ($(this).is('.select-duplicate')) {
                    element.addClass('duplicatebox');
                }
                else {
                    element.addClass('removebox');
                }

            });
            element.find('a').on('mouseout', function () {
                element.removeClass('duplicatebox');
                element.removeClass('removebox');
            });

            //复制
            element.find('a').on('click', function () {
                var _id = $('.ic-select-box').attr("data-value");
                if ($(this).is('.select-duplicate')) {

                    doBroadcast(appBroadcastCode.btnSelectBox, { "cmd": "duplicate", "id": _id });
                }
                else {
                    doBroadcast(appBroadcastCode.btnSelectBox, { "cmd": "remove", "id": _id });
                }

            });


        }
    }
})
;