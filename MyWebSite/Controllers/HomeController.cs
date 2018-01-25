using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Datas.Config.Home;
using MyWebSite.ViewModels;
using System.Diagnostics;
using System.Linq;

namespace MyWebSite.Controllers
{
    public class HomeController : AppController
    {
        private readonly MyProfile _myProfile;

        public HomeController(IOptions<MyProfile> myProfile)
        {
            _myProfile = myProfile.Value;
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
            return View("Index");
        }

        /// <summary>
        /// 获取个人信息
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMyProfile()
        {
//            _myProfile.Projects.ToList().ForEach(project =>
//            {
//                if (project.ProjectImgs != null)
//                {
//                    project.ProjectImgs.ToList().ForEach(img =>
//                    {
//#if DEBUG
//                        img = @"/images/profile/" + img;
//#else 
//                        img= @"http://mysite.bj.bcebos.com/images%2Fprofile%2F"+img;
//#endif
//                    });
//                }
//            });

            return new JsonResult(_myProfile);
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
