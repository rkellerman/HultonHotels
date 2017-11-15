using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class ReservationReservesRoom
    {
        [Key]
        [Column(Order=1)]
        public int ReservationId { get; set; }

        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [Key]
        [Column(Order=2)]
        [ForeignKey("Room")]
        public int HotelId { get; set; }

        [Key]
        [Column(Order=3)]
        [ForeignKey("Room")]
        public int RoomNo { get; set; }

        public Room Room { get; set; }

        public DateTime OutDate { get; set; }
        public DateTime InDate { get; set; }
        public int NumberOfDays { get; set; }
    }
}