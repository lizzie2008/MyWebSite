app.controller('EssayCreateController', ['$scope', '$state', function ($scope, $state) {
    //构造富文本编辑器
    var editor = CKEDITOR.replace('editorEssay');

    $('.select2').select2({
        language: 'zh-CN',
        allowClear: true,
    })

    //调用创建接口
    $.ajax({
        type: 'GET',
        url: '/Essay/Essay/Create',
        success: function (data) {
            $scope.essayCatalogs = data.essayCatalogs;
            $scope.$apply();
        }
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
        var selectdatas=$('.select2').val();
        //$scope.submitting = true;
        //$.ajax({
        //    type: 'POST',
        //    url: '/Essay/Essay/Create',
        //    data: {
        //        Title: $scope.essay.title,
        //        Content: content
        //    },
        //    success: function (data) {
        //        $scope.submitting = false;
        //        //保存成功跳转列表页
        //        $state.go('EssayDetails', { id: data });
        //    }
        //});
    }
}]);