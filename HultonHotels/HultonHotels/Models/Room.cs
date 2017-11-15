using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class Room
    {
        [Key]
        [Column(Order = 1)]
        public int HotelId { get; set; }

        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }

        [Key]
        [Column(Order = 2)]
        public int RoomNo { get; set; }

        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }


    }
}