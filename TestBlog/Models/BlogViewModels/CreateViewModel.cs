using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using TestBlog.Data.Models;

namespace TestBlog.Models.BlogViewModels
{
    public class CreateViewModel
    {
        [Required, Display(Name="Header Image")]
        public IFormFile BlogHeaderImage { get; set; }
        public Blog Blog { get; set; }
    }
}
