app.controller('MenuManagementController', function ($scope) {
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
        BootstrapDialog.show({
            message: '确认删除菜单？',
            size: BootstrapDialog.SIZE_SMALL,
            draggable: true,
            buttons: [
                {
                    icon: 'fa fa-check',
                    label: '确定',
                    cssClass: 'btn-primary',
                    action: function (dialogRef) {
                        dialogRef.close();
                        $($event.target).closest('li').remove();
                    }
                }, {
                    icon: 'fa fa-close',
                    label: '取消',
                    action: function (dialogRef) {
                        dialogRef.close();
                    }
                }
            ]
        });
    };

    //保存菜单
    $scope.save = function (menus) {
        $scope.saveSubmitting = true;
        $.ajax({
            type: 'POST',
            url: '/Configuration/MenuManagement/Save',
            data: {},
            success: function (data) {
               //TODO:
            },
            complete: function () {
                $scope.saveSubmitting = false;
                $scope.$apply();
            }
        });
    }
    //重置菜单
    $scope.reset = function () {
        $scope.resetSubmitting = true;
        $.ajax({
            type: 'GET',
            url: '/Configuration/MenuManagement/GetMenus',
            success: function (data) {
                $scope.navMenus = data;
                $scope.$apply();
                setTimeout(function () {
                    $('.dd').nestable();
                }, 100);
            },
            complete: function () {
                $scope.resetSubmitting = false;
                $scope.$apply();
            }
        });
    }

    $scope.selectedMenu = {};
    $scope.reset();
});
