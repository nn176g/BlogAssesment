using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TestBlog.Data.Models;
using TestBlog.Models.BlogViewModels;
using TestBlog.Models.HomeViewModels;

namespace TestBlog.BusinessManager.Interfaces
{
    public interface IBlogBusinessManager
    {
        Task<Blog> CreateBlog(CreateViewModel createBlogViewModel, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<EditViewModel>> UpdateBlog(EditViewModel editViewModel, ClaimsPrincipal claimsPrincipal);
        ActionResult<EditViewModel> GetEditViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        ActionResult<BlogViewModel> GetBlogViewModel(int? id, ClaimsPrincipal claimsPrincipal);
        Task<IndexViewModel> GetIndexViewModel(string searchString, int? page, ClaimsPrincipal claimsPrincipal);
        Task<ActionResult<Comment>> CreateComment(BlogViewModel blogViewModel, ClaimsPrincipal claimsPrincipal);
        HttpStatusCode DeleteBlog(int id);
    }
}
