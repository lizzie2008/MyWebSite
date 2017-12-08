using System.ComponentModel.DataAnnotations;

namespace MyWebSite.Models.Configuration
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单组内编码
        /// </summary>
        [Required]
        public int IndexCode { get; set; }

        /// <summary>
        /// 菜单Area地址
        /// </summary>
        [StringLength(256)]
        public string AreaUrl { get; set; }

        /// <summary>
        /// Controller
        /// </summary>
        [Required]
        [StringLength(256)]
        public string ControllerUrl { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        [Required]
        public int Type { get; set; }

        /// <summary>
        /// 菜单图标名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
