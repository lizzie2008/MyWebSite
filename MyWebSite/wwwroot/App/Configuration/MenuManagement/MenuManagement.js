app.controller('MenuManagementController', function ($scope) {
    //获取API请求列表
    $.ajax({
        type: 'GET',
        url: '/Configuration/MenuManagement/GetMenus',
        success: function (data) {
            //$scope.navMenus = data.navMenus;
            //$scope.$apply();

            $('.dd').append(data);
            $('.dd').nestable().on('change', function(e) {
                //var list = e.length ? e : $(e.target);
                //var json = list.nestable('serialize');
            });
            $('.dd').nestable('collapseAll');
        },
        error: function (xhr) {
            if (xhr.status === 401)
                window.location.href = "/Account/Login?ReturnUrl="
                    + encodeURIComponent(window.location.href);
        }
    });
});