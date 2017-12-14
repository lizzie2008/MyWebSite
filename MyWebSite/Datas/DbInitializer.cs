using MyWebSite.Datas;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Configuration.Models;
using MyWebSite.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            if (!context.ApplicationUsers.AsNoTracking().Any())
            {
                var user = new ApplicationUser { UserName = @"34878936@qq.com", Email = @"34878936@qq.com" };
                userManager.CreateAsync(user, @"@Lizzie08");
            }

            #region 菜单数据初始化

            if (!context.Menus.AsNoTracking().Any())
            {
                var menus = new Menu[]
                {
                    //后台管理
                    new Menu
                    {
                        Id = "M01_00_00",
                        Name = "后台管理",
                        ParentId = "",
                        IndexCode=1,
                        Url = "",
                        MenuType = MenuTypes.导航菜单,
                        Icon = "fa-cog",
                    },
                    //后台管理-用户管理
                    new Menu
                    {
                        Id = "M01_01_00",
                        Name = "用户管理",
                        ParentId = "M01_00_00",
                        IndexCode=1,
                        Url = "/Configuration/User/Index",
                        MenuType = MenuTypes.操作菜单,
                        Icon = "fa-user",
                    },
                    //后台管理-角色管理
                    new Menu
                    {
                        Id = "M01_02_00",
                        Name = "角色管理",
                        ParentId = "M01_00_00",
                        IndexCode=2,
                        Url = "",
                        MenuType = MenuTypes.导航菜单,
                        Icon = "fa-user-secret",
                    },
                    //后台管理-菜单管理
                    new Menu
                    {
                        Id = "M01_03_00",
                        Name = "菜单管理",
                        ParentId = "M01_00_00",
                        IndexCode=3,
                        Url = "/Configuration/Menu/Index",
                        MenuType = MenuTypes.操作菜单,
                        Icon = "fa-user-secret",
                    },
                    //后台管理-角色管理-角色信息配置
                    new Menu
                    {
                        Id = "M01_02_01",
                        Name = "角色信息配置",
                        ParentId = "M01_02_00",
                        IndexCode=1,
                        Url = "/Configuration/Role/Index",
                        MenuType = MenuTypes.操作菜单,
                        Icon = "fa-circle-o",
                    },
                    //后台管理-角色管理-角色菜单配置
                    new Menu
                    {
                        Id = "M01_02_02",
                        Name = "角色菜单配置",
                        ParentId = "M01_02_00",
                        IndexCode=2,
                        Url = "/Configuration/Role/RoleMenu",
                        MenuType = MenuTypes.操作菜单,
                        Icon = "fa-circle-o",
                    },
                 };

                foreach (var s in menus)
                {
                    s.Remarks = s.Name + "菜单";
                    context.Menus.Add(s);
                }
                context.SaveChanges();
            }
            #endregion
        }
    }
}