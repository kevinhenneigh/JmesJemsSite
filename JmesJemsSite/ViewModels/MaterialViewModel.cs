using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.ViewModels
{
    public class MaterialViewModel
    {
        public int MaterialId { get; set; }
        [Display(Name = "Material name")]
        public string Title { get; set; }
        [Display(Name = "Material type")]
        public string Category { get; set; }
    }
}
