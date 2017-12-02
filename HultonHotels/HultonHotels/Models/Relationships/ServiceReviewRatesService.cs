using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using HultonHotels.Models.Entities;

namespace HultonHotels.Models.Relationships
{
    public class ServiceReviewRatesService
    {
        [ForeignKey("ReviewId")]
        public ServiceReview ServiceReview { get; set; }
        public Service Service { get; set; }

        [Key]
        public int ReviewId { get; set; }

        [Column(Order=2)]
        [ForeignKey("Service")]
        public string ServiceType { get; set; }
        [Column(Order=1)]
        [ForeignKey("Service")]
        public int HotelId { get; set; }
    }
}