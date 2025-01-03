﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text.RegularExpressions;
using AllUpProject.Models;
using AllUpProject.Utils.Enums;

using AllUpProject.ViewModels;

namespace AllUpProject.Controllers

{
    
        public class AccountController : Controller
        {
            private readonly UserManager<AppUser> _usermanager;
            private readonly SignInManager<AppUser> _sighinManeger;
            private readonly RoleManager<IdentityRole> _roleManager;

            public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, RoleManager<IdentityRole> roleManager)
            {
                _usermanager = userManager;
                _sighinManeger = signInManager;
                _roleManager = roleManager;
            }
            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]

            public async Task<IActionResult> Register(RegisterVM userVM)
            {
                if (!ModelState.IsValid) return View();

                string regex = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                if (Regex.IsMatch(userVM.Email, regex))
                {
                    ModelState.AddModelError(userVM.Email, "Email is not Valid");
                }

                AppUser user = new AppUser
                {
                    UserName = userVM.UserName,
                    Email = userVM.Email,
                    Surname = userVM.Surname,
                    Name = userVM.Name,

                };

                IdentityResult result = await _usermanager.CreateAsync(user, userVM.Password);
                if (!result.Succeeded)
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View();
                }

                await _usermanager.AddToRoleAsync(user, UserRole.Member.ToString());
                await _sighinManeger.SignInAsync(user, false);

                return RedirectToAction(nameof(HomeController.Index), "Home");


            }
            public IActionResult Login()
            {
                return View();
            }


            [HttpPost]

            public async Task<IActionResult> Login(LoginVM userVM, string? returnUrl)
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                AppUser user = _usermanager.Users.FirstOrDefault(u => u.UserName == userVM.EmailOrUserName || u.Email == userVM.EmailOrUserName);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Email or Username is incoorect");
                    return View();
                }

                var signResult = await _sighinManeger.PasswordSignInAsync(user, userVM.Password, userVM.IsPersistent, true);

                if (signResult.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account is Locked please try again later");
                    return View();
                }
                if (!signResult.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Email or Username is incoorect");
                    return View();
                }

                if (returnUrl is null)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                return Redirect(returnUrl);
         

            }

            public async Task<IActionResult> LogOut()
            {
                await _sighinManeger.SignOutAsync();

                return RedirectToAction(nameof(HomeController.Index), "Home");
            }

        }
    }
