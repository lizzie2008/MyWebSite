using System.Collections.Generic;

namespace MyWebSite.Datas.Config.Home
{
    /// <summary>
    /// 导航菜单项
    /// </summary>
    public class NavMenu
    {
        public string Name { get; set; }
        public string TemplateUrl { get; set; }
        public string Icon { get; set; }

        /// <summary>
        /// 子菜单
        /// </summary>
        public IList<NavMenu> SubNavMenus { get; set; }
    }

    /// <summary>
    /// 左侧导航菜单视图模型
    /// </summary>
    public class NavBarMenus
    {
        public IList<NavMenu> NavMenus { get; set; }
    }
}
