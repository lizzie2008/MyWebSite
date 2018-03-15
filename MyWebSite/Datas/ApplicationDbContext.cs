using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyWebSite.Areas.Configuration.Models;
using MyWebSite.Areas.Essays.Models;
using MyWebSite.Models;

namespace MyWebSite.Datas
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<RoleMenu>().HasKey(rm => new { rm.RoleId, rm.MenuId });
            builder.Entity<EssayTagAssignment>().HasKey(c => new { c.EssayID, c.EssayTagID });



        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }

        public DbSet<Menu> Menus { get; set; }
        public DbSet<RoleMenu> RoleMenus { get; set; }

        public DbSet<Essay> Essays { get; set; }
        public DbSet<EssayCatalog> EssayCatalogs { get; set; }
        public DbSet<EssayTag> EssayTags { get; set; }
        public DbSet<EssayArchive> EssayArchives { get; set; }
        public DbSet<EssayTagAssignment> EssayTagAssignments { get; set; }

    }
}
