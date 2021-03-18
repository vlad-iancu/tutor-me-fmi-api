using Microsoft.AspNetCore.Mvc.Filters;
using TutorMeFMI.App.Auth.Dal;

namespace TutorMeFMI.App.Auth
{
    public class AuthorizationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            /*context.ActionArguments["customArgument"] = "hello argument";*/
            var token = context.HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
            var user = JwtUtils.GetUserFromToken(token);
            context.ActionArguments["user"] = user;
        }
    }
}