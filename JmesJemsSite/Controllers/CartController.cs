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

            CookieHelper.AddProductToCart(_httpContext, p);

            // redirect to previous page
            return RedirectToAction("Index", "Products");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>A view of all products in the cart</returns>
        public IActionResult Summary()
        {
           return View(CookieHelper.GetCartProducts(_httpContext));
        }
    }
}
