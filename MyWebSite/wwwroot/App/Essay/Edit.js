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

    //获取内容，并调用保存接口
    $scope.save = function () {
        $.ajax({
            type: 'POST',
            url: '/Essay/Essay/Edit',
            data: {
                Id: $stateParams.id,
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