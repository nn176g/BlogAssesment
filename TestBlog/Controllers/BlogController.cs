using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Models.BlogViewModels;

namespace TestBlog.Controllers
{
    [Authorize]
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager _blogBusinessManager;

        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            _blogBusinessManager=blogBusinessManager ;
        }

        [Authorize(Roles ="Public,Writer,Editor")]
        [Route("Blog/{id}"), AllowAnonymous]
        public IActionResult Index(int? id)
        {
            var result = _blogBusinessManager.GetBlogViewModel(id, User);

            if (result.Result is null)
            {
                return View(result.Value);
            }
            return result.Result;
        }

        public IActionResult Create()
        {
            return View( new CreateViewModel());
        }
        [HttpPost]
        [Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add(CreateViewModel createBlogViewModel)
        {
           await _blogBusinessManager.CreateBlog(createBlogViewModel,User);
            return RedirectToAction("Index","Admin");
        }

        [Authorize(Roles = "Writer")]
        public IActionResult Edit(int? id)
        {
            var result=  _blogBusinessManager.GetEditViewModel(id,User);

            if(result.Result is null)
            {
                return View(result.Value);
            }
            return result.Result;
        }

        [HttpPost]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var result = await _blogBusinessManager.UpdateBlog(editViewModel,User);

            if (result.Result is null)
            {
                return RedirectToAction("Index","Admin");
            }
            return result.Result;
        }
        public IActionResult Delete(int id)
        {
            var result =  _blogBusinessManager.DeleteBlog(id);

            if (result == HttpStatusCode.OK)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "Public,Writer,Editor")]
        public async Task<IActionResult> Comment(BlogViewModel blogViewModel)
        {
            var result = await _blogBusinessManager.CreateComment(blogViewModel, User);

            if (result.Result is null)
            {
                return RedirectToAction("Index", new { blogViewModel.Blog.Id});
            }
            
            return View(result.Result);
        }
    }
}