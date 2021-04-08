﻿using System.ComponentModel.DataAnnotations;
using TutorMeFMI.App.Auth.Model;

namespace TutorMeFMI.App.Offers
{
    public class Offer
    {
        [SqlKata.Key]
        [SqlKata.Column("id")]
        public int Id { get; set; }

        [SqlKata.Column("title")]
        [Required(ErrorMessage = "The mentoring offer needs to have a title")]
        public string Title { get; set; }
        [SqlKata.Column("description")]
        public string Description { get; set; }
        [SqlKata.Column("price")]
        [Required(ErrorMessage = "The mentoring offer needs to have a price")]
        public int Price { get; set; }
        [SqlKata.Column("meetingType")]
        [Required(ErrorMessage = "The mentoring offer needs to have a meetingType")]
        public string MeetingType { get; set; }
        
        [SqlKata.Column("meetingSpecifications")]
        [Required(ErrorMessage = "The mentoring offer needs to have meetingSpecifications")]
        public string MeetingSpecifications { get; set; }

        [SqlKata.Column("userId")]
        public int UserId { get; set; }
    }
}