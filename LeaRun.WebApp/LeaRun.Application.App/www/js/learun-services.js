angular.module('starter.services', [])
  //顶上提示框
  .factory('$learunTopAlert', [
    '$rootScope',
    '$compile',
    '$animate',
    '$timeout',
    '$ionicBody',
  function ($rootScope, $compile, $animate, $timeout, $ionicBody) {
      return {
          show: topAlert
      };
      function topAlert(opts) {
          var scope = $rootScope.$new(true);
          ionic.extend(scope, {
              text: "提示消息",
              timeout: 1500
          }, opts || {});
          // Compile the template
          var element = scope.element = $compile('<div class="nav-title-slide-ios7 bar bar-header lr-topAlert"><div class="title title-center header-item" >{{text}}</div></div>')(scope);
          // removes the actionSheet from the screen
          scope.removeSheet = function () {
              if (scope.removed) return;
              scope.removed = true;
              $animate.removeClass(element, 'active').then(function () {
                  scope.$destroy();
                  element.remove();
              });
          };
          scope.showSheet = function () {
              if (scope.removed) return;
              $ionicBody.append(element);
              $animate.addClass(element, 'active');
          };
          scope.showSheet();
          $timeout(function () {
              scope.removeSheet();
          }, scope.timeout);

          return null;
      }
  }])
  //post 请求
  .factory('$learunHttp', [
    '$rootScope',
    '$base64',
    '$http',
    '$learunTopAlert',
    'UserInfo',
  function ($rootScope, $base64, $http, $learunTopAlert, UserInfo) {
      return {
          post: httpPost
      };
      function httpPost(opts) {
          var scope = $rootScope.$new(true);
          ionic.extend(scope, {
              data: {},
              error: function () { }
          }, opts || {});
          var userInfo = UserInfo.get();
          var platform = ionic.Platform.platform();
          $http.post(scope.url,
             $base64.encode(JSON.stringify({
                 "data": scope.data,
                 token: userInfo.token,
                 userid: userInfo.userid,
                 platform: platform,
             })
         ))
          .success(function (data) {
              if(scope.isverify)
              {
                  if (data.status.code != "0") {
                      $learunTopAlert.show({ text: data.status.desc });
                      return false;
                  }
              }
              scope.success(data);
          })
          .error(function () { scope.error(); })
          .finally(function () { scope.finally(); });
      }
  }])
   //本地存储数据
  .factory('$learunLocals', ['$window', function ($window) {
      return {
          //存储单个属性
          set: function (key, value) {
              $window.localStorage[key] = value;
          },
          //读取单个属性
          get: function (key, defaultValue) {
              return $window.localStorage[key] || defaultValue;
          },
          //存储对象，以JSON格式存储
          setObject: function (key, value) {
              $window.localStorage[key] = JSON.stringify(value);
          },
          //读取对象
          getObject: function (key) {
              return JSON.parse($window.localStorage[key] || '{}');
          }
      }
  }])
  //弹层
  .factory('$learunPopup', ['$rootScope', '$ionicPopup', function ($rootScope,$ionicPopup) {
      return {
          confirm: confirm
      }
      function confirm(opts) {
          var scope = $rootScope.$new(true);
          ionic.extend(scope, {
              title: '<p> </p>是否确定?<p> </p>',
              cancelText: '取消',
              okText: '确定',
              okType: 'positive',
              ok: function () { },
              cancel: function () { },
          }, opts || {});

          var confirmPopup = $ionicPopup.confirm(scope);
          confirmPopup.then(function (res) {
              if (res) {
                  scope.ok();
              }
              else {
                  scope.cancel();
              }
          });
      };
  }])
  //触发下拉刷新
  .service('$learunTriggerRefresh', ['$timeout', '$ionicScrollDelegate', function ($timeout, $ionicScrollDelegate) {
      /**
       * Trigger the pull-to-refresh on a specific scroll view delegate handle.
       * @param {string} delegateHandle - The `delegate-handle` assigned to the `ion-content` in the view.
       */
      this.triggerRefresh = function (delegateHandle) {

          $timeout(function () {

              var scrollView = $ionicScrollDelegate.$getByHandle(delegateHandle).getScrollView();

              if (!scrollView) return;

              scrollView.__publish(
                scrollView.__scrollLeft, -scrollView.__refreshHeight,
                scrollView.__zoomLevel, true);

              var d = new Date();

              scrollView.refreshStartTime = d.getTime();

              scrollView.__refreshActive = true;
              scrollView.__refreshHidden = false;
              if (scrollView.__refreshShow) {
                  scrollView.__refreshShow();
              }
              if (scrollView.__refreshActivate) {
                  scrollView.__refreshActivate();
              }
              if (scrollView.__refreshStart) {
                  scrollView.__refreshStart();
              }

          });

      }
  }])
  //格式化时间
  .factory('$learunFormatDate', function () {
      return formatDate = function (v, format) {
          if (!v) return "";
          var d = v;
          if (typeof v === 'string') {
              if (v.indexOf("/Date(") > -1)
                  d = new Date(parseInt(v.replace("/Date(", "").replace(")/", ""), 10));
              else
                  d = new Date(Date.parse(v.replace(/-/g, "/").replace("T", " ").split(".")[0]));//.split(".")[0] 用来处理出现毫秒的情况，截取掉.xxx，否则会出错
          }
          var o = {
              "M+": d.getMonth() + 1,  //month
              "d+": d.getDate(),       //day
              "h+": d.getHours(),      //hour
              "m+": d.getMinutes(),    //minute
              "s+": d.getSeconds(),    //second
              "q+": Math.floor((d.getMonth() + 3) / 3),  //quarter
              "S": d.getMilliseconds() //millisecond
          };
          if (/(y+)/.test(format)) {
              format = format.replace(RegExp.$1, (d.getFullYear() + "").substr(4 - RegExp.$1.length));
          }
          for (var k in o) {
              if (new RegExp("(" + k + ")").test(format)) {
                  format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
              }
          }
          return format;

      }
  })
  //页面模型
  .factory('$learunPageModal',['$ionicModal',function($ionicModal){

    return function(scope,templateUrl,fnMethod,callbackRemmove){
      var pageModel;
      scope.openPageModel= function(pageId,id){
        var _pageId = templateUrl+pageId;
        if(pageModel != null)
        {
          pageModel.remove();
        }
        if(fnMethod != undefined)
        {
          fnMethod(id);
        }
        $ionicModal.fromTemplateUrl(templateUrl+pageId, {
          scope: scope,
          animation: 'lr-slide-in-right'
        }).then(function (modal) {
          pageModel = modal;
          pageModel.show();
        });
      };
      return function () {
          if (callbackRemmove != undefined) {
              callbackRemmove();
        }
        pageModel.remove();
      };
    };
  }])
  //选择页面模型
  .factory('$learunSelectModal',['$rootScope','$ionicModal', function ($rootScope,$ionicModal) {
    var selcetModal = null;
    var scope = $rootScope.$new(true);
    var onChange = null;
    scope.selectValue = "";
    scope.closeSelectModal = function(){
      selcetModal.remove();
    };
    scope.changeSelectModal = function(item){
      if(onChange != null)
      {
        onChange(item);
      }
      selcetModal.remove();
    };
    return function(ops){
      if (selcetModal != null) {
        selcetModal.remove();
      };
      scope.selectValue = ops.selectValue;
      scope.selectData = [];
      for(var i in ops.data) {
        scope.selectData.push({text:ops.data[i][ops.text],value:ops.data[i][ops.value],bgColor:ops.data[i][ops.bgColor]});
      }
      scope.title = ops.title;
      scope.bgAllColor=ops.bgAllColor;
      onChange = ops.onChange;
      $ionicModal.fromTemplateUrl('templates/util/util-selectModal.html', {
        scope: scope,
        animation: 'slide-in-up'
      }).then(function (modal) {
        selcetModal = modal;
        selcetModal.show();
      });
    };

  }])
  //判断数据是否完整
  .factory('$learunDataIsAll',function () {
    return {
      isAll:function (data,fileds) {
        for(var i in fileds) {
          var item  = fileds[i];
          if(item.isRequire) {
            if(data[item.id] == null || data[item.id] == "" || data[item.id] == undefined) {
              return item;
            }
          }
        }
        return null;
      },
      isHave: function (data) {
        for(var i in data) {
          var item = data[i];
          if(item != null && item != undefined && item != "" ) {
            return true;
          }
        }
        return false;
      }
    }
  })
  //获取数据字典的数据
  .factory('$learunGetDataItem',['$learunHttp','ApiUrl',function($learunHttp,ApiUrl){
    return function (ops) {
      $learunHttp.post({
        "url": ApiUrl.dataItemListApi,
        "data": { "enCode": ops.itemName},
        "isverify": true,
        "success": function (data) {
          var lsdata = {};
          var bgColorList = ["positive-bg","royal-bg","balanced-bg","energized-bg","assertive-bg","calm-bg","dark-bg"];
          for(var i in data.result) {
            var item = data.result[i];
            lsdata[item.ItemValue] = item;
            lsdata[item.ItemValue].bgColor = bgColorList[i];
          }
          ops.callback(lsdata);
        }
      });
    }
  }])
  //工作流表单转换
  .factory('$learunFormDesign', function () {
        return {
            "Preview":function(data){

            }
        };
  })
//动态加载js
.factory('$learunLoadJs', ['$http', '$q', function ($http, $q) {
    return function (url) {
        var deferred = $q.defer();
        $http.get(url)
            .success(function (data) {
                eval(data);
                deferred.resolve("success");
            })
            .error(function () {
                deferred.resolve("error");
            });
        return deferred.promise;
    }
}])
;
