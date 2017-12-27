$('.main-sidebar a').click(function () {
    //记录菜单展开状态
    var href = $(this).attr('href')
    if (href === null || href === "#") return
    var menuids = [];
    $('.menu-open').each(function () {
        menuids.push($(this).attr('menuid'))
    })
    $.cookie('menuids_open', menuids.join(','), { path: "/" })
})

//对话框本地化
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_DEFAULT] = '提示';
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_INFO] = '提示';
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_PRIMARY] = '提示';
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_SUCCESS] = '成功';
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_WARNING] = '警告';
BootstrapDialog.DEFAULT_TEXTS[BootstrapDialog.TYPE_DANGER] = '错误';
BootstrapDialog.DEFAULT_TEXTS['OK'] = '确定';
BootstrapDialog.DEFAULT_TEXTS['CANCEL'] = '取消';
BootstrapDialog.DEFAULT_TEXTS['CONFIRM'] = '确定';

$(function () {
    //表格控件初始化
    var table = $('.table').DataTable({
        "lengthChange": false,
        "searching": false,
        "scrollX": true,
        //"iDisplayLength": '25',
        "language": {
            "url": "//cdn.datatables.net/plug-ins/1.10.16/i18n/Chinese.json"
        }
    })
    //勾选框样式
    $('.icheck input').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%' // optional
    })
    //下拉框样式
    $('.select2').select2({
        language: "zh-CN"
    })
})