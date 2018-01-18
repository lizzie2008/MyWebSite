using MyWebSite.Areas.Configuration.ViewModels;
using System.Collections.Generic;
using MyWebSite.Datas.Config.Home;

namespace MyWebSite.Services.Interfaces
{
    public interface INavMenuService
    {
        void InitOrUpdate();
        IList<NavMenu> GetNavMenus();
    }
}
