using DynamicVML;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JmesJemsSite.Models
{
    public class Artwork
    {
        public int ArtId { get; set; }
        [Display(Name = "Name of artwork")]
        public string Title { get; set; }
        [Display(Name = "Type of Art")]
        public string Type { get; set; }
        [Display(Name = "Length")]
        public double Length { get; set; }
        [Display(Name = "Width")]
        public double Width { get; set; }
        [Display(Name = "Materials used")]
        public virtual List<Material> Materials { get; set; } = new List<Material>();
    }
}
