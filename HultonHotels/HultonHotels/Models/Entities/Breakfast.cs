using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class Breakfast
    {
        [Key]
        [Column(Order = 1)]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public Hotel Hotel { get; set; }

        [Key]
        [Column(Order = 2)]
        public string BreakfastType { get; set; }

        public decimal BreakfastCost { get; set; }
        public string Description { get; set; }

    }
}