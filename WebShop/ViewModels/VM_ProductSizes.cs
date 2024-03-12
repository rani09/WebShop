using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.ViewModels
{
        public class VM_ProductSizes_List
        {
            [Key, Column(Order = 0)]
            public int ProductId { get; set; }

            public virtual Product Product { get; set; }

            [Key, Column(Order = 1)]
            public int SizeId { get; set; }

            public virtual Size Size { get; set; }
        }
        public class VM_ProductSizes_Create
        {
            [Key, Column(Order = 0)]
            public int ProductId { get; set; }

            public virtual Product Product { get; set; }

            [Key, Column(Order = 1)]
            public int SizeId { get; set; }

            public virtual Size Size { get; set; }
        }
        public class VM_ProductSizes_Edit
        {
            [Key, Column(Order = 0)]
            public int ProductId { get; set; }

            public virtual Product Product { get; set; }

            [Key, Column(Order = 1)]
            public int SizeId { get; set; }

            public virtual Size Size { get; set; }
        }
        public class VM_ProductSizes_Details
        {
            [Key, Column(Order = 0)]
            public int ProductId { get; set; }

            public virtual Product Product { get; set; }

            [Key, Column(Order = 1)]
            public int SizeId { get; set; }

            public virtual Size Size { get; set; }
        }
    }