using EComm.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace EComm.Web.Models
{
    public class ShoppingCart
    {
        public ShoppingCart()
        {
            LineItems = new List<LineItem>();
        }

        public List<LineItem> LineItems { get; set; }
        public string FormattedGrandTotal => $"{LineItems.Sum(i => i.TotalCost):C}";

        public class LineItem
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }
            public decimal TotalCost => Product.UnitPrice.Value * Quantity;
        }
    }
}
