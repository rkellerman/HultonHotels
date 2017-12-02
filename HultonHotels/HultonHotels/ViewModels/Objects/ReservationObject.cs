using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HultonHotels.Models;

namespace HultonHotels.ViewModels.Objects
{
    public class ReservationObject
    {
        public int CustomerId { get; set; }
        public string Email { get; set; }
        public int HotelId { get; set; }
        public string HotelAddress { get; set; }
        public int RoomNo { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public CreditCard CreditCard { get; set; }
        public ReservationReservesRoom ReservationReservesRoom { get; set; }
        public SelectListItem[] BreakfastItems { get; set; }
        public string BreakfastChoice { get; set; }
        public SelectListItem[] ServiceItems { get; set; }
        public string ServiceChoice { get; set; }
        public Review Review { get; set; }
        public string ReviewChoice { get; set; }
        public SelectListItem[] ReviewItems { get; set; }


    }
}