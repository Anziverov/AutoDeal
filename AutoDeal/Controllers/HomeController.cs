using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoDeal.Models;
using System.Data.Entity;

namespace AutoDeal.Controllers
{
    public class HomeController : Controller
    {
        private AutoDealContext db;
        public HomeController(AutoDealContext context)
        {
            db = context;
        }
        public async Task<IActionResult> GetUsers() 
        {
            return View(await db.Users.ToListAsync());
        }
        public async Task<IActionResult> GetDeals()
        {
            return View(await db.Deals.ToListAsync());
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
            return RedirectToAction("GetUsers"); // checking if new User appeared
        }
        public IActionResult CreateDeal()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDeal(Deal deal)
        {
            db.Deals.Add(deal);
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
