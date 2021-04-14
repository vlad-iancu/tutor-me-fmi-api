using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.App.Auth;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;
using TutorMeFMI.Models;

namespace TutorMeFMI.Controllers
{
    public class OffersController : Controller
    {
        /**
         * GET method that returns all the existing offers posted by an user
         * param @user = User entity representing the user to retrieve the offers for
         * Returns a list of type Offer containing the retrieved offers
         */
        [HttpGet]
        [Authorization]
        public IActionResult UserOfs(User user)
        {
            using var database = new Database().GetQueryFactory();
            var offers = database.Query("offer").Where("email", "=", user.Email).Get<Offer>();
            return Json(new {offers});
        }
        
        
        /**
        * GET method that returns all the existing offers posted by all the other users
        * other than @user itself
        * param @user = User entity representing the user to retrieve the global offers for
        * Returns a list of type Offer containing the retrieved offers
        */
        [HttpGet]
        [Authorization]
        public IActionResult AllOfs(User user)
        {
            using var database = new Database().GetQueryFactory();
            var offers = database.Query("offer").Where("email", "!=", user.Email).Get<Offer>();
            return Json(new {offers});
        }
    }
}