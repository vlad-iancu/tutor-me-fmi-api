using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SqlKata.Execution;
using TutorMeFMI.App.Auth.Dal;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;

namespace TutorMeFMI.App.Auth
{
    public class AuthorizationAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            /*context.ActionArguments["customArgument"] = "hello argument";*/
            var token = context.HttpContext.Request.Headers["Authorization"][0].Split(" ")[1];
            var user = JwtUtils.GetUserFromToken(token);
            using var database = new Database().GetQueryFactory();
            var dbUsers = database.Query("user").Where("email", "=", user.Email).Get<DbUser>();
            var auth = false;
            foreach (var dbUser in dbUsers)
            {
                context.ActionArguments["user"] = (User)dbUser;
                auth = true;
            }

            if (!auth) context.Result = new UnauthorizedResult();
        }
    }
}