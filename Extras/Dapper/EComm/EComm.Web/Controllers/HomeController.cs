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
        public IActionResult Index()
        {
            return View();
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

        [HttpGet("clienterror")]
        [HttpPost("clienterror")]
        public IActionResult ClientError(int statusCode)
        {
            ViewBag.Message = statusCode switch
            {
                400 => "Bad Request (400)",
                404 => "Not Found (404)",
                418 => "I'm a teapot (418)",
                _ => $"Other ({statusCode})"
            };
            return View();
        }
    }
}
