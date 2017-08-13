var app = angular.module('starter', ['ionic', 'starter.uirouter','starter.controllers'])
.run(['$ionicPlatform', '$rootScope', '$state',
function ($ionicPlatform, $rootScope, $state) {
$ionicPlatform.ready(function() {
if (window.cordova && window.cordova.plugins && window.cordova.plugins.Keyboard){
cordova.plugins.Keyboard.hideKeyboardAccessoryBar(false);
cordova.plugins.Keyboard.disableScroll(true);
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
$ionicConfigProvider.tabs.position('bottom');
}]);

