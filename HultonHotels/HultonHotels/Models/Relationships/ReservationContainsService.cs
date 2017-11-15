using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models.Relationships
{
    public class ReservationContainsService
    {

        [Key]
        [Column(Order=1)]
        [ForeignKey("Reservation")]
        public int ReservationId { get; set; }

        [Key]
        [Column(Order = 2)]
        [ForeignKey("Service")]
        public int HotelId { get; set; }

        [Key]
        [Column(Order = 3)]
        [ForeignKey("Service")]
        public string ServiceType { get; set; }

        public Service Service { get; set; }
        public Reservation Reservation { get; set; }
    }
}