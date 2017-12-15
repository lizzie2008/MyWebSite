using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Models;
using MyWebSite.Services.Interfaces;
using MyWebSite.ViewModels;
using System.Diagnostics;

namespace MyWebSite.Controllers
{
    public class HomeController : AppController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INavMenuService _navMenuService;


        public HomeController(SignInManager<ApplicationUser> signInManager, INavMenuService navMenuService)
        {
            _signInManager = signInManager;
            _navMenuService = navMenuService;
        }

        public IActionResult Index()
        {
            //如果已经登录，返回主页面
            if (_signInManager.IsSignedIn(User))
            {
                return View();
            }
            //如果未登录，跳转登录界面
            else
            {
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
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
