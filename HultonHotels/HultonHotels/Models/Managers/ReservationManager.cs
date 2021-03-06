﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HultonHotels.Models.Entities;
using HultonHotels.Models.Relationships;
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

        public List<List<ReservationObject>> Get(ReservationObject entity)
        {
            List<ReservationObject> ret = new List<ReservationObject>();
            ret = CreateData();

            if (!string.IsNullOrEmpty(entity.HotelAddress))
            {
                ret = ret.FindAll(r => r.HotelAddress.ToLower().Contains(entity.HotelAddress.ToLower()));
            }
            var ret2 = new List<ReservationObject>();
            if (!string.IsNullOrEmpty(entity.Email))
            {
                var customer = _context.Customers.FirstOrDefault(c => c.Email == entity.Email);
                var customerMakesReservationsWithCreditCard = _context.CustomerMakesReservationWithCreditCards
                    .Where(c => c.CustomerId == customer.CustomerId).ToList();
                foreach (var customerMakesReservationWithCreditCard in customerMakesReservationsWithCreditCard)
                {
                    var reservationReservesRoom = _context.ReservationReservesRooms.FirstOrDefault(r =>
                        r.ReservationId == customerMakesReservationWithCreditCard.ReservationId);

                    var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == reservationReservesRoom.HotelId);
                    var room = _context.Rooms.FirstOrDefault(r =>
                        r.HotelId == hotel.HotelId && r.RoomNo == reservationReservesRoom.RoomNo);

                    var obj = new ReservationObject
                    {
                        HotelAddress = hotel.Street + ", " + hotel.City + ", " + hotel.State + " " + hotel.Zip,
                        RoomNo = reservationReservesRoom.RoomNo,
                        Capacity = room.Capacity,
                        HotelId = hotel.HotelId,
                        Price = room.Price
                    };

                    ret2.Add(obj);
                }

            }

            return new List<List<ReservationObject>>
            {
                ret, ret2
            };
        }

        private List<ReservationObject> CreateData()
        {

            var ret = new List<ReservationObject>();

            var hotels = _context.Hotels.ToList();

            var reservationsReserveRooms = _context.ReservationReservesRooms.ToList();

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

            ret = ret.Where(r => !reservationsReserveRooms.Any(r2 => r2.RoomNo == r.RoomNo && r2.HotelId == r.HotelId)).ToList();
            return ret;
        }

        public ReservationObject Select(string eventArgument, string prevEventArgument)
        {
            var hotelId = int.Parse(eventArgument);
            var roomNo = int.Parse(prevEventArgument);

            var breakfasts = _context.Breakfasts.Where(b => b.HotelId == hotelId).ToList();
            var services = _context.Services.Where(s => s.HotelId == hotelId).ToList();

            var breakfastList = new SelectListItem[breakfasts.Count];

            for (var i = 0; i < breakfasts.Count; i++)
            {
                breakfastList[i] = new SelectListItem {Text = breakfasts[i].BreakfastType, Value = breakfasts[i].BreakfastType};
            }

            var serviceList = new SelectListItem[services.Count];

            for (var i = 0; i < services.Count; i++)
            {
                serviceList[i] = new SelectListItem {Text = services[i].ServiceType, Value = services[i].ServiceType};
            }

            var hotel = _context.Hotels.FirstOrDefault(h => h.HotelId == hotelId);
            var room = _context.Rooms.FirstOrDefault(r => r.RoomNo == roomNo);

            var ret = new ReservationObject
            {
                Capacity = room.Capacity,
                CreditCard = new CreditCard(),
                HotelAddress = hotel.Street + ", " + hotel.City + ", " + hotel.State + " " + hotel.Zip,
                HotelId = hotel.HotelId,
                Price = room.Price,
                RoomNo = room.RoomNo,
                ServiceItems = serviceList,
                ServiceChoice = serviceList[0].Text,
                BreakfastItems = breakfastList,
                BreakfastChoice = breakfastList[0].Text

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

            var breakfast = _context.Breakfasts.FirstOrDefault(b => b.BreakfastType == entity.BreakfastChoice);
            var service = _context.Services.FirstOrDefault(s => s.ServiceType == entity.ServiceChoice);

            var reservation = new Reservation
            {
                Amount = entity.Price * entity.ReservationReservesRoom.NumberOfDays + breakfast.BreakfastCost * entity.ReservationReservesRoom.NumberOfDays + 
                   service.ServiceCost * entity.ReservationReservesRoom.NumberOfDays,
                ReservationDate = DateTime.Now
            };



            var creditCard = _context.CreditCards.FirstOrDefault(c => c.CreditCardId == entity.CreditCard.CreditCardId);

            if (creditCard == null) {
                creditCard = new CreditCard
                {
                    BillingAddress = entity.CreditCard.BillingAddress,
                    CreditCardId = entity.CreditCard.CreditCardId,
                    ExpDate = DateTime.Now,
                    NameOnCard = entity.CreditCard.NameOnCard,
                    SecurityCode = entity.CreditCard.SecurityCode,
                    Type = entity.CreditCard.Type
                };

                _context.CreditCards.Add(creditCard);
            }

            _context.Reservations.Add(reservation);
            
            _context.SaveChanges();

            var reservationincludesBreakfast = new ReservationIncludesBreakfast
            {
                Breakfast = breakfast,
                BreakfastType = breakfast.BreakfastType,
                HotelId = entity.HotelId,
                Reservation = reservation,
                ReservationId = reservation.ReservationId
            };

            var reservationContainsService = new ReservationContainsService
            {
                HotelId = entity.HotelId,
                Reservation = reservation,
                ReservationId = reservation.ReservationId,
                Service = service,
                ServiceType = service.ServiceType
            };

            _context.ReservationContainsServices.Add(reservationContainsService);
            _context.ReservationIncludesBreakfasts.Add(reservationincludesBreakfast);
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

            var reservationReservesRoom = new ReservationReservesRoom
            {
                HotelId = entity.HotelId,
                InDate = entity.ReservationReservesRoom.InDate,
                NumberOfDays = entity.ReservationReservesRoom.NumberOfDays,
                OutDate = entity.ReservationReservesRoom.InDate.AddDays(entity.ReservationReservesRoom.NumberOfDays),
                Reservation = reservation,
                ReservationId = reservation.ReservationId,
                Room = _context.Rooms.FirstOrDefault(r => r.RoomNo == entity.RoomNo),
                RoomNo = entity.RoomNo
            };

            _context.ReservationReservesRooms.Add(reservationReservesRoom);
            _context.CustomerMakesReservationWithCreditCards.Add(customerMakesReservationWithCreditCard);
            _context.SaveChanges();
        }

        public void Delete(ReservationObject entity)
        {
            var reservationReservesRoom =
                _context.ReservationReservesRooms.Include(r => r.Reservation).FirstOrDefault(r =>
                    r.RoomNo == entity.RoomNo && r.HotelId == entity.HotelId);
            var customerMakesReservationWithCreditCard =
                _context.CustomerMakesReservationWithCreditCards.FirstOrDefault(r =>
                    r.ReservationId == reservationReservesRoom.ReservationId);

            _context.CustomerMakesReservationWithCreditCards.Remove(customerMakesReservationWithCreditCard);

            var reservation = reservationReservesRoom.Reservation;

            _context.Reservations.Remove(reservation);
            _context.SaveChanges();
        }

        public ReservationObject View(string eventArgument, string prevEventArgument)
        {
            var hotelId = int.Parse(eventArgument);
            var roomNo = int.Parse(prevEventArgument);

            var tempObj = _context.Rooms.Include(h => h.Hotel)
                .FirstOrDefault(r => r.RoomNo == roomNo && r.HotelId == hotelId);

            var hotel = tempObj.Hotel;
            var room = tempObj;

            var breakfasts = _context.Breakfasts.Where(b => b.HotelId == hotelId).ToList();
            var services = _context.Services.Where(s => s.HotelId == hotelId).ToList();

            var breakfastList = new SelectListItem[breakfasts.Count];

            for (var i = 0; i < breakfasts.Count; i++)
            {
                breakfastList[i] = new SelectListItem { Text = breakfasts[i].BreakfastType, Value = breakfasts[i].BreakfastType };
            }

            var serviceList = new SelectListItem[services.Count];

            for (var i = 0; i < services.Count; i++)
            {
                serviceList[i] = new SelectListItem { Text = services[i].ServiceType, Value = services[i].ServiceType };
            }

            var reservation =
                _context.ReservationReservesRooms.Include(r => r.Reservation).FirstOrDefault(r => r.HotelId == hotelId && r.RoomNo == roomNo).Reservation;

            var breakfast = _context.ReservationIncludesBreakfasts.Include(r => r.Breakfast)
                .FirstOrDefault(r => r.ReservationId == reservation.ReservationId).Breakfast;

            var service = _context.ReservationContainsServices.Include(r => r.Service)
                .FirstOrDefault(r => r.ReservationId == reservation.ReservationId).Service;

            var temp = _context.CustomerMakesReservationWithCreditCards.Include(c => c.Customer).Include(c => c.CreditCard)
                .FirstOrDefault(r => r.ReservationId == reservation.ReservationId);

            var customer = temp.Customer;
            var creditCard = temp.CreditCard;

            var reviewList = new SelectListItem[]
            {
                new SelectListItem {Text = "Room Review", Value = "0" },
                new SelectListItem {Text = "Breakfast Review", Value = "1" },
                new SelectListItem {Text = "Service Review", Value = "2" }
            };
            

            var reservationObject = new ReservationObject
            {
                HotelId = hotelId,
                BreakfastChoice = breakfast.BreakfastType,
                Capacity = room.Capacity,
                CreditCard = creditCard,
                CustomerId = customer.CustomerId,
                BreakfastItems = breakfastList,
                Email = customer.Email,
                HotelAddress = hotel.Street + ", " + hotel.City + ", " + hotel.State + " " + hotel.Zip,
                Price = room.Price,
                ServiceItems = serviceList,
                ServiceChoice = service.ServiceType,
                RoomNo = roomNo,
                ReviewChoice = reviewList[0].Value,
                ReviewItems = reviewList,
                Review = new Review
                {
                    Comment = "",
                    Rating = 1
                }

            };
            

            return reservationObject;
        }

        public void WriteReview(ReservationObject entity)
        {
            var review = new Review
            {
                Comment = entity.Review.Comment,
                Rating = entity.Review.Rating,
                Date = DateTime.Now
            };

            var customer = _context.Customers.FirstOrDefault(c => c.CustomerId == entity.CustomerId);

            _context.Reviews.Add(review);

            var customerWritesReview = new CustomerWritesReview
            {
                Customer = customer,
                CustomerId = customer.CustomerId,
                Review = review,
                ReviewId = review.ReviewId
            };

            _context.CustomerWritesReviews.Add(customerWritesReview);

            RoomReview roomReview = null;
            BreakfastReview breakfastReview = null;
            ServiceReview serviceReview = null;

            int value = int.Parse(entity.ReviewChoice);

            switch (value)
            {
                case 0:
                    roomReview = new RoomReview
                    {
                        Review = review,
                        ReviewId = review.ReviewId
                    };

                    var roomReviewRatesRoom = new RoomReviewEvaluatesRoom
                    {
                        RoomReview = roomReview,
                        HotelId = entity.HotelId,
                        ReviewId = review.ReviewId,
                        Room = _context.Rooms.FirstOrDefault(r => r.RoomNo == entity.RoomNo),
                        RoomNo = entity.RoomNo
                    };

                    _context.RoomReviews.Add(roomReview);
                    _context.RoomReviewEvaluatesRooms.Add(roomReviewRatesRoom);

                    break;

                case 1:
                    breakfastReview = new BreakfastReview
                    {
                        Review = review,
                        ReviewId = review.ReviewId
                    };

                    var breakfast = _context.Breakfasts.FirstOrDefault(b => b.BreakfastType == entity.BreakfastChoice);

                    var breakfastReviewRates = new BreakfastReviewAssessesBreakfast
                    {
                        BreakfastReview = breakfastReview,
                        Breakfast = breakfast,
                        BreakfastType = breakfast.BreakfastType,
                        HotelId = entity.HotelId,
                        ReviewId = review.ReviewId
                    };

                    _context.BreakfastReviews.Add(breakfastReview);
                    _context.BreakfastReviewAssessesBreakfasts.Add(breakfastReviewRates);

                    break;
                case 2:
                    var service = _context.Services.FirstOrDefault(s => s.ServiceType == entity.ServiceChoice);

                    serviceReview = new ServiceReview
                    {
                        Review = review,
                        ReviewId = review.ReviewId
                    };

                    var serviceReviewRatesService = new ServiceReviewRatesService
                    {
                        ServiceReview = serviceReview,
                        Service = service,
                        HotelId = entity.HotelId,
                        ReviewId = review.ReviewId,
                        ServiceType = service.ServiceType
                    };

                    _context.ServiceReviews.Add(serviceReview);
                    _context.ServiceReviewRatesServices.Add(serviceReviewRatesService);

                    break;
                default:
                    break;
            }

            _context.SaveChanges();

        }
    }
}