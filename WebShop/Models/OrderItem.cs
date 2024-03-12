using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class OrderItem
    {
        public int ID { get; set; }
        public int ProductId { get; set; }
        public int? VariantID { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal
        {
            get
            {
                return (this.Price * this.Quantity);
            }
        }
    }
}