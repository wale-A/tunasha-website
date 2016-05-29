using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TunashaProjects.Misc;

namespace TunashaProjects.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public int PostedFileID { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public PostedFile Image { get; set; }
    }

    public class ProductViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Required, DataType(DataType.MultilineText)]
        public string Description { get; set; }
        [DataType(DataType.Upload)]
        public string ImageName { get; set; }
    }

    public class Order
    {
        public Order()
        {
            Details = new List<OrderDetail>();
        }
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string CustomerPhone { get; set; }
        public DateTime Date { get; set; }
        public string TransactionNumber { get; set; }
        public int UserID { get; set; }
        //public PurchaseStatus Status { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> Details { get; set; }
    }

    public class OrderDetail
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int OrderID { get; set; }
        public decimal Amount { get; set; }
        public string Date { get; set; }

        public virtual Product Product { get; set; }
        //public virtual Order Order { get; set; }
        //public PurchaseStatus Status { get; set; }
    }

    public class Cart
    {
        public int ID { get; set; }        
        public string Guid { get; set; }
        public int ProductID { get; set; }

        public virtual Product Product { get; set; }
    }
}