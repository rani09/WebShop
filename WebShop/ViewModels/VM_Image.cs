using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.ViewModels
{
        public class VM_Image_List
        {
            public int ImageId { get; set; }

            [Display(Name = "Billed")]
            public string Images { get; set; }

            public int ProductId { get; set; }

            public virtual Product Products { get; set; }
        }
        public class VM_Image_Create
        {
            [Display(Name = "Billed")]
            public string Images { get; set; }

            public int ProductId { get; set; }

            public virtual Product Products { get; set; }
        }
        public class VM_Image_Edit
        {
            public int ImageId { get; set; }

            [Display(Name = "Billed")]
            public string Images { get; set; }

            public int ProductId { get; set; }

            public virtual Product Products { get; set; }
        }
        public class VM_Image_Details
        {
            public int ImageId { get; set; }

            [Display(Name = "Billed")]
            public string Images { get; set; }

            public int ProductId { get; set; }

            public virtual Product Products { get; set; }
        }
    }