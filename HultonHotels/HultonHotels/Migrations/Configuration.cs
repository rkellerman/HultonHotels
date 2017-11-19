using System.Collections.Generic;
using HultonHotels.Models;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json.Linq;

namespace HultonHotels.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HultonHotels.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        

        protected override void Seed(HultonHotels.Models.ApplicationDbContext context)
        {

            var serviceTypes = new List<string>
            {
                "Laundry",
                "Breakfast in bed",
                "Dry cleaning",
                "Massage and Spa",
                "Fitness Center"
            };

            var breakfastTypes = new List<string>
            {
                "American",
                "Mexican",
                "Meat Lover's",
                "Healthy",
                "Dessrt Lover's"
            };

            var breakfastDescriptions = new List<string>
            {
                "Typical American breakfast platter",
                "Spicy platter of eggs and vegetables",
                "Ham, turkey, and sausage",
                "Egg whites, grilled chicken slices, and fruit",
                "Ice cream, waffles or pancakes, and a smoothie"
            };

            var streets = new List<string>
            {
                "10 Vliet Drive",
                "22 Amwell Road",
                "40 Easton Avenue",
                "100 College Avenue",
                "1 Mine Street"

            };

            var cities = new List<string>
            {
                "New Brunswick",
                "Princeton",
                "Montgomery",
                "Hillsborough",
                "Trenton"
            };

            Random random = new Random();

            for (var j = 0; j < 5; j++)
            {
                Hotel hotel = new Hotel
                {
                    Street = streets[j],
                    City = cities[j],
                    Country = "United States",
                    State = "New Jersey",
                    Zip = random.Next(10000, 99999)
                };

                context.Hotels.Add(hotel);
                context.SaveChanges();

                for (var i = 0; i < 5; i++)
                {
                    var breakfast = new Breakfast
                    {
                        BreakfastType = breakfastTypes[i],
                        BreakfastCost = random.Next(10, 20),
                        Description = breakfastDescriptions[i],
                        Hotel = hotel,
                        HotelId = hotel.HotelId
                    };

                    context.Breakfasts.Add(breakfast);

                    var service = new Service
                    {
                        Hotel = hotel,
                        HotelId = hotel.HotelId,
                        ServiceCost = random.Next(20,40),
                        ServiceType = serviceTypes[i]
                    };

                    context.Services.Add(service);
                    context.SaveChanges();
                }

                for (var i = 0; i < 10; i++)
                {
                    Room room = new Room
                    {
                        Hotel = hotel,
                        Capacity = random.Next(1, 5),
                        Description = "Just a simple room",
                        Price = (decimal)random.Next(10, 100) + (decimal)i,
                        RoomNo = i + j*10,
                        Type = "Suite"
                    };

                    context.Rooms.Add(room);
                    context.SaveChanges();
                }

                


            }


            
        }
    }
}
