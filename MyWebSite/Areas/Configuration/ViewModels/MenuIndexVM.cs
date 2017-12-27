using MyWebSite.Areas.Configuration.Models;
using System.Collections.Generic;

namespace MyWebSite.Areas.Configuration.ViewModels
{
    /// <summary>
    /// 菜单列表页VM
    /// </summary>
    public class MenuIndexVM
    {
        public MenuIndexQuery Query { get; set; }
        /// <summary>
        /// 菜单明细
        /// </summary>
        public IList<Menu> Menus = new List<Menu>();
    }

    public class MenuIndexQuery
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string QName { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public string QId { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string QParentId { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        public MenuTypes? QMenuType { get; set; }
    }
}
