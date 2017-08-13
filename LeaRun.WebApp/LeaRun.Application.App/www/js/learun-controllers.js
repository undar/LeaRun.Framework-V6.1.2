angular.module('starter.controllers', [])
.controller('lrTabsCtrl', [
  '$scope', 'lrmBaseInfo', 'AreaInfo', 'lrmBusinesss', 'lrmCustomers',
  function ($scope, lrmBaseInfo, AreaInfo, lrmBusinesss, lrmCustomers) {
      AreaInfo.init();//初始化区域列表
      lrmBaseInfo.init();//初始化基础信息
      lrmBusinesss.init();//初始化商机基础信息
      lrmCustomers.baseInit();//初始化客户基础信息
  }])
//登录
.controller('LoginCtrl', ['$scope', '$state', '$timeout', 'md5', '$learunTopAlert', '$learunHttp', 'ApiUrl', 'UserInfo', 'lrmIM',
    function ($scope, $state, $timeout, md5, $learunTopAlert, $learunHttp, ApiUrl, UserInfo, lrmIM) {
        $scope.data = {};
        $scope.logining = false;
        $scope.doLogin = function () {
            if ($scope.data.username == undefined || $scope.data.username == "") {
                $learunTopAlert.show({ text: "请输入账号" });
                return false;
            }
            if ($scope.data.password == undefined || $scope.data.password == "") {
                $learunTopAlert.show({ text: "请输入密码" });
                return false;
            }
            $scope.logining = true;
            $learunHttp.post({
                "url": ApiUrl.loginApi,
                "data": { "password": md5.createHash($scope.data.password), "username": $scope.data.username },
                "success": function (data) {
                    $scope.logining = false;
                    if (data.status.code != 0) {
                        $learunTopAlert.show({ text: data.status.desc });
                    }
                    else {
                        UserInfo.set(data.result, data.token, true);
                        $scope.data.password = "";
                        //登录成功连接即时通讯
                        lrmIM.init().then(function (data) { console.log(data) });
                        $state.go("tab.home");
                    }
                },
                "error": function () {
                    $scope.logining = false;
                    $learunTopAlert.show({ text: "通信失败" });
                }
            });
        };
    }])
//主页
.controller('HomeCtrl', [
    '$scope', '$learunPageModal', 'HomeApps', 'Announces',
  function ($scope, $learunPageModal, HomeApps, Announces) {
      $scope.homeApps = HomeApps.all();
      $scope.announces = Announces.all();
      //打开应用列表
      $scope.closePageModel = $learunPageModal($scope, 'templates/homeApps/');
  }])
//商机管理
.controller('BusinesssCtrl', [
    '$scope', '$timeout', '$ionicModal', '$learunPopup', '$ionicLoading', '$learunTopAlert', '$learunTriggerRefresh', '$learunSelectModal', '$learunDataIsAll', 'lrmBusinesss', 'lrmBaseInfo',
  function ($scope, $timeout, $ionicModal, $learunPopup, $ionicLoading, $learunTopAlert, $learunTriggerRefresh, $learunSelectModal, $learunDataIsAll, lrmBusinesss, lrmBaseInfo) {
      $scope.data = lrmBusinesss.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('businesslist-content');
      }, 450);
      $scope.doRefresh = function () {
          lrmBusinesss.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrmBusinesss.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //搜索页
      $scope.closeSearchModal = function () {
          $scope.searchModal.remove();
      };
      $scope.openSearch = function () {
          $scope.searchData = lrmBusinesss.getSearchList();
          console.log($scope.searchData);
          if ($scope.searchModal != null) {
              $scope.searchModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/business/businessSearch.html', {
              scope: $scope,
              animation: 'lr-slide-in-right',
              focusFirstInput: true
          }).then(function (modal) {
              $scope.searchModal = modal;
              $scope.searchModal.show();
          });

      };
      $scope.doSearch = function () {
          lrmBusinesss.searchData();
      };
      $scope.loadMoreSearch = function () {
          lrmBusinesss.searchDataAdd(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //详情页
      $scope.openDetailsModal = function (chanceId) {
          $scope.detailsData = lrmBusinesss.get(chanceId);
          if ($scope.detailsModal != null) {
              $scope.detailsModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/business/businessDetails.html', {
              scope: $scope,
              animation: 'lr-slide-in-right'
          }).then(function (modal) {
              $scope.detailsModal = modal;
              $scope.detailsModal.show();
          });
      };
      $scope.closeDetailsModal = function () {
          $scope.detailsModal.remove();
      };
      //编辑页
      $scope.openEditModal = function (item) {
          $scope.editData = {};
          $scope.editTitle = "新建";
          if (item != undefined) {
              $scope.editData = item;
              $scope.editTitle = "编辑";
          }
          if ($scope.editModal != undefined) {
              $scope.editModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/business/businessEdit.html', {
              scope: $scope,
              animation: 'slide-in-up',
          }).then(function (modal) {
              $scope.editModal = modal;
              $scope.editModal.show();
          });
      };
      $scope.closeEditModal = function () {
          if ($learunDataIsAll.isHave($scope.editData)) {
              $learunPopup.confirm({
                  title: '<p> </p>你输入的内容未保存，是否放弃本次编辑?<p> </p>',
                  okText: '放弃',
                  ok: function () {
                      $scope.editModal.remove();
                  }
              });
          } else {
              $scope.editModal.remove();
          }
      };
      $scope.isInputActive = function (name) {
          return ($scope.editData[name] != null && $scope.editData[name] != "" && $scope.editData[name] != undefined);
      };

      $scope.openSourceSelectModal = function () {//打开选择商机来源
          $learunSelectModal({
              title: "商机来源",
              bgAllColor: "royal-bg",
              selectValue: $scope.editData.SourceId,
              text: "ItemName",
              value: "ItemValue",
              data: lrmBusinesss.getChanceSource(),
              onChange: function (item) {
                  $scope.editData.SourceId = item.value;
                  $scope.editData.SourceName = item.text;
              }
          });
      };
      $scope.openStageSelectModal = function () {//打开选择商机阶段
          $learunSelectModal({
              title: "商机阶段",
              selectValue: $scope.editData.StageId,
              text: "ItemName",
              value: "ItemValue",
              bgColor: "bgColor",
              data: lrmBusinesss.getChancePhases(),
              onChange: function (item) {
                  $scope.editData.StageId = item.value;
                  $scope.editData.StageName = item.text;
                  $scope.editData.BgStageColor = item.bgColor;
              }
          });
      };
      $scope.openTraceUserSelectModal = function () {
          $learunSelectModal({
              title: "跟进人员",
              bgAllColor: "calm-bg",
              selectValue: $scope.editData.TraceUserId,
              text: "RealName",
              value: "UserId",
              data: lrmBaseInfo.getUserInfoList("2f077ff9-5a6b-46b3-ae60-c5acdc9a48f1"),
              onChange: function (item) {
                  $scope.editData.TraceUserId = item.value;
                  $scope.editData.TraceUserName = item.text;
              }
          });
      };

      //删除
      $scope.doDelete = function (business) {
          $learunPopup.confirm({
              title: '<p> </p>确定要删除?<p> </p>',
              okText: '删除',
              ok: function () {
                  lrmBusinesss.remove(business);
                  if ($scope.detailsModal != null) {
                      $scope.detailsModal.remove();
                  }
              }
          });
      };
      //添加保存
      $scope.doSubmit = function () {
          var res = $learunDataIsAll.isAll($scope.editData, lrmBusinesss.getEditDataEx());
          if (res != null) {
              $learunTopAlert.show({ text: res.name + " 不能为空" });
          } else {
              $ionicLoading.show();
              lrmBusinesss.editSubmit($scope.editData, function () {
                  $ionicLoading.hide();
                  $scope.editModal.remove();
                  $learunTriggerRefresh.triggerRefresh('businesslist-content');
              });
          }
      }
  }])
