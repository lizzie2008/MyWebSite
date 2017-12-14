using MyWebSite.Areas.Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyWebSite.Areas.Configuration.ViewModels;

namespace MyWebSite.Services.Interfaces
{
    public interface INavMenuService
    {
        IList<NavMenu> GenerateNavMenus();
    }
}
