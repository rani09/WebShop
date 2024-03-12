using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.ViewModels
{
        public class VM_Size_List
        {
            public int SizeId { get; set; }

            [Display(Name = "Størrelse")]
            public int Name { get; set; }
        }
        public class VM_Size_Create
        {
            [Display(Name = "Størrelse")]
            public int Name { get; set; }
        }
        public class VM_Size_Edit
        {
            public int SizeId { get; set; }

            [Display(Name = "Størrelse")]
            public int Name { get; set; }
        }
        public class VM_Size_Details
        {
            public int SizeId { get; set; }

            [Display(Name = "Størrelse")]
            public int Name { get; set; }
        }
    }