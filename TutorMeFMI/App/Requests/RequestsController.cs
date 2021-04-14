using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.App.Auth;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.App.Requests
{
    public class RequestsController : Controller
    {
        /**
         * GET method that returns all the existing requests posted by an user
         * param @user = User entity representing the user to retrieve the requests for
         * Returns a list of type Request containing the retrieved requests
         */
        [HttpGet]
        [Authorization]
        public IActionResult UserReqs(User user)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request").Where("email", "=", user.Email).Get<Request>();
            return Json(new {requests});
        }
        
        /**
         * GET method that returns all the existing requests posted by all the other users
         * other than @user itself
         * param @user = User entity representing the user to retrieve the global requests for
         * Returns a list of type Request containing the retrieved requests
         */
        [HttpGet]
        [Authorization]
        public IActionResult AllReqs(User user)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request").Where("email", "!=", user.Email).Get<Request>();
            return Json(new {requests});
        }
    }
}