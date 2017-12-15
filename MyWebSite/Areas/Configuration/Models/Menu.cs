using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebSite.Areas.Configuration.Models
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
        [Required(ErrorMessage = "请输入菜单编号")]
        public string Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        [Required(ErrorMessage = "请输入菜单名称")]
        [StringLength(256)]
        public string Name { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        [DisplayFormat(NullDisplayText = "无")]
        public string ParentId { get; set; }

        /// <summary>
        /// 菜单组内排序
        /// </summary>
        [Range(0, 99, ErrorMessage = "请选择1-99范围内的整数")]
        public int IndexCode { get; set; }

        /// <summary>
        /// 菜单路径
        /// </summary>
        [StringLength(256)]
        [DisplayFormat(NullDisplayText = "无")]
        public string Url { get; set; }

        /// <summary>
        /// 类型：0导航菜单；1操作按钮。
        /// </summary>
        [Required(ErrorMessage = "请选择菜单类型")]
        public MenuTypes? MenuType { get; set; }

        /// <summary>
        /// 菜单图标名称
        /// </summary>
        [Required(ErrorMessage = "请输入菜单图标")]
        [StringLength(50)]
        public string Icon { get; set; }

        /// <summary>
        /// 菜单备注
        /// </summary>
        public string Remarks { get; set; }
    }
    /// <summary>
    /// 菜单类型
    /// </summary>
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
