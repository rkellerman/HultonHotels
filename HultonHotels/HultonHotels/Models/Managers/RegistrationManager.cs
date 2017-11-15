using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class RegistrationManager
    {
        private readonly ApplicationDbContext _context;

        public RegistrationManager()
        {
            _context = new ApplicationDbContext();
        }
    }
}