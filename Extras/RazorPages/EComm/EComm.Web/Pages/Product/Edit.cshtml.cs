using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EComm.Data;
using EComm.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using EE = EComm.Data.Entities;

namespace EComm.Web.Pages.Product
{
    public class EditModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly ILogger<EditModel> _logger;

        public EditModel(IRepository repository, ILogger<EditModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

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

        private IEnumerable<EE.Supplier> Suppliers { get; set; }

        public IEnumerable<SelectListItem> SupplierItems =>
            Suppliers?.Select(s => new SelectListItem { Text = s.CompanyName, Value = s.Id.ToString() })
            .OrderBy(item => item.Text);

        public async Task OnGet(int id)
        {
            var product = await _repository.GetProduct(id, includeSupplier: true);
            Id = product.Id;
            ProductName = product.ProductName;
            UnitPrice = product.UnitPrice;
            Package = product.Package;
            IsDiscontinued = product.IsDiscontinued;
            SupplierId = product.SupplierId;
            Suppliers = await _repository.GetAllSuppliers();
        }

        public async Task<IActionResult> OnPost(int id, EE.Product product)
        {
            if (!ModelState.IsValid) {
                Suppliers = await _repository.GetAllSuppliers();
                return Page();
            }
            await _repository.SaveProduct(product);
            return RedirectToPage("details", new { id = id });
        }
    }
}