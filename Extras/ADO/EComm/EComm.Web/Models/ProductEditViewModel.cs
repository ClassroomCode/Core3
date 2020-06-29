using EComm.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.Models
{
    public class ProductEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A product must have a name")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "A product must have a price")]
        [Range(0.0, 500.0, ErrorMessage = "Price of a product must be between 0 and 500")]
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public IEnumerable<Supplier> Suppliers { get; set; }

        public IEnumerable<SelectListItem> SupplierItems =>
            Suppliers?.Select(s => new SelectListItem { Text = s.CompanyName, Value = s.Id.ToString() })
            .OrderBy(item => item.Text);
    }
}
