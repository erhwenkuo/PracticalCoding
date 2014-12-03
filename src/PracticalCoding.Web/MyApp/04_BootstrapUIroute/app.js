var myapp = angular.module('myapp', ['ui.router']);

myapp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/home');

    $stateProvider
        // HOME STATES AND NESTED VIEWS =============
        .state('home', {
            url: '/home',
            templateUrl: 'Partials/home.html'
        })

        // nested list with custome controller
        .state('home.list', {
            url: '/list',
            templateUrl: 'Partials/home-list.html',
            controller: function ($scope) {
                $scope.dogs = ['Bernese', 'Husky', 'Goldendoodle'];
            }
        })

        // nested list with just some random string data
        .state('home.paragraph', {
            url: '/paragraph',
            template: 'I could use a drink right now.'
        })

        // ABOUT PAGE AND MULTIPLE NAMED VIEWS ======
        .state('about', {
            url: '/about',
            views: {
                // the main template will be placed here (relatively named)
                '': { templateUrl: 'Partials/about.html' },

                // the child views will be defined here (absolutely named)
                'columnOne@about': { template: 'Look I am a column!' },

                // for column two, we'll define a separate controller 
                'columnTwo@about': {
                    templateUrl: 'Partials/table-data.html',
                    controller: 'scotchController'
                }
            }
        })
});