var myapp = angular.module('myapp', ['ui.router', 'highcharts-ng', 'ui.bootstrap','xeditable', 'ngAnimate','EventBus']);

myapp.run(function (editableOptions) {
    editableOptions.theme = 'bs3'; // bootstrap3 theme. Can be also 'bs2', 'default'
});

myapp.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/00_home');

    $stateProvider
        // [00 HOME] STATES =============
        .state('00_home', {
            url: '/00_home',
            templateUrl: 'Partials/00_home.html'
        })

        // [01 BASIC] PAGE AND MULTIPLE NAMED VIEWS ======
        .state('01_basicchart', {
            url: '/01_basicchart',
            views: {
                // the main template will be placed here (relatively named)
                '': { templateUrl: 'Partials/01_basicchart-a-main.html' },

                // Basic Line chart
                '01_basicLine@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicLineCtrl'
                },

                // Basic Area chart
                '01_basicArea@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicAreaCtrl'
                },

                // Basic Bar chart
                '01_basicBar@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicBarCtrl'
                },

                // Basic Column chart 
                '01_basicColumn@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicColumnCtrl'
                },

                // Basic Pie chart
                '01_basicPie@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicPieCtrl'
                },

                // Basic Donut chart
                '01_basicDonut@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BasicDonutCtrl'
                },

                // Scatter Plot chart
                '01_scatterPlot@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_ScatterPlotCtrl'
                },

                // Bubble chart
                '01_bubbleChart@01_basicchart': {
                    templateUrl: 'Partials/01_basicchart-c-chart.html',
                    controller: '01_myapp_BubbleChartCtrl'
                }
            }
        })

        // [02 INTERACT] PAGE ======
        .state('02_chartinteract', {
            url: '/02_chartinteract',
            templateUrl: 'Partials/02_chartinteraction.html',
            controller: '02_myapp_ChartInteractionCtrl'
        })


        // [03 INTEGRATION] PAGE AND NESTED CHILD VIEWS ======
        .state('03_chartintegration', {
            url: '/03_chartintegration',
            templateUrl: 'Partials/03_chartintegrate-a-main.html',
            controller: function ($scope) {
                $scope.dogs = ['Bernese', 'Husky', 'Goldendoodle'];
            }
        })

        // [03 INTEGRATION.DATATABLE] NESTED VIEW
        .state('03_chartintegration.03_datatable', {
            url: '/03_datatable',
            templateUrl: 'Partials/03_chartintegrate-c-datatable.html',
            controller: '03_myapp_integrateDataTableCtrl'
        })

        // [03 INTEGRATION.LINECHART] NESTED VIEW
        .state('03_chartintegration.03_linechart', {
            url: '/03_linechart',
            templateUrl: 'Partials/03_chartintegrate-c-chart.html',
            controller: '03_myapp_integrateLineCtrl'
        })

        // [03 INTEGRATION.DUALAXES] NESTED VIEW
        .state('03_chartintegration.03_dualaxes', {
            url: '/03_dualaxes',
            templateUrl: 'Partials/03_chartintegrate-c-chart.html',
            controller: '03_myapp_integrateDualAxesCtrl'
        })

        // [03 INTEGRATION.MULTIAXES] NESTED VIEW
        .state('03_chartintegration.03_multiaxes', {
            url: '/03_multiaxes',
            templateUrl: 'Partials/03_chartintegrate-c-chart.html',
            controller: '03_myapp_integrateMultiAxesCtrl'
        })

        // [03 INTEGRATION.GAUGE] NESTED VIEW
        .state('03_chartintegration.03_gauage', {
            url: '/03_gauage',
            templateUrl: 'Partials/03_chartintegrate-c-gauage.html',
            controller: '03_myapp_integrateGauageCtrl'
        })
});