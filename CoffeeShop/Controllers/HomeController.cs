using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoffeeShop.Models;

namespace CoffeeShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;   

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Welcome(User user, string password2, string password)
        {
            CoffeeShopContext db = new CoffeeShopContext();
            //User ValidUser = new User();

            if (password != password2)
            {
                return ModalAction();
            }
            else
            {
                db.Add(user);
                db.SaveChanges();
            }
            return View();

        }

        public IActionResult Login(string email, string password)
        {
            CoffeeShopContext db = new CoffeeShopContext();

            User foundUser = new User();

            TempData["Registered"] = false;

            foreach (User valid in db.User)
            {
                if (email == valid.Email && password == valid.Password)
                {
                    foundUser = valid;
                    TempData["Registered"] = true;
                }
            }
            return View(foundUser);
        }


        public IActionResult ModalAction()
        {
            return View("ModalAction");
        }

        //need one action to load our Registration Page, also need a View

        public IActionResult Registration()
        {

            return View();
        }

        //need on action to take thoe user inputs, and display the user name in a new View
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
