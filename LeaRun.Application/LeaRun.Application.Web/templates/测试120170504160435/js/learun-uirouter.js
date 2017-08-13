angular.module('starter.uirouter', [])
.config(function ($stateProvider, $urlRouterProvider) {
$stateProvider
.state('tab', {
url: '/tab',
abstract: true,
templateUrl: 'templates/tabs.html',
controller: 'lrTabsCtrl'
}).state('tabf5fbc070-4707-be5d-3120-d798a5344ab5', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tabd07a0481-6bb1-2a13-7910-674592af1015', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tabe35c82f9-d7f5-80e5-2e09-8dd27976fa9a', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tab2830e182-d564-1deb-7f01-456f9b6a9f52', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})

$urlRouterProvider.otherwise('/')

