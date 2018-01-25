using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MyWebSite.Controllers.Abstract;
using MyWebSite.Core;
using MyWebSite.Datas.Config.Home;

namespace MyWebSite.Areas.Configuration.Controllers
{
    [Area("Configuration")]
    public class MenuManagementController : AppController
    {
        private readonly NavBarMenus _navBarMenus;

        public MenuManagementController(IOptions<NavBarMenus> navBarMenus)
        {
            _navBarMenus = navBarMenus.Value;
        }

        /// <summary>
        /// 获取菜单
        /// </summary>
        /// <returns></returns>
        public IActionResult GetMenus()
        {
            return new JsonResult(_navBarMenus.NavMenus);
        }

        [ApiAuthorize]
        public IActionResult Save()
        {
            return new StatusCodeResult(401);
        }
    }
}
