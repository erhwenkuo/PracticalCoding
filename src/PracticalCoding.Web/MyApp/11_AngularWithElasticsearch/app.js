var myapp = angular.module('myapp', ['ui.router', 'elasticsearch', 'ngSanitize','ui.bootstrap']);

myapp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/fulltext_search');

    $stateProvider
        .state('fulltext_search', {
            url: '/fulltext_search',
            templateUrl: 'Partials/fulltext-search.html',
            controller: 'fulltextsearchCtrl'
        })
});

