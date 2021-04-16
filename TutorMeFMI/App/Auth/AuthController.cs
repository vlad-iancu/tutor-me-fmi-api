using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
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

        [HttpGet]
        [Authorization]
        public IActionResult Profile(User user)
        {
            var storage = new Storage();
            storage.UploadSampleFile();
            return Json(new {user = new {email = user.Email, name = user.Name, url = storage.GetSampleDownloadUrl()}});
        }
    }
}