// define a factory for later charting demo
angular.module('myapp')
.factory('ChartDataFactory', function ($http) {
    // data source URL#1: http://index.ndc.gov.tw/inQuery.aspx?lang=1&type=it03 
    // data source URL#2: http://index.ndc.gov.tw/inQuery.aspx?lang=1&type=it02
    var trainingdatas = [];

    var factory = {};
    var initialized = false;

    // Get all training datas 
    factory.getTrainingDatas = function (scope) {
        if (initialized) {
            if (scope) {
                scope.vm.chartdata.loading = false;
            }
            return trainingdatas;
        } else {
            // Get data from remote backend
            $http.get('/api/dashboard')
                .success(function (data, status, headers, config) {
                    trainingdatas = data;
                    initialized = true;
                    
                    if (scope) {
                        scope.vm.chartdatas = trainingdatas;
                        scope.vm.chartdata.loading = false;
                    }
                    return trainingdatas;
                });            
        }
    };

    // Get the last period of data
    factory.getLatesPeriodData = function () {
        if (initialized != true) {
            factory.getTrainingDatas.call();
        }

        var lastIdx = trainingdatas.length - 1;
        return trainingdatas[lastIdx];
    };

    // Get data series array base on property name
    factory.getDataSeriesByProp = function (propName) {
        if (initialized != true) {
            factory.getTrainingDatas.call();
        }
        propName.toLowerCase();
        var result = [];
        for (var idx = 0; idx < trainingdatas.length; idx++) {
            switch (propName) {
                case "period":
                    result.push(trainingdatas[idx].Period);
                    break;
                case "period_utc":
                    result.push(trainingdatas[idx].Period_UTC);
                    break;
                case "taiex":
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].TAIEX });
                    break;
                case "monitoringindex":
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].MonitoringIndex });
                    break;
                case "leadingindex":
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].LeadingIndex });
                    break;
                case "coincidentindex":
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].CoincidentIndex });
                    break;
                case "laggingindex":
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].LaggingIndex });
                    break;
            }
        }
        return result;
    };    

    // Get initialization flag
    factory.isInitialized = function () {
        return initialized;
    };

    // Reset initialization flag
    factory.resetInitFlag = function () {
        initialized = false;
    }

    //新增(Create): Create new chartdata
    factory.createTrainingChartData = function (scope, chartdata) {
        // Post data to remote backend WebApi
        var request = $http({
            method: "post",
            url: "/api/dashboard",
            data: chartdata
        });

        request.success(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'success', msg: 'New chartdata [' + data + '] created successfully!' });
            initialized = false; //reset flag
            scope.vm.action.mode = 'NoOp'; 
            scope.getAllChartdatas(); //trigger to refresh data
        });

        request.error(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'danger', msg: 'New chartdata created error!' })
        });
    }

    //修改(Update): update exsiting chartdata
    factory.updateTrainingChartData = function (scope, chartdata) {
        // Put data to remote backend WebApi
        var request = $http({
            method: "put",
            url: "/api/dashboard/"+chartdata.Id,
            data: chartdata
        });

        request.success(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'success', msg: 'Update chartdata [' + chartdata.Id + '] successfully!' });
            initialized = false; //reset flag
            scope.vm.action.mode = 'NoOp';
            scope.getAllChartdatas(); //trigger to refresh data
        });

        request.error(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'danger', msg: 'Update chartdata [' + chartdata.Id + '] error!' })
        });
    }

    //刪除(Delete): delete existing chartdata
    factory.deleteTrainingChartData = function (scope, chartdata) {
        // Delete data via remote backend WebApi
        var request = $http({
            method: "delete",
            url: "/api/dashboard/" + chartdata.Id
        });

        request.success(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'success', msg: 'Delete chartdata [' + chartdata.Id + '] successfully!' });
            initialized = false; //reset flag
            scope.vm.action.mode = 'NoOp';
            scope.getAllChartdatas(); //trigger to refresh data
        });

        request.error(function (data, status, headers, config) {
            scope.vm.alerts.push({ type: 'danger', msg: 'Delete chartdata [' + chartdata.Id + '] error!' })
        });
    }

    return factory;
});

