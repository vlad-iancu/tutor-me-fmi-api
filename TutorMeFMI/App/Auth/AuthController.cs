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
            var id = repository.AddUser(request);
            if (id <= 0) return BadRequest();
            return Json(new
            {
                id,
                request.email,
                request.name
            });
        }
        
        [ValidateModel]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            var user = repository.GetUser(request.email, request.password);
            if (user == null) return NotFound(new {message = "User does not exist"});
            var token = JwtUtils.GenerateUserToken(user);
            return Json(new
            {
                token, id = user.Id
            });
        }
    }
}