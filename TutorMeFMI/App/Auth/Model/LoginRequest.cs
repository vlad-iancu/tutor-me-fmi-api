using System.ComponentModel.DataAnnotations;

namespace TutorMeFMI.App.Auth.Model
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(128)]
        public string email { get; set; }

        [Required]
        public string password { get; set; }
    }
}