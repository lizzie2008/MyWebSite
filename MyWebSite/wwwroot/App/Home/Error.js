app.controller('ErrorController', ['$scope', function ($scope) {
    $scope.code = window.location.hash.split('?')[1].split('=')[1];
}]);