using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.ViewModels
{
    public class VM_SubCategory_List
    {
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public class VM_SubCategory_Create
    {
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public class VM_SubCategory_Details
    {
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
    public class VM_SubCategory_Edit
    {
        public int SubCategoryId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "SubCategory")]
        public string Name { get; set; }

        public string Description { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
    }
}