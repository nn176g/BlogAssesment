using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Data.Models;
using TestBlog.Models.AdminViewModels;
using TestBlog.Service.Interfaces;

namespace TestBlog.BusinessManager
{
    public class AdminBusinessManager:IAdminBusinessManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AdminBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IndexViewModel> GetAdminDashboard(ClaimsPrincipal claimsPrincipal)
        {
            var applicationUser = await _userManager.GetUserAsync(claimsPrincipal);
            return new IndexViewModel
            {
                Blogs = _blogService.GetBlogs(applicationUser)
            };
        }
    }
}
