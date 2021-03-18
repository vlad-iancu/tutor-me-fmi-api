using System.ComponentModel.DataAnnotations;

namespace TutorMeFMI.App.Auth.Model
{
    public class RegisterRequest
    {
        
        [Required(ErrorMessage = "You need to provide an user name")]
        [StringLength(128, ErrorMessage = "The user name can have no more than 128 characters")]
        public string name { get; set; }
        
        [Required(ErrorMessage = "You need to provide an email")]
        [EmailAddress]
        [StringLength(128, ErrorMessage = "The email address can have no more than 128 characters")]
        public string email { get; set; }
        
        [Required(ErrorMessage = "You need to provide a password")]
        [StringLength(6)]
        public string password { get; set; }
    }
}