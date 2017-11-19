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

        public ReservationObject Select(string eventArgument, string prevEventArgument)
        {
            int hotelId = int.Parse(eventArgument);
            int roomNo = int.Parse(prevEventArgument);

            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == hotelId);
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNo == roomNo);

            var ret = new ReservationObject
            {
                Capacity = room.Capacity,
                CreditCard = new CreditCard(),
                HotelAddress = hotel.Street + ", " + hotel.City + ", " + hotel.State + " " + hotel.Zip,
                HotelId = hotel.HotelId,
                Price = room.Price,
                RoomNo = room.RoomNo
            };

            return ret;
        }

        public int Login(string searchEntityEmail)
        {
            return _context.Customers.FirstOrDefault(c => c.Email == searchEntityEmail).CustomerId;
        }

        public void Save(ReservationObject entity, string email)
        {
            var customer = _context.Customers.FirstOrDefault(c => c.Email == email);

            var reservation = new Reservation
            {
                Amount = entity.Price,
                ReservationDate = DateTime.Now
            };

            var creditCard = new CreditCard
            {
                BillingAddress = entity.CreditCard.BillingAddress,
                CreditCardId = entity.CreditCard.CreditCardId,
                ExpDate = DateTime.Now,
                NameOnCard = entity.CreditCard.NameOnCard,
                SecurityCode = entity.CreditCard.SecurityCode,
                Type = entity.CreditCard.Type
            };

            _context.Reservations.Add(reservation);
            _context.CreditCards.Add(creditCard);
            _context.SaveChanges();

            var customerMakesReservationWithCreditCard = new CustomerMakesReservationWithCreditCard
            {
                CreditCard = creditCard,
                CreditCardId = creditCard.CreditCardId,
                Customer = customer,
                CustomerId = customer.CustomerId,
                Reservation = reservation,
                ReservationId = reservation.ReservationId
            };

            _context.CustomerMakesReservationWithCreditCards.Add(customerMakesReservationWithCreditCard);
            _context.SaveChanges();
        }
    }
}