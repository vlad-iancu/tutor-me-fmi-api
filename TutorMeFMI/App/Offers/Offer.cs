using System.ComponentModel.DataAnnotations;
using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.Models
{
    public class Offer
    {
        [SqlKata.Key]
        public int id { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Price { get; set; }

        public string MeetingType { get; set; }
        
        public string MeetingSpecifications { get; set; }

        public int user_id { get; set; }

        public virtual DbUser User { get; set; }
    }
}