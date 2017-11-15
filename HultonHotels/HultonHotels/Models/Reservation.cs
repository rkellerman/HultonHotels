using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        public DateTime ReservationDate { get; set; }
        public decimal Amount { get; set; }

    }
}