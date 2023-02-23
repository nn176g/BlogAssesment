using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestBlog.Utils
{
    [HtmlTargetElement(Attributes = "list, count")]
    public class Helper : TagHelper
    {
        public IEnumerable<object> List { get; set; }
        public int Count { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (List.Count() <= Count)
                output.SuppressOutput();
        }
    }
}
