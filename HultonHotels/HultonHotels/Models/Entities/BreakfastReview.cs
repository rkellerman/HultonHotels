using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models.Entities
{
    public class BreakfastReview
    {
        [ForeignKey("ReviewId")]
        public Review Review { get; set; }

        [Key]
        public int ReviewId { get; set; }
    }
}