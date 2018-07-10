using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Filtros.Models.Services;
using Filtros.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Filtros.Controllers
{
    [Route("account")]    
    public class AccountController : Controller
    {
        IAccountServices _accountServices;
        public AccountController(IAccountServices accountServices)
        {
            _accountServices = accountServices;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid && _accountServices.CheckCredentials(vm.Username, vm.Password))
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));
                foreach (var roleName in _accountServices.GetRolesForUser(vm.Username))
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
                }
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal
                );
                return RedirectToAction("Index", "Home");
            }
            vm.ErrorMessage = "Not authorized";
            return View(vm);
        }
        
        [Route("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}