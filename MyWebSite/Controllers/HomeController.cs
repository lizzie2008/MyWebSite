using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config;
using MyWebSite.Models;
using MyWebSite.ViewModels;
using System.Diagnostics;

namespace MyWebSite.Controllers
{
    public class HomeController : AppController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<MyProfile> _myProfile;

        public HomeController(SignInManager<ApplicationUser> signInManager, IOptions<MyProfile> myProfile)
        {
            _signInManager = signInManager;
            _myProfile = myProfile;
        }

        /// <summary>
        /// 欢迎页
        /// </summary>
        /// <returns></returns>
        public IActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View("Index", _myProfile.Value);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        /// <summary>
        /// 错误显示页面
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        [Route("Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {
            return View(new ErrorViewModel
            {
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
