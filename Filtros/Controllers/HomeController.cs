using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Filtros.Models;
using Filtros.Extensions.Filters;

namespace Filtros.Controllers
{
    public class HomeController : Controller
    {
        [ResponseCache(Duration = 10)]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [SimpleCache]
        public IActionResult GetTime()
        {
            return Content(DateTime.Now.ToString("T"));
        }
    }
}
