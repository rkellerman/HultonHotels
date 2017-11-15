using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class CreditCard
    {
        [Key]
        public int CreditCardId { get; set; }

        public DateTime ExpDate { get; set; }
        public string Type { get; set; }
        public int SecurityCode { get; set; }
        public string NameOnCard { get; set; }
        public string BillingAddress { get; set; }
    }
}