using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public class Products
    {
        /// <summary>
        /// A salable product
        /// </summary>
        [Key]
        public int ProductId { get; set; }

        /// <summary>
        /// Category product is under EX. Jewelry
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Consumer facing name of product
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The retail price in US currency 
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// A list of materials used to make the product
        /// </summary>
        public virtual List<Material> Materials { get; set; } = new List<Material>();
    }
}
