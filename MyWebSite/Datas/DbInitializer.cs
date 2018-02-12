using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Datas;
using MyWebSite.Models;
using System.Linq;
using MyWebSite.Areas.Essays.Models;

namespace ContosoUniversity.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            context.Database.EnsureCreated();

            //创建管理员用户
            if (!context.ApplicationUsers.AsNoTracking().Any())
            {
                var userAdmin = new ApplicationUser { UserName = @"34878936@qq.com", Email = @"34878936@qq.com", NickName = "原子蛋" };
                userManager.CreateAsync(userAdmin, @"@Lizzie08");
            }

            if (!context.EssayCatalogs.AsNoTracking().Any())
            {
                context.EssayCatalogs.Add(new EssayCatalog { Name = "01.计算机基础" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "02.工具配置" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "03.软件设计" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "04.软件测试" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "05.WEB前端" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "06.数据库技术" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "07.移动开发" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "08.软件工程" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "09.企业架构" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "10.项目管理" });
                context.EssayCatalogs.Add(new EssayCatalog { Name = "11.其他分类" });
            }

            if (!context.EssayTags.AsNoTracking().Any())
            {
                context.EssayTags.Add(new EssayTag { Name = ".NET Core" });
                context.EssayTags.Add(new EssayTag { Name = "Entity Framework" });
                context.EssayTags.Add(new EssayTag { Name = "JavaScript" });
                context.EssayTags.Add(new EssayTag { Name = "AngularJS" });
                context.EssayTags.Add(new EssayTag { Name = "Bootstrap" });
                context.EssayTags.Add(new EssayTag { Name = "Unity3D" });
                context.EssayTags.Add(new EssayTag { Name = "微信小程序" });
                context.EssayTags.Add(new EssayTag { Name = "区块链" });
                context.EssayTags.Add(new EssayTag { Name = "设计模式" });
                context.EssayTags.Add(new EssayTag { Name = "Linux/Unix" });
                context.EssayTags.Add(new EssayTag { Name = "Docker" });
            }

            context.SaveChanges();
        }
    }
}