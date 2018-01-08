//百度流量统计
var _hmt = _hmt || [];
var hm = document.createElement("script");
hm.src = "https://hm.baidu.com/hm.js?ba97b9ac7e6eb8c1d4e5e331e329edd0";
var s = document.getElementsByTagName("script")[0];
s.parentNode.insertBefore(hm, s);

//记录菜单展开状态,防止导航菜单折叠
$('.main-sidebar a').click(function () {
    var href = $(this).attr('href');
    if (href === null || href === "#") return;
    var menuids = [];
    $('.menu-open').each(function () {
        menuids.push($(this).attr('menuid'));
    });
    $.cookie('menuids_open', menuids.join(','), { path: "/" });
});

//ajax请求Pace效果
$(document).ajaxStart(function () {
    Pace.restart();
})

//选择语言
$("input[name='languageOpts']").change(function() {
    switch($(this).data('culture')) {
        case 'zh':  //中文
            $.cookie('.AspNetCore.Culture', 'c=zh-CN|uic=zh-CN', { path: "/" });
            window.location.reload();
            break;
        case 'en':  //英文
            $.cookie('.AspNetCore.Culture', 'c=en-US|uic=en-US', { path: "/" });
            window.location.reload();
            break;
        default:
            console.log("语言选择有误！");
    }
})