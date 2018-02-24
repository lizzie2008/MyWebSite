app.controller('MenuManagementController', ['$scope', function ($scope) {
    $scope.isAuthenticated = IsAuthenticated();

    //编辑菜单
    $scope.editMenu = function (item) {
        $scope.selectedMenu = item;
    };
    //新增菜单
    $scope.addMenu = function () {
        var newMenu = { name: "<新增菜单>", icon: "fa-circle-o", templateUrl: "" };
        $scope.navMenus.push(newMenu);
        $scope.selectedMenu = newMenu;
    };
    //删除菜单
    $scope.deleteMenu = function (item, $event) {
        $.confirm({
            icon: 'fa fa-info',
            title: '删除',
            content: '确认删除菜单[' + item.name + ']？',
            type: 'dark',
            closeIcon: true,
            typeAnimated: true,
            buttons: {
                confirm: {
                    text: '确定',
                    btnClass: 'btn-dark',
                    action: function () {
                        $($event.target).closest('li').remove();
                    }
                },
                cancel: {
                    text: '取消',
                    btnClass: 'btn-dark'
                }
            }
        });
    };
    //保存菜单
    $scope.save = function (menus) {
        $scope.submitting = true;
        $.ajax({
            type: 'POST',
            url: '/Configuration/MenuManagement/Save',
            data: {},
            success: function (data) {
                //TODO:
            },
            complete: function () {
                $scope.submitting = false;
                $scope.$apply();
            }
        });
    }
    $scope.selectedMenu = {};
    //加载菜单
    $.ajax({
        type: 'GET',
        url: '/Configuration/MenuManagement/GetMenus',
        success: function (data) {
            $scope.navMenus = data;
            $scope.$apply();
            setTimeout(function () {
                $('.dd').nestable();
            }, 100);
        }
    });
}]);
