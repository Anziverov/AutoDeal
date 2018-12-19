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
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;

namespace AutoDeal.Controllers
{
    public class HomeController : Controller
    {
        private string userName;
        private AutoDealContext db;
        public HomeController(AutoDealContext context)
        {
            db = context;
        }
        public IActionResult SearchDeals(string searchString)
        {
            Regex regex = new Regex(searchString);
            List<TestDeal> searchResult = new List<TestDeal>();
            foreach (TestDeal item in db.TestDeals.ToList())
            {
                if(regex.IsMatch(item.Header))
                {
                    searchResult.Add(item);
                }
                    
            }
            return View("Cars",searchResult);
        }
        public IActionResult DealDetails(int id)
        {
            ViewData["ContactPhone"] = (db.Users.FirstOrDefault(user => user.Id == (db.TestDeals.FirstOrDefault(testDeal => testDeal.Id == id).Owner))).PhoneNumber; //TODO: need to check if new var will be faster
            return View(db.TestDeals.FirstOrDefault(d => d.Id == id));
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
        public async Task<IActionResult> CreateDeal(TestDeal testDeal,IFormFile[] photos)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                if (photos != null || photos.Length != 0)
                {
                    //Stringuilder or just string?

                    string pathString = "";
                    foreach (IFormFile photo in photos)
                    {
                        var writePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/Машины/", testDeal.Header, photo.FileName);
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/Машины/", testDeal.Header));// НЕ СЛИШКОМ ЛИ КОСТЫЛЬНО?
                        Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/Машины/", "Miniatures", testDeal.Header ));
                        var stream = new FileStream(writePath, FileMode.Create);
                        using (stream)
                        {
                            await photo.CopyToAsync(stream); // DO I NEED AWAIT AND USING HERE? 
                        }
                        PictureMiniatureConverter.Convert(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images/Машины/", "Miniatures", testDeal.Header, photo.FileName), writePath, 80, 80);
                        pathString += testDeal.Header + @"/" + photo.FileName + @";";
                    }
                    testDeal.Pictures = pathString;

                    testDeal.Owner = db.Users.FirstOrDefault(d => d.NickName == HttpContext.User.Identity.Name).Id;
                
               

                }

                db.TestDeals.Add(testDeal);
                await db.SaveChangesAsync();
            }
            return RedirectToAction("Cars"); // checking if new Deal appeared

        }
        public IActionResult Index()
        {
            return RedirectToAction("Cars");
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
