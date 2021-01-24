using JmesJemsSite.Models;
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
    public class Jewelry
    {
        public int JewelryId { get; set; }

        public Type Type { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public List<Material> Materials { get; set; }

        public double Price { get; set; }

    }
    
}