//客户管理
.controller('CustomersCtrl', [
  '$scope', '$timeout', '$ionicModal', '$ionicLoading', '$learunTopAlert', '$learunTriggerRefresh', '$learunPopup', '$learunSelectModal', '$learunDataIsAll', 'lrmBaseInfo', 'lrmCustomers'
  , function ($scope, $timeout, $ionicModal, $ionicLoading, $learunTopAlert, $learunTriggerRefresh, $learunPopup, $learunSelectModal, $learunDataIsAll, lrmBaseInfo, lrmCustomers) {
      $scope.data = lrmCustomers.getList();
      //console.log(JSON.stringify(lrmCustomers.getList()));
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('customerlist-content');
      }, 450);
      $scope.doRefresh = function () {
          lrmCustomers.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrmCustomers.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //详情页
      $scope.openDetailsModal = function (customerId) {
          $scope.detailsData = lrmCustomers.get(customerId);
          if ($scope.detailsModal != null) {
              $scope.detailsModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/customers/customersDetails.html', {
              scope: $scope,
              animation: 'lr-slide-in-right',
          }).then(function (modal) {
              $scope.detailsModal = modal;
              $scope.detailsModal.show();
          });
      };
      $scope.closeDetailsModal = function () {
          $scope.detailsModal.remove();
      };
      //搜索页
      $scope.closeSearchModal = function () {
          $scope.searchModal.remove();
      };
      $scope.openSearch = function () {
          $scope.searchData = lrmCustomers.getSearchList();
          if ($scope.searchModal != null) {
              $scope.searchModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/customers/customersSearch.html', {
              scope: $scope,
              animation: 'lr-slide-in-right',
              focusFirstInput: true
          }).then(function (modal) {
              $scope.searchModal = modal;
              $scope.searchModal.show();
          });

      };
      $scope.doSearch = function () {
          lrmCustomers.searchData();
      };
      $scope.loadMoreSearch = function () {
          lrmCustomers.searchDataAdd(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //编辑页
      $scope.openEditModal = function (item) {
          $scope.editData = {
          };
          $scope.editTitle = "新建";
          if (item != undefined) {
              $scope.editData = item;
              $scope.editTitle = "编辑";
          }

          if ($scope.editModal != undefined) {
              $scope.editModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/customers/customersEdit.html', {
              scope: $scope,
              animation: 'slide-in-up',
          }).then(function (modal) {
              $scope.editModal = modal;
              $scope.editModal.show();
          });
      };
      $scope.closeEditModal = function () {
          if ($learunDataIsAll.isHave($scope.editData)) {
              $learunPopup.confirm({
                  title: '<p> </p>你输入的内容未保存，是否放弃本次编辑?<p> </p>',
                  okText: '放弃',
                  ok: function () {
                      $scope.editModal.remove();
                  }
              });
          } else {
              $scope.editModal.remove();
          }
      };
      $scope.isInputActive = function (name) {
          return ($scope.editData[name] != null && $scope.editData[name] != "" && $scope.editData[name] != undefined);
      };

      $scope.openTypeSelectModal = function () {//客户类别
          $learunSelectModal({
              title: "客户类别",
              bgAllColor: "royal-bg",
              selectValue: $scope.editData.custTypeId,
              text: "itemName",
              value: "itemValue",
              data: lrmCustomers.getCustType(),
              onChange: function (item) {
                  $scope.editData.custTypeId = item.value;
                  $scope.editData.custTypeName = item.text;
              }
          });
      };
      $scope.openLevelSelectModal = function () {//客户级别
          $learunSelectModal({
              title: "客户级别",
              bgAllColor: "positive-bg",
              selectValue: $scope.editData.custLevelId,
              text: "itemName",
              value: "itemValue",
              data: lrmCustomers.getCustLevel(),
              onChange: function (item) {
                  $scope.editData.custLevelId = item.value;
                  $scope.editData.custLevelName = item.text;
              }
          });
      };
      $scope.openDegreeSelectModal = function () {//客户程度
          $learunSelectModal({
              title: "客户程度",
              bgAllColor: "balanced-bg",
              selectValue: $scope.editData.custDegreeId,
              text: "itemName",
              value: "itemValue",
              data: lrmCustomers.getCustDegree(),
              onChange: function (item) {
                  $scope.editData.custDegreeId = item.value;
                  $scope.editData.custDegreeName = item.text;
              }
          });
      };
      $scope.openTraceUserSelectModal = function () {
          $learunSelectModal({
              title: "跟进人员",
              bgAllColor: "calm-bg",
              selectValue: $scope.editData.traceUserId,
              text: "realName",
              value: "userId",
              data: lrmBaseInfo.getUserInfoList("2f077ff9-5a6b-46b3-ae60-c5acdc9a48f1"),
              onChange: function (item) {
                  $scope.editData.traceUserId = item.value;
                  $scope.editData.traceUserName = item.text;
              }
          });
      };
      //删除
      $scope.doDelete = function (customer) {
          $learunPopup.confirm({
              title: '<p> </p>确定要删除?<p> </p>',
              okText: '删除',
              ok: function () {
                  lrmCustomers.remove(customer);
                  if ($scope.detailsModal != null) {
                      $scope.detailsModal.remove();
                  }
              }
          });
      };
      //添加保存
      $scope.submitCust = function () {
          var res = $learunDataIsAll.isAll($scope.editData, lrmCustomers.getEditDataEx());
          if (res != null) {
              $learunTopAlert.show({ text: res.name + "不能为空" });
          } else {
              $ionicLoading.show();
              $scope.editData.shortName = $scope.editData.fullName;
              lrmCustomers.editSubmit($scope.editData, function () {
                  $ionicLoading.hide();
                  $scope.editModal.remove();
                  $learunTriggerRefresh.triggerRefresh('customerlist-content');
              });
          }
      }
  }])
