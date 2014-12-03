// let's define the scotch controller that we call up in the about state
myapp.controller('scotchController', function ($scope) {
    $scope.title = 'Fine Scotches';

    $scope.scotches = [
        {
            name: 'Macallan 12',
            price: 50
        },
        {
            name: 'Chivas Regal Royal Salute',
            price: 10000
        },
        {
            name: 'Glenfiddich 1937',
            price: 20000
        }
    ];
});

