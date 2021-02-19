using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public class Products
    {
        public int ProductId { get; set; }

        public string Category { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }

        public virtual List<Material> Materials { get; set; } = new List<Material>();
    }
}