//协同办公获取列表
.controller('WorkflowCtrl', [
    '$scope', '$learunPageModal', 'lrmWFlows',
  function ($scope, $learunPageModal, lrmWFlows) {
      $scope.wflows = lrmWFlows.all();
      $scope.closeFlowPageModel = $learunPageModal($scope, 'templates/homeApps/workflow/');
  }])
//发起流程
.controller('flowDesignCtrl', [
    '$scope', '$timeout', '$learunTriggerRefresh', '$ionicModal', '$learunDataIsAll', '$learunTopAlert', '$ionicLoading', 'lrmFlowDesigns',
  function ($scope, $timeout, $learunTriggerRefresh, $ionicModal, $learunDataIsAll, $learunTopAlert, $ionicLoading, lrmFlowDesigns) {
      $scope.data = lrmFlowDesigns.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowDesignlist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrmFlowDesigns.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrmFlowDesigns.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };

      $scope.openEditModal = function (flow) {
          $scope.editData = flow;
          if ($scope.editModal != undefined) {
              $scope.editModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowdesignEdit.html', {
              scope: $scope,
              animation: 'slide-in-up',
          }).then(function (modal) {
              $scope.editModal = modal;
              $scope.editModal.show();
          });
      };
      $scope.closeEditModal = function () {
          $scope.editModal.remove();
      };
      $scope.isInputActive = function (name) {
          return ($scope.editData[name] != null && $scope.editData[name] != "" && $scope.editData[name] != undefined);
      };
      // 等级
      $scope.flowLevelSideList = [
        { text: "重要", value: "3", bgColor: "bgcolor_a" },
        { text: "普通", value: "2", bgColor: "bgcolor_b" },
        { text: "一般", value: "1", bgColor: "bgcolor_c" }
      ];
      $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowLevelSelect.html', {
          scope: $scope,
          animation: 'slide-in-up'
      }).then(function (modal) {
          $scope.flowLevelModal = modal;
      });
      $scope.openFlowLevelModal = function () {
          $scope.flowLevelModal.show();
      }
      $scope.closeFlowLevelModal = function () {
          $scope.flowLevelModal.hide();
          $scope.flowLevelModal.remove();
      }
      $scope.flowLevelChangeServer = function (item) {
          $scope.flowLevelModal.hide();
      }
      $scope.flowReasonSideList = [
        { text: "事假", value: "1", bgColor: "bgcolor_a" },
        { text: "婚假", value: "2", bgColor: "bgcolor_b" },
        { text: "病假", value: "3", bgColor: "bgcolor_c" },
        { text: "产假", value: "4", bgColor: "bgcolor_d" },
        { text: "其它", value: "5", bgColor: "bgcolor_e" }
      ];
      $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowReasonSelect.html', {
          scope: $scope,
          animation: 'slide-in-up'
      }).then(function (modal) {
          $scope.flowReasonModal = modal;
      });
      $scope.openFlowReasonModal = function () {
          $scope.flowReasonModal.show();
      }
      $scope.closeFlowReasonModal = function () {
          $scope.flowReasonModal.hide();
          $scope.flowReasonModal.remove();
      }
      $scope.flowReasonChangeServer = function (item) {
          $scope.flowReasonModal.hide();
      }
      $scope.doSubmit = function () {
          //$ionicLoading.show();
          //JSON.stringify($scope.editData){"keyValue":$scope.editData.f_id},{"data":$scope.editData}
          //console.log($scope.editData.f_id);
          console.log(JSON.stringify($scope.editData));

          var f_wflevel = $scope.editData.f_wflevel;
          if (f_wflevel == "重要") {
              $scope.editData.f_wflevel = 3;
          } else if (f_wflevel == "普通") {
              $scope.editData.f_wflevel = 2;
          } else if (f_wflevel == "一般") {
              $scope.editData.f_wflevel = 1;
          }
          var f_wfReason = $scope.editData.f_wfReason;
          if (f_wfReason == "事假") {
              $scope.editData.f_wfReason = 1;
          } else if (f_wfReason == "婚假") {
              $scope.editData.f_wfReason = 2;
          } else if (f_wfReason == "病假") {
              $scope.editData.f_wfReason = 3;
          } else if (f_wfReason == "产假") {
              $scope.editData.f_wfReason = 4;
          } else if (f_wfReason == "其它") {
              $scope.editData.f_wfReason = 5;
          }
          //console.log(JSON.stringify($scope.editData));
          console.log($scope.editData.f_wflevel);
          console.log($scope.editData.f_wfReason);
          /*console.log($scope.editData.F_ActivityId);
          console.log($scope.editData.F_ActivityName);
          console.log($scope.editData.F_Code);*/

          $scope.editData.f_schemeContent = $scope.editData.f_wflevel + ',' + $scope.editData.f_wfReason + ',' + $scope.editData.begintime + ',' + $scope.editData.endtime + ',' + $scope.editData.whyflow + ',' + $scope.editData.qnum;
          console.log($scope.editData.f_schemeContent);
          lrmFlowDesigns.editSubmit($scope.editData.f_id, $scope.editData, function () {
              //console.log($scope.editData);
              //$ionicLoading.hide();
              //$scope.editModal.remove();
              //$learunTriggerRefresh.triggerRefresh('businesslist-content');
          });
      }
  }])
