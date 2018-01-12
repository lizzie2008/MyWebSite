# MyWebSite
个人网站

调试说明：appsettings.json文件请自行添加，格式如下：
```{
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
      "TokenMobileApi": "<聚合数据>",
      //百度私密信息
      "TokenBaiduApi": "<百度统计Token>",
      "UserNameBaiduApi": "<百度统计用户名>",
      "PasswordBaiduApi": "<百度统计密码>"
    }
  }
}
