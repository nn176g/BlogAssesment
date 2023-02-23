using Microsoft.AspNetCore.Mvc;
using RestSharp;
using System.IO;
using System.Linq;
using System.Security.Claims;


namespace TestBlog.BusinessManager
{
    public class Utils
    {
        public static void EnsureFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);

            if(directoryName.Length >0)
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
        }

        public static ActionResult DetermineActionResult(ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

    }
}
