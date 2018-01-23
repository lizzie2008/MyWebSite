//百度流量统计
var _hmt = _hmt || [];
var hm = document.createElement("script");
hm.src = "https://hm.baidu.com/hm.js?ba97b9ac7e6eb8c1d4e5e331e329edd0";
var s = document.getElementsByTagName("script")[0];
s.parentNode.insertBefore(hm, s);

// 设置jQuery Ajax全局的参数  
$.ajaxSetup({
    type: "POST",
    error: function (jqXHR, textStatus, errorThrown) {
        switch (jqXHR.status) {
            case (500):
                window.location.href = "/#!/Error?code=" + jqXHR.status;
                break;
            case (401):
                window.location.href = "/Account/Login?ReturnUrl="
                    + encodeURIComponent(window.location.href);
                break;
            default:
                window.location.href = "/#!/Error?code=" + jqXHR.status;
        }
    }
});

//ajax请求Pace效果
$(document).ajaxStart(function () {
    window.Pace.restart();
});

//选择语言
$("input[name='languageOpts']").change(function () {
    switch ($(this).data('culture')) {
        case 'zh': //中文
            $.cookie('.AspNetCore.Culture', 'c=zh-CN|uic=zh-CN', { path: "/" });
            window.location.reload();
            break;
        case 'en': //英文
            $.cookie('.AspNetCore.Culture', 'c=en-US|uic=en-US', { path: "/" });
            window.location.reload();
            break;
        default:
            console.log("语言选择有误！");
    }
});

//Angular相关配置
var app = angular.module('app', ['ui.router', 'angular-ladda']);
//路由配置
app.config(function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');
    $stateProvider.state('Home', {
        //主页
        url: '/',
        templateUrl: 'App/Home/Home.html'
    }).state('MenuManagement', {
        //菜单管理
        url: '/Configuration/MenuManagement',
        templateUrl: 'App/Configuration/MenuManagement/MenuManagement.html'
    }).state('ApiSimulator', {
        //API模拟
        url: '/Tools/ApiSimulator',
        templateUrl: 'App/Tools/ApiSimulator/ApiSimulator.html'
    }).state('SiteAnalytics', {
        //网站分析
        url: '/Tools/SiteAnalytics',
        templateUrl: 'App/Tools/SiteAnalytics/SiteAnalytics.html'
    }).state('Error', {
        url: '/Error',
        templateUrl: 'App/Home/Error.html'
    });
});