//草稿流程
.controller('flowRoughdraftCtrl', [
    '$scope', '$timeout', '$learunHttp', '$learunTopAlert', '$learunTriggerRefresh', '$learunPopup', 'ApiUrl', 'lrFlowRoughdrafts',
  function ($scope, $timeout, $learunHttp, $learunTopAlert, $learunTriggerRefresh, $learunPopup, ApiUrl, lrFlowRoughdrafts) {
      $scope.dat = lrFlowRoughdrafts.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowRoughdraftlist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrFlowRoughdrafts.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrFlowRoughdrafts.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //删除
      $scope.doDelete = function (flow) {
          $learunPopup.confirm({
              title: '<p> </p>确定要删除?<p> </p>',
              okText: '删除',
              ok: function () {
                  lrFlowRoughdrafts.remove(flow);
                  if ($scope.detailsModal != null) {
                      $scope.detailsModal.remove();
                  }
              }
          });
      };
  }])
//我的流程
.controller('flowProcessCtrl', [
    '$scope', '$timeout', '$learunHttp', '$learunTopAlert', '$learunTriggerRefresh', '$ionicModal', 'ApiUrl', 'lrWorkflows',
  function ($scope, $timeout, $learunHttp, $learunTopAlert, $learunTriggerRefresh, $ionicModal, ApiUrl, lrWorkflows) {
      $scope.dat = lrWorkflows.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowProcesslist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrWorkflows.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrWorkflows.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //详情页
      $scope.openDetailsModal = function (flowId) {
          //console.log(flowId);
          $scope.detailsData = lrWorkflows.get(flowId);
          console.log($scope.detailsData);
          if ($scope.detailsModal != null) {
              $scope.detailsModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowProcessDetails.html', {
              scope: $scope,
              animation: 'lr-slide-in-right',
          }).then(function (modal) {
              $scope.detailsModal = modal;
              $scope.detailsModal.show();
          });
      };
      $scope.closeDetailsModal = function () {
          $scope.detailsModal.remove();
      };
  }])
