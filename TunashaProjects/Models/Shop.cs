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
    }

    public class Sale
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMail { get; set; }
        public int Quantity { get; set; }
        public string CustomerPhone { get; set; }
        public int ProductID { get; set; }
        public string Date { get; set; }
        public decimal Amount { get; set; }
        public PurchaseStatus Status { get; set; }
    }
}