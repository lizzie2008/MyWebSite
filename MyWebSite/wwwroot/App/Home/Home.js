app.controller('HomeController', function ($scope,$http) {
    //获取主页信息
    //$.ajax({
    //    type: 'GET',
    //    url: '/Home/GetMyProfile',
    //    success: function (data) {
    //        //项目倒序排列
    //        $scope.projects = data.projects.reverse();
    //        $scope.$apply();

    //        ////延迟加载图片
    //        $("img.lazyload").lazyload().click(function () {
    //            var file = $(this).attr('data-src');
    //            $("#myModal").find("#img_show").html("<image src='" + file +
    //                "' class='img-responsive img-rounded' />");
    //            $("#myModal").modal();
    //        })
    //    }
    //});

    $http({
        method: 'GET',
        url: '/Home/GetMyProfile'
    }).then(function successCallback(response) {
        //项目倒序排列
        $scope.projects = response.data.projects.reverse();

        //延迟加载图片
        $("img.lazyload").lazyload().click(function () {
            var file = $(this).attr('data-src');
            $("#myModal").find("#img_show").html("<image src='" + file +
                "' class='img-responsive img-rounded' />");
            $("#myModal").modal();
        })
    });
});

