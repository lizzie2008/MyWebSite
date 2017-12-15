using System.Collections.Generic;

namespace MyWebSite.ViewModels
{
    /// <summary>
    /// 面包屑导航
    /// </summary>
    public class BreadCrumb
    {
        public BreadCrumb(string title, string subTitle, IList<NavCrumb> navCrumbs)
        {
            Title = title;
            SubTitle = subTitle;
            NavCrumbs = navCrumbs;
        }

        public string Title { get; private set; }
        public string SubTitle { get; private set; }

        public IList<NavCrumb> NavCrumbs { get; private set; }
    }

    public class NavCrumb
    {
        public NavCrumb(string name, string url="")
        {
            Name = name;
            Url = url;
        }
        public string Name { get; private set; }
        public string Url { get; private set; }
    }
}
