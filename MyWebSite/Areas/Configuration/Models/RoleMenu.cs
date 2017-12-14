using MyWebSite.Models;

namespace MyWebSite.Areas.Configuration.Models
{
    /// <summary>
    /// 角色菜单权限
    /// </summary>
    public class RoleMenu
    {
        public string RoleId { get; set; }
        public ApplicationRole Role { get; set; }

        public string MenuId { get; set; }
        public Menu Menu { get; set; }
    }
}
