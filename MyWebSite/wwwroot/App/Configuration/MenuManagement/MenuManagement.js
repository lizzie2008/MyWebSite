app.controller('MenuManagementController', function ($scope) {
    //获取API请求列表
    $.ajax({
        type: 'GET',
        url: '/Configuration/MenuManagement/GetMenus',
        success: function (data) {
            $scope.navMenus = data.navMenus;
            $scope.$apply();

            $('#nestable2').nestable();
            $('.dd').nestable('collapseAll');
        },
        error: function (xhr) {
            if (xhr.status === 401)
                window.location.href = "/Account/Login?ReturnUrl="
                    + encodeURIComponent(window.location.href);
        }
    });
});