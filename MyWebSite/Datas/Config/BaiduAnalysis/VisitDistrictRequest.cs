namespace MyWebSite.Areas.Tools.Models.BaiduAnalysis
{
    /// <summary>
    /// 请求体
    /// </summary>
    public class VisitDistrictRequest
    {
        public Header Header { set; get; }
        public Body Body { set; get; }
    }

    public class Header
    {
        public string Username { set; get; }
        public string Password { set; get; }
        public string Token { set; get; }
        public int Account_Type { set; get; }
    }
    public class Body
    {

    }

}
