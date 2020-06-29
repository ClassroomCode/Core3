using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using EComm.Web.Models;
using EComm.Data;

namespace EComm.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IRepository repository, ILogger<HomeController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var products = await _repository.GetAllProducts();
            //return Content($"Number of products: {products.Count()}");
            return Json(products);
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("error")]
        [HttpPost("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
