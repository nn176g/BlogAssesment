using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Service.Interfaces
{
    public interface IBlogService
    {
        Blog Add(Blog blog);
        Blog Update(Blog blog);
        IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser);
        IEnumerable<Blog> GetBlogs(string searchString, string id);
        IEnumerable<Blog> GetBlog(int blogId);
        IEnumerable<Comment> GetComment(int commentId);
        Comment Add(Comment comment);
        IEnumerable<Comment> GetComments(string userId);
        HttpStatusCode Delete(int id);
    }
}
