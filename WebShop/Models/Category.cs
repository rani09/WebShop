using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public string Title { get; set; }

        public virtual ICollection<SubCategory> SubCategoies { get; set; }
    }
}