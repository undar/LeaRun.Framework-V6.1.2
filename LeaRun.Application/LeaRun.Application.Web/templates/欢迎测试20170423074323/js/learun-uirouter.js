angular.module('starter.uirouter', [])
.config(function ($stateProvider, $urlRouterProvider) {
$stateProvider
.state('tab', {
url: '/tab',
abstract: true,
templateUrl: 'templates/tabs.html',
controller: 'lrTabsCtrl'
}).state('tabc41a466e-081f-bb00-06cc-d461a7e88e9f', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tab30aeaae5-4d2b-2591-c249-d3a5b6e80ee2', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tab68cd61b9-d1da-6c3e-4799-eabe8130f296', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})
.state('tab77fbf778-d054-a2fd-7a2b-6eeff6174dca', {
url: '/',
views: {
'tab-home': {
templateUrl: 'templates/.html'
}
}
})

$urlRouterProvider.otherwise('/')

