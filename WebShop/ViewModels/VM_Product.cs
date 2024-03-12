using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebShop.Models;

namespace WebShop.ViewModels
{
    public class VM_Product_List
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "Product Navn")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Antal på lager")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Billed")]
        public string Image { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brands { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
    public class VM_Product_Create
    {
        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "Navn")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Antal på lager")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Billed")]
        public string Image { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brands { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
    public class VM_Product_Edit
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "Navn")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Antal på lager")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Billed")]
        public string Image { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brands { get; set; }


        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
    public class VM_Product_Details
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Skal Udfyldes"), MinLength(2, ErrorMessage = "Min 2 tegn"), MaxLength(20, ErrorMessage = "Max 20 tegn")]
        [Display(Name = "Navn")]
        public string Title { get; set; }

        [Required]
        [Display(Name = "Beskrivelse")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Pris")]
        public decimal Price { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Dato")]
        public DateTime Date { get; set; }

        [Required]
        [Display(Name = "Antal på lager")]
        public int Quantity { get; set; }

        [Required]
        [Display(Name = "Billed")]
        public string Image { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brands { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }
    }
}