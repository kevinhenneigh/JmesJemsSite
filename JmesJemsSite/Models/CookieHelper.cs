using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{

    public static class CookieHelper
    {
        const string CartCookie = "CartCookie";

        /// <summary>
        /// Returns current list of cart products
        /// If cart is empty an empty list will be returned
        /// </summary>
        /// <param name="http"></param>
        /// <returns>An empty list if cart is empty</returns>
        public static List<Products> GetCartProducts(IHttpContextAccessor http)
        {
            // Get existing cart items
            string existingItems = http.HttpContext.Request.Cookies[CartCookie];

            List<Products> cartProducts = new List<Products>();

            // Are there items in the customers cart already
            if (existingItems != null)
            {
                cartProducts = JsonConvert.DeserializeObject<List<Products>>(existingItems);
            }

            return cartProducts;
        }

        /// <summary>
        /// Adds an item to the shopping cart when button is clicked
        /// </summary>
        /// <param name="p">The item added to the shopping cart</param>
        public static void AddProductToCart(IHttpContextAccessor http, Products p)
        {
            List<Products> cartProducts = GetCartProducts(http);
            cartProducts.Add(p);

            string data = JsonConvert.SerializeObject(cartProducts);

            // Holds items in cart for 1 year for existing customers
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        } 

        /// <summary>
        /// Removes a single item from the cart on button click
        /// </summary>
        /// <param name="p">The product to remove from the shopping cart</param>
        public static void RemoveProductFromCart(IHttpContextAccessor http, Products p)
        {
            List<Products> cartProducts = GetCartProducts(http);
            Products product = cartProducts.Where(prod => prod.ProductId == p.ProductId).First();
            
            cartProducts.Remove(product);

            string data = JsonConvert.SerializeObject(cartProducts);

            // Holds items in cart for 1 year for existing customers
            CookieOptions options = new CookieOptions()
            {
                Expires = DateTime.Now.AddYears(1),
                Secure = true,
                IsEssential = true
            };

            http.HttpContext.Response.Cookies.Append(CartCookie, data, options);
        }

        /// <summary>
        /// Gets the item total in the shopping cart
        /// Updates next to the shopping cart icon every 
        /// time a product is added or removed from the cart
        /// </summary>
        /// <returns>The number of items in the shopping cart</returns>
        public static int GetTotalCartProducts(IHttpContextAccessor http)
        {
            List<Products> cartProducts = GetCartProducts(http);
            return cartProducts.Count;
        }
    }
}
