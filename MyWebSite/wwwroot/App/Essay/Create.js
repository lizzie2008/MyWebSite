app.controller('EssayCreateController', ['$scope', '$state', function ($scope, $state) {
    //构造富文本编辑器
    var editor = CKEDITOR.replace('editorEssay');
    $scope.save = function () {
        //获取内容，并调用保存接口
        $.ajax({
            type: 'POST',
            url: '/Essay/Essay/Create',
            data: {
                Title: $scope.essay.title,
                Content: editor.getData()
            },
            success: function (data) {
                //保存成功跳转列表页
                $state.go('EssayDetails', { id: data });
            }
        });
    }
}]);