//待办流程
.controller('flowBefProcessCtrl', [
    '$scope', '$timeout', '$learunHttp', '$learunTopAlert', '$learunTriggerRefresh', '$ionicModal', 'UserInfo', 'ApiUrl', 'lrFlowBefProcesss',
  function ($scope, $timeout, $learunHttp, $learunTopAlert, $learunTriggerRefresh, $ionicModal, UserInfo, ApiUrl, lrFlowBefProcesss) {
      $scope.dat = lrFlowBefProcesss.getList();
      $scope.userinfo = UserInfo.get();
      /*console.log(UserInfo.get());
      console.log(lrFlowBefProcesss.getList());*/
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowBefProcesslist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrFlowBefProcesss.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrFlowBefProcesss.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      $scope.openEditModal = function (flow) {
          //$scope.editData = {};
          $scope.editData = flow;
          console.log($scope.editData);
          $scope.editData.realname = $scope.userinfo.realname;
          /*console.log($scope.editData.realname);*/
          $scope.editTitle = "新建";
          if ($scope.editModal != undefined) {
              $scope.editModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowBefProcessEdit.html', {
              scope: $scope,
              animation: 'slide-in-up',
          }).then(function (modal) {
              $scope.editModal = modal;
              $scope.editModal.show();
              //alert(JSON.stringify($scope.editData.schemename));
          });
      };
      $scope.closeEditModal = function () {
          $scope.editModal.remove();
      };
      $scope.isInputActive = function (name) {
          return ($scope.editData[name] != null && $scope.editData[name] != "" && $scope.editData[name] != undefined);
      };
      // 审核
      $scope.flowVerificationSideList = [
          { text: "同意", value: "同意", bgColor: "bgcolor_a" },
          { text: "不同意", value: "不同意", bgColor: "bgcolor_b" },
          { text: "驳回", value: "驳回", bgColor: "bgcolor_c" }
      ];
      $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowVerificationSelect.html', {
          scope: $scope,
          animation: 'slide-in-up'
      }).then(function (modal) {
          $scope.flowVerificationModal = modal;
      });
      $scope.openFlowVerificationModal = function () {
          $scope.flowVerificationModal.show();
      }
      $scope.closeFlowVerificationModal = function () {
          $scope.flowVerificationModal.hide();
          $scope.flowVerificationModal.remove();
      }
      $scope.flowVerificationChangeServer = function (item) {
          $scope.flowVerificationModal.hide();
      }
      // 事由
      $scope.flowReasonSideList = [
          { text: "事假", value: "事假", bgColor: "bgcolor_a" },
          { text: "婚假", value: "婚假", bgColor: "bgcolor_b" },
          { text: "病假", value: "病假", bgColor: "bgcolor_c" },
          { text: "产假", value: "产假", bgColor: "bgcolor_d" },
          { text: "其它", value: "其它", bgColor: "bgcolor_e" }
      ];
      $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowReasonSelect.html', {
          scope: $scope,
          animation: 'slide-in-up'
      }).then(function (modal) {
          $scope.flowReasonModal = modal;
      });
      $scope.openFlowReasonModal = function () {
          $scope.flowReasonModal.show();
      }
      $scope.closeFlowReasonModal = function () {
          $scope.flowReasonModal.hide();
          $scope.flowReasonModal.remove();
      }
      $scope.flowReasonChangeServer = function (item) {
          $scope.flowReasonModal.hide();
      }
      $scope.doSubmit = function () {
          //$ionicLoading.show();
          lrFlowBefProcesss.editSubmit($scope.editData, function () {
              console.log($scope.editData);
              //$ionicLoading.hide();
              //$scope.editModal.remove();
              //$learunTriggerRefresh.triggerRefresh('businesslist-content');
          });
      }
  }])
//已办流程
.controller('flowAftProcessCtrl', [
    '$scope', '$timeout', '$learunHttp', '$learunTopAlert', '$learunTriggerRefresh', '$ionicModal', 'ApiUrl', 'lrFlowAftProcesss',
  function ($scope, $timeout, $learunHttp, $learunTopAlert, $learunTriggerRefresh, $ionicModal, ApiUrl, lrFlowAftProcesss) {
      $scope.dat = lrFlowAftProcesss.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowAftProcesslist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrFlowAftProcesss.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrFlowAftProcesss.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };
      //详情页
      $scope.openDetailsModal = function (flowId) {
          console.log(flowId);
          $scope.detailsData = lrFlowAftProcesss.get(flowId);
          console.log($scope.detailsData);
          if ($scope.detailsModal != null) {
              $scope.detailsModal.remove();
          }
          $ionicModal.fromTemplateUrl('templates/homeApps/workflow/flowAftProcessDetails.html', {
              scope: $scope,
              animation: 'lr-slide-in-right',
          }).then(function (modal) {
              $scope.detailsModal = modal;
              $scope.detailsModal.show();
          });
      };
      $scope.closeDetailsModal = function () {
          $scope.detailsModal.remove();
      };
  }])
//工作委托
.controller('flowDelegateCtrl', [
  '$scope', '$timeout', '$learunTriggerRefresh', 'ApiUrl', 'lrFlowDelegates',
  function ($scope, $timeout, $learunTriggerRefresh, ApiUrl, lrFlowDelegates) {
      $scope.dat = lrFlowDelegates.getList();
      //刷新数据
      $timeout(function () {
          $learunTriggerRefresh.triggerRefresh('flowDelegatelist-content');
      }, 200);
      $scope.doRefresh = function () {
          lrFlowDelegates.update(function () {
              $scope.$broadcast('scroll.refreshComplete');
          });
      };
      $scope.doLoadMore = function () {
          lrFlowDelegates.add(function () {
              $scope.$broadcast('scroll.infiniteScrollComplete');
          });
      };

  }])
