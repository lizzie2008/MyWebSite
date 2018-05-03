app.controller('EssayIndexController', ['$scope', '$stateParams', function ($scope, $stateParams) {

    $scope.isAuthenticated = IsAuthenticated();
    //生成背景图
    $('.geopattern').each(function () {
        $(this).geopattern($(this).data('pattern-id'));
    });

    //获取随笔列表
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Index',
        data: {
            pageIndex: $stateParams.pageIndex,
            pageSize: 5,
            essayCatalogID: $stateParams.essayCatalogID,
            essayArchiveID: $stateParams.essayArchiveID,
            essayTagID: $stateParams.essayTagID
        },
        success: function (data) {
            $scope.vm = data;
            $scope.$apply();
        }
    });
}]);

