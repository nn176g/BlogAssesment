using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBlog.BusinessManager.Interfaces;

namespace TestBlog.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IAdminBusinessManager _adminBuisnessManager;

        public AdminController(IAdminBusinessManager adminBuisnessManager)
        {
            _adminBuisnessManager = adminBuisnessManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _adminBuisnessManager.GetAdminDashboard(User));
        }
    }
}