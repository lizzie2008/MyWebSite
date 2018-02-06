app.controller('EssayCreateController', ['$scope', '$state', 'EssayService', function ($scope, $state, EssayService) {
    //构造富文本编辑器
    var editor = CKEDITOR.replace('editorEssay');

    //调用自动保存
    EssayService.autoSaveBeg();

    $scope.$on('$destroy', function () {
        // Make sure that the interval is destroyed too
        EssayService.autoSaveEnd();
    });

    //保存
    $scope.onSubmit = function () {
        //获取内容，并调用保存接口
        var content = editor.getData();
        if (content == "") {
            $.alert({
                icon: 'fa fa-warning',
                title: '警告',
                type: 'orange',
                closeIcon: true,
                content: '尚未输入内容.',
                buttons: {
                    close: {
                        text: '确定',
                        btnClass: 'btn-warning'
                    }
                }
            });
            return false;
        }
        $scope.submitting = true;
        $.ajax({
            type: 'POST',
            url: '/Essay/Essay/Create',
            data: {
                Title: $scope.essay.title,
                Content: content
            },
            success: function (data) {
                $scope.submitting = false;
                //保存成功跳转列表页
                $state.go('EssayDetails', { id: data });
            }
        });
    }
}]);