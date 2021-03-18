using Microsoft.AspNetCore.Mvc;
using TutorMeFMI.App.Auth.Dal;
using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.App.Auth
{
    public class AuthController : Controller
    {
        private IUserDataAccess repository;

        public AuthController()
        {
            repository = new UserRepository();
        }

        [ValidateModel]
        [HttpPost]
        public IActionResult Register([FromBody] RegisterRequest request)
        {
            repository.AddUser(request);
            return Json(new {foo = "foo", bar = "bar"});
        }
        
        [ValidateModel]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = repository.GetUser(request.email, request.password);
            if (user == null) return NotFound(new {message = "User does not exist"});
            var token = JwtUtils.GenerateUserToken(user);
            return Json(new {token});
        }
    }
}