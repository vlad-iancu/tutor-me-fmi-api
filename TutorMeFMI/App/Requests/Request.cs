using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.Models
{
    public class Request
    {
        [SqlKata.Key]
        [SqlKata.Column("id")]
        public int Id { get; set; }

        [SqlKata.Column("title")]
        public string Title { get; set; }
        
        [SqlKata.Column("description")]

        public string Description { get; set; }
        
        [SqlKata.Column("price")]

        public int Price { get; set; }

        [SqlKata.Column("meetingType")]

        public string MeetingType { get; set; }
        
        [SqlKata.Column("meetingSpecs")]

        public string MeetingSpecifications { get; set; }
        
        [SqlKata.Column("userId")]
        public int UserId { get; set; }

        public virtual DbUser User { get; set; }
    }
}