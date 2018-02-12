using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebSite.Areas.Essays.Models
{
    /// <summary>
    /// 随笔标签
    /// </summary>
    public class EssayTag
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EssayTagID { get; set; }

        /// <summary>
        /// 标签名称
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}
