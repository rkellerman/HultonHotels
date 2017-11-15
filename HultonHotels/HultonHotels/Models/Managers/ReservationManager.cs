using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class ReservationManager
    {
        private readonly ApplicationDbContext _context;

        public ReservationManager()
        {
            _context = new ApplicationDbContext();
        }
    }
}