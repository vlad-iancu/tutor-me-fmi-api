using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.Controllers
{
    public class RequestsController : Controller
    {
        /**
         * GET method that returns all the existing requests posted by an user
         * param @userId = int value representing the id of the user to retrieve the requests for
         * Returns a list of type Request containing the retrieved requests
         */        
        public IActionResult UserReqs(int userId)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request").Where("user_id", "=", userId).Get<Request>();
            return View();
        }
    }
}