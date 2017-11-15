using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class CustomerMakesReservationWithCreditCard
    {
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [ForeignKey("CreditCardId")]
        public CreditCard CreditCard { get; set; }

        [Key]
        public int ReservationId { get; set; }

        public int CustomerId { get; set; }

        public int CreditCardId { get; set; }
    }
}