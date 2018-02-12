using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyWebSite.Areas.Essays.Models
{
    /// <summary>
    /// 随笔
    /// </summary>
    public class Essay
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string EssayID { get; set; }

        /// <summary>
        /// 随笔标题
        /// </summary>
        [Required]
        public string Title { get; set; }

        /// <summary>
        /// 摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 随笔内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 发表时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 随笔分类
        /// </summary>
        public int? EssayCatalogID { get; set; }
        public EssayCatalog EssayCatalog { get; set; }

        /// <summary>
        /// 随笔归档
        /// </summary>
        public int? EssayArchiveID { get; set; }
        public EssayArchive EssayArchive { get; set; }

        /// <summary>
        /// 随笔标签
        /// </summary>
        public ICollection<EssayTagAssignment> EssayTagAssignments { get; set; }
    }
}