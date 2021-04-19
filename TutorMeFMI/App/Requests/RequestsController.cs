using System;
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
        [HttpDelete]
        [Authorization]
        public IActionResult Delete(User user, int id)
        {
            using var database = new Database().GetQueryFactory();
            var deleted = database.Query("request").Where("id", id).Delete();
            return Json(new {success = deleted > 0});
        }
        
        [HttpPut]
        [Authorization]
        public IActionResult Update(User user, [FromBody] Request request)
        {
            var updateObject = new System.Collections.Generic.Dictionary<String, object>();
            if (request.Id <= 0) return BadRequest("Id must ne non-null and greater than zero");
            if (!String.IsNullOrEmpty(request.Title)) updateObject.Add("title", request.Title);
            if (!String.IsNullOrEmpty(request.Description)) updateObject.Add("description", request.Description);
            if (!String.IsNullOrEmpty(request.MeetingType)) updateObject.Add("meetingType", request.MeetingType);
            if (!String.IsNullOrEmpty(request.MeetingSpecifications)) updateObject.Add("meetingSpecifications", request.MeetingSpecifications);
            if (request.Price > 0) updateObject.Add("price", request.Price);
            if (request.SubjectId > 0) updateObject.Add("subjectId", request.SubjectId);
            using var database = new Database().GetQueryFactory();
            database.Query("request").Where("id", request.Id).Update(updateObject);

            return Json(new {success = true});
        }
    }
}