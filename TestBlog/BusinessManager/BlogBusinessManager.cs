using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using PagedList.Core;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TestBlog.Authorization;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Data.Models;
using TestBlog.Models.BlogViewModels;
using TestBlog.Service.Interfaces;
using IndexViewModel = TestBlog.Models.HomeViewModels.IndexViewModel;

namespace TestBlog.BusinessManager
{
    public class BlogBusinessManager:IBlogBusinessManager
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IBlogService _blogService;
        private readonly IAuthorizationService _authorizationService;

        public BlogBusinessManager(UserManager<ApplicationUser> userManager, IBlogService blogService, IWebHostEnvironment webHostEnvironment, IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
            _userManager = userManager;
            _blogService = blogService;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<Blog> CreateBlog(CreateViewModel createViewModel, ClaimsPrincipal claimsPrincipal)
        {
            Blog blog = createViewModel.Blog;
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            blog.Creator = user;
            blog.Approver = user;
            blog.Approved = true;
            blog.CreatedOn = DateTime.Now;
            blog.UpdatedOn = DateTime.Now;
            blog=  _blogService.Add(blog);

            string filePath = _webHostEnvironment.WebRootPath;
            string imagePath = $@"{filePath }\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";
            Utils.EnsureFolder(imagePath);
            using(var fileStream= new FileStream(imagePath, FileMode.Create))
            {
                await createViewModel.BlogHeaderImage.CopyToAsync(fileStream);
            }
            return blog;
        }
        public async Task<ActionResult<Comment>> CreateComment(BlogViewModel blogViewModel, ClaimsPrincipal claimsPrincipal)
        {
            if (blogViewModel.Blog is null || blogViewModel.Blog.Id == 0)
                return new BadRequestResult();

            var blog = _blogService.GetBlog(blogViewModel.Blog.Id);

            if (blog is null)
                return new NotFoundResult();

            var comment = blogViewModel.Comment;

            comment.Author = await _userManager.GetUserAsync(claimsPrincipal);
            comment.Blog = blog.FirstOrDefault();
            comment.CreatedOn = DateTime.Now;

            if (comment.Parent != null)
            {
                comment.Parent = _blogService.GetComment(comment.Parent.Id).FirstOrDefault();
            }

            Comment result= _blogService.Add(comment);
            
            //result.Comments = _blogService.GetComments(comment.Author.Id);

            return result;
        }

        public async Task<IndexViewModel> GetIndexViewModel(string searchString, int? page, ClaimsPrincipal claimsPrincipal)
        {
            int pageSize = 10;
            int pageNumber = page ?? 1;
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var blogs = _blogService.GetBlogs(searchString ?? string.Empty,user?.Id);

            return new IndexViewModel
            {
                Blogs = new StaticPagedList<Blog>(blogs.Skip((pageNumber - 1) * pageSize), pageNumber, pageSize, blogs.Count()),
                SearchString = searchString,
                PageNumber = pageNumber
            };
        }

        public ActionResult<BlogViewModel> GetBlogViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;

            var blog = _blogService.GetBlog(blogId);

            if (blog is null)
                return new NotFoundResult();


            return new BlogViewModel
            {
                Blog = blog.FirstOrDefault()
            };
        }

        public ActionResult<EditViewModel> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal)
        {
            if (id is null)
                return new BadRequestResult();

            var blogId = id.Value;

            var blog = _blogService.GetBlog(blogId);

            if (blog is null)
                return new NotFoundResult();

            return new EditViewModel
            {
                Blog = blog.FirstOrDefault()
            };
        }

        public HttpStatusCode DeleteBlog(int id)
        {
            var blog = _blogService.GetBlog(id).FirstOrDefault();

            if (blog is null)
                return HttpStatusCode.NotFound;

            return _blogService.Delete(id);

        }

        public async Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal)
        {
            var blog = _blogService.GetBlog(editViewModel.Blog.Id).FirstOrDefault();

            if (blog is null)
                return new NotFoundResult();

            blog.Published = editViewModel.Blog.Published;
            blog.Title = editViewModel.Blog.Title;
            blog.Content = editViewModel.Blog.Content;
            blog.UpdatedOn = DateTime.Now;

            if (editViewModel.BlogHeaderImage != null)
            {
                string Path = _webHostEnvironment.WebRootPath;
                string imagePath = $@"{Path}\UserFiles\Blogs\{blog.Id}\HeaderImage.jpg";

                Utils.EnsureFolder(imagePath);

                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await editViewModel.BlogHeaderImage.CopyToAsync(fileStream);
                }
            }

            return new EditViewModel
            {
                Blog =  _blogService.Update(blog)
            };
        }
    }
}
