个人网站
===========================

#### 环境依赖
Linux/Windows/Mac
dotnet-sdk-2.0.0
MySQL5.7

#### 调试部署
调试说明：appsettings.json文件请自行添加，格式如下：
```
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=<Database IP>;Initial Catalog=MyWebSite;
    Persist Security Info=True;User ID=<UserID>;Password=<Password>"
  },
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "PrivateInfo": {
    "InfoDic": {
      //电话归属地私密信息
      "TokenMobileApi": "<聚合数据Token>",
      //百度私密信息
      "TokenBaiduApi": "<百度统计Token>",
      "UserNameBaiduApi": "<百度统计用户名>",
      "PasswordBaiduApi": "<百度统计密码>"
    }
  }
}
```
具体可以参考我的系列文章：
[.NET Core 搭建个人网站 | (7) Linux系统移植](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite07/)

#### 目录结构描述

├── wwwroot                     // 网站根目录
│   ├── App                     // 应用逻辑
│   ├── css                     // 网站样式
│   ├── dist                    // 打包部署
│   ├── images                  // 图片
│   ├── js                      // JavaScript
│   └── lib                     // 第三方库
├── Areas                       // Areas
├── Controllers                 // Controllers
├── Core                        // 通用代码逻辑
├── Datas                       // 数据配置
├── Extensions                  // 扩展逻辑
├── Migrations                  // EF数据迁移
├── Models                      // Models
├── Resources                   // 资源文件
├── Services                    // 服务类
├── ViewModels                  // ViewModels
├── Views                       // Views
│   └── Shared                  // 公共视图
├── appsettings.json            // 应用程序配置
├── bundleconfig.json           // 打包配置
├── Startup.cs                  // 启动入口
└── Readme.md                   // help

#### V1.0.0 版本内容更新
1. 后台管理
	- 用户管理
	- 角色管理
	- 菜单管理


