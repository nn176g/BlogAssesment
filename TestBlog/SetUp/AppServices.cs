using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;
using TestBlog.Authorization;
using TestBlog.BusinessManager;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Data;
using TestBlog.Data.Models;
using TestBlog.Service;
using TestBlog.Service.Interfaces;

namespace TestBlog.SetUp
{
    public static class AppServices
    {
        public static void AddDefaultServices(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            serviceCollection.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            serviceCollection.AddControllersWithViews().AddRazorRuntimeCompilation();
            serviceCollection.AddRazorPages();
            serviceCollection.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
        }
        public static void AddCustomServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBlogService, BlogService>();


            serviceCollection.AddScoped<IBlogBusinessManager,BlogBusinessManager>();
            serviceCollection.AddScoped<IAdminBusinessManager, AdminBusinessManager>();
            serviceCollection.AddScoped<IHomeBusinessManager, HomeBusinessManager>();
        }
        public static void AddCustomAuthorization(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAuthorizationHandler, BlogAuthorizationHandler>();
        }
    }
}
