/*
* 创建者：LearunChen
* 时间：  2016-06-08
* 描述：  数据模型存放js，统一处理数据交互和后台
* */
angular.module('starter.modules', [])
//主页应用列表
  .factory('HomeApps', function () {
    var homeApps = [{
        name: "商机管理",
        icon: "ion-ios-paperplane-outline",
        color: "bgcolor_a",
        viewid: 'home-business.html'
    }
    , {
        name: "客户管理",
        icon: "ion-ios-people-outline",
        color: "bgcolor_b",
        viewid: 'home-customers.html'
    }, {
        name: "订单管理",
        icon: "ion-ios-list-outline",
        color: "bgcolor_c",
        viewid: 'home-order.html'
    }, {
        name: "应收账款",
        icon: "ion-ios-paper-outline",
        color: "bgcolor_d",
        viewid: 'list.html'
    }, {
        name: "收款报表",
        icon: "ion-ios-pulse",
        color: "bgcolor_e",
        viewid: 'list.html'
    }, {
        name: "协同办公",
        icon: "ion-ios-browsers-outline",
        color: "bgcolor_f",
        viewid: 'list.html'
    }, {
        name: "即时通信",
        icon: "ion-ios-chatboxes-outline",
        color: "bgcolor_g",
        viewid: 'home-wechat.html'
    }, {
        name: "报表中心",
        icon: "ion-ios-pie-outline",
        color: "bgcolor_h",
        viewid: 'home-charts.html'
    }];

    return {
        all: function () {
            return homeApps;
        },
        remove: function (homeApps) {
            homeApps.splice(homeApps.indexOf(homeApps), 1);
        },
        get: function (homeAppId) {
            for (var i = 0; i < homeApps.length; i++) {
                if (homeApps[i].id === parseInt(homeAppId)) {
                    return homeApps[i];
                }
            }
            return null;
        }
    };
})
//实例列表信息
  .factory('Cases', function () {
    var cases = [{
      id: 0,
      name: '列表',
      icon: 'ion-ios-list-outline',
      bgcolor: 'positive-bg',
      viewid: 'case-list.html'
    }, {
      id: 1,
      name: '表单',
      icon: 'ion-ios-paper-outline',
      bgcolor: 'bgcolor_b',
      viewid: 'case-form.html'
    }, {
      id: 2,
      name: '按钮',
      icon: 'ion-ios-circle-filled',
      bgcolor: 'bgcolor_c',
      viewid: 'case-button.html'
    }, {
      id: 3,
      name: '选择相册图片',
      icon: 'ion-ios-cloud-upload',
      bgcolor: 'bgcolor_d',
      viewid: 'case-picture.html'
    }, {
      id: 4,
      name: '拍照',
      icon: 'ion-ios-camera',
      bgcolor: 'bgcolor_e',
      viewid: 'case-camera.html'
    }, {
      id: 5,
      name: '扫描条码',
      icon: 'ion-ios-barcode-outline',
      bgcolor: 'royal-bg',
      viewid: 'case-barcode.html'
    }, {
      id: 6,
      name: '通讯录',
      icon: 'ion-person-stalker',
      bgcolor: 'calm-bg',
      viewid: 'case-contact.html'
    }, {
      id: 7,
      name: '打电话',
      icon: 'ion-ios-telephone',
      bgcolor: 'assertive-bg',
      viewid: 'case-tel.html'
    }, {
      id: 8,
      name: '地理位置',
      icon: 'ion-ios-location',
      bgcolor: 'dark-bg',
      viewid: 'case-location.html'
    }];

    return {
      all: function () {
        return cases;
      },
      remove: function (cases) {
        cases.splice(cases.indexOf(cases), 1);
      },
      get: function (caseId) {
        for (var i = 0; i < cases.length; i++) {
          if (cases[i].id === parseInt(caseId)) {
            return cases[i];
          }
        }
        return null;
      }
    };
  })
