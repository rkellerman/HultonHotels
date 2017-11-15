using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HultonHotels.Models
{
    public class CustomerWritesReview
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("ReviewId")]
        public Review Review { get; set; }

        public int CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }


    }
}