//图表
.controller('lrChartsCtrl', function ($scope) {
    $scope.chartPie = {
        options: {
            chart: {
                type: 'pie',
                // marginTop: '10px'
            },
            colors: ['#058dc7', '#50b432'],
        },
        series: [{
            data: [
              ['Download', 100],
              ['Upload', 500]
            ],
            name: 'InternetUsage MB',
            //data:[50,40],
            dataLabels: {
                rotation: 270,
                enabled: false,
                format: '<b>{point.name}</b>: {point.percentage:.1f} MB'
            }
        }],
        title: {
            text: 'Pie Chart'
        },
        tooltip: {
            valueDecimals: 2,
            valueSuffix: ' USD'
        },
        credits: {
            enabled: false
        },
        loading: false
    }
    $scope.chartarea = {
        options: {
            chart: {
                type: 'area',
                inverted: false,
                zoomType: 'xy',
                height: 250,
            },
            plotOptions: {
                series: {
                    cursor: 'pointer',
                    column: {
                        size: '30%',
                    },
                }
            },
            colors: ['#058dc7', '#50b432']
        },
        xAxis: {
            categories: ['10 jan', '11 jan', '12 jan', '13 jan', '14 jan', '15 jan'],
            title: {
                text: ''
            },
            labels: {
                rotation: -90,
                style: {
                    fontSize: '12px',
                }
            }
        },
        yAxis: {
            min: 0,
            title: {
                text: 'Bandthwidth (MB)',
                align: 'high'
            },
            labels: {
                overflow: 'justify'
            }
        },
        tooltip: {
            valueSuffix: ' '
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'top',

            floating: false,
            borderWidth: 1,
            backgroundColor: ((Highcharts.theme && Highcharts.theme.legendBackgroundColor) || '#FFFFFF'),
            shadow: true
        },
        credits: {
            enabled: false
        },
        title: {
            text: 'Internet Usage',
            style: {
                //color: '#FF00FF',
                fontSize: '12px'
            },
        },
        series: [{
            name: 'Download',
            data: [50, 60, 70, 50, 60, 70],
        }, {
            name: 'Upload',
            data: [40, 30, 60, 50, 60, 70],
        }, ],
        loading: false
    }
    $scope.chartDonut = {
        options: {
            plotOptions: {
                pie: {
                    dataLabels: {
                        enabled: false,

                        style: {
                            fontWeight: 'bold',
                            color: 'white',
                            textShadow: '0px 1px 2px black',

                        }
                    },
                    startAngle: -90,
                    endAngle: 90,
                    center: ['50%', '75%']
                }
            },
            colors: ['#058dc7', '#50b432'],
        },
        series: [{
            type: 'pie',
            innerSize: '50%',
            data: [
              ['Download', 100],
              ['Upload', 500]
            ],
            name: 'InternetUsage MB',
            //data:[50,40],
            dataLabels: {
                rotation: 270,
                enabled: false,
                format: '<b>{point.name}</b>: {point.percentage:.1f} MB'

            }
        }],
        title: {
            text: '',
            align: 'center',
            verticalAlign: 'middle',
            y: -60
        },
        credits: {
            enabled: false
        },
        loading: false
    }
})
//即时通讯
  .controller('IMCtrl', ['$scope', '$ionicScrollDelegate', '$learunPageModal', 'lrmIM', '$learunFormatDate', function ($scope, $ionicScrollDelegate, $learunPageModal, lrmIM, $learunFormatDate) {
      var scrollBar = $ionicScrollDelegate.$getByHandle('messageDetailsScroll');
      $scope.vm = {
          "isConnected": false,
          "connectText": "拼命连接服务中...",
          "getHeadImg": function (isOnLine) {
              if (isOnLine == 1) {
                  return "img/on-line.png";
              }
              else {
                  return "img/off-line.png";
              }
          },
          "send_content": ""
      }

      lrmIM.init(function () {//服务断开后处理函数
          $scope.vm.isConnected = false;
          $scope.vm.connectText = "服务器断开";
          $scope.$apply();
      }, $scope).then(function (data) {
          console.log(data, "ctrlim");
          if (data.code == "success") {//连接成功
              $scope.vm.isConnected = true;
          }
          else {
              $scope.vm.isConnected = false;
              $scope.vm.connectText = "连接失败";
          }
      });
      //连接服务成功后执行
      //指定联系人列表
      $scope.imServer = lrmIM.getServer();
      //格式化时间
      $scope.formatDate = function (date) {
          return $learunFormatDate(date, "MM-dd hh:mm");
      };
      $scope.closeMessageDetailModel = $learunPageModal($scope, 'templates/homeApps/wechat/wechatDetails.html', function (otherUserId) {//打开聊天窗口
          $scope.imServer.currentUserId = otherUserId;
          $scope.imServer.IMGetMsgList(1, 1000, otherUserId).then(function (msglists) {
              //$scope.messages = msglists;
              scrollBar.scrollBottom();
          });
          $scope.imServer.IMUpdateMessageStatus(otherUserId);
          if ($scope.imServer.lastUserList[otherUserId] != undefined) {
              $scope.imServer.lastUserList[otherUserId].UnReadNum = 0;
          }

      }, function () {
          $scope.imServer.currentUserId = "";
      });


      $scope.sendMsg = function () {
          if ($scope.vm.send_content.length > 0) {
              $scope.imServer.IMSendToOne($scope.imServer.currentUserId, $scope.vm.send_content);
              $scope.vm.send_content = $scope.vm.send_content;
              $scope.vm.send_content = '';
          }
      }

      window.addEventListener("native.keyboardshow", function (e) {
          scrollBar.scrollBottom();
      });

      $scope.scrollBottom = function () {
          scrollBar.scrollBottom();
      };
  }])
//实例
.controller('CasesCtrl', [
    '$scope', '$learunPageModal', 'Cases', function ($scope, $learunPageModal, Cases) {
        $scope.cases = Cases.all();
        //打开应用列表
        $scope.closePageModel = $learunPageModal($scope, 'templates/cases/', function (id) { $scope.caseItem = Cases.get(id); });
    }])
.controller('CaseListCtrl', [
    '$scope', '$stateParams', '$ionicModal', function ($scope, $stateParams, $ionicModal) {
        $scope.showList = function (type) {
            $ionicModal.fromTemplateUrl('templates/cases/list/' + type + '.html', {
                scope: $scope
            }).then(function (modal) {
                $scope.CaseListModal = modal;
                $scope.CaseListModal.show();
            });
            $scope.closeModal = function () {
                $scope.CaseListModal.remove();
            };
        };
    }])
