using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HultonHotels.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CreditCard> CreditCards { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Breakfast> Breakfasts { get; set; }
        public DbSet<Reservation> Reservations { get; set; }


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