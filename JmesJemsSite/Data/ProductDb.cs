using JmesJemsSite.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Data
{
    public static class ProductDb
    {
        public static async Task<Products> GetProductAsync(ApplicationDbContext context, int id)
        {
            Products p = await (from products in context.Products
                                where products.ProductId == id
                                select products).SingleAsync();
            return p;
        }
    }
}
