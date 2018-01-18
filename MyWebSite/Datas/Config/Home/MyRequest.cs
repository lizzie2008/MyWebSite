using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Datas.Config.Home
{
    #region 用户请求格式
    public class MyRequest
    {
        public IList<ApiRequest> ApiRequests { set; get; }
    }
    /// <summary>
    /// API调用请求
    /// </summary>
    public class ApiRequest
    {
        /// <summary>
        /// API编号
        /// </summary>
        public string ApiCode { set; get; }
        /// <summary>
        /// API名称
        /// </summary>
        [Required(ErrorMessage = "请选择测试的API")]
        public string ApiName { set; get; }
        /// <summary>
        /// 请求地址
        /// </summary>
        [Required(ErrorMessage = "请输入请求地址")]
        public string Url { set; get; }
        /// <summary>
        /// 请求方式
        /// </summary>
        [Required(ErrorMessage = "请选择请求方式")]
        public string Methord { set; get; }
        /// <summary>
        /// Get请求参数
        /// </summary>
        public IList<ApiPara> ApiParas { set; get; }
        /// <summary>
        /// Post请求数据
        /// </summary>
        public string ApiDatas { set; get; }
    }
    public class ApiPara
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { set; get; }
        /// <summary>
        /// 参数类型 int string bool
        /// </summary>
        public string ParaType { set; get; }
        /// <summary>
        /// 是否必填
        /// </summary>
        public bool Required { set; get; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Description { set; get; }
        /// <summary>
        /// 参数值
        /// </summary>
        public string ParaValue { set; get; }
    }
    #endregion

    /// <summary>
    /// 私密信息
    /// </summary>
    public class PrivateInfo
    {
        public Dictionary<string,string> InfoDic { set; get; }
    }
}
