using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class MetricsObject
    {
        public DateTime StartDate { get; set; } = SqlDateTime.MinValue.Value;
        public DateTime EndDate { get; set; } = SqlDateTime.MaxValue.Value;

        public List<Room> Rooms { get; set; }
        public List<Customer> Customers { get; set; }

        public List<Breakfast> Breakfasts { get; set; }
        public List<Service> Services { get; set; }

        public List<Hotel> Hotels { get; set; }
    }
}