using System.Data.Entity;
using HultonHotels.Models.Entities;
using HultonHotels.Models.Relationships;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HultonHotels.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        /*
         * Entities
         */

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Breakfast> Breakfasts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomReview> RoomReviews { get; set; }
        public DbSet<BreakfastReview> BreakfastReviews { get; set; }
        public DbSet<ServiceReview> ServiceReviews { get; set; }

        /*
         * Relationships
         */

        public DbSet<CustomerWritesReview> CustomerWritesReviews { get; set; }
        public DbSet<ReservationReservesRoom> ReservationReservesRooms { get; set; }
        public DbSet<CustomerMakesReservationWithCreditCard> CustomerMakesReservationWithCreditCards { get; set; }
        public DbSet<ReservationIncludesBreakfast> ReservationIncludesBreakfasts { get; set; }
        public DbSet<ReservationContainsService> ReservationContainsServices { get; set; }
        public DbSet<RoomReviewEvaluatesRoom> RoomReviewEvaluatesRooms { get; set; }
        public DbSet<BreakfastReviewAssessesBreakfast> BreakfastReviewAssessesBreakfasts { get; set; }
        public DbSet<ServiceReviewRatesService> ServiceReviewRatesServices { get; set; }



        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}