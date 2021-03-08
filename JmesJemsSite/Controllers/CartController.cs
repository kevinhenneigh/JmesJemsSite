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

            const string CartCookie = "CartCookie";

            // Get existing cart items
            string existingItems = _httpContext.HttpContext.Request.Cookies[CartCookie];

            
            List<Products> cartProducts = new List<Products>();

            // Are there items in the customers cart already
            if (existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Products>>(existingItems);
            }

            // Add current product to existing cart
            cartProducts.Add(p);

            // Add products list to cart cookie
            string data = JsonConvert.SerializeObject(cartProducts);

            // Holds items in cart for 1 year for existing customers
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            _httpContext.HttpContext.Response.Cookies.Append(CartCookie, data, options);

            // redirect to previous page
            return RedirectToAction("Index", "Summary");
        }

        public IActionResult Summary()
        {
            string cookieData = _httpContext.HttpContext.Request.Cookies["CartCookie"];

            List<Products> cartProducts = JsonConvert.DeserializeObject<List<Products>>(cookieData);

            return View(cartProducts);
        }
    }
}
