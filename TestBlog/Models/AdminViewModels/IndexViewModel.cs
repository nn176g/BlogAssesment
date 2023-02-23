using System.Collections.Generic;
using TestBlog.Data.Models;

namespace TestBlog.Models.AdminViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<Blog> Blogs { get; set; }
    }
}
