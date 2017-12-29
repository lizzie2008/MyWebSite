using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Extensions;
using System.Threading.Tasks;

namespace MyWebSite.Controllers
{
    public class SiteAnalyticsController : AppController
    {
        public async Task<IActionResult> Index()
        {
            var hc = new HttpClient();
            var ret = await hc.HttpGetAsync("http://apis.juhe.cn/mobile/get?phone=15011264377&dtype=&key=339e89edc137921c680a4bc39e6abf95");

            return View("Index",ret);
        }
    }
}