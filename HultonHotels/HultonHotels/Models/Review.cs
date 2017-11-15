using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int Ratin { get; set; }
        public string Comment { get; set; }
    }
}