using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Add(int id) // Id of the product to add
        {
            // Get product from database

            // Add product to cart cookie

            // redirect to previous page

            return View();
        }
        public IActionResult Summary()
        {
            // Display all products in shopping cart
            return View();
        }
    }
}
