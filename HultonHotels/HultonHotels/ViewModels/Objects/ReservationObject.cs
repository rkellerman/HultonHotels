using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HultonHotels.ViewModels.Objects
{
    public class ReservationObject
    {
        public int HotelId { get; set; }
        public string HotelAddress { get; set; }
        public int RoomNo { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }

    }
}