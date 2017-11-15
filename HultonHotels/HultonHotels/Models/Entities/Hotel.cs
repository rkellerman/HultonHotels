using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace HultonHotels.Models
{
    public class Hotel
    {
        [Key]
        public int HotelId { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int Zip { get; set; }
        public List<int> PhoneNumbers { get; set; }

    }
}