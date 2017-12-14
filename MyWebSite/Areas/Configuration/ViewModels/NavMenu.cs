using MyWebSite.Areas.Configuration.Models;
using System.Collections.Generic;

namespace MyWebSite.Areas.Configuration.ViewModels
{
    /// <summary>
    /// 导航菜单
    /// </summary>
    public class NavMenu
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public MenuTypes MenuType { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public bool IsOpen { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<NavMenu> SubNavMenus = new List<NavMenu>();
    }
}
