app.controller('ApiSimulatorController', ['$scope',function ($scope) {
    //获取API请求列表
    $.ajax({
        type: 'GET',
        url: '/Tools/ApiSimulator/GetApiRequestList',
        success: function (data) {
            //项目倒序排列
            $scope.apiList = data;
            $scope.selectedApi = null;
            $scope.$apply();
        }
    });
    //提交查询
    $scope.onSubmit = function () {
        $scope.submitting = true;
        $.ajax({
            type: 'POST',
            url: '/Tools/ApiSimulator/InvokApi',
            data: $scope.selectedApi,
            success: function (data) {
                $scope.response = data;
                $scope.$apply();
            },
            complete: function () {
                $scope.submitting = false;
                $scope.$apply();
            }
        });
    };
}]);