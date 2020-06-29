using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using EE = EComm.Data.Entities;

namespace EComm.Web.Pages.Product
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository _repository;
        private readonly ILogger<DetailsModel> _logger;

        public DetailsModel(IRepository repository, ILogger<DetailsModel> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public EE.Product Product { get; set; }

        public async Task<IActionResult> OnGet(int id)
        {
            var product = await _repository.GetProduct(id, includeSupplier: true);
            if (product == null) return NotFound();
            Product = product;
            return Page();
        }
    }
}