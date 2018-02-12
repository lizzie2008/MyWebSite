app.controller('EssayEditController', ['$scope', '$state', '$stateParams', function ($scope, $state, $stateParams) {

    var editor = {};
    //作用域销毁时，销毁富文本编辑器
    $scope.$on('$destroy', function () {
        editor.destroy(true);
    })  

    //获取随笔数据
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Edit/' + $stateParams.id,
        success: function (data) {
            $scope.vm = data;
            $scope.$apply();

            editor = CKEDITOR.replace('editorEssay');
            editor.setData(data.essay.content);
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
        //获取富文本内容
        $scope.vm.essay.content = content;
        //选中的标签
        var selectedTags = $.map($scope.vm.essayTags, function (obj) {
            if (obj.selected)
                return obj.essayTagID;
        });
        //调用保存接口
        $scope.submitting = true;
        $.ajax({
            type: 'POST',
            url: '/Essay/Essay/Edit',
            data: { essay: $scope.vm.essay, selectedTags: selectedTags },
            success: function (data) {
                $scope.submitting = false;
                //保存成功跳转列表页
                $state.go('EssayDetails', { id: data });
            },
            complete: function () {
                $scope.submitting = false;
            }
        });
    }
}]);