using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
    public class VM_Order_Checkout
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(4)]
        public string ZipCode { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }
    }
}