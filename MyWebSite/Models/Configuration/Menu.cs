using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 菜单级别
        /// </summary>
        [Required]
        public int Level { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单组内排序
        /// </summary>
        [Required]
        public int IndexCode { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        [StringLength(256)]
        public string Url { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        [Required]
        public MenuTypes MenuType { get; set; }

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

    public enum MenuTypes
    {
        /// <summary>
        /// 导航菜单
        /// </summary>
        导航菜单,
        /// <summary>
        /// 操作菜单
        /// </summary>
        操作菜单
    }
}
