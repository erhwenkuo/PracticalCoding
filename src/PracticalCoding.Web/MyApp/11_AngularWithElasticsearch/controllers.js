angular.module('myapp')
  .controller('fulltextsearchCtrl', function ($scope, esClient) {

      $scope.esIdxName = 'stackoverflow'; //定義我們要search的index
      $scope.esDocType = 'post'; //定義我要要search的docType
      $scope.queryString = ''; //用來存放search的條件

      //Elasticsearch Search Result
      $scope.timeTook = 0; //用來存放search所花的時間
      $scope.totalItems = 0;//用來存放search有符合條件的資料筆數
      $scope.currentPage = 1;//預定的分頁頁數(1為第一頁)
      $scope.maxSize = 5; //預定的分頁資料筆數
      $scope.numPages = 0; //總共的分頁頁數

      //定義search方法來讓UI呼叫
      $scope.search = function (pageChanged) {
          if (!pageChanged) {
              $scope.currentPage = 1;
          }
          //使用elasticsearch的javascript client來執行search
          esClient.search({
              index: $scope.esIdxName, //設定要search的index
              type: $scope.esDocType,  //設定要search的docType
              body: {
                  size: $scope.maxSize, //設定回傳筆數
                  from: (($scope.currentPage - 1) * $scope.maxSize), //設定offset
                  query: {
                      filtered: {
                          query: {
                              query_string: { query: $scope.queryString }
                          },
                          filter: {
                              term: { postType: 'question' }
                          }
                      }
                  }
                
              }
          })
          .then(function (results) { //根據search的結果, 來update在UI的View Model
              $scope.timeTook = results.took;
              $scope.totalItems = results.hits.total;
              $scope.hits = results.hits.hits;
              $scope.numPages = Math.ceil($scope.totalItems / $scope.maxSize);
          });
      }
      //當Pagination的UI有被使用者呼叫時的方法
      $scope.pageChanged = function () {
          var pageChanged = true;
          $scope.search(pageChanged);
      };
  });

