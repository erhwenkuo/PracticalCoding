// define a factory for later charting demo
angular.module('myapp')
.factory('ChartDataFactory', function ($http) {
    // data source URL#1: http://index.ndc.gov.tw/inQuery.aspx?lang=1&type=it03 
    // data source URL#2: http://index.ndc.gov.tw/inQuery.aspx?lang=1&type=it02
    var trainingdatas = [];

    var factory = {};
    var initialized = false;

    // Get all training datas via WebApi
    factory.getTrainingDatas = function (callbackFn) {
        // Get data from remote backend WebApi
        $http.get('/api/redisdashboard')
            .success(function (data, status, headers, config) {
                trainingdatas = data;
                initialized = true;

                if (callbackFn) {
                    //呼叫callbackFn並把最新取得的資料當引數傳入
                    callbackFn(trainingdatas); 
                }
            });
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
                    result.push({ x: trainingdatas[idx].Period_UTC, y: trainingdatas[idx].Taiex });
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
    factory.createTrainingChartData = function (chartdata, successFn, errorFn) {
        // Post data to remote backend WebApi
        var request = $http({
            method: "post",
            url: "/api/redisdashboard",
            data: chartdata
        });

        request.success(function (data, status, headers, config) {
            if (successFn)
                successFn(data, status, headers, config);
        });

        request.error(function (data, status, headers, config) {
            if (errorFn)
                errorFn(data, status, headers, config);
        });
    }

    //修改(Update): update exsiting chartdata    
    factory.updateTrainingChartData = function (chartdata, successFn, errorFn) {
        // Post data to remote backend WebApi
        var request = $http({
            method: "put",
            url: "/api/redisdashboard/" + chartdata.Id,
            data: chartdata
        });

        request.success(function (data, status, headers, config) {
            if (successFn)
                successFn(data, status, headers, config);
        });

        request.error(function (data, status, headers, config) {
            if (errorFn)
                errorFn(data, status, headers, config);
        });
    }

    //刪除(Delete): delete existing chartdata
    factory.deleteTrainingChartData = function (chartdata, successFn, errorFn) {
        // Delete data via remote backend WebApi
        var request = $http({
            method: "delete",
            url: "/api/redisdashboard/" + chartdata.Id
        });

        request.success(function (data, status, headers, config) {
            if (successFn)
                successFn(data, status, headers, config);
        });

        request.error(function (data, status, headers, config) {
            if (errorFn)
                errorFn(data, status, headers, config);
        });
    }

    return factory;
});

