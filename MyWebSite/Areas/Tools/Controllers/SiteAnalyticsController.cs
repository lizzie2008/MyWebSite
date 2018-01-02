using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config;
using MyWebSite.Extensions;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWebSite.Areas.Tools.Controllers
{
    [Area("Tools")]
    public class SiteAnalyticsController : AppController
    {
        private readonly ApiRequest _request;

        public SiteAnalyticsController(IOptions<MyRequest> myRequest)
        {
            _request = myRequest.Value.ApiRequests.FirstOrDefault(s => s.ApiCode == "BaiduGetVisitDistrict");
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
            var data = _request.ApiDatas;
            data = data.Replace("30daysago", startDate).Replace("today", endDate);
            return Json(await hc.HttpPostAsync(_request.Url, data));
        }
    }
}