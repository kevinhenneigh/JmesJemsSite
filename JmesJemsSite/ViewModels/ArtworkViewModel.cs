﻿using DynamicVML;
using JmesJemsSite.ViewModels;
using JmesJemsSite.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace JmesJemsSite.ViewModels
{
    public class ArtworkViewModel : EditImageViewModel
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
        [Display(Name = "Price")]
        public double Price { get; set; }

        public virtual DynamicList<MaterialViewModel> Materials { get; set; } = new DynamicList<MaterialViewModel>();
    }
}
