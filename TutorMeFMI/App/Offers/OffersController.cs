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

        public readonly string[] OfferProjection =
        {
            "offer.id", "title", "description", "price", "meetingType", "meetingSpecifications", "userId",
            "subject.name as subjectName", "subject.id as subjectId"
        };

        /**
         * GET method that returns all the existing offers posted by an user
         * param @user = User entity representing the user to retrieve the offers for
         * Returns a list of type Offer containing the retrieved offers
         */
        [HttpGet]
        [Authorization]
        public IActionResult List(User user)
        {
            using var database = new Database().GetQueryFactory();
            var offers = database.Query("offer")
                .Join("subject", "subject.id", "offer.subjectId")
                .Select(OfferProjection)
                .Where("userId", "=", user.Id).Get();
            return Json(new {offers});
        }
        
        /**
         * GET method that returns all the existing offers posted by the other users
         * (other than @user itself)
         * param @user = User entity representing the user to retrieve the global offers for
         * Returns a list of type Offer containing the retrieved offers
         */
        [HttpGet]
        [Authorization]
        public IActionResult ListAll(User user)
        {
            using var database = new Database().GetQueryFactory();
            var offers = database.Query("offer")
                .Join("subject", "subject.id", "offer.subjectId")
                .Select(OfferProjection)
                .Where("userId", "!=", user.Id).Get();
            return Json(new {offers});
        }

        
        /**
         * POST method that adds a new offer in the system
         * param @user = User entity representing the user that posts the offer
         * Returns a new Offer with the given parameters 
         */
        [HttpPost]
        [Authorization]
        [ValidateModel]
        public IActionResult Add(User user, [FromBody] Offer offer)
        {
            using var database = new Database().GetQueryFactory();
            var requestId = database.Query("offer").InsertGetId<int>(new
            {
                title = offer.Title,
                description = offer.Description,
                price = offer.Price,
                meetingType = offer.MeetingType,
                meetingSpecifications = offer.MeetingSpecifications,
                userId = user.Id,
                subjectId = offer.SubjectId
            });
            offer.UserId = user.Id;
            offer.Id = requestId;
            return Json(new {offer});
        }
    }
}