using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.BusinessManager;
using TestBlog.BusinessManager.Interfaces;
using TestBlog.Controllers;
using TestBlog.Data;
using TestBlog.Data.Models;
using TestBlog.Service;
using TestBlog.Service.Interfaces;

namespace TestBlog.Tests.BlogBusinessManagerTest
{
    [TestClass]
    public class BlogBusinessManagerTest: Utils
    {
        public BlogBusinessManagerTest( )
        {

        }

        [TestMethod]
        public void GetBlogSucess()
        {
            //Arrange
            var dbName = Guid.NewGuid().ToString();
            var dbContext = CreateContext(dbName);
            var _blogService = new BlogService(dbContext);
            var expected = "test data";
            Blog record = new Blog
            {
                Id = 1,
                Approved = true,
                Content = "test data",
                Published = true,
                CreatedOn = DateTime.Now
            };
            //Act
            var resultRecord=_blogService.Add(record);
            var result = _blogService.GetBlog(1);
            //Assert
            Assert.AreEqual(expected, result.FirstOrDefault().Content);
        }

    }
}

