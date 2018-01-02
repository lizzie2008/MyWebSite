//菜单图标自动刷新
$('#Icon').blur(function () {
    $("#IconfShow").attr('class', 'fa ' + $('#Icon').val())
});
//菜单路径可编辑状态
$('#MenuType').change(function (parameters) {
    //如果菜单是操作菜单，才允许编辑
    if ($(this).val() === '1') {
        $("#Url").removeAttr('readonly')
    } else {
        $("#Url").attr('readonly', 'readonly')
        $("#Url").val('')
    }
})
//下拉框样式
$('.select2').select2({
    language: "zh-CN",
})