//客户列表信息
  .factory('lrmCustomers',['$learunFormatDate', 'AreaInfo','$learunGetDataItem','$learunHttp','ApiUrl','$learunTopAlert',
    function ($learunFormatDate, AreaInfo,$learunGetDataItem,$learunHttp,ApiUrl,$learunTopAlert) {
    //客户列表
    var custList = {
      records: 0,
      page: 1,
      total: 1,
      moredata: false,
      customers: []
    };
    var custListSearch={};

    var custType = {};  //客户类别
    var custLevel = {}; //客户级别
    var custDegree = {} //客户程度
    var custTrade = {}  //客户行业

    //客户编辑数据字段
    var editDataEx = [
      {
        "name":"客户名称",
        "id":"fullName",
        "isRequire":true
      },
      {
        "name":"客户类别",
        "id":"custTypeId",
        "isRequire":true
      },
      {
        "name":"客户级别",
        "id":"custLevelId",
        "isRequire":true
      },
      {
        "name":"客户程度",
        "id":"custDegreeId",
        "isRequire":true
      },
      {
        "name":"跟进人员",
        "id":"traceUserName",
        "isRequire":true
      },
      {
        "name":"联系人",
        "id":"contact",
        "isRequire":true
      },
      {
        "name":"手机",
        "id":"mobile",
        "isRequire":false
      },
      {
        "name":"QQ",
        "id":"aQ",
        "isRequire":false
      }
    ];
    //方法函数
    function translateData(data, obj) {
      //console.log(data);
        for (var i in data) {
          var item = data[i];

          if(item.custIndustryId == null)
          {
            data[i].custIndustryName = "未知业";
            data[i].custIndustrybgColor = "dark-bg";
          }
          else {
              //data[i].custIndustryName = custTrade[item.custIndustryId].ItemName;
              //data[i].custIndustrybgColor = custTrade[item.custIndustryId].bgColor;
              data[i].custIndustryName = item.custIndustryId;
              data[i].custIndustrybgColor = "assertive-bg";
              //data[i].custIndustrybgColor = custTrade[item.custIndustryId].bgColor;
          }
          //data[i].custLevelName = custLevel[item.custLevelId].ItemName;
          //data[i].custTypeName = custType[item.custTypeId].ItemName;
          //data[i].custDegreeName = custDegree[item.custDegreeId].ItemName;
          data[i].custLevelName = item.custLevelId;
          data[i].custTypeName = item.custTypeId;
          data[i].custDegreeName = item.custDegreeId;

          if (data[i].province != null)
          {
              data[i].provinceName = AreaInfo.getProvinceName(item.province);
              if (data[i].city != null) {
                  data[i].cityName = AreaInfo.getCityName(item.province, item.city);
              }
          }
          data[i].lastDate = item.modifyDate == null ? $learunFormatDate(item.createDate, 'MM-dd') : $learunFormatDate(item.modifyDate, 'MM-dd');
          data[i].createDate = $learunFormatDate(item.createDate, 'yyyy-MM-dd hh:mm');
          data[i].modifyDate = $learunFormatDate(item.modifyDate, 'yyyy-MM-dd hh:mm');
          obj.push(data[i]);
        }
    };
    //获取数据
    function getData(page,queryData, obj,callback) {
      $learunHttp.post({
        "url": ApiUrl.customerListApi,
        "data": { "page": page, "rows": 10, "sidx": "ModifyDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
        "isverify": true,
        "success": function (data) {
          if(page == 1) {
            obj.customers =[];
          }
          translateData(data.result.rows, obj.customers);
          obj.records = data.result.records;
          obj.page = data.result.page;
          obj.total = data.result.total;
          if (data.result.page == data.result.total || data.result.total == 0) {
            obj.moredata = false;
          }
          else {
            obj.moredata = true;
          }
          obj.page = obj.page+1;
        },
        "error": function () {
          $learunTopAlert.show({ text: "刷新失败" });
        },
        "finally": function () {
          callback();
        }
      });
    }
    //返回
    return {
      baseInit: function(){
        $learunGetDataItem({
          "itemName":'Client_Sort',
          "callback": function (data) {
            custType = data;
          }
        });
        $learunGetDataItem({
          "itemName":'Client_Level',
          "callback": function (data) {
            custLevel = data;
          }
        });
        $learunGetDataItem({
          "itemName":'Client_Degree',
          "callback": function (data) {
            custDegree = data;
          }
        });
        $learunGetDataItem({
          "itemName":'Client_Trade',
          "callback": function (data) {
            custTrade = data;
          }
        });
      },
      getList: function () {
        return custList;
      },
      update: function (callback) {
        getData(1,{},custList,callback);
      },
      add: function (callback) {
        getData(custList.page,{},custList,callback);
      },
      get: function (custId) {
        for (var i = 0; i < custList.customers.length; i++) {
          if (custList.customers[i].customerId === custId) {
            return custList.customers[i];
          }
        }
        if(custListSearch.customers != undefined)
        {
          for (var i = 0; i < custListSearch.customers.length; i++) {
            if (custListSearch.customers[i].customerId === custId) {
              return custListSearch.customers[i];
            }
          }
        }
        return null;
      },
      getSearchList: function () {
        custListSearch = {
          records: 0,
          page: 1,
          total: 1,
          moredata: false,
          customers: [],
          keyword: ""
        };
        return custListSearch;
      },
      searchData: function () {
        if (custListSearch.keyword == "") {
          return false;
        }
        getData(1,{ "condition": "All", "keyword": custListSearch.keyword },custListSearch, function () {});
      },
      searchDataAdd:function (callback){
        if (custListSearch.keyword == "") {
          return false;
        }
        getData(custListSearch.page,{ "condition": "All", "keyword": custListSearch.keyword },custListSearch,callback);
      },
      remove: function (customer) {
        $learunHttp.post({
          "url": ApiUrl.deleteCustomerApi,
          "data": { "customerId": customer.customerId },
          "success": function (data) {
            if(custListSearch.records > 0) {
              custListSearch.customers.splice(custListSearch.customers.indexOf(customer), 1);
              custListSearch.records = custListSearch.records -1;
              for (var i = 0; i < custList.customers.length; i++) {
                if (custList.customers[i].customerId === customer.customerId) {
                  customer =  custList.customers[i];
                }
              }
            }
            custList.customers.splice(custList.customers.indexOf(customer), 1);
            custList.records = custList.records -1;
          },
          "error": function () {
            $learunTopAlert.show({ text: "删除失败！" });
          }
        });
      },
      editSubmit: function (editData,callback) {
        $learunHttp.post({
          "url": ApiUrl.saveCustomerApi,
          "data": editData,
          "isverify": true,
          "success": function (data) {
            $learunTopAlert.show({ text: "保存成功！" });
          },
          "error": function () {
            $learunTopAlert.show({ text: "保存失败！" });
          },
          "finally":function(){
            callback();
          }
        });
      },
      getCustType:function()
      {
        return custType;
      },
      getCustLevel:function()
      {
        return custLevel;
      },
      getCustDegree:function()
      {
        return custDegree;
      },
      getCustTrade:function()
      {
        return custTrade;
      },
      getEditDataEx:function(){
        return editDataEx;
      }
    };
}])
//商机列表信息
  .factory('lrmBusinesss',['$learunFormatDate','$learunGetDataItem','$learunHttp','ApiUrl','$learunTopAlert',
    function ($learunFormatDate,$learunGetDataItem,$learunHttp,ApiUrl,$learunTopAlert) {
    //商机列表数据
    var businessList ={
      records: 0,
      page: 1,
      total: 1,
      moredata: false,
      businesss: []
    };
    var businessListSearch = {};//搜索列表数据
    var chancePhases = {};//商机阶段
    var chanceSource = {};//商机来源
    var editDataEx = [
      {
        "name":"商机名称",
        "id":"fullName",
        "isRequire":true
      },
      {
        "name":"预计金额",
        "id": "amount",
        "isRequire":true
      },
      {
        "name":"商机来源",
        "id":"sourceId",
        "isRequire":true
      },
      {
        "name":"商机阶段",
        "id":"stageId",
        "isRequire":true
      },
      {
        "name":"公司名称",
        "id":"companyName",
        "isRequire":true
      },
      {
        "name":"跟进人员",
        "id":"traceUserName",
        "isRequire":true
      },
      {
        "name":"联系人",
        "id":"contacts",
        "isRequire":true
      },
      {
        "name":"手机",
        "id":"mobile",
        "isRequire":false
      },
      {
        "name":"QQ",
        "id":"qQ",
        "isRequire":false
      }
    ];//商家编辑数据字段

    //方法函数(数据遍历转化)
    function translateData(data,obj) {
      //console.log(data);
        for (var i in data) {
          var item = data[i];
          //data[i].stageName =  chancePhases[data[i].stageId].ItemName;
            //data[i].bgStageColor = chancePhases[data[i].stageId].bgColor;
          data[i].stageName = data[i].stageId;
          data[i].bgStageColor = 'calm-bg';
          //data[i].sourceName = chanceSource[data[i].sourceId].ItemName;
          data[i].sourceName = data[i].sourceId;

          data[i].lastDate =  $learunFormatDate(item.modifyDate, 'MM-dd');
          data[i].createDate = $learunFormatDate(item.createDate, 'yyyy-MM-dd hh:mm');
          data[i].modifyDate = $learunFormatDate(item.modifyDate, 'yyyy-MM-dd hh:mm');
          data[i].dealDate = $learunFormatDate(item.dealDate, 'yyyy-MM-dd');
          obj.push(data[i]);
        }
    };
    //获取数据方法
    function getData(page,queryData,obj,callback) {
      $learunHttp.post({
        "url": ApiUrl.chanceListApi,
        "data": { "page": page, "rows": 10, "sidx": "ModifyDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
        "isverify": true,
        "success": function (data) {
          if(page == 1) {
            obj.businesss =[];
          }
          translateData(data.result.rows, obj.businesss);
          obj.records = data.result.records;
          obj.page = data.result.page;
          obj.total = data.result.total;
          if (data.result.page == data.result.total || data.result.total == 0) {
            obj.moredata = false;
          }
          else {
            obj.moredata = true;
          }
          obj.page = obj.page+1;
        },
        "error": function () {
          $learunTopAlert.show({ text: "刷新失败" });
        },
        "finally": function () {
          callback();
        }
      });
    }
    return {
      init: function () {//初始化信息
        $learunGetDataItem({
          "itemName":'Client_ChancePhase',
          "callback": function (data) {
            //console.log(data);
            chancePhases = data;
          }
        });
        $learunGetDataItem({
          "itemName":'Client_ChanceSource',
          "callback": function (data) {
            chanceSource = data;
          }
        });
      },
      getList: function () {
        return businessList;
      },
      update: function (callback) {//刷新数据
        getData(1,{},businessList,callback);
      },
      add:function(callback) {
        getData(businessList.page,{},businessList,callback);
      },
      getSearchList: function () {
        businessListSearch = {
          records: 0,
          page: 1,
          total: 1,
          moredata: false,
          businesss: [],
          keyword: ""
        };
        return businessListSearch;
      },
      searchData: function () {
        if (businessListSearch.keyword == "") {
          return false;
        }
        getData(1,{ "condition": "All", "keyword": businessListSearch.keyword },businessListSearch, function () {});
      },
      searchDataAdd:function (callback){
        if (businessListSearch.keyword == "") {
          return false;
        }
        getData(businessListSearch.page,{ "condition": "All", "keyword": businessListSearch.keyword },businessListSearch,callback);
      },
      get: function (chanceId) {
        for (var i = 0; i < businessList.businesss.length; i++) {
          if (businessList.businesss[i].chanceId === chanceId) {
            return businessList.businesss[i];
          }
        }
        if(businessListSearch.businesss != undefined){
          for (var i = 0; i < businessListSearch.businesss.length; i++) {
            if (businessListSearch.businesss[i].chanceId === chanceId) {
              return businessListSearch.businesss[i];
            }
          }
        }
        return null;
      },
      getChancePhases: function () {
        return chancePhases;
      },
      getChanceSource: function () {
        return chanceSource;
      },
      getEditDataEx:function(){
        return editDataEx;
      },
      remove: function (business) {
        $learunHttp.post({
          "url": ApiUrl.deleteChanceApi,
          "data": { "chanceId": business.chanceId },
          "success": function () {
            if(businessListSearch.records > 0) {
              businessListSearch.businesss.splice(businessListSearch.businesss.indexOf(business), 1);
              businessListSearch.records = businessListSearch.records -1;
              for (var i = 0; i < businessList.businesss.length; i++) {
                if (businessList.businesss[i].chanceId === business.chanceId) {
                  business = businessList.businesss[i];
                }
              }
            }
            businessList.businesss.splice(businessList.businesss.indexOf(business), 1);
            businessList.records = businessList.records -1;
          },
          "error": function () {
            $learunTopAlert.show({ text: "删除失败！" });
          }
        });
      },
      editSubmit: function (editData,callback) {
        $learunHttp.post({
          "url": ApiUrl.saveChanceApi,
          "data": editData,
          "isverify": true,
          "success": function (data) {
            $learunTopAlert.show({ text: "保存成功!" });
          },
          "error": function () {
            $learunTopAlert.show({ text: "保存失败！" });
          },
          "finally":function(){
            callback();
          }
        });
      }
    };
  }])
//流程列表
.factory('lrmWFlows', function () {
    var wflows = {"createProcess":{
        name: '发起流程',
        icon: 'ion-ios-folder-outline',
        bgcolor: 'positive-bg',
        viewid: 'flowdesign.html'
    },"flowRoughdraft": {
        name: '草稿流程',
        icon: 'ion-ios-list-outline',
        bgcolor: 'calm-bg',
        viewid: 'flowRoughdraft.html'
    }, "myProcess":{
        name: '我的流程',
        icon: 'ion-ios-paper-outline',
        bgcolor: 'balanced-bg',
        viewid: 'flowProcess.html'
    }, "befProcess":{
        name: '待办流程',
        icon: 'ion-ios-locked-outline',
        bgcolor: 'energized-bg',
        viewid: 'flowBefProcess.html'
    },"aftProcess": {
        name: '已办流程',
        icon: 'ion-ios-unlocked-outline',
        bgcolor: 'assertive-bg',
        viewid: 'flowAftProcess.html'
    },"delegate": {
        name: '工作委托',
        icon: 'ion-ios-barcode-outline',
        bgcolor: 'royal-bg',
        viewid: 'flowDelegate.html'
    }};
    return {
        all: function () {
            return wflows;
        },
        get: function (id) {
            return wflows[id];
        }
    };
})
//发起流程
.factory('lrmFlowDesigns', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
  //发起流程列表数据
  var flowdesignList = {
    records: 0,
    page: 1,
    total: 1,
    moredata: false,
    flowdesigns: []
  };
  //方法函数
  function translateData(data, obj) {
    for (var i in data) {
      var item = data[i];
      data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
      obj.push(data[i]);
    }
  };
  //获取数据方法
  function getData(page, queryData, obj, callback) {
    $learunHttp.post({
      "url": ApiUrl.flowDesignListApi,
      "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
      "isverify": true,
      "success": function (data) {
        if (page == 1) {
          obj.flowdesigns = [];
        }
        var re = JSON.parse(data.result);
        translateData(re.rows, obj.flowdesigns);
        obj.records = re.records;
        obj.page = re.page;
        obj.total = re.total;
        if (re.page == re.total || re.total == 0) {
          obj.moredata = false;
        }
        else {
          obj.moredata = true;
        }
        obj.page = obj.page + 1;
      },
      "error": function () {
        $learunTopAlert.show({ text: "刷新失败" });
      },
      "finally": function () {
        callback();
      }
    });
  }
  return {
    getList: function () {
      return flowdesignList;
    },
    update: function (callback) {
      getData(1, {}, flowdesignList, callback);
    },
    add: function (callback) {
      getData(flowdesignList.page, {}, flowdesignList, callback);
    },
    get: function (flowId) {
      for (var i = 0; i < flowdesignList.flowdesigns.length; i++) {
          if (flowdesignList.flowdesigns[i].f_id === flowId) {
            return flowdesignList.flowdesigns[i];
          }
      }
      return null;
    },
    editSubmit: function (keyValue, editData, callback) {
        //alert(JSON.stringify(editData));
        $learunHttp.post({
            "url": ApiUrl.saveFlowDesignApi,
            "data" : {
                data: JSON.stringify(editData),
                keyValue: keyValue.toString()
            },
            //"data": {editData,keyValue},
            "isverify": true,
            "success": function (data) {
                $learunTopAlert.show({ text: "保存成功！" });
            },
            "error": function () {
                $learunTopAlert.show({ text: "保存失败！" });
            },
            "finally": function () {
                callback();
            }
        });
    },
    remove: function (flowdesign) {
      //console.log(flowdesign.flowId);
      $learunHttp.post({
            "url": ApiUrl.deleteFlowDesignApi,
            "data": { "flowId": flowdesign.flowId },
            "success": function () {
                for (var i = 0; i < flowdesignList.flowdesigns.length; i++) {
                    if (flowdesignList.flowdesigns[i].f_id === flowdesign.flowId) {
                        flowdesign = flowdesignList.flowdesigns[i];
                    }
                }
                flowdesignList.flowdesigns.splice(flowdesignList.flowdesigns.indexOf(flowdesign), 1);
                flowdesignList.records = flowdesignList.records - 1;
            },
            "error": function () {
                $learunTopAlert.show({ text: "删除失败！" });
            }
        });
    }
  };
}])
//草稿流程
.factory('lrFlowRoughdrafts', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
    //草稿流程列表数据
    var flowroughdraftList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        flowroughdrafts: []
    };
    //方法函数
    function translateData(data, obj) {
        for (var i in data) {
            var item = data[i];
            data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
            obj.push(data[i]);
        }
    };
    //获取数据方法
    function getData(page, queryData, obj, callback) {
        $learunHttp.post({
            "url": ApiUrl.flowRoughdraftListApi,
            "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
            "isverify": true,
            "success": function (data) {
                if (page == 1) {
                    obj.flowroughdrafts = [];
                }
                //console.log(data.result);

                var re = JSON.parse(data.result);

                translateData(re.rows, obj.flowroughdrafts);
                obj.records = re.records;
                obj.page = re.page;
                obj.total = re.total;
                if (re.page == re.total || re.total == 0) {
                    obj.moredata = false;
                }
                else {
                    obj.moredata = true;
                }
                obj.page = obj.page + 1;
            },
            "error": function () {
                $learunTopAlert.show({ text: "刷新失败" });
            },
            "finally": function () {
                callback();
            }
        });
    }
    return {
        getList: function () {
            return flowroughdraftList;
        },
        update: function (callback) {
            getData(1, {}, flowroughdraftList, callback);
        },
        add: function (callback) {
            getData(flowroughdraftList.page, {}, flowroughdraftList, callback);
        },
        get: function (flowId) {
            for (var i = 0; i < flowroughdraftList.flowroughdrafts.length; i++) {
                if (flowroughdraftList.flowroughdrafts[i].Id === flowId) {
                    return flowroughdraftList.flowroughdrafts[i];
                }
            }
            return null;
        },
        remove: function (flowroughdraft) {
          /*alert(JSON.stringify(flowroughdraft));*/
            $learunHttp.post({
                "url": ApiUrl.deleteFlowRoughdraftApi,
                "data": { "flowId": flowroughdraft.id },
                "success": function () {
                    for (var i = 0; i < flowroughdraftList.flowroughdrafts.length; i++) {
                        if (flowroughdraftList.flowroughdrafts[i].id === flowroughdraft.flowId) {
                            flowroughdraft = flowroughdraftList.flowroughdrafts[i];
                        }
                    }
                    flowroughdraftList.flowroughdrafts.splice(flowroughdraftList.flowroughdrafts.indexOf(flowroughdraft), 1);
                    flowroughdraftList.records = flowroughdraftList.records - 1;
                },
                "error": function () {
                    $learunTopAlert.show({ text: "删除失败！" });
                }
            });
        }
    };
}])
//我的流程列表
.factory('lrWorkflows', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
    //我的流程列表数据
    var workflowList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        workflows: []
    };
    //方法函数
    function translateData(data, obj) {
        for (var i in data) {
            var item = data[i];
            data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
            if (data[i].f_isfinish == 0) {
                data[i].f_isfinish = "处理中";
                data[i].bgcolor = "positive-bg";
            } else if (data[i].f_isfinish == 1) {
                data[i].f_isfinish = "通过完成";
                data[i].bgcolor = "calm-bg";
            } else if (data[i].f_isfinish == 2) {
                data[i].f_isfinish = "被召回";
                data[i].bgcolor = "bgcolor_d";
            } else if (data[i].f_isfinish == 3) {
                data[i].f_isfinish = "不通过";
                data[i].bgcolor = "bgcolor_d";
            } else if (data[i].f_isfinish == 4) {
                data[i].f_isfinish = "被驳回";
                data[i].bgcolor = "bgcolor_d";
            } else {
                data[i].f_isfinish = "暂停";
            }
            if (data[i].f_wflevel == 1) {
                data[i].f_wflevel = "重要";
            } else if (data[i].f_wflevel == 2) {
                data[i].f_wflevel = "普通";
            } else if (data[i].f_wflevel == 3) {
                data[i].f_wflevel = "一般";
            }
            obj.push(data[i]);
        }
        //alert(JSON.stringify(data));
    };
    //获取数据方法
    function getData(page, queryData, obj, callback) {
        $learunHttp.post({
            "url": ApiUrl.flowProcessListApi,
            "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
            "isverify": true,
            "success": function (data) {
                if (page == 1) {
                    obj.workflows = [];
                }
                //console.log(data.result);
                var re = JSON.parse(data.result);
                //console.log(re.rows[0].code);

                translateData(re.rows, obj.workflows);
                obj.records = re.records;
                obj.page = re.page;
                obj.total = re.total;
                if (re.page == re.total || re.total == 0) {
                    obj.moredata = false;
                }
                else {
                    obj.moredata = true;
                }
                obj.page = obj.page + 1;
            },
            "error": function () {
                $learunTopAlert.show({ text: "刷新失败" });
            },
            "finally": function () {
                callback();
            }
        });
    }
    return {
        getList: function () {
            return workflowList;
        },
        update: function (callback) {
            getData(1, {}, workflowList, callback);
        },
        add: function (callback) {
            getData(workflowList.page, {}, workflowList, callback);
        },
        get: function (flowId) {
            for (var i = 0; i < workflowList.workflows.length; i++) {
                if (workflowList.workflows[i].f_id === flowId) {
                    return workflowList.workflows[i];
                }
            }
            return null;
        },
        remove: function (workflow) {
            $learunHttp.post({
                "url": ApiUrl.deleteWorkflowApi,
                "data": { "flowId": workflow.flowId },
                "success": function () {
                    for (var i = 0; i < workflowList.workflows.length; i++) {
                        if (workflowList.workflows[i].f_id === workflow.flowId) {
                            workflow = workflowList.workflows[i];
                        }
                    }
                    workflowList.workflows.splice(workflowList.workflows.indexOf(workflow), 1);
                    workflowList.records = workflowList.records - 1;
                },
                "error": function () {
                    $learunTopAlert.show({ text: "删除失败！" });
                }
            });
        }
    };
}])
//代办流程
.factory('lrFlowBefProcesss', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
    //代办流程列表数据
    var flowbefprocessList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        flowbefprocesss: []
    };
    //方法函数
    function translateData(data, obj) {
        for (var i in data) {
            var item = data[i];
            data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
            if (data[i].f_wflevel == 1) {
                data[i].f_wflevel = "重要";
            } else if (data[i].f_wflevel == 2) {
                data[i].f_wflevel = "普通";
            } else if (data[i].f_wflevel == 3) {
                data[i].f_wflevel = "一般";
            }
            obj.push(data[i]);
        }
    };
    //获取数据方法
    function getData(page, queryData, obj, callback) {
        $learunHttp.post({
            "url": ApiUrl.flowBefProcessListApi,
            "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
            "isverify": true,
            "success": function (data) {
                if (page == 1) {
                    obj.flowbefprocesss = [];
                }
                var re = JSON.parse(data.result);

                translateData(re.rows, obj.flowbefprocesss);
                obj.records = re.records;
                obj.page = re.page;
                obj.total = re.total;
                if (re.page == re.total || re.total == 0) {
                    obj.moredata = false;
                }
                else {
                    obj.moredata = true;
                }
                obj.page = obj.page + 1;
            },
            "error": function () {
                $learunTopAlert.show({ text: "刷新失败" });
            },
            "finally": function () {
                callback();
            }
        });
    }
    return {
        getList: function () {
            return flowbefprocessList;
        },
        update: function (callback) {
            getData(1, {}, flowbefprocessList, callback);
        },
        add: function (callback) {
            getData(flowbefprocessList.page, {}, flowbefprocessList, callback);
        },
        get: function (flowId) {
            for (var i = 0; i < flowbefprocessList.flowbefprocesss.length; i++) {
                if (flowbefprocessList.flowbefprocesss[i].Id === flowId) {
                    return flowbefprocessList.flowbefprocesss[i];
                }
            }
            return null;
        },
        remove: function (flowbefprocess) {
            $learunHttp.post({
                "url": ApiUrl.deleteFlowBefProcessApi,
                "data": { "flowId": flowbefprocess.flowId },
                "success": function () {
                    for (var i = 0; i < flowbefprocessList.flowbefprocesss.length; i++) {
                        if (flowbefprocessList.flowbefprocesss[i].Id === flowbefprocess.flowId) {
                            flowbefprocess = flowbefprocessList.flowbefprocesss[i];
                        }
                    }
                    flowbefprocessList.flowbefprocesss.splice(flowbefprocessList.flowbefprocesss.indexOf(flowbefprocess), 1);
                    flowbefprocessList.records = flowbefprocessList.records - 1;
                },
                "error": function () {
                    $learunTopAlert.show({ text: "删除失败！" });
                }
            });
        },
        editSubmit: function (editData, callback) {
          //alert(JSON.stringify(editData));
          $learunHttp.post({
            "url": ApiUrl.saveFlowBefProcessApi,
            "data": editData,
            //"isverify": true,
            "success": function (data) {
              $learunTopAlert.show({ text: "保存成功！" });
            },
            "error": function () {
              $learunTopAlert.show({ text: "保存失败！" });
            },
            "finally": function () {
              callback();
            }
          });
        }
    };
}])
//已办流程
.factory('lrFlowAftProcesss', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
    //已办流程列表数据
    var flowaftprocessList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        flowaftprocesss: []
    };
    //方法函数
    function translateData(data, obj) {
        for (var i in data) {
            var item = data[i];
            data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
            if (data[i].f_isfinish == 0) {
                data[i].f_isfinish = "处理中";
            } else if (data[i].f_isfinish == 1) {
                data[i].f_isfinish = "通过完成";
            } else if (data[i].f_isfinish == 2) {
                data[i].f_isfinish = "被召回";
            } else if (data[i].f_isfinish == 3) {
                data[i].f_isfinish = "不通过";
            } else if (data[i].f_isfinish == 4) {
                data[i].f_isfinish = "被驳回";
            }
            obj.push(data[i]);
        }
        //console.log(JSON.stringify(data[i]));
    };
    //获取数据方法
    function getData(page, queryData, obj, callback) {
        $learunHttp.post({
            "url": ApiUrl.flowAftProcessListApi,
            "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
            "isverify": true,
            "success": function (data) {
                if (page == 1) {
                    obj.flowaftprocesss = [];
                }

                //console.log(data.result);

                var re = JSON.parse(data.result);
                translateData(re.rows, obj.flowaftprocesss);
                obj.records = re.records;
                obj.page = re.page;
                obj.total = re.total;
                if (re.page == re.total || re.total == 0) {
                    obj.moredata = false;
                }
                else {
                    obj.moredata = true;
                }
                obj.page = obj.page + 1;
            },
            "error": function () {
                $learunTopAlert.show({ text: "刷新失败" });
            },
            "finally": function () {
                callback();
            }
        });
    }
    return {
        getList: function () {
            return flowaftprocessList;
        },
        update: function (callback) {
            getData(1, {}, flowaftprocessList, callback);
        },
        add: function (callback) {
            getData(flowaftprocessList.page, {}, flowaftprocessList, callback);
        },
        get: function (flowId) {
          console.log(flowaftprocessList.flowaftprocesss.length);
            for (var i = 0; i < flowaftprocessList.flowaftprocesss.length; i++) {
                if (flowaftprocessList.flowaftprocesss[i].f_id === flowId) {
                    return flowaftprocessList.flowaftprocesss[i];
                }
            }
            return null;
        },
        remove: function (flowaftprocess) {

            $learunHttp.post({
                "url": ApiUrl.deleteFlowAftProcessApi,
                "data": { "flowId": flowaftprocess.flowId },
                "success": function () {
                    for (var i = 0; i < flowaftprocessList.flowaftprocesss.length; i++) {
                        if (flowaftprocessList.flowaftprocesss[i].f_id === flowaftprocess.flowId) {
                            flowaftprocess = flowaftprocessList.flowaftprocesss[i];
                        }
                    }
                    flowaftprocessList.flowaftprocesss.splice(flowaftprocessList.flowaftprocesss.indexOf(flowaftprocess), 1);
                    flowaftprocessList.records = flowaftprocessList.records - 1;
                },
                "error": function () {
                    $learunTopAlert.show({ text: "删除失败！" });
                }
            });
        }
    };
}])
//工作委托
.factory('lrFlowDelegates', ['$learunFormatDate', '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunGetDataItem',
  function ($learunFormatDate, $learunHttp, ApiUrl, $learunTopAlert, $learunGetDataItem) {
  //发起流程列表数据
  var flowdelegateList = {
    records: 0,
    page: 1,
    total: 1,
    moredata: false,
    flowdelegates: []
  };
  //方法函数
  function translateData(data, obj) {
      for (var i in data) {
          var item = data[i];
          data[i].f_createdate = $learunFormatDate(item.f_createdate, 'MM-dd');
          data[i].f_begindate = $learunFormatDate(item.f_begindate, 'MM-dd');
          data[i].f_enddate = $learunFormatDate(item.f_enddate, 'MM-dd');
          obj.push(data[i]);
      }
  };
  //获取数据方法
  function getData(page, queryData, obj, callback) {
    $learunHttp.post({
      "url": ApiUrl.flowDelegateListApi,
      "data": { "page": page, "rows": 10, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
      "isverify": true,
      "success": function (data) {
        if (page == 1) {
            obj.flowdelegates = [];
        }
        //console.log(data.result);

        var re = JSON.parse(data.result);
        translateData(re.rows, obj.flowdelegates);
        obj.records = re.records;
        obj.page = re.page;
        obj.total = re.total;
        if (re.page == re.total || re.total == 0) {
            obj.moredata = false;
        }
        else {
            obj.moredata = true;
        }
        obj.page = obj.page + 1;
      },
      "error": function () {
          $learunTopAlert.show({ text: "刷新失败" });
      },
      "finally": function () {
          callback();
      }
    });
  }
  return {
    getList: function () {
      return flowdelegateList;
    },
    update: function (callback) {
      getData(1, {}, flowdelegateList, callback);
    },
    add: function (callback) {
      getData(flowdelegateList.page, {}, flowdelegateList, callback);
    },
    get: function (flowId) {
      for (var i = 0; i < flowdelegateList.flowdelegates.length; i++) {
        if (flowdelegateList.flowdelegates[i].f_id === flowId) {
          return flowdelegateList.flowdelegates[i];
        }
      }
      return null;
    },
    editSubmit: function (editData, callback) {
      //alert(JSON.stringify(editData));
      $learunHttp.post({
        "url": ApiUrl.saveFlowDelegateApi,
        "data": editData,
        //"isverify": true,
        "success": function (data) {
          $learunTopAlert.show({ text: "保存成功！" });
        },
        "error": function () {
          $learunTopAlert.show({ text: "保存失败！" });
        },
        "finally": function () {
          callback();
        }
      });
    },
    remove: function (flowdelegate) {
      console.log(flowdelegate.flowId);
      $learunHttp.post({
        "url": ApiUrl.deleteFlowDeleteApi,
        "data": { "flowId": flowdelegate.flowId },
        "success": function () {
          for (var i = 0; i < flowdelegateList.flowdelegates.length; i++) {
            if (flowdelegateList.flowdelegates[i].f_id === flowdelegate.flowId) {
              flowdelegate = flowdelegateList.flowdelegates[i];
            }
          }
          flowdelegateList.flowdelegates.splice(flowdelegateList.flowdelegates.indexOf(flowdelegate), 1);
          flowdelegateList.records = flowdelegateList.records - 1;
        },
        "error": function () {
          $learunTopAlert.show({ text: "删除失败！" });
        }
      });
    }
  };
}])
//即时通讯
    .factory('lrmIM', ['$q', 'UserInfo', 'SignalrUrl', '$learunLoadJs', '$learunFormatDate', function ($q, UserInfo, SignalrUrl, $learunLoadJs, $learunFormatDate) {
        var imServer = {
            "userInfo": UserInfo.get(),
            "isIMLoaded":false,
            "fnDisconnected":null,

            "updateUserStatus": function (userId, isOnLine) { },//刷用户在线状态0离线。1在线
            "revMessage": function (formUser, message, dateTime) { },//接收消息
            "revGroupMessage": function (userId, toGroup, message, dateTime) { },//接收群组消息
            "afterLinkSuccess": function () { },//连接服务成功后
            "disconnected": function () { },//断开连接后

            "userAllList": [],
            "userAllListByDepartment": {},
            "lastUserList": {},
            "lastUserArray":[],
            "currentUserId": "",
            "messages":[]

        };

        function LastUserListToArray()
        {
            imServer.lastUserArray = [];
            for (var i in imServer.lastUserList) {
                imServer.lastUserList[i].IMCreateTime = $learunFormatDate(imServer.lastUserList[i].IMCreateTime, "MM-dd hh:mm");
                imServer.lastUserArray.push(imServer.lastUserList[i]);
            }
        }

        return {
            "init": function (fnDisconnected,$scope) {
                imServer.fnDisconnected = fnDisconnected;
                var deferred = $q.defer();
                console.log(imServer.isIMLoaded, "isIMLoaded -mim");
                if (!imServer.isIMLoaded) {
                    $learunLoadJs(SignalrUrl+"/hubs").then(
                        function (data) {
                            if (data == "success") {
                                console.log(data, "js获取成功mim");
                                $.connection.hub.url = SignalrUrl;
                                $.connection.hub.qs = { "userId": imServer.userInfo.userid };
                                var chat = $.connection.ChatsHub;

                                //注册客户端方法
                                //更新联系人列表
                                chat.client.IMUpdateUserList = function (userAllList, onLineUserList) {
                                    imServer.userAllListByDepartment = {};
                                    imServer.userAllList = userAllList;
                                    for (var Id in userAllList){
                                        var item = userAllList[Id];
                                        if (Id != imServer.userInfo.userid) {
                                            if (imServer.userAllListByDepartment[item.DepartmentId] == undefined) {
                                                imServer.userAllListByDepartment[item.DepartmentId] = {};
                                                imServer.userAllListByDepartment[item.DepartmentId].DepartmentName = item.DepartmentId;
                                                if (item.UserOnLine == 1) {
                                                    imServer.userAllListByDepartment[item.DepartmentId].onLineNum = 1;
                                                }
                                                else {
                                                    imServer.userAllListByDepartment[item.DepartmentId].onLineNum = 0;
                                                }
                                                imServer.userAllListByDepartment[item.DepartmentId].UserList = [];
                                                imServer.userAllListByDepartment[item.DepartmentId].UserList.push(item);
                                            }
                                            else {
                                                if (item.UserOnLine == 1) {
                                                    imServer.userAllListByDepartment[item.DepartmentId].onLineNum++;
                                                }
                                                imServer.userAllListByDepartment[item.DepartmentId].UserList.push(item);
                                            }
                                        }
                                    };
                                    if ($scope != null && $scope != undefined)
                                    {
                                        $scope.$apply();
                                    }
                                    console.log(imServer.userAllListByDepartment);
                                }
                                //刷新最近的联系人
                                chat.client.IMUpdateLastUser = function (lastUserList) {
                                    imServer.lastUserList = lastUserList;
                                    LastUserListToArray();
                                    if ($scope != null && $scope != undefined) {
                                        $scope.$apply();
                                    }

                                }
                                //刷新用户在线状态
                                chat.client.IMUpdateUserStatus = function (userId, isOnLine) {
                                    imServer.userAllList[userId].UserOnLine = isOnLine;
                                    if ($scope != null && $scope != undefined) {
                                        $scope.$apply();
                                    }
                                }
                                //接收消息
                                chat.client.RevMessage = function (formUser, message, dateTime) {
                                    if (imServer.currentUserId != "") {
                                        var msgContent = {
                                            "Content": message,
                                            "CreateDate": dateTime,
                                            "SendId": formUser
                                        };
                                        imServer.messages.push(msgContent);
                                        imServer.IMUpdateMessageStatus(imServer.currentUserId);
                                        if (imServer.lastUserList[imServer.currentUserId] == undefined) {
                                            imServer.lastUserList[imServer.currentUserId] = {};
                                            imServer.lastUserList[imServer.currentUserId].UnReadNum = 0;
                                            imServer.lastUserList[imServer.currentUserId].OtherId = imServer.currentUserId;
                                            imServer.lastUserList[imServer.currentUserId].SendId = formUser;
                                            var id = (formUser == imServer.userInfo.userid ? imServer.currentUserId : formUser);
                                            imServer.lastUserList[imServer.currentUserId].UserId = id;
                                        }
                                        imServer.lastUserList[imServer.currentUserId].IMCreateTime = dateTime;
                                        imServer.lastUserList[imServer.currentUserId].IMContent = message;

                                    } else {
                                        if (imServer.lastUserList[formUser] == undefined)
                                        {
                                            imServer.lastUserList[formUser] = {};
                                            imServer.lastUserList[formUser].UnReadNum = 0;
                                        }
                                        imServer.lastUserList[formUser].IMContent = message;
                                        imServer.lastUserList[formUser].OtherId = formUser;
                                        imServer.lastUserList[formUser].SendId = formUser;

                                        imServer.lastUserList[formUser].IMCreateTime = dateTime;
                                        imServer.lastUserList[formUser].UserId = imServer.userInfo.userid;

                                        imServer.lastUserList[formUser].UnReadNum++;
                                    }
                                    LastUserListToArray();
                                    if ($scope != null && $scope != undefined) {
                                        $scope.$apply();
                                        $scope.scrollBottom();
                                    }
                                }

                                //开启连接
                                $.connection.hub.start().done(function () {
                                    console.log("连接成功hub");
                                    //连接成功后注册方法
                                    imServer.IMGetMsgList = function (page, rownum, sendId) {//获取与某用户的消息列表
                                        var deferredGetMsglist = $q.defer();
                                        var pagination = { rows: rownum, page: page, sidx: 'CreateDate', sord: 'desc' }
                                        chat.server.getMsgList(pagination, sendId).done(function (resdata) {
                                            var data = [],i=0;
                                            for (i = resdata.length, i >= 0; i--;) {
                                                data.push(resdata[i]);
                                            }
                                            imServer.messages = data;
                                            console.log(data);
                                            deferredGetMsglist.resolve(data);
                                        });
                                        return deferredGetMsglist.promise;
                                    };
                                    imServer.IMUpdateMessageStatus = function (sendId) {//更新消息状态
                                        return chat.server.updateMessageStatus(sendId);
                                    };
                                    imServer.IMSendToOne = function (toUser, message) {//发送消息
                                        chat.server.imSendToOne(toUser, message);
                                    };

                                    imServer.isIMLoaded = true;
                                    deferred.resolve({ "code": "success", "msg": "即时通讯连接成功" });
                                });

                                //断开连接后
                                $.connection.hub.disconnected(function () {
                                    imServer.isIMLoaded = false;
                                    if (imServer.fnDisconnected != null) {
                                        imServer.fnDisconnected();
                                    }
                                });
                            }
                            else {
                                console.log(data, "连接成功服务端未开启mim");
                                deferred.resolve({ "code": "error", "msg": "服务端未开启" });
                            }
                        }
                    );
                }
                else {
                    deferred.resolve({ "code": "success", "msg": "即时通讯连接成功" });
                }

                return deferred.promise;
            },
            "getServer": function () {
                return imServer;
            }
        }
    }])
  //首页公告
  .factory('Announces', function () {
    var announces = [{
        id: 0,
        name: '新一代快速开发框架是什么',
        icon: 'ion-ios-list-outline',
        bgcolor: 'positive-bg',
        viewid: 'list',
    }, {
        id: 1,
        name: '新一代手机APP问世',
        icon: 'ion-ios-list-outline',
        bgcolor: 'positive-bg',
        viewid: 'list',
    }, {
        id: 2,
        name: '新一代快速开发框架发布',
        icon: 'ion-ios-list-outline',
        bgcolor: 'positive-bg',
        viewid: 'list',
    }, {
        id: 3,
        name: '获得政府的大力支持',
        icon: 'ion-ios-list-outline',
        bgcolor: 'positive-bg',
        viewid: 'list',
    }];
    return {
        all: function () {
            return announces;
        },
        remove: function (announces) {
            announces.splice(announces.indexOf(announces), 1);
        },
        get: function (announceId) {
            for (var i = 0; i < announces.length; i++) {
                if (announces[i].id == parseInt(announceId)) {
                    return announces[i];
                }
            }
            return null;
        }
    };
})
  //订单信息
  .factory('lrmOrderInfos',[
    '$learunHttp','ApiUrl','$learunTopAlert','$learunFormatDate',
    function ($learunHttp, ApiUrl,$learunTopAlert,$learunFormatDate) {
      //客户列表
      var orderInfoList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        orderInfos: []
      };
      //方法函数
      function translateData(data, obj) {
        for (var i in data) {
          var item = data[i];
          if(i == 0){
            data[i].orderbgColor = "balanced-bg";
          }else if(i%5 == 0){
            data[i].orderbgColor = "energized-bg";
          }else if(i%3 == 0){
            data[i].orderbgColor = "calm-bg";
          }else if(i%4 == 0){
            data[i].orderbgColor = "royal-bg";
          }else if(i%2 == 0){
            data[i].orderbgColor = "positive-bg";
          }else {
            data[i].orderbgColor = "assertive-bg";
          }
          data[i].createDate = $learunFormatDate(item.createDate, 'yyyy-MM-dd');
          data[i].modifyDate = $learunFormatDate(item.modifyDate, 'yyyy-MM-dd');
          data[i].paymentDate = $learunFormatDate(item.paymentDate, 'yyyy-MM-dd');
          data[i].orderDate = $learunFormatDate(item.orderDate, 'yyyy-MM-dd');
          obj.push(data[i]);
        }
      };
      //获取数据
      function getData(page,queryData, obj,callback) {
        $learunHttp.post({
          "url": ApiUrl.getOrderListApi,
          "data": { "page": page, "rows": 10, "sidx": "ModifyDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
          "isverify": true,
          "success": function (data) {
            if(page == 1) {
              obj.orderInfos =[];
            }
            translateData(data.result.rows, obj.orderInfos);
            obj.records = data.result.records;
            obj.page = data.result.page;
            obj.total = data.result.total;
            if (data.result.page == data.result.total || data.result.total == 0) {
              obj.moredata = false;
            }
            else {
              obj.moredata = true;
            }
            obj.page = obj.page+1;
          },
          "error": function () {
            $learunTopAlert.show({ text: "刷新失败" });
          },
          "finally": function () {
            callback();
          }
        });
      }
      return{
        getList: function () {
          return orderInfoList;
        },
        update: function (callback) {
          getData(1,{},orderInfoList,callback);
        },
        add: function (callback) {
          getData(orderInfoList.page,{},orderInfoList,callback);
        },
        get: function (orderId) {
            for (var i = 0; i < orderInfoList.orderInfos.length; i++) {
            console.log(orderInfoList.orderInfos[i].orderId);
            if (orderInfoList.orderInfos[i].orderId == orderId) {
              return orderInfoList.orderInfos[i];
            }
          }
          return null;
        }
      };
    }])
  //收款信息
  .factory('lrmReceivables',[
    '$learunHttp', 'ApiUrl', '$learunTopAlert', '$learunFormatDate',
    function ($learunHttp, ApiUrl, $learunTopAlert, $learunFormatDate) {
      //客户列表
      var receivableList = {
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        receivables: []
      };
      //方法函数
      function translateData(data, obj) {
        for (var i in data) {
          var item = data[i];
          //console.log(i);royal-bg
          if(i == 0){
            data[i].f_OrderbgColor = "balanced-bg";
          }else if(i%5 == 0){
            data[i].f_OrderbgColor = "energized-bg";
          }else if(i%3 == 0){
            data[i].f_OrderbgColor = "calm-bg";
          }else if(i%4 == 0){
            data[i].f_OrderbgColor = "royal-bg";
          }else if(i%2 == 0){
            data[i].f_OrderbgColor = "positive-bg";
          }else {
            data[i].f_OrderbgColor = "assertive-bg";
          }
          data[i].f_CreateDate = $learunFormatDate(item.f_CreateDate, 'yyyy-MM-dd');
          data[i].f_ModifyDate = $learunFormatDate(item.f_ModifyDate, 'yyyy-MM-dd');
          data[i].f_OrderDate = $learunFormatDate(item.f_OrderDate, 'yyyy-MM-dd');
          data[i].f_PaymentDate = $learunFormatDate(item.f_PaymentDate, 'yyyy-MM-dd');
          obj.push(data[i]);
        }
      };
      //获取数据
      function getData(page,queryData, obj,callback) {
        //console.log(obj.receivables);
        $learunHttp.post({
          "url": ApiUrl.getReceivableListApi,
          "data": { "page": page, "rows": 10, "sidx": "F_ModifyDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
          "isverify": true,
          "success": function (data) {
            if(page == 1) {
              obj.receivables =[];
            }
            translateData(data.result.rows, obj.receivables);
            obj.records = data.result.records;
            obj.page = data.result.page;
            obj.total = data.result.total;
            if (data.result.page == data.result.total || data.result.total == 0) {
              obj.moredata = false;
            }
            else {
              obj.moredata = true;
            }
            obj.page = obj.page+1;
          },
          "error": function () {
            $learunTopAlert.show({ text: "刷新失败" });
          },
          "finally": function () {
            callback();
          }
        });
      }
      return{
        getList: function () {
          //console.log(receivableList);
          return receivableList;
        },
        update: function (callback) {
          getData(1,{},receivableList,callback);
        },
        add: function (callback) {
          getData(receivableList.page,{},receivableList,callback);
        },
        get: function (receivableId) {
          for (var i = 0; i < receivableList.receivables.length; i++) {
            //console.log(receivableList.receivables[i].f_OrderId);
            if (receivableList.receivables[i].OrderId == receivableId) {
              return receivableList.receivables[i];
            }
          }
          return null;
        }
      };
    }])
  //收款报表
  .factory('lrmReceivableReports',[
    '$learunHttp','$learunFormatDate','$learunTopAlert','ApiUrl',
    function ($learunHttp, $learunFormatDate, $learunTopAlert, ApiUrl) {
      //客户列表
      var receivableReportList ={
        records: 0,
        page: 1,
        total: 1,
        moredata: false,
        receivableReports: []
      };
      //方法函数
      function translateData(data, obj) {
        for (var i in data) {
          var item = data[i];
          if(i == 0){
            data[i].f_OrderbgColor = "balanced-bg";
          }else if(i%5 == 0){
            data[i].f_OrderbgColor = "energized-bg";
          }else if(i%3 == 0){
            data[i].f_OrderbgColor = "calm-bg";
          }else if(i%4 == 0){
            data[i].f_OrderbgColor = "royal-bg";
          }else if(i%2 == 0){
            data[i].f_OrderbgColor = "positive-bg";
          }else {
            data[i].f_OrderbgColor = "assertive-bg";
          }
          data[i].f_CreateDate = $learunFormatDate(item.f_CreateDate, 'yyyy-MM-dd');
          data[i].f_PaymentTime = $learunFormatDate(item.f_PaymentTime, 'yyyy-MM-dd');
          obj.push(data[i]);
        }
      };
      //获取数据
      function getData(page, queryData, obj, callback) {
        $learunHttp.post({
          "url": ApiUrl.getReceivableReportListApi,
          "data": { "page": page, "rows": 8, "sidx": "F_CreateDate", "sord": "desc", "queryData": JSON.stringify(queryData) },
          "isverify": true,
          "success": function (data) {
            console.log(data);
            if(page == 1) {
              obj.receivableReports = [];
            }
            //console.log(JSON.stringify(data.result.rows));
            translateData(data.result.rows, obj.receivableReports);
            obj.records = data.result.records;
            obj.page = data.result.page;
            obj.total = data.result.total;
            if (data.result.page == data.result.total || data.result.total == 0) {
              obj.moredata = false;
            }
            else {
              obj.moredata = true;
            }
            obj.page = obj.page+1;
          },
          "error": function () {
            $learunTopAlert.show({ text: "刷新失败" });
          },
          "finally": function () {
            callback();
          }
        });
      }

      return{
        getList: function () {
          console.log(receivableReportList);
          return receivableReportList;
        },
        update: function (callback) {
          getData(1,{},receivableReportList,callback);
        }
      };
    }])
  //我的账号信息
  .factory('UserInfo',['$learunLocals',function ($learunLocals) {
    var userInfo = {
        userid: "",
        account: "",
        password: "",
        realname: "",
        headicon: "img/logo.png",
        gender: "",
        organizeid: "",
        organizename: "",
        departmentid: "",
        departmentname: "",
        dutyid: "",
        dutyname: "",
        postid: "",
        postname: "",
        roleid: "",
        rolename: "",
        managerid: "",
        manager: "",
        description: "",
        mobile: "",
        telephone: "",
        email: "",
        oicq: "",
        wechat: "",
        msn: "",
        token: "",
        isLogin:false,
    };
    return {
        get: function () {
            var newUserInfo = $learunLocals.getObject('userInfo');
            if (newUserInfo.isLogin == undefined)
            {
                userInfo.isLogin = false;
                userInfo.token = "";
            }
            ionic.extend(userInfo, userInfo, newUserInfo || {});
            return userInfo;
        },
        set: function (data, token, isLogin) {
            data.headicon = "img/logo.png";//(data.headicon == undefined ? userInfo.headicon : data.headicon);
            ionic.extend(userInfo, userInfo, data || {});
            userInfo.token = token;
            userInfo.isLogin = isLogin;
            $learunLocals.setObject('userInfo', userInfo);
        },
        clear: function ()
        {
            userInfo.isLogin = false;
            userInfo.token = "";
            $learunLocals.setObject('userInfo', {});
        }
    };
}])
  //行政区域数据
  .factory('AreaInfo', ['$learunHttp','ApiUrl',function ($learunHttp, ApiUrl) {
    var areaInfo = {};
    return {
      init: function () {
        $learunHttp.post({
          "url": ApiUrl.areaListApi,
          "isverify": true,
          "success": function (data) {
              areaInfo = data.result;
          }
        });
      },
      getProvinceName: function (provinceId) {
        return areaInfo[provinceId].areaName;
      },
      getCityName: function (provinceId, cityId)
      {
        return areaInfo[provinceId].children[cityId];
      }
    };
}])
  //基础信息
  .factory('lrmBaseInfo',['$learunHttp','ApiUrl', function ($learunHttp,ApiUrl) {
    var baseInfo ={
      userListInfo:[]//用户列表
    };
    return{
      init:function(){
        $learunHttp.post({
          "url": ApiUrl.getUserListApi,
          "isverify": true,
          "success": function (data) {
            baseInfo.userListInfo = data.result;
          }
        });
      },
      getUserInfoList:function(depId){
        var data = [];
        for(var i in baseInfo.userListInfo)
        {
          var item = baseInfo.userListInfo[i];
          if(item.DepartmentId == depId) {
            data.push(item);
          }
        }
        return data;
      }
    };
  }])
;
