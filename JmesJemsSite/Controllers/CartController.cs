using JmesJemsSite.Data;
using JmesJemsSite.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public CartController(ApplicationDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        /// <summary>
        /// Adds an item to the shopping cart
        /// </summary>
        /// <param name="id"> Id of the product to add </param>
        /// <returns></returns>
        public async Task<IActionResult> Add(int id)
        {
            Products p = await ProductDb.GetProductAsync( _context, id);

            // Add product to cart cookie
            string data = JsonConvert.SerializeObject(p);
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append("CartCookie", data, options);

            // redirect to previous page

            return RedirectToAction("Index", "Summary");
        }

        public IActionResult Summary()
        {
            // Display all products in shopping cart
            return View();
        }
    }
}
