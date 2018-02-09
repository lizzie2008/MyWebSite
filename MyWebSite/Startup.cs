using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyWebSite.Areas.Tools.Models.BaiduAnalysis;
using MyWebSite.Datas;
using MyWebSite.Datas.Config;
using MyWebSite.Datas.Config.Home;
using MyWebSite.Extensions;
using MyWebSite.Models;
using MyWebSite.Services;
using MyWebSite.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace MyWebSite
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityExtensions>();

            //添加应用程序服务
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddSession();

            //全球化和本地化
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();

            //初始化应用配置
            InitAppConfig(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");

                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error/500");
                app.UseStatusCodePagesWithReExecute("/Home/Error/{0}");
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("zh-CN"),
            };

            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("zh-CN"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            });

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "areaRoute",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// 初始化配置
        /// </summary>
        /// <param name="services"></param>
        private void InitAppConfig(IServiceCollection services)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile("Datas/Config/SiteConfig.json")
                .AddJsonFile("Datas/Config/Home/NavBarMenus.json")
                .AddJsonFile("Datas/Config/Home/MyProfile.json")
                .AddJsonFile("Datas/Config/Home/MyRequest.json")
                .AddJsonFile("Datas/Config/BaiduAnalysis/VisitDistrictRequest.json");

            var config = builder.Build();

            services.Configure<SiteConfig>(config.GetSection("SiteConfig"));
            services.Configure<NavBarMenus>(config.GetSection("NavBarMenus"));
            services.Configure<PrivateInfo>(config.GetSection("PrivateInfo"));
            services.Configure<MyProfile>(config.GetSection("MyProfile"));
            services.Configure<MyRequest>(config.GetSection("MyRequest"));
            services.Configure<VisitDistrictRequest>(config.GetSection("VisitDistrictRequest"));
        }
    }
}
