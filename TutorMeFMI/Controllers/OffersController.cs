using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.Controllers
{
    public class OffersController : Controller
    {
        /**
         * GET method that returns all the existing offers posted by an user
         * param @userId = int value representing the id of the user to retrieve the offers for
         * Returns a list of type Offer containing the retrieved offers
         */
        public IEnumerable<Offer> GetUserOffers(int userId)
        {
            using var database = new Database().GetQueryFactory();
            var offers = database.Query("offer").Where("user_id", "=", userId).Get<Offer>();
            return offers;
        }
    }
}