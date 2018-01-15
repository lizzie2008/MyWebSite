using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Configuration.Models;
using MyWebSite.Areas.Configuration.ViewModels;
using MyWebSite.Datas;
using MyWebSite.Models;
using System.Collections.Generic;
using System.Linq;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            #region 菜单数据初始化
            //清空数据
            context.Database.ExecuteSqlCommand("Delete From Menus");

            var navMenuList = new List<NavMenu>();
            //主页
            navMenuList.Add(new NavMenu
            {
                Name = "Home",
                Url = "/Home/Index",
                Icon = "fa-home",
            });
            //后台管理
            navMenuList.Add(new NavMenu
            {
                Name = "Configuration",
                Url = "",
                Icon = "fa-cog",
                SubNavMenus = new List<NavMenu>
                    {
                        //用户管理
                        new NavMenu
                        {
                            Name = "UserManagement",
                            Url = "/Configuration/User/Index",
                            Icon = "fa-user"
                        },
                        //角色管理
                        new NavMenu{
                            Name = "RoleManagement",
                            Url = "",
                            Icon = "fa-user-secret",
                            SubNavMenus = new List<NavMenu>
                            {
                                //角色信息
                                new NavMenu{
                                    Name = "RoleInfo",
                                    Url = "/Configuration/Role/RoleInfo",
                                    Icon = "fa-circle-o"
                                },
                                //角色菜单
                                new NavMenu{
                                    Name = "RoleMenu",
                                    Url = "/Configuration/Role/RoleMenu",
                                    Icon = "fa-circle-o"
                                }
                            }
                        },
                        //菜单管理
                        new NavMenu{
                            Name = "MenuManagement",
                            Url = "/Configuration/Menu/Index",
                            Icon = "fa-bars"
                        }
                    }
            });
            //我的博客
            navMenuList.Add(new NavMenu
            {
                Name = "MyBlog",
                Url = "",
                Icon = "fa-firefox",
                SubNavMenus = new List<NavMenu>
                    {
                        //Unity3D手机斗地主
                        new NavMenu
                        {
                            Name = "Unity3D手机斗地主",
                            Url = "",
                            Icon = "fa-rebel",
                            SubNavMenus = new List<NavMenu>
                            {
                                //01.发牌功能实现
                                new NavMenu{
                                    Name = "01.发牌功能实现",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7761586.html",
                                    Icon = "fa-circle-o",
                                },
                                //02.叫地主功能实现
                                new NavMenu{
                                    Name = "02.叫地主功能实现",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7771713.html",
                                    Icon = "fa-circle-o",
                                },
                                //03.地主牌和出牌逻辑
                                new NavMenu{
                                    Name = "03.地主牌和出牌逻辑",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7794466.html",
                                    Icon = "fa-circle-o",
                                },
                                //04.出牌判断大小
                                new NavMenu{
                                    Name = "04.出牌判断大小",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7977040.html",
                                    Icon = "fa-circle-o",
                                }
                            }
                        },
                        //.Net Core个人网站
                        new NavMenu{
                            Name = ".Net Core个人网站",
                            Url = "",
                            Icon = "fa-sitemap",
                            SubNavMenus = new List<NavMenu>
                            {
                                //01.环境搭建
                                new NavMenu{
                                    Name = "01.环境搭建",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7943128.html",
                                    Icon = "fa-circle-o",
                                },
                                //02.一键部署和注册登录
                                new NavMenu{
                                    Name = "02.一键部署和注册登录",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/7999851.html",
                                    Icon = "fa-circle-o",
                                },
                                //03.菜单管理
                                new NavMenu{
                                    Name = "03.菜单管理",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/8136442.html",
                                    Icon = "fa-circle-o",
                                },
                                //04.主页和登录验证
                                new NavMenu{
                                    Name = "04.主页和登录验证",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/8193133.html",
                                    Icon = "fa-circle-o",
                                },
                                //05.主页和登录验证
                                new NavMenu{
                                    Name = "05.Api模拟和网站分析",
                                    Url = "http://www.cnblogs.com/lizzie-xhu/p/8258904.html",
                                    Icon = "fa-circle-o",
                                }
                            }
                        }
                    }
            });
            //工具箱
            navMenuList.Add(new NavMenu
            {
                Name = "Tools",
                Url = "",
                Icon = "fa-cogs",
                SubNavMenus = new List<NavMenu>
                {
                    //API模拟
                    new NavMenu
                    {
                        Name = "ApiSimulator",
                        Url = "/Tools/ApiSimulator/Index",
                        Icon = "fa-bug"
                    },
                    //网站分析
                    new NavMenu{
                        Name = "SiteAnalytics",
                        Url = "/Tools/SiteAnalytics/Index",
                        Icon = "fa-line-chart"
                    }
                }
            });

            for (int i = 0; i < navMenuList.Count; i++)
            {
                InsertMenu(context, navMenuList[i], "", i + 1);
            }
            context.SaveChanges();

            #endregion

            //创建管理员用户
            if (!context.ApplicationUsers.AsNoTracking().Any())
            {
                var userAdmin = new ApplicationUser { UserName = @"34878936@qq.com", Email = @"34878936@qq.com", NickName = "原子蛋" };
                userManager.CreateAsync(userAdmin, @"@Lizzie08");
            }
        }
        //插入一条菜单项到数据库
        private static void InsertMenu(ApplicationDbContext context, NavMenu navMenu, string parentId = "", int IndexCode = 1)
        {
            //添加菜单项
            var menu = new Menu
            {
                Id = string.IsNullOrEmpty(parentId) ?
                "M" + IndexCode.ToString().PadLeft(2, '0') : parentId + "_" + IndexCode.ToString().PadLeft(2, '0'),
                Name = navMenu.Name,
                ParentId = parentId,
                IndexCode = IndexCode,
                Url = navMenu.Url,
                MenuType = string.IsNullOrEmpty(navMenu.Url) ? MenuTypes.导航菜单 : MenuTypes.操作菜单,
                Icon = navMenu.Icon,
            };
            context.Menus.Add(menu);

            //添加子菜单项
            for (int i = 0; i < navMenu.SubNavMenus.Count; i++)
            {
                InsertMenu(context, navMenu.SubNavMenus[i], menu.Id, i + 1);
            }
        }
    }
}