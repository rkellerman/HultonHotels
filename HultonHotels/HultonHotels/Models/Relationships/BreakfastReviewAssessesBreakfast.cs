using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HultonHotels.Models.Entities;

namespace HultonHotels.Models.Relationships
{
    public class BreakfastReviewAssessesBreakfast
    {
        [ForeignKey("ReviewId")]
        public BreakfastReview BreakfastReview { get; set; }
        public Breakfast Breakfast { get; set; }

        [Key]
        public int ReviewId { get; set; }

        [Column(Order=2)]
        [ForeignKey("Breakfast")]
        public string BreakfastType { get; set; }

        [Column(Order=1)]
        [ForeignKey("Breakfast")]
        public int HotelId { get; set; }
    }
}