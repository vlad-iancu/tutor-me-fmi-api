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

        public readonly string[] RequestProjection = 
        {
            "request.id","title","description","price","meetingType","meetingSpecifications", "userId",
            "subject.name as subjectName", "subject.id as subjectId"
        };
        
        /**
         * GET method that returns all the existing requests posted by an user
         * param @user = User entity representing the user to retrieve the requests for
         * Returns a list of type Request containing the retrieved requests
         */
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
        
        /**
         * GET method that returns all the existing requests posted by the other users
         * (other than @user itself)
         * param @user = User entity representing the user to retrieve the global requests for
         * Returns a list of type Request containing the retrieved requests
         */
        [HttpGet]
        [Authorization]
        public IActionResult ListAll(User user)
        {
            using var database = new Database().GetQueryFactory();
            var requests = database.Query("request")
                .Join("subject", "subject.id","request.subjectId")
                .Select(RequestProjection)
                .Where("userId", "!=", user.Id).Get();
            return Json(new {requests});
        }
        

        /**
         * POST method that adds a new request in the system
         * param @user = User entity representing the user that posts the request
         * Returns a new Request with the given parameters 
         */
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