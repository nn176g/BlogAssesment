using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestBlog.BusinessManager.Interfaces;

namespace TestBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBlogBusinessManager _blogBusinessManager;
        private readonly IHomeBusinessManager _homeBusinessManager;

        public HomeController(IBlogBusinessManager blogBusinessManager, IHomeBusinessManager homeBusinessManager)

        {
           _blogBusinessManager = blogBusinessManager;
           _homeBusinessManager = homeBusinessManager;
        }

        [Route("/")]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            return View(await _blogBusinessManager.GetIndexViewModel(searchString, page,User));
        }

    }
}
