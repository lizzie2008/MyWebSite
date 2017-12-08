using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyWebSite.Models;
using System.Diagnostics;
using MyWebSite.Models.Configuration;
using MyWebSite.ViewModels;

namespace MyWebSite.Controllers
{
    public class HomeController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public HomeController(SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
