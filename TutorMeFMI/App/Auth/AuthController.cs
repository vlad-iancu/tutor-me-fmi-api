using System;
using System.IO;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlKata.Execution;
using TutorMeFMI.App.Auth.Dal;
using TutorMeFMI.App.Auth.Model;
using TutorMeFMI.Data;

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
            var photoUrl = String.IsNullOrEmpty(user.ProfilePath) ? null : storage.GetDownloadUrl(user.ProfilePath);
            return Json(new {user = new {email = user.Email, name = user.Name, photoUrl}});
        }

        [HttpPost]
        [Authorization]
        public IActionResult UpdatePicture(User user, IFormFile newPhoto)
        {
            var storage = new Storage();
            var nameParts = newPhoto.FileName.Split(".");
            string extension = "";
            if (nameParts.Length == 2) extension = "." + nameParts[1];
            storage.UploadFile($"profile_photos/{user.Id}{extension}", newPhoto.OpenReadStream());
            using var database = new Database().GetQueryFactory();
            database.Query("user").Where("id", user.Id).Update(new {profilePath = $"profile_photos/{user.Id}{extension}"});
            return Json(new {});
        }
    }
}