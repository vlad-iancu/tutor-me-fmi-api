using System.ComponentModel.DataAnnotations;
using TutorMeFMI.App;
using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.Models
{
    public class Request
    {
        [SqlKata.Key]
        [SqlKata.Column("id")]
        public int Id { get; set; }

        [SqlKata.Column("title")]
        [Required(ErrorMessage = "The mentoring request needs to have a title")]
        public string Title { get; set; }
        [SqlKata.Column("description")]
        public string Description { get; set; }
        [SqlKata.Column("price")]
        [Required(ErrorMessage = "The mentoring request needs to have a price")]
        public int Price { get; set; }
        [SqlKata.Column("meetingType")]
        [Required(ErrorMessage = "The mentoring request needs to have a meetingType")]
        public string MeetingType { get; set; }
        
        [SqlKata.Column("meetingSpecifications")]
        [Required(ErrorMessage = "The mentoring request needs to have meetingSpecifications")]
        public string MeetingSpecifications { get; set; }
        
        [SqlKata.Column("subjectId")]
        [Required(ErrorMessage = "The mentoring request needs to have a subjectId")]
        public int SubjectId { get; set; }
        
        [SqlKata.Column("userId")]
        public int UserId { get; set; }
    }
}