using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TestBlog.Data;

namespace TestBlog.Tests
{
    public class Utils
    {
        protected ApplicationDbContext CreateContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(dbName).Options;
            var dbContext = new ApplicationDbContext(options);
            return dbContext;
        }
    }
}
