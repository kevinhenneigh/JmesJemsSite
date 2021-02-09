using DynamicVML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    
    public class Jewelry
    {
        public int JewelryId { get; set; }
        [Display(Name = "Name of jewelry")]
        public string Title { get; set; }
        [Display(Name = "Type of jewelry")]
        public string Type { get; set; }
        [Display(Name = "Jewelry color")]
        public string Color { get; set; }
        [Display(Name = "Size")]
        public string Size { get; set; }
        [Display(Name = "Price")]
        public double Price { get; set; }
        //public string Image { get; set; }
        [Display(Name = "Material used")]
        public virtual List<Material> Materials { get; set; } = new List<Material>();


    }
}
