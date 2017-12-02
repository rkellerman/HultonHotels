using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class MetricsManager
    {
        private readonly ApplicationDbContext _context;

        public MetricsManager()
        {
            _context = new ApplicationDbContext();
        }

        public MetricsObject Get(MetricsObject entity)
        {

            var ret = CreateData(entity);


            return ret;
        }

        private MetricsObject CreateData(MetricsObject ent)
        {
            MetricsObject entity = ent;
            entity.Rooms = new List<Room>();
            entity.Breakfasts = new List<Breakfast>();
            entity.Customers = new List<Customer>();
            entity.Hotels = new List<Hotel>();
            entity.Services = new List<Service>();

            var hotels = _context.Hotels.ToList();
            foreach (var hotel in hotels)
            {
                var roomReviewEvaluatesRoom = _context.RoomReviewEvaluatesRooms.Include(r => r.Room).Include(r => r.RoomReview.Review)
                    .Where(r => r.HotelId == hotel.HotelId).ToList();
                var bestRooms = roomReviewEvaluatesRoom.OrderByDescending(r => r.RoomReview.Review.Rating);
                
                var breakfastReviewRatesBreakfast = _context.BreakfastReviewAssessesBreakfasts.Include(b => b.Breakfast)
                    .Include(r => r.BreakfastReview.Review)
                    .Where(b => b.HotelId == hotel.HotelId).ToList();
                var bestBreakfasts = breakfastReviewRatesBreakfast
                    .OrderByDescending(r => r.BreakfastReview.Review.Rating);

                var serviceReviewRatesService = _context.ServiceReviewRatesServices.Include(s => s.Service)
                    .Include(s => s.ServiceReview.Review).Where(s => s.HotelId == hotel.HotelId).ToList();
                var bestServices = serviceReviewRatesService.OrderByDescending(s => s.ServiceReview.Review.Rating);

                
                entity.Hotels.Add(hotel);
                entity.Rooms.Add(bestRooms.Any() ? bestRooms.First().Room : null);
                entity.Breakfasts.Add(bestBreakfasts.Any() ? bestBreakfasts.First().Breakfast : null);
                entity.Services.Add(bestServices.Any() ? bestServices.First().Service : null);
            }

            var customers =
                _context.CustomerMakesReservationWithCreditCards.Include(r => r.Reservation).Include(c => c.Customer).OrderByDescending(p => p.Reservation.Amount).Select(c => c.Customer).ToList();
            customers = customers.GroupBy(c => c.Name).Select(y => y.First()).ToList();
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    entity.Customers.Add(customers[i]);
                }
                catch (Exception e)
                {
                    break;
                }
            }

            return entity;
        }
    }
}