using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public class Material
    {
        public int MaterialId { get; set; }

        public string Title { get; set; }
        public int JewelryId { get; set; }
    }
}
