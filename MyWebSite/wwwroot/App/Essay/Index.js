app.controller('EssayIndexController', ['$scope', '$stateParams', function ($scope, $stateParams) {
    //获取随笔列表
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Index',
        data: {
            pageIndex: $stateParams.pageIndex
        },
        success: function (data) {
            $scope.essayPaginatedList = data;
            $scope.$apply();
        }
    });
}]);