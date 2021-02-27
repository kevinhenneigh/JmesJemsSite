using DynamicVML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public class Artwork : Products
    {
        
        [Display(Name = "Type of Art")]
        public string Type { get; set; }

        [Display(Name = "Length")]
        public double Length { get; set; }

        [Display(Name = "Width")]
        public double Width { get; set; }

        [Display(Name = "Artwork Image")]
        public string ArtImage { get; set; }


    }
}
