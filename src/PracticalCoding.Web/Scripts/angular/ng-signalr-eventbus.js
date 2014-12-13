angular.module('EventBus', ['SignalR'])
.factory('EventBusHub', ['$rootScope', 'Hub', function ($rootScope, Hub) {
    //宣告hub的連線物件
    var hub = new Hub('eventBusHub', {
        //宣告可以讓Server來呼叫client side的methods
        listeners: {
            //'broadcastMessage': function (name, message) {
            'broadcastSignalrEvent': function (signalrEvent) {
                //檢查event是否有name的property
                var eventName = 'default';
                if (signalrEvent.EventName)
                    eventName = angular.lowercase(signalrEvent.EventName);

                //使用Angularjs的$rootScope.broadcast的功能來publish事件
                $rootScope.$broadcast(eventName, signalrEvent);
                console.log("Receive Event:" + eventName);
            }
        },
        //宣告server side hub的methods
        methods: ['BroadcastAll'],
        //handle connection error
        errorHandler: function (error) {
            console.error(error);
        }
    });

    var broadcastEventViaServer = function (signalrEvent) {
        hub.BroadcastAll(signalrEvent); //呼叫Sever的method
    };
    //回傳一個javascript物件裡面包含了一個可以用來
    //廣播Event給其它Singalr的connection的方法(Method)
    return {
        broadcastEvent: broadcastEventViaServer
    };
}]);


