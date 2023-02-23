using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Models.IdentityViewModels
{
    public class RolesViewModel
    {

        [Required]
        [Display(Name = "IdRole")]
        public RoleManager IdRole { get; set; }

        [Required]
        [Display(Name = "RoleName")]
        public RoleManager RoleName { get; set; }
    }
}
