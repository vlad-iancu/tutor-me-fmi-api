using System.Linq;
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
         * param @userId = int value representing the id of the user to retrieve the requests for
         * Returns a list of type Request containing the retrieved requests
         */
        public readonly string[] RequestProjection = 
        {
            "request.id","title","description","price","meetingType","meetingSpecifications", "userId",
            "subject.name as subjectName", "subject.id as subjectId"
        };
        [HttpGet]
        [Authorization]
        public IActionResult List(User user)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request")
                .Join("subject", "subject.id","request.subjectId")
                .Select(RequestProjection)
                .Where("userId", "=", user.Id).Get();
            return Json(new {requests});
        }
        [HttpPost]
        [Authorization]
        [ValidateModel]
        public IActionResult Add(User user, [FromBody] Request request)
        {
            using var database = new Database().GetQueryFactory();
            var requestId = database.Query("request").InsertGetId<int>(new
            {
                title = request.Title,
                description = request.Description,
                price = request.Price,
                meetingType = request.MeetingType,
                meetingSpecifications = request.MeetingSpecifications,
                userId = user.Id,
                subjectId = request.SubjectId
            });
            request.Id = requestId;
            request.UserId = user.Id;
            return Json(new {request});
        }
    }
}