using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebSite.Areas.Essays.Models
{
    /// <summary>
    /// 随笔归档
    /// </summary>
    public class EssayArchive
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EssayArchiveID { get; set; }

        /// <summary>
        /// 归档名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 一条对应多条随笔
        /// </summary>
        public ICollection<Essay> Essays { get; set; }
    }
}
