namespace MyWebSite.Areas.Essays.Models
{
    /// <summary>
    /// 随笔和标签映射关系
    /// </summary>
    public class EssayTagAssignment
    {
        public string EssayID { get; set; }
        public Essay Essay { get; set; }

        public int EssayTagID { get; set; }
        public EssayTag EssayTag { get; set; }
    }
}
