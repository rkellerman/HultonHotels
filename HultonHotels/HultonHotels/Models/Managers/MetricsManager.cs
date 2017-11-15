using System;
using System.Collections.Generic;
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
    }
}