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
}])
.service('EssayService', ['$interval', function ($interval) {
    var intvalAutoSave = 30;
    this.autoSaveBeg = function () {
        $('#intervalInfo').html(intvalAutoSave + '秒后保存');
        var count = intvalAutoSave / 10;
        essayInterval = $interval(function () {
            $('#intervalInfo').html(--count * 10 + '秒后保存');
        }, 10000);
    };
    this.autoSaveEnd = function () {
        $interval.cancel(essayInterval);
    }
}]);

