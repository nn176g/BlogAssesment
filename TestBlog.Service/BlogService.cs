using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TestBlog.Data;
using TestBlog.Data.Models;
using TestBlog.Service.Interfaces;

namespace TestBlog.Service
{
    public class BlogService:IBlogService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private ApiHelper<Blog> blogApi = new ApiHelper<Blog>("blog");
        private ApiHelper<Comment> commentApi = new ApiHelper<Comment>("comment");
        public BlogService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;

        }
        public IEnumerable<Blog> GetBlog(int blogId)
        {
           return blogApi.GetItem(blogId);
        }

        public IEnumerable<Blog> GetBlogs(string searchString, string id)
        {
            return blogApi.SearchItem(searchString, id);
        }


        public IEnumerable<Comment> GetComment(int commentId)
        {
            return commentApi.GetComment(commentId);
        }

        public IEnumerable<Comment> GetComments(string userId)
        {
            return commentApi.GetComments(userId);
        }

        public Blog Add(Blog blog)
        {

            return blogApi.PostBlog(blog);
        }

        public Comment Add(Comment comment)
        {
            return commentApi.PostComment(comment);
        }

        public Blog Update(Blog blog)
        {
            return blogApi.UpdateItem(blog);
        }

        public HttpStatusCode Delete(int id)
        {
            return blogApi.DeleteItem(id.ToString());
        }
        public IEnumerable<Blog> GetBlogs(ApplicationUser applicationUser)
        {
            if(applicationUser ==null)
            {
                applicationUser= new ApplicationUser();
            }
            return blogApi.GetItems(applicationUser.Id);

        }
    }
}
