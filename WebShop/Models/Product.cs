using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Product
    {
        public Product()
        {
            Date = DateTime.Now;
            Active = true;
        }
        public int ProductId { get; set; }

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public string Image { get; set; }

        public bool Active { get; set; }

        public int BrandId { get; set; }

        public virtual Brand Brands { get; set; }

        public int SubCategoryId { get; set; }

        public virtual SubCategory SubCategory { get; set; }

        public virtual ICollection<Image> Images { get; set; }

        public virtual ICollection<Size> Size { get; set; }

        public virtual ICollection<Rating> Rating { get; set; }
    }
}