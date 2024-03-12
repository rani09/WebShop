using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Image
    {
        public int ImageId { get; set; }

        public string Images { get; set; }

        public int ProductId { get; set; }

        public virtual Product Products { get; set; }
    }
}