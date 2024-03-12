using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Size
    {
        public int SizeId { get; set; }

        public int Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}