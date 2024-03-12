using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
        public class VM_Category_List
        {
            public int CategoryId { get; set; }

            [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Category")]
            public string Title { get; set; }

        }
        public class VM_Category_Create
        {
            [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Category")]
            public string Title { get; set; }
        }
        public class VM_Category_Edit
        {
            public int CategoryId { get; set; }

            [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Category")]
            public string Title { get; set; }
        }
        public class VM_Category_Details
        {
            public int CategoryId { get; set; }

            [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
            [Display(Name = "Category")]
            public string Title { get; set; }

        }
    }