using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using HultonHotels.ViewModels.Objects;

namespace HultonHotels.Models
{
    public class ReservationManager
    {
        private readonly ApplicationDbContext _context;

        public ReservationManager()
        {
            _context = new ApplicationDbContext();
        }

        public List<ReservationObject> Get(ReservationObject entity)
        {
            List<ReservationObject> ret = new List<ReservationObject>();
            ret = CreateData();

            if (!string.IsNullOrEmpty(entity.HotelAddress))
            {
                // refine list
            }

            return ret;
        }

        private List<ReservationObject> CreateData()
        {

            var ret = new List<ReservationObject>();

            var hotels = _context.Hotels.ToList();

            foreach (var hotel in hotels)
            {
                var rooms = _context.Rooms.Where(r => r.HotelId == hotel.HotelId).ToList();

                foreach (var room in rooms)
                {
                    var obj = new ReservationObject
                    {
                        HotelId = hotel.HotelId,
                        HotelAddress = hotel.Street + ", " + hotel.City + ", " + hotel.State + " " + hotel.Zip,
                        RoomNo = room.RoomNo,
                        Capacity = room.Capacity,
                        Price = room.Price
                    };

                    ret.Add(obj);
                }
            }
            return ret;
        }
    }
}