using DynamicVML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{   
    public class Jewelry : Products
    {

        [Display(Name = "Type of jewelry")]
        public string Type { get; set; }

        [Display(Name = "Jewelry color")]
        public string Color { get; set; }

        [Display(Name = "Size")]
        public string Size { get; set; }

        [Display(Name = "Image of Jewelry")]
        public string JewelryImage { get; set; }

    }
}
