using PagedList.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Models.HomeViewModels
{
    public class IndexViewModel
    {
        public IPagedList<Blog> Blogs {get; set; }
        public string SearchString { get; set; }
        public int PageNumber { get; set; }
    }
}
