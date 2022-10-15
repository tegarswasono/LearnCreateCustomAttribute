using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnBasicAuth.Auth
{
    public class BasicAuthMyBroAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var registeredUser = new Dictionary<string, string>()
            {
                { "user1", "password1" },
                { "user2", "password2" },
                { "user3", "password3" }
            };
            if (context.HttpContext.Request.Headers["Authorization"].FirstOrDefault() == null)
            {
                context.Result = new UnauthorizedResult();
            }
            else
            {
                var authToken = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
                if (authToken.Contains("Basic "))
                {
                    authToken = authToken.Replace("Basic ", "");
                }
                var decodedToken = Encoding.GetEncoding("iso-8859-1").GetString(Convert.FromBase64String(authToken));
                var separatorIndex = decodedToken.IndexOf(":");
                var username = decodedToken.Substring(0, separatorIndex);
                var password = decodedToken.Substring(separatorIndex + 1);

                var isExist = registeredUser.Any(x => x.Key == username && x.Value == password);
                if (!isExist)
                {
                    context.Result = new UnauthorizedResult();
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
