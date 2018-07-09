﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Filtros.Viewmodels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Filtros.Controllers
{
    [Route("account")]
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.Name, vm.Username));
                var principal = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme, principal
                );
                return RedirectToAction("Index", "Home");
            }
            vm.ErrorMessage = "Not authorized";
            return View(vm);
        }
    }
}