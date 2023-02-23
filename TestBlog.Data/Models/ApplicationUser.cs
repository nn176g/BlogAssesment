using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestBlog.Data.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    public class RoleManager : IdentityRole
    {
        public string IdRole { get; set; }
        public string RoleName { get; set; }
    }
}
