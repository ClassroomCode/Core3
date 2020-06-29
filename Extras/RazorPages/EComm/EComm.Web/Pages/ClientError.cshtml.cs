using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace EComm.Web.Pages
{
    public class ClientErrorModel : PageModel
    {
        public string Message { get; set; }

        private readonly ILogger<ErrorModel> _logger;

        public ClientErrorModel(ILogger<ErrorModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(int statusCode)
        {
            Message = statusCode switch
            {
                400 => "Bad Request (400)",
                404 => "Not Found (404)",
                418 => "I'm a teapot (418)",
                _ => $"Other ({statusCode})"
            };
        }
    }
}
