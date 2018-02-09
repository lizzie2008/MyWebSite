using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Datas;
using MyWebSite.Models;
using System.Linq;

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
        }
    }
}