using JmesJemsSite.Models;
using DynamicVML;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.ViewModels
{
    public enum Type
    {
        Necklace, Bracelete
    }
    public class JewelryViewModel
    {
        public int JewelryId { get; set; }

        public Type Type { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public DynamicList <Material> Materials { get; set; }

        public double Price { get; set; }
        public IFormFile JewelryImage { get; set; }

    }
    
}
