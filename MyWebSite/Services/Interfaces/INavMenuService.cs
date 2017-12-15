using MyWebSite.Areas.Configuration.ViewModels;
using System.Collections.Generic;

namespace MyWebSite.Services.Interfaces
{
    public interface INavMenuService
    {
        void InitOrUpdate();
        IList<NavMenu> GetNavMenus();
    }
}
