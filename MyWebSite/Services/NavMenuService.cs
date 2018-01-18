//using MyWebSite.Areas.Configuration.ViewModels;
//using MyWebSite.Datas;
//using MyWebSite.Services.Interfaces;
//using System.Collections.Generic;
//using System.Linq;
//using Microsoft.EntityFrameworkCore;
//using MyWebSite.Areas.Configuration.Models;
//using MyWebSite.Datas.Config.Home;

//namespace MyWebSite.Services
//{
//    /// <summary>
//    /// 菜单服务
//    /// </summary>
//    public class NavMenuService : INavMenuService
//    {
//        private readonly ApplicationDbContext _context;
//        public NavMenuService(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        private static IList<NavMenu> NavMenus { get; set; }

//        /// <summary>
//        /// 获取导航菜单
//        /// </summary>
//        /// <returns></returns>
//        public IList<NavMenu> GetNavMenus()
//        {
//            if (NavMenus == null)
//                InitOrUpdate();

//            return NavMenus;
//        }
//        /// <summary>
//        /// 生成导航菜单
//        /// </summary>
//        /// <returns></returns>
//        public void InitOrUpdate()
//        {
//            var navMenus = new List<NavMenu>();

//            var rootMenus = _context.Menus
//                .Where(s => string.IsNullOrEmpty(s.ParentId))
//                .AsNoTracking()
//                .OrderBy(s => s.IndexCode)
//                .ToList();

//            foreach (var rootMenu in rootMenus)
//            {
//                navMenus.Add(GetOneNavMenu(rootMenu));
//            }

//            NavMenus = navMenus;
//        }
//        /// <summary>
//        /// 根据给定的Menu，生成对应的导航菜单
//        /// </summary>
//        /// <param name="menu"></param>
//        /// <returns></returns>
//        public NavMenu GetOneNavMenu(Menu menu)
//        {
//            //构建菜单项
//            var navMenu = new NavMenu
//            {
//                TemplateUrl = menu.Id,
//                Name = menu.Name,
//                MenuType = menu.MenuType.Value,
//                Url = menu.Url,
//                Icon = menu.Icon
//            };

//            //构建子菜单
//            var subMenus = _context.Menus
//                .Where(s => s.ParentId == menu.Id)
//                .AsNoTracking()
//                .OrderBy(s => s.IndexCode)
//                .ToList();

//            foreach (var subMenu in subMenus)
//            {
//                navMenu.SubNavMenus.Add(GetOneNavMenu(subMenu));
//            }

//            return navMenu;
//        }
//    }
//}
