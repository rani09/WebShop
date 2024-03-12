using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Rating
    {
        public int RatingId { get; set; }

        public int ratingValue { get; set; }

        public DateTime DateCreated { get; set; }

        public Rating()
        {
            DateCreated = DateTime.Now;
        }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
        

    }
}