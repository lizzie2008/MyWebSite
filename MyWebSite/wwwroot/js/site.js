//百度流量统计
var _hmt = _hmt || [];
var hm = document.createElement("script");
hm.src = "https://hm.baidu.com/hm.js?ba97b9ac7e6eb8c1d4e5e331e329edd0";
var s = document.getElementsByTagName("script")[0];
s.parentNode.insertBefore(hm, s);

//登录跳转
$('#loginLink').click(function () {
    window.location.href = "/Account/Login?ReturnUrl="
        + encodeURIComponent(window.location.href);
})

// 设置jQuery Ajax全局的参数  
$.ajaxSetup({
    type: "POST",
    error: function (jqXhr) {
        switch (jqXhr.status) {
            case (500):
                window.location.href = "/#!/Error?code=" + jqXhr.status;
                break;
            case (401):
                window.location.href = "/Account/Login?ReturnUrl="
                    + encodeURIComponent(window.location.href);
                break;
            default:
                window.location.href = "/#!/Error?code=" + jqXhr.status;
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

var IsAuthenticated = function () {
    var isAuthenticated = null;
    $.ajax({
        type: 'GET',
        url: '/Account/IsAuthenticated',
        async: false,//同步
        success: function (data) {
            isAuthenticated = data;
        }
    });
    return isAuthenticated;
}

//Angular相关配置
var app = angular.module('app', ['ui.router', 'angular-ladda']);
//全局配置
app.run(['$rootScope', '$transitions', '$state', function ($rootScope, $transitions, $state) {
    if (window.environment == 'Production') {
        $rootScope.resourcePath = document.location.protocol + '//mysite.bj.bcebos.com/';
    } else {
        $rootScope.resourcePath = '/';
    }

    //UI权限配置
    var needAuthStateNames = ['EssayCreate', 'EssayEdit'];
    $transitions.onStart({}, function (trans) {
        var toStateName = trans.to().name;
        if ($.inArray(toStateName, needAuthStateNames) >= 0) {
            var isAuthenticated = IsAuthenticated();
            //没有权限，跳转登录界面
            if (!isAuthenticated) {
                window.location.href = "/Account/Login?ReturnUrl="
                    + encodeURIComponent(window.location.href);
                return false;
            }
        }
        $("html,body").scrollTop(0);
    })
}]);
//路由配置
app.config(['$stateProvider', '$urlRouterProvider', function ($stateProvider, $urlRouterProvider) {
    $urlRouterProvider.otherwise('/');

    $stateProvider.state('MyProfile', {
        //博主信息
        url: '/',
        templateUrl: 'App/Home/Home.html'
    }).state('EssayIndex', {
        //个人随笔
        url: '/EssayIndex',
        params: { pageIndex: null, essayCatalogID: null, essayArchiveID: null, essayTagID: null },
        templateUrl: 'App/Essay/Index.html'
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
    }).state('EssayCreate', {
        //随笔新建
        url: '/EssayCreate',
        templateUrl: 'App/Essay/Create.html'
    }).state('EssayDetails', {
        //随笔详情
        url: '/EssayDetails/:id',
        templateUrl: 'App/Essay/Details.html'
    }).state('EssayEdit', {
        //随笔编辑
        url: '/EssayEdit/:id',
        templateUrl: 'App/Essay/Edit.html'
    }).state('Error', {
        //错误页面
        url: '/Error',
        templateUrl: 'App/Home/Error.html'
    });
}]);

$(window).scroll(function () {
    var scrollTop = document.body.scrollTop || document.documentElement.scrollTop;
    //通过判断滚动条的top位置与可视网页之和与整个网页的高度是否相等来决定是否加载内容；  
    if (scrollTop > 400) {
        $('#back-to-top').show();
    } else {
        $('#back-to-top').hide();
    }
});
$('#back-to-top').click(function () {
    $("html,body").animate({ scrollTop: 0 }, 500);
});



/**
 * AdminLTE Demo Menu
 * ------------------
 * You should not use this file in production.
 * This file is for demo purposes only.
 */
$(function () {
    'use strict'

    /**
     * Get access to plugins
     */

    $('[data-toggle="control-sidebar"]').controlSidebar()
    $('[data-toggle="push-menu"]').pushMenu()

    var $pushMenu = $('[data-toggle="push-menu"]').data('lte.pushmenu')
    var $controlSidebar = $('[data-toggle="control-sidebar"]').data('lte.controlsidebar')
    var $layout = $('body').data('lte.layout')

    /**
     * List of all the available skins
     *
     * @type Array
     */
    var mySkins = [
        'skin-blue',
        'skin-black',
        'skin-red',
        'skin-yellow',
        'skin-purple',
        'skin-green',
        'skin-blue-light',
        'skin-black-light',
        'skin-red-light',
        'skin-yellow-light',
        'skin-purple-light',
        'skin-green-light'
    ]

    /**
     * Get a prestored setting
     *
     * @param String name Name of of the setting
     * @returns String The value of the setting | null
     */
    function get(name) {
        if (typeof (Storage) !== 'undefined') {
            return localStorage.getItem(name)
        } else {
            window.alert('Please use a modern browser to properly view this template!')
        }
    }

    /**
     * Store a new settings in the browser
     *
     * @param String name Name of the setting
     * @param String val Value of the setting
     * @returns void
     */
    function store(name, val) {
        if (typeof (Storage) !== 'undefined') {
            localStorage.setItem(name, val)
        } else {
            window.alert('Please use a modern browser to properly view this template!')
        }
    }

    /**
     * Toggles layout classes
     *
     * @param String cls the layout class to toggle
     * @returns void
     */
    function changeLayout(cls) {
        $('body').toggleClass(cls)
        $layout.fixSidebar()
        if ($('body').hasClass('fixed') && cls == 'fixed') {
            $pushMenu.expandOnHover()
            $layout.activate()
        }
        $controlSidebar.fix()
    }

    /**
     * Replaces the old skin with the new skin
     * @param String cls the new skin class
     * @returns Boolean false to prevent link's default action
     */
    function changeSkin(cls) {
        $.each(mySkins, function (i) {
            $('body').removeClass(mySkins[i])
        })

        $('body').addClass(cls)
        store('skin', cls)
        return false
    }

    /**
     * Retrieve default settings and apply them to the template
     *
     * @returns void
     */
    function setup() {
        var tmp = get('skin')
        if (tmp && $.inArray(tmp, mySkins))
            changeSkin(tmp)

        // Add the change skin listener
        $('[data-skin]').on('click', function (e) {
            if ($(this).hasClass('knob'))
                return
            e.preventDefault()
            changeSkin($(this).data('skin'))
        })

        // Add the layout manager
        $('[data-layout]').on('click', function () {
            changeLayout($(this).data('layout'))
        })

        $('[data-controlsidebar]').on('click', function () {
            changeLayout($(this).data('controlsidebar'))
            var slide = !$controlSidebar.options.slide

            $controlSidebar.options.slide = slide
            if (!slide)
                $('.control-sidebar').removeClass('control-sidebar-open')
        })

        $('[data-sidebarskin="toggle"]').on('click', function () {
            var $sidebar = $('.control-sidebar')
            if ($sidebar.hasClass('control-sidebar-dark')) {
                $sidebar.removeClass('control-sidebar-dark')
                $sidebar.addClass('control-sidebar-light')
            } else {
                $sidebar.removeClass('control-sidebar-light')
                $sidebar.addClass('control-sidebar-dark')
            }
        })

        $('[data-enable="expandOnHover"]').on('click', function () {
            $(this).attr('disabled', true)
            $pushMenu.expandOnHover()
            if (!$('body').hasClass('sidebar-collapse'))
                $('[data-layout="sidebar-collapse"]').click()
        })

        //  Reset options
        if ($('body').hasClass('fixed')) {
            $('[data-layout="fixed"]').attr('checked', 'checked')
        }
        if ($('body').hasClass('layout-boxed')) {
            $('[data-layout="layout-boxed"]').attr('checked', 'checked')
        }
        if ($('body').hasClass('sidebar-collapse')) {
            $('[data-layout="sidebar-collapse"]').attr('checked', 'checked')
        }

    }

    // Create the new tab
    var $tabPane = $('<div />', {
        'id': 'control-sidebar-theme-demo-options-tab',
        'class': 'tab-pane active'
    })

    // Create the tab button
    var $tabButton = $('<li />', { 'class': 'active' })
        .html('<a href=\'#control-sidebar-theme-demo-options-tab\' data-toggle=\'tab\'>'
        + '<i class="fa fa-wrench"></i>'
        + '</a>')

    // Add the tab button to the right sidebar tabs
    $('[href="#control-sidebar-home-tab"]')
        .parent()
        .before($tabButton)

    // Create the menu
    var $demoSettings = $('<div />')

    // Layout options
    //$demoSettings.append(
    //  '<h4 class="control-sidebar-heading">'
    //  + 'Layout Options'
    //  + '</h4>'
    //  // Fixed layout
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-layout="fixed"class="pull-right"/> '
    //  + 'Fixed layout'
    //  + '</label>'
    //  + '<p>Activate the fixed layout. You can\'t use fixed and boxed layouts together</p>'
    //  + '</div>'
    //  // Boxed layout
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-layout="layout-boxed" class="pull-right"/> '
    //  + 'Boxed Layout'
    //  + '</label>'
    //  + '<p>Activate the boxed layout</p>'
    //  + '</div>'
    //  // Sidebar Toggle
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-layout="sidebar-collapse"class="pull-right"/> '
    //  + 'Toggle Sidebar'
    //  + '</label>'
    //  + '<p>Toggle the left sidebar\'s state (open or collapse)</p>'
    //  + '</div>'
    //  // Sidebar mini expand on hover toggle
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-enable="expandOnHover"class="pull-right"/> '
    //  + 'Sidebar Expand on Hover'
    //  + '</label>'
    //  + '<p>Let the sidebar mini expand on hover</p>'
    //  + '</div>'
    //  // Control Sidebar Toggle
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-controlsidebar="control-sidebar-open"class="pull-right"/> '
    //  + 'Toggle Right Sidebar Slide'
    //  + '</label>'
    //  + '<p>Toggle between slide over content and push content effects</p>'
    //  + '</div>'
    //  // Control Sidebar Skin Toggle
    //  + '<div class="form-group">'
    //  + '<label class="control-sidebar-subheading">'
    //  + '<input type="checkbox"data-sidebarskin="toggle"class="pull-right"/> '
    //  + 'Toggle Right Sidebar Skin'
    //  + '</label>'
    //  + '<p>Toggle between dark and light skins for the right sidebar</p>'
    //  + '</div>'
    //)
    var $skinsList = $('<ul />', { 'class': 'list-unstyled clearfix' })

    // Dark sidebar skins
    var $skinBlue =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-blue" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px; background: #367fa9"></span><span class="bg-light-blue" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Blue</p>')
    $skinsList.append($skinBlue)
    var $skinBlack =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-black" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div style="box-shadow: 0 0 2px rgba(0,0,0,0.1)" class="clearfix"><span style="display:block; width: 20%; float: left; height: 7px; background: #fefefe"></span><span style="display:block; width: 80%; float: left; height: 7px; background: #fefefe"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Black</p>')
    $skinsList.append($skinBlack)
    var $skinPurple =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-purple" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-purple-active"></span><span class="bg-purple" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Purple</p>')
    $skinsList.append($skinPurple)
    var $skinGreen =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-green" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-green-active"></span><span class="bg-green" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Green</p>')
    $skinsList.append($skinGreen)
    var $skinRed =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-red" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-red-active"></span><span class="bg-red" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Red</p>')
    $skinsList.append($skinRed)
    var $skinYellow =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-yellow" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-yellow-active"></span><span class="bg-yellow" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #222d32"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin">Yellow</p>')
    $skinsList.append($skinYellow)

    // Light sidebar skins
    var $skinBlueLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-blue-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px; background: #367fa9"></span><span class="bg-light-blue" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Blue Light</p>')
    $skinsList.append($skinBlueLight)
    var $skinBlackLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-black-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div style="box-shadow: 0 0 2px rgba(0,0,0,0.1)" class="clearfix"><span style="display:block; width: 20%; float: left; height: 7px; background: #fefefe"></span><span style="display:block; width: 80%; float: left; height: 7px; background: #fefefe"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Black Light</p>')
    $skinsList.append($skinBlackLight)
    var $skinPurpleLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-purple-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-purple-active"></span><span class="bg-purple" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Purple Light</p>')
    $skinsList.append($skinPurpleLight)
    var $skinGreenLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-green-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-green-active"></span><span class="bg-green" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Green Light</p>')
    $skinsList.append($skinGreenLight)
    var $skinRedLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-red-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-red-active"></span><span class="bg-red" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Red Light</p>')
    $skinsList.append($skinRedLight)
    var $skinYellowLight =
        $('<li />', { style: 'float:left; width: 33.33333%; padding: 5px;' })
            .append('<a href="javascript:void(0)" data-skin="skin-yellow-light" style="display: block; box-shadow: 0 0 3px rgba(0,0,0,0.4)" class="clearfix full-opacity-hover">'
            + '<div><span style="display:block; width: 20%; float: left; height: 7px;" class="bg-yellow-active"></span><span class="bg-yellow" style="display:block; width: 80%; float: left; height: 7px;"></span></div>'
            + '<div><span style="display:block; width: 20%; float: left; height: 20px; background: #f9fafc"></span><span style="display:block; width: 80%; float: left; height: 20px; background: #f4f5f7"></span></div>'
            + '</a>'
            + '<p class="text-center no-margin" style="font-size: 12px">Yellow Light</p>')
    $skinsList.append($skinYellowLight)

    $demoSettings.append('<h4 class="control-sidebar-heading">Skins</h4>')
    $demoSettings.append($skinsList)

    $tabPane.append($demoSettings)
    $('#control-sidebar-home-tab').after($tabPane)

    setup()

    $('[data-toggle="tooltip"]').tooltip()
})
