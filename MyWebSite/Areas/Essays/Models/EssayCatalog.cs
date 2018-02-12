using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebSite.Areas.Essays.Models
{
    /// <summary>
    /// 随笔分类
    /// </summary>
    public class EssayCatalog
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EssayCatalogID { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 一条对应多条随笔
        /// </summary>
        public ICollection<Essay> Essays { get; set; }
    }
}
