app.controller('EssayEditController', ['$scope', '$state', '$stateParams', 'EssayService', function ($scope, $state, $stateParams, EssayService) {

    var editor = {};
    //获取随笔数据
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Details/' + $stateParams.id,
        success: function (data) {
            $scope.essay = data;
            $scope.$apply();
            editor = CKEDITOR.replace('editorEssay');
            editor.setData(data.content);

            //调用自动定时保存，并在页面离开自动关闭
            EssayService.autoSaveBeg({ intervalTime: 20000, editor: editor });
            $scope.$on('$destroy', function () {
                EssayService.autoSaveEnd();
            });
        }
    });

    //提交保存
    $scope.onSubmit = function () {
        //判读未输入内容
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
        //调用保存接口
        $scope.submitting = true;
        $.ajax({
            type: 'POST',
            url: '/Essay/Essay/Edit',
            data: {
                Id: $stateParams.id,
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