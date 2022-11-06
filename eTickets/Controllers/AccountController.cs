﻿using eTickets.Data;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _appDbContext;
        public AccountController( UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInMangager, AppDbContext appDbContext)
        {
            _userManager = userManager;
            _signInManager = signInMangager;
            _appDbContext = appDbContext;
        }
        public IActionResult Login()
        {
            var response = new LoginVM();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVm)
        {
            if(!ModelState.IsValid) return View(loginVm);

            var user = await _userManager.FindByEmailAsync(loginVm.EmailAddress);
            if (user != null)
            {
                 var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVm.Password);

                if(passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVm.Password, false, false);
                    if(result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVm);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVm);
        }
    }
}
