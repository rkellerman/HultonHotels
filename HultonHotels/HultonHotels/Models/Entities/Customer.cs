using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string Email { get; set; }
        public string Address { get; set; }
        public int PhoneNo { get; set; }
        public string Name { get; set; }

    }
}