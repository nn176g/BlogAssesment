
using System;
using System.Collections.Generic;
using TestBlog.Data.Models;

namespace TestBlog.Data.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Blog Blog { get; set; }
        public ApplicationUser Author { get; set; }
        public string Content { get; set; }
        public Comment Parent { get; set; }
        public DateTime CreatedOn { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }
    }
}