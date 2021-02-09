using JmesJemsSite.Models;
using DynamicVML;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace JmesJemsSite.ViewModels
{
    
    public class JewelryViewModel
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
        //public IFormFile JewelryImage { get; set; }
        [Display(Name = "Materials used")]
        public virtual DynamicList<MaterialViewModel> Materials { get; set; } = new DynamicList<MaterialViewModel>();


    }
    
}
