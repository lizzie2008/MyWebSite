/// <reference path="../../app/configuration/menu/detail.cshtml" />
//百度流量统计
var _hmt = _hmt || [];
var hm = document.createElement("script");
hm.src = "https://hm.baidu.com/hm.js?ba97b9ac7e6eb8c1d4e5e331e329edd0";
var s = document.getElementsByTagName("script")[0];
s.parentNode.insertBefore(hm, s);

//ajax请求Pace效果
$(document).ajaxStart(function () {
    Pace.restart();
})

//选择语言
$("input[name='languageOpts']").change(function () {
    switch ($(this).data('culture')) {
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

//Angular相关配置
var app = angular.module('app', ['ui.router', 'angular-ladda']);
//全局配置
app.run(function ($transitions) {
    $transitions.onStart({}, function (trans) {
        //路由变化
        jQuery.event.trigger("ajaxStart");
    });
})
//路由配置
app.config(function ($stateProvider, $urlRouterProvider) {



    $urlRouterProvider.otherwise('/');

    $stateProvider.state('Home', {
        //主页
        url: '/',
        templateUrl: 'App/Home/Home.html',
    }).state('MenuInfo', {
        //菜单信息
        url: '/Configuration/Menus',
        templateUrl: 'App/Configuration/Menu/Index.html',
    }).state('ApiSimulator', {
        //API模拟
        url: '/Tools/ApiSimulator',
        templateUrl: 'App/Tools/ApiSimulator/ApiSimulator.html',
    });
});
