using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestBlog.Data.Models;

namespace TestBlog.Authorization
{
    public class BlogAuthorizationHandler : AuthorizationHandler<OperationAuthorizationRequirement, Blog>
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public BlogAuthorizationHandler(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, Blog tempRecord)
        {
            var applicationUser = await _userManager.GetUserAsync(context.User);

            if ((requirement.Name == Operations.Update.Name || requirement.Name == Operations.Delete.Name) && applicationUser == tempRecord.Creator)
            {
                context.Succeed(requirement);
            }

            if (requirement.Name == Operations.Read.Name && !tempRecord.Published && applicationUser == tempRecord.Creator)
                context.Succeed(requirement);
        }
    }
}
