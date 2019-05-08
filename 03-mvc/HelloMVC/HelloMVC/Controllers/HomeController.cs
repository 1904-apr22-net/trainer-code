using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using HelloMVC.Models;

namespace HelloMVC.Controllers
{
    //[Route("House")]
    // attribute routing is an alternative to
    // conventional routing, but for MVC, we like to use conventional routing.
    public class HomeController : Controller
    {
        // controllers have "action methods"
        // each action method is meant to handle a request to one path
        // it should return a ViewResult to be rendered into HTML
        // and sent as a HTTP response.

        // this Index method handles requests to the following paths:
        // - /
        // - /Home
        // - /Home/Index
        // (why? because of the conventional routing in Startup.)
        public IActionResult Index()
        {
            // this is a helper method from the parent class
            // "View()" searches for and returns the view which has the same
            // name as the current action method.

            // so this means: "get the Home/Index view and return it."
            // ("Home" because this is the HomeController.)
            //return View();

            // now that the Index view is strongly-typed for Timestamp
            //... i need to provide a value.
            var model = new Timestamp { Value = DateTime.Now };
            return View(model);
        }

        public IActionResult Privacy()
        {
            // "get the Home/Privacy" view and return it.
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
