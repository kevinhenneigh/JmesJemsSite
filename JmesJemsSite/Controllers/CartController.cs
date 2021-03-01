using JmesJemsSite.Data;
using JmesJemsSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds an item to the shopping cart
        /// </summary>
        /// <param name="id"> Id of the product to add </param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id) 
        {
            // Get product from database
            Products p = await (from products in _context.Products
                                where products.ProductId == id
                                select products).SingleAsync();

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
