using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config;
using MyWebSite.Models;
using MyWebSite.Services.Interfaces;
using MyWebSite.ViewModels;
using System.Diagnostics;

namespace MyWebSite.Controllers
{
    [Authorize]
    public class HomeController : AppController
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptions<MyProfile> _myProfile;


        public HomeController(SignInManager<ApplicationUser> signInManager, IOptions<MyProfile> myProfile)
        {
            _signInManager = signInManager;
            _myProfile = myProfile;
        }
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
