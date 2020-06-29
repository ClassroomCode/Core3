using EComm.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EComm.Web.ViewComponents
{
    public class ProductList : ViewComponent
    {
        private readonly IRepository _repository;
        private readonly ILogger<ProductList> _logger;

        public ProductList(IRepository repository, ILogger<ProductList> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = await _repository.GetAllProducts(includeSuppliers: true);
            return View(products);
        }
    }
}
