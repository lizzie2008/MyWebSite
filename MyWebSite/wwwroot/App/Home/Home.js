app.controller('HomeController', ['$scope', function ($scope) {
    //获取主页信息
    $.ajax({
        type: 'GET',
        url: '/Home/GetMyProfile',
        success: function (data) {
            //项目倒序排列
            $scope.projects = data.projects.reverse();
            $scope.$apply();
            ////延迟加载图片
            $("img.lazyload").lazyload().click(function () {
                var file = $(this).attr('data-original');
                $.dialog({
                    icon: 'fa fa-photo',
                    title: '预览',
                    type: 'dark',
                    content: "<image src='" + file + "' class='img-responsive img-rounded'/>",
                    columnClass: 'col-md-10 col-md-offset-1',
                    backgroundDismiss: true
                });
            });
        }
    });
}]);

