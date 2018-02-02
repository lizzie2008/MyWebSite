app.controller('EssayDetailsController', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {
    //获取内容，并调用保存接口
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Details/' + $stateParams.id,
        success: function (data) {
            $scope.essay = data;
            $scope.$apply();
            //渲染内容区域
            $('#essayContent').append($scope.essay.content);
            $('pre code').each(function (i, block) {
                hljs.highlightBlock(block);
            });
            $("html,body").animate({ scrollTop: 0 }, 500);
        }
    });
}]);