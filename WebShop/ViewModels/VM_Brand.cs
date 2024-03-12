using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
        public class VM_Brand_List
        {
            public int BrandId { get; set; }

            [Required, MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Brand")]
            public string Name { get; set; }
        }
        public class VM_Brand_Create
        {
            [Required, MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Brand")]
            public string Name { get; set; }
        }
        public class VM_Brand_Edit
        {
            public int BrandId { get; set; }

            [Required, MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Brand")]
            public string Name { get; set; }
        }
        public class VM_Brand_Details
        {
            public int BrandId { get; set; }

            [Required, MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Brand")]
            public string Name { get; set; }
        }
    }