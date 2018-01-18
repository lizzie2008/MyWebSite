using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config;
using MyWebSite.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using MyWebSite.Datas.Config.Home;

namespace MyWebSite.Areas.Tools.Controllers
{
    [Area("Tools")]
    public class SiteAnalyticsController : AppController
    {
        private readonly IList<ApiRequest> _requests;
        private readonly PrivateInfo _privateInfo;

        public SiteAnalyticsController(IOptions<MyRequest> myRequest, IOptions<PrivateInfo> privateInfo)
        {
            _requests = myRequest.Value.ApiRequests;
            _privateInfo = privateInfo.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取百度访客区域统计数据
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetVisitDistrictAnalytics(string startDate, string endDate)
        {
            var hc = new HttpClient();
            var request = _requests.FirstOrDefault(s => s.ApiCode == "BaiduGetVisitDistrict");
            //隐匿隐私信息
            foreach (var valuePair in _privateInfo.InfoDic)
            {
                request.ApiDatas = request.ApiDatas.Replace($"<{valuePair.Key}>", valuePair.Value);
            }
            var data = request.ApiDatas.Replace("30daysago", startDate).Replace("today", endDate);
            return Json(await hc.HttpPostAsync(request.Url, data));
        }

        /// <summary>
        /// 获取百度趋势分析数据
        /// </summary>
        /// <returns></returns>
        public async Task<JsonResult> GetTrendAnalytics(string startDate, string endDate)
        {
            var hc = new HttpClient();
            var request = _requests.FirstOrDefault(s => s.ApiCode == "BaiduGetTrend");
            //隐匿隐私信息
            foreach (var valuePair in _privateInfo.InfoDic)
            {
                request.ApiDatas = request.ApiDatas.Replace($"<{valuePair.Key}>", valuePair.Value);
            }
            var data = request.ApiDatas.Replace("30daysago", startDate).Replace("today", endDate);
            return Json(await hc.HttpPostAsync(request.Url, data));
        }
    }
}