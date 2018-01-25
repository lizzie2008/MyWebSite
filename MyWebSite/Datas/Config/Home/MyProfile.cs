using System.Collections.Generic;

namespace MyWebSite.Datas.Config.Home
{
    /// <summary>
    /// 个人资料
    /// </summary>
    public class MyProfile
    {
        public IEnumerable<Project> Projects { set; get; }
    }
    
    /// <summary>
    /// 项目经历
    /// </summary>
    public class Project
    {
        /// <summary>
        /// 持续至日期
        /// </summary>
        public string LastToDate { set; get; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 持续时间
        /// </summary>
        public int Lasting { set; get; }
        /// <summary>
        /// 所在公司
        /// </summary>
        public string Company { set; get; }
        /// <summary>
        /// 项目职务
        /// </summary>
        public string MyTitle { set; get; }
        /// <summary>
        /// 项目职责
        /// </summary>
        public string MyDuty { set; get; }
        /// <summary>
        /// 项目技术
        /// </summary>
        public string Technology { set; get; }
        /// <summary>
        /// 项目规格
        /// </summary>
        public int NumOfPeople { set; get; }
        /// <summary>
        /// 项目描述
        /// </summary>
        public IList<string> ProjectDescs { set; get; }
        /// <summary>
        /// 项目图片
        /// </summary>
        public IList<string> ProjectImgs { set; get; }
    }

}
