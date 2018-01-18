app.controller('ApiSimulatorController', function ($scope, $http) {
    //获取API请求列表
    $http({
        method: 'GET',
        url: '/Tools/ApiSimulator/GetApiRequestList'
    }).then(function successCallback(response) {
        //项目倒序排列
        $scope.apiList = response.data;
        $scope.selectedApi = null;
    }, function errorCallback(response) {
        if (response.status == 401)
            window.location.href = "/Account/Login?ReturnUrl="
                + encodeURIComponent(window.location.href);
    });
    //提交查询
    $scope.onSubmit = function () {
        $scope.submitting = true;
        $http({
            method: 'POST',
            url: '/Tools/ApiSimulator/InvokApi',
            data: $scope.selectedApi
        }).then(function successCallback(response) {
            $scope.response = response.data;
            $scope.submitting = false;
        }, function errorCallback(response) {
            $scope.submitting = false;
        });
    };
});