using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Models.BlogViewModels;

namespace TestBlog.Controllers
{
    [Authorize]
    [Authorize(Roles = "Writer")]
    public class BlogController : Controller
    {
        private readonly IBlogBusinessManager _blogBusinessManager;

        public BlogController(IBlogBusinessManager blogBusinessManager)
        {
            _blogBusinessManager=blogBusinessManager ;
        }

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

        [Route("Create")]
        public IActionResult Create()
        {
            return View( new CreateViewModel());
        }

        [HttpPost]
        [Route("Add")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Add(CreateViewModel createBlogViewModel)
        {
           await _blogBusinessManager.CreateBlog(createBlogViewModel,User);
            return RedirectToAction("Index","Admin");
        }

        //[Authorize(Roles = "Writer")]
        [Route("Edit")]
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
        [Route("Update")]
        public async Task<IActionResult> Update(EditViewModel editViewModel)
        {
            var result = await _blogBusinessManager.UpdateBlog(editViewModel,User);

            if (result.Result is null)
            {
                return RedirectToAction("Index","Admin");
            }
            return result.Result;
        }
        [Route("Delete")]
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
        [Route("Comment")]
        //[Authorize(Roles = "Public,Writer,Editor")]
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