.controller('CaseFormCtrl', ['$scope', function ($scope) {
    $scope.checkbox = true;
    $scope.choice = 'A';
}])
//--选择相册
.controller('CasesPictureCtrl', [
    '$scope', '$cordovaImagePicker', '$cordovaFileTransfer', function ($scope, $cordovaImagePicker, $cordovaFileTransfer) {
        $scope.imgSrc = "";
        $scope.pickImage = function () {
            var options = {
                maximumImagesCount: 1,
                width: 355,
                height: 300,
                quality: 80
            };
            $cordovaImagePicker.getPictures(options).then(function (results) {

                $scope.imgSrc = results[0];
                alert("上传成功!" + results[0]);
            }, function (error) {
                // error getting photos
            });
        }
    }])
//拍照
.controller('CasesCameraCtrl', [
    '$scope', '$cordovaCamera', function ($scope, $cordovaCamera) {
        $scope.imgSrc = "";
        $scope.doCemera = function () {
            var options = {
                targetWidth: 500,
                targetHeight: 500,
                destinationType: Camera.DestinationType.FILE_URI,
                sourceType: Camera.PictureSourceType.CAMERA,
            };
            $cordovaCamera.getPicture(options).then(function (imageURI) {
                alert(imageURI);
                $scope.imgSrc = imageURI;
            }, function (err) {
                // error
            });
        };
    }])
//扫描条码
.controller('CasesBarcodeCtrl', [
    '$scope', '$cordovaBarcodeScanner', function ($scope, $cordovaBarcodeScanner) {
        $scope.barcode = "";
        $scope.doBarcode = function () {
            $cordovaBarcodeScanner.scan().then(function (barcodeData) {
                // Success! Barcode data is here
                $scope.barcode = barcodeData;

                alert(barcodeData.text);
            }, function (error) {
                // An error occurred
            });
            // NOTE: encoding not functioning yet
            $cordovaBarcodeScanner.encode(BarcodeScanner.Encode.TEXT_TYPE, " ").then(function (success) {
                // Success!
                alert(success);
            }, function (error) {
                // An error occurred
            });
        };
    }])
//通讯录
.controller('CasesContactCtrl', [
    '$scope', '$cordovaContacts', '$ionicPlatform', function ($scope, $cordovaContacts, $ionicPlatform) {
        $scope.addContact = function () {
            $cordovaContacts.save($scope.contactForm).then(function (result) {
                // Contact saved

            }, function (err) {
                // Contact error
            });
        };
        $scope.getAllContacts = function () {
            var options = {};
            options.filter = '';
            options.multiple = true;
            $cordovaContacts.find(options).then(function (allContacts) { //省略参数，获取所有的信息
                alert(JSON.stringify(allContacts));
                $scope.contacts = allContacts;
            });
        };
        $scope.findContactsBySearchTerm = function (searchTerm) {
            var opts = {
                filter: searchTerm,
                multiple: true,// 返回匹配条件的任何信息
                fields: ['displayName', 'name'],
                desiredFields: [id]
            };
            if ($ionicPlatform.isAndroid()) {
                opts.hasPhoneNumber = true;//hasPhoneNumber 只适用于android
            };

            $cordovaContacts.find(opts).then(function (contactsFound) {
                $scope.contacts = contactsFound;
            });
            $scope.pickContactUsingNativeUI = function () {
                $cordovaContacts.pickContact().then(function (contactPicked) {
                    $scope.contact = contactPicked;
                })
            }
        };
    }])
//打电话
.controller('CasesTelCtrl', ['$scope', function ($scope) {
    $scope.doCallPhone10010 = function () {
        document.location.href = "tel:10010";
    };
    $scope.doCallPhone10086 = function () {
        document.location.href = "tel:10086";
    };
}])
//地理位置
.controller('CasesGeolocationCtrl', [
    '$scope', '$cordovaGeolocation', '$ionicLoading', function ($scode, $cordovaGeolocation, $ionicLoading) {
        $scode.geoFindMe = function () {
            //output.innerHTML = " ";
            $ionicLoading.show();
            var output = document.getElementById("out");
            var posOptions = { timeout: 10000, enableHighAccuracy: true };
            $cordovaGeolocation.getCurrentPosition(posOptions).then(function (position) {
                //$rootScope.$broadcast('selfLocation:update', position);
                lat = position.coords.latitude
                long = position.coords.longitude
                alert([lat, long]);
                output.innerHTML = '<p>Latitude is ' + lat + '° <br>Longitude is ' + long + '°</p>';
                $ionicLoading.hide();
            }, function (err) {
                console.log("无法获取地理位置!");
                $ionicLoading.hide();
                return;
            });
            var watchOptions = {
                timeout: 3000,
                enableHighAccuracy: false // may cause errors if true
            };
            var watch = $cordovaGeolocation.watchPosition(watchOptions);
            watch.then(null, function (err) {
                // error
            }, function (position) {
                var lat = position.coords.latitude
                var long = position.coords.longitude
            });
            watch.clearWatch();
            // OR
            $cordovaGeolocation.clearWatch(watch).then(function (result) {
                // success
            }, function (error) {
                // error
            });
        }
    }])
