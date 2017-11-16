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

        public List<Customer> Get(Customer Entity)
        {
            var ret = CreateData();

            ret = ret.FindAll(c => c.Email.ToLower().Contains(Entity.Email.ToLower()));

            return ret;
        }

        public Customer Get(int id)
        {
            var ret = CreateData().Find(c => c.CustomerId == id);
            return ret;
        }

        public bool Insert(Customer Entity)
        {
            _context.Customers.Add(Entity);
            _context.SaveChanges();
            return true;

        }

        public bool Update(string prevEventArgument, Customer Entity)
        {
            return true;
        }

        private List<Customer> CreateData()
        {
            return _context.Customers.ToList();
        }
    }

}