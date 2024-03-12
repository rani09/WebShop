using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class Todolist
    {
        public int TodolistId { get; set; }
        public string Description { get; set; }
        public bool IsDone { get; set; }
    }
}