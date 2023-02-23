using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Models.BlogViewModels
{
    public class BlogViewModel
    {
        public Comment Comment { get; set; }
        public Blog Blog { get; set; }
    }
}
