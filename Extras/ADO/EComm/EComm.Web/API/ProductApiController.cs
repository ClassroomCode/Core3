using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EComm.Data;
using EComm.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EComm.Web.API
{
    [ApiController]
    public class ProductApiController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger<ProductApiController> _logger;

        public ProductApiController(IRepository repository,
                                    ILogger<ProductApiController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("api/products")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<Product>>> GetProducts()
        {
            var products = await _repository.GetAllProducts(includeSuppliers: true);
            return products.ToList();
        }

        [HttpGet("api/product/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _repository.GetProduct(id, true);
            if (product == null) return NotFound();
            return product;
        }

        [HttpPut("api/product/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            if (id != product.Id) return BadRequest();

            await _repository.SaveProduct(product);
            return NoContent();
        }
    }
}