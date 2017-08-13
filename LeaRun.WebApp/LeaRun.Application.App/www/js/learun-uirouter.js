angular.module('starter.uirouter', [])
.config(function ($stateProvider, $urlRouterProvider) {
    // Ionic uses AngularUI Router which uses the concept of states
    // Learn more here: https://github.com/angular-ui/ui-router
    // Set up the various states which the app can be in.
    // Each state's controller can be found in controllers.js
    $stateProvider
    // setup an abstract state for the tabs directive
    .state('tab', {
        url: '/tab',
        abstract: true,
        templateUrl: 'templates/tabs.html',
        controller: 'lrTabsCtrl'
    })
    // Each tab has its own nav history stack:
    // 主页
    .state('tab.home', {
        url: '/home',
        views: {
            'tab-home': {
                templateUrl: 'templates/tab-home.html',
                controller: 'HomeCtrl'
            }
        }
    })
    // 实例
    .state('tab.cases', {
        url: '/cases',
        views: {
            'tab-cases': {
                templateUrl: 'templates/tab-cases.html',
                controller: 'CasesCtrl'
            }
        }
    })
    // 通知(已读未读)
    .state('tab.notice', {
        url: '/notice',
        views: {
            'tab-notice': {
                templateUrl: 'templates/tab-notice.html',
                controller: 'NoticeCtrl'
            }
        }
    })
    //公告
    .state('tab.announce', {
        url: '/home/:announceId',
        views: {
            'tab-announce': {
                templateUrl: 'templates/tab-home.html',
                controller: 'AnnounceCtrl'
            }
        }
    })
    /*.state('home.wechat', {
        url: '/message',
        views: {
            'home-wechat': {
              templateUrl: 'templates/homeApps/home-wechat.html',
              controller: "lrWeChatCtrl"
            }
        }
    })*/
    /*.state('messageDetail', {
      url: '/messageDetail/:messageId',
      templateUrl: "templates/homeApps/wechat/wechatDetails.html",
      controller: "messageDetailCtrl"
    })*/
    /*.state('home.friends', {
        url: '/friends',
        views: {
            'home-friends': {
              templateUrl: 'templates/homeApps/home-friends.html',
              controller: "lrFriendsCtrl"
            }
        }
    })*/
      .state('tab.message', {
          url: '/message',
          views: {
              'tab-message': {
                  templateUrl: 'templates/homeApps/home-wechat.html',
                  controller: "messageCtrl"
              }
          }
      })
      .state('tab.friends', {
          url: '/friends',
          views: {
              'tab-friends': {
                  templateUrl: 'templates/homeApps/home-friends.html',
                  controller: "friendsCtrl"
              }
          }
      })
      .state('messageDetail', {
          url: '/messageDetail/:messageId',
          templateUrl: "templates/homeApps/wechat/wechatDetails.html"
      })
    // 我的
    .state('tab.personCenter', {
        url: '/personCenter',
        views: {
            'tab-personCenter': {
                templateUrl: 'templates/tab-personCenter.html',
                controller: 'PersonCenterCtrl'
            }
        }
    })
    //登录页
    .state('login', {
        url: '/login',
        templateUrl: 'templates/login.html',
        controller:'LoginCtrl'
    });
    // if none of the above states are matched, use this as the fallback
    $urlRouterProvider.otherwise('/login');
});