//通知
.controller('NoticeCtrl', ['$scope', '$ionicModal', function ($scope, $ionicModal) {
    $scope.allNotices = {
        "btnNoRead": [
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月26日', id: '1', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月18日', id: '2', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月15日', id: '3', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月11日', id: '4', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月06日', id: '5', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '04月03日', id: '6', type: '1' },
          { title: '【信息】框架:.net 框架', content: '框架:.net 框架', datatime: '03月12日', id: '13', type: '1' }],
        "btnRead": [
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '05月05日', id: '7', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '04月29日', id: '8', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '04月26日', id: '9', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '04月23日', id: '10', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '03月21日', id: '11', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '03月18日', id: '12', type: '2' },
          { title: '【信息】APP进度', content: 'APP框架即将面世:实现部分功能', datatime: '03月27日', id: '14', type: '2' }]
    };
    $scope.active = 'btnNoRead';
    $scope.notices = $scope.allNotices[$scope.active];
    $scope.setActive = function (type) {
        $scope.active = type;
        $scope.notices = $scope.allNotices[type];
    };
    $scope.isActive = function (type) {
        return type === $scope.active;
    };

    //打开详情页
    $scope.openNoticesModal = function (noticeId) {
        $ionicModal.fromTemplateUrl(('templates/notices/noticeDetails.html'), {
            scope: $scope,
            animation: 'lr-slide-in-right'
        }).then(function (modal) {
            $scope.noticesModal = modal;
            $scope.noticesModal.show();
        });
    };
    $scope.closeNoticesModal = function () {
        $scope.noticesModal.remove();
    };
}])
.controller('NoticeListCtrl', function ($scope) {
})
  //订单
  .controller('lrOrderCtrl', ['$scope', '$timeout', '$learunTriggerRefresh', '$ionicModal', 'lrmOrderInfos',
    function ($scope, $timeout, $learunTriggerRefresh, $ionicModal, lrmOrderInfos) {
        //console.log(JSON.stringify(lrmOrderInfos.getList()));
        $scope.data = lrmOrderInfos.getList();
        //刷新数据
        $timeout(function () {
            $learunTriggerRefresh.triggerRefresh('orderlist-content');
        }, 450);
        $scope.doRefresh = function () {
            lrmOrderInfos.update(function () {
                $scope.$broadcast('scroll.refreshComplete');
            });
        };
        $scope.doLoadMore = function () {
            lrmOrderInfos.add(function () {
                $scope.$broadcast('scroll.infiniteScrollComplete');
            });
        };
        //详情页
        $scope.openDetailsModal = function (orderId) {
            console.log(orderId);
            $scope.detailsData = lrmOrderInfos.get(orderId);
            //console.log(JSON.stringify($scope.detailsData));
            if ($scope.detailsModal != null) {
                $scope.detailsModal.remove();
            }
            $ionicModal.fromTemplateUrl('templates/homeApps/orders/orderDetails.html', {
                scope: $scope,
                animation: 'lr-slide-in-right'
            }).then(function (modal) {
                $scope.detailsModal = modal;
                $scope.detailsModal.show();
            });
        };
        $scope.closeDetailsModal = function () {
            $scope.detailsModal.remove();
        };
    }])
  //收款
  .controller('lrReceivableCtrl', ['$scope', '$timeout', '$learunTriggerRefresh', '$ionicModal', 'lrmReceivables',
    function ($scope, $timeout, $learunTriggerRefresh, $ionicModal, lrmReceivables) {
        $scope.data = lrmReceivables.getList();
        //刷新数据
        $timeout(function () {
            $learunTriggerRefresh.triggerRefresh('receivablelist-content');
        }, 450);
        $scope.doRefresh = function () {
            lrmReceivables.update(function () {
                $scope.$broadcast('scroll.refreshComplete');
            });
        };
        $scope.doLoadMore = function () {
            lrmReceivables.add(function () {
                $scope.$broadcast('scroll.infiniteScrollComplete');
            });
        };
        //详情页
        $scope.openDetailsModal = function (receivableId) {
            $scope.detailsData = lrmReceivables.get(receivableId);
            if ($scope.detailsModal != null) {
                $scope.detailsModal.remove();
            }
            $ionicModal.fromTemplateUrl('templates/homeApps/orders/orderDetails.html', {
                scope: $scope,
                animation: 'lr-slide-in-right'
            }).then(function (modal) {
                $scope.detailsModal = modal;
                $scope.detailsModal.show();
            });
        };
        $scope.closeDetailsModal = function () {
            $scope.detailsModal.remove();
        };
    }])
  //收款报表
  .controller('lrReceivableReportCtrl', ['$scope', '$timeout', '$learunTriggerRefresh', 'lrmReceivableReports',
    function ($scope, $timeout, $learunTriggerRefresh, lrmReceivableReports) {
        //console.log(lrmReceivableReports.getList());
        $scope.data = lrmReceivableReports.getList();
        //console.log($scope.data);
        //刷新数据
        $timeout(function () {
            $learunTriggerRefresh.triggerRefresh('receivableReportlist-content');
        }, 450);
        $scope.doRefresh = function () {
            lrmReceivableReports.update(function () {
                $scope.$broadcast('scroll.refreshComplete');
            });
        };
        $scope.doLoadMore = function () {
            lrmReceivableReports.add(function () {
                $scope.$broadcast('scroll.infiniteScrollComplete');
            });
        };
    }])
  //我的
  .controller('PersonCenterCtrl', [
    '$scope', '$state', '$learunPageModal', '$learunPopup', '$learunHttp', 'UserInfo', 'ApiUrl',
    function ($scope, $state, $learunPageModal, $learunPopup, $learunHttp, UserInfo, ApiUrl) {
        $scope.userinfo = UserInfo.get();
        $scope.outLogin = function () {
            $learunPopup.confirm({
                title: '<p> </p>确定要退出登录?<p> </p>',
                okText: '退出',
                ok: function () {
                    $learunHttp.post({
                        "url": ApiUrl.outLoginApi,
                        "success": function (data) {
                            console.log(data);
                            UserInfo.clear();
                            $state.go("login");
                        },
                        "error": function () {
                            console.log("error");
                            UserInfo.clear();
                            $state.go("login");
                        }
                    });
                },
            });
        };
        //打开个人信息列表
        $scope.closePageModel = $learunPageModal($scope, 'templates/personCenter/');
    }]);
