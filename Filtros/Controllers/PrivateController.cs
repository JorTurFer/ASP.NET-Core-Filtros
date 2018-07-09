using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Filtros.Controllers
{
    [Route("private")]
    [Authorize]
    public class PrivateController : Controller
    {
        public IActionResult Index()
        {
            return Content("Private index");
        }

        [Route("admin")]
        [Authorize(Roles = "admin")]
        public IActionResult Admin()
        {
            return Content("Only Admins");
        }

        [Route("four")]
        [Authorize(Policy = "FourCharacters")]
        public IActionResult four()
        {
            return Content("Four letters only");
        }

        
    }
}