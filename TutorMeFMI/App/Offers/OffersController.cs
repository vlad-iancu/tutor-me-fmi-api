using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.App.Auth;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.App.Offers
{
    public class OffersController : Controller
    {
        /**
         * GET method that returns all the existing offers posted by an user
         * param @userId = int value representing the id of the user to retrieve the offers for
         * Returns a list of type Offer containing the retrieved offers
         */
        [HttpGet]
        [Authorization]
        public IActionResult List(User user)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("offer").Where("userId", "=", user.Id).Get<Request>();
            return Json(new {requests});
        }
        [HttpPost]
        [Authorization]
        [ValidateModel]
        public IActionResult Add(User user, [FromBody] Offer request)
        {
            using var database = new Database().GetQueryFactory();
            var requestId = database.Query("offer").InsertGetId<int>(new
            {
                title = request.Title,
                description = request.Description,
                price = request.Price,
                meetingType = request.MeetingType,
                meetingSpecifications = request.MeetingSpecifications,
                userId = user.Id
            });
            request.Id = requestId;
            return Json(new {request});
        }
    }
}