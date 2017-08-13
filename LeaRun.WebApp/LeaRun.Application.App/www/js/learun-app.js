// Ionic Starter App

// angular.module is a global place for creating, registering and retrieving Angular modules
// 'starter' is the name of this angular module example (also set in a <body> attribute in index.html)
// the 2nd parameter is an array of 'requires'
var app = angular.module('starter', ['ionic', 'starter.config', 'starter.uirouter', 'starter.directive', 'starter.controllers', 'starter.modules', 'starter.services', 'base64', 'angular-md5', 'ionic-datepicker', 'ngCordova','highcharts-ng','monospaced.elastic'])
.run(['$ionicPlatform', '$rootScope', '$state', 'UserInfo',
  function ($ionicPlatform, $rootScope, $state,UserInfo) {
    $ionicPlatform.ready(function() {
      // Hide the accessory bar by default (remove this to show the accessory bar above the keyboard
      // for form inputs)
      if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard) {
          cordova.plugins.Keyboard.hideKeyboardAccessoryBar(false);
          cordova.plugins.Keyboard.disableScroll(true);
      }
      /*判断当前是否有登录信息*/
      $rootScope.$on('$stateChangeStart',
      function (event, toState) {
          if (toState.name != 'login' && UserInfo.get().isLogin != true)
          {
            event.preventDefault();
            $state.go('login');
          }
      });
      if ($state.current.name != 'login')
      {
          if (UserInfo.get().isLogin != true)
          {
            $state.go('login');
          }
      }
    });
}])
.config(['$ionicConfigProvider',function ($ionicConfigProvider) {
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
    $ionicConfigProvider.tabs.position('bottom'); // other values: top
}])
.config(['ionicDatePickerProvider',function (ionicDatePickerProvider) {
    var datePickerObj = {
        inputDate: new Date(),
        setLabel: 'Set',
        todayLabel: 'Today',
        closeLabel: 'Close',
        mondayFirst: false,
        weeksList: ["S", "M", "T", "W", "T", "F", "S"],
        monthsList: ["Jan", "Feb", "March", "April", "May", "June", "July", "Aug", "Sept", "Oct", "Nov", "Dec"],
        templateType: 'popup',
        from: new Date(2014, 8, 1),
        to: new Date(2020, 8, 1),
        showTodayButton: true,
        dateFormat: 'yyyy-MMMM-dd',
        closeOnSelect: false,
        disableWeekdays: [6],
    };
    ionicDatePickerProvider.configDatePicker(datePickerObj);
}]);

