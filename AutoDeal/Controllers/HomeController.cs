using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoDeal.Models;
using System.Data.Entity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace AutoDeal.Controllers
{
    public class HomeController : Controller
    {
        private string userName;
        private AutoDealContext db;
        public HomeController(AutoDealContext context)
        {
            db = context;
            #region Testing picture converter
            //Queue<PicturesConverter> picturesConverters = new Queue<PicturesConverter>();
            //foreach (TestDeal item in db.TestDeals)
            //{
            //    picturesConverters.Enqueue(new PicturesConverter(item.Pictures));
            //}

            #endregion
        }
        public IActionResult Cars()
        {
            return View(db.TestDeals.ToList());
        }
        public IActionResult Profile()
        {
            return View(db.Users.FirstOrDefault(d => d.NickName == HttpContext.User.Identity.Name));
        }
        public IActionResult LogIn()
        {
            return View(); 
        }
        #region authentification
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LogIn(User model)
        {
            if (ModelState.IsValid)
            {
                User user =  db.Users.FirstOrDefault(u => u.NickName == model.NickName && u.Password == model.Password);
                if (user != null)
                {
                     Authenticate(model.NickName); // аутентификация 

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {   
            userName = HttpContext.User.Identity.Name;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("LogIn", "Home");
        }
        #endregion
        //TODO: make loginpost method
        public IActionResult GetUsers() 
        {
            return View( db.Users.ToList());
        }
        public IActionResult GetDeals()
        {
            return View( db.TestDeals.ToList());
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(User user)
        {
            db.Users.Add(user);
            await db.SaveChangesAsync();
            return RedirectToAction("GetUsers");
        }
        public IActionResult CreateDeal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDeal(TestDeal testDeal)
        {
            db.TestDeals.Add(testDeal);
            await db.SaveChangesAsync();
            return RedirectToAction("GetDeals"); // checking if new Deal appeared
        }
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
    }
}
