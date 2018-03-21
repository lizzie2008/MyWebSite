个人网站
===========================

#### 环境依赖
- Linux/Windows/Mac  
- dotnet-sdk-2.0.0  
- MySQL5.7

#### 调试部署
调试说明：appsettings.json文件请自行添加，格式如下：
```
{
  "ConnectionStrings": {
    "DefaultConnection": "server=<Database IP>;port=<Database Port>;database=MyWebSite;
    user=<UserID>;password=<Password>"
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
- [.NET Core 搭建个人网站 | (1) 环境搭建](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite01/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (2) 一键部署和用户登录](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite02/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (3) 菜单管理](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite03/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (4) 主页和登录验证](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite04/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (5) Api模拟和网站分析](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite05/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (6) 单页模式和优化](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite06/ ".NET Core 搭建个人网站系列")
- [.NET Core 搭建个人网站 | (7) Linux系统移植](https://blog.lancel0t.cn/posts/mywebsite/MyWebSite07/ ".NET Core 搭建个人网站系列")

#### 目录结构描述
```
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
```
#### V1.0.0 版本
- 后台管理
  - 用户管理
  - 角色管理
  - 菜单管理
#### V1.0.1 版本
- 工具箱 
  - API模拟
  - 网站分析
#### V1.0.2 版本
- 我的博客
- 博主信息
- 我的随笔
#### V1.0.3 版本
- 切换部署环境（CentOS）
- 切换数据库（MySQL）

