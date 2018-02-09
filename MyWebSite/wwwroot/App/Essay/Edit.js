app.controller('EssayEditController', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {

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