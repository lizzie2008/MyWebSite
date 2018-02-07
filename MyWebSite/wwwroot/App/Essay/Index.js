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
    //自动保存开始
    this.autoSaveBeg = function (options) {
        var intervalTime = options.intervalTime;
        var editor = options.editor;
        //定时保存任务
        essayInterval = $interval(function () {
            //执行保存
            $.cookie('essayTmp', editor.getData());
            intervalTime = options.intervalTime;
            $('#autoSaveInfo').html('数据已于' + new moment().format('HH:mm:ss') + '保存');
        }, intervalTime);

        //恢复数据
        $('#recoveryTmpEssay').on('click', function () {
            var essayTmp = $.cookie('essayTmp')
            if (essayTmp != null && essayTmp != 'null')
                editor.setData(essayTmp);
        });
    };
    //自动保存停止
    this.autoSaveEnd = function () {
        $interval.cancel(essayInterval);
    }
}]);

