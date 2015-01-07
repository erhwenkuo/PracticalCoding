// 從esFactory產生連結Elasticsearch的esClient
angular.module('myapp')
  .service('esClient', function (esFactory) {
    return esFactory({
        hosts:[
            'http://localhost:9200',
            'http://localhost:9201'
        ],
        log: 'trace'
    });
  });

