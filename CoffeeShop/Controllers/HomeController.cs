using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using CoffeeShop.Models;
using System.Text.Json;

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
            User ValidUser = new User();

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

        public IActionResult Login(string email = null, string password = null)
        {
            CoffeeShopContext db = new CoffeeShopContext();
            
            TempData["Registered"] = false;

            if (email != null && password != null)
            {
                bool valid = ValidLogin(email, password);

                if(valid)
                {
                    TempData["Registered"] = true;
                    User user = (User)TempData["User"];
                    return View("Shop",db);
                }
            }

            return View(db);
        }

        private bool ValidLogin(string email, string password)
        {
            //make a session Key Value pair User is the key, USERID is the value and must be string
            //HttpContext.Session.SetString("User", "user");

            //call get string to retrieve the value, as many times as you need, until the end of the session
            //HttpContext.Session.GetString("User");

            //this pulls the object data out as a string, and desearializes back into our object
            //User user = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("User"));

            CoffeeShopContext db = new CoffeeShopContext();

            User user = new User();

            TempData["Registered"] = false;

            foreach (User valid in db.User)
            {
                if (email == valid.Email && password == valid.Password)
                {
                    user = valid;
                    //make a session and hold a serialized object
                    HttpContext.Session.SetString("User", JsonSerializer.Serialize(user));
                    TempData["Registered"] = true;
                    return true;
                }
                //else
                //{
                //    return View("ModalAction");
                //}
            }

            return false;
        }

        public IActionResult Shop(User user)
        {
            CoffeeShopContext db = new CoffeeShopContext();
            //Items items = new Items();
            return View("Shop", db);
        }

        public IActionResult Purchase(decimal? price, User user)
        {
            CoffeeShopContext db = new CoffeeShopContext();

            user = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("User"));

            if (user.CartFunds >= price)
            {
                user.CartFunds = user.CartFunds - price;
                db.Update(user);
                db.SaveChanges();

            }
            //else if (foundUser.CartFunds <= price)
            //{
            //    return View();
            //}

            return View("Shop");
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
