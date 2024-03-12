using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebShop.Models
{
    public class OrderDetail
    {
        public enum OrderStatus
        {
            Error = 0,
            Created = 1,
            PaymentWindow = 2,
            Payed = 3,
            Done = 4,
            PaymentError = 5,
            Deleted = 6,
            Cancled = 7
        }

        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(100)]
        public string Address { get; set; }

        [Required, MaxLength(4)]
        public string ZipCode { get; set; }

        [Required, MaxLength(50)]
        public string City { get; set; }

        public string TransactionId { get; set; }
        public string PaymentLogMessage { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateDate { get; set; }

        public OrderDetail()
        {
            CreateDate = DateTime.Now;
            Status = OrderStatus.Created;
        }
    }
}