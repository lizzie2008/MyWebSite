using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Controllers.Abstract;

namespace MyWebSite.Areas.Tools.Controllers
{
    [Area("Tools")]
    public class SiteAnalyticsController : AppController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}