using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HultonHotels.Models.Entities;

namespace HultonHotels.Models.Relationships
{
    public class RoomReviewEvaluatesRoom
    {

        public Room Room { get; set; }

        [ForeignKey("ReviewId")]
        public RoomReview RoomReview { get; set; }

        [Key]
        public int ReviewId { get; set; }

        [Column(Order = 2)]
        [ForeignKey("Room")]
        public int RoomNo { get; set; }
        [Column(Order = 1)]
        [ForeignKey("Room")]
        public int HotelId { get; set; }


    }
}