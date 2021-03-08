using SunglassesMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.ViewModels
{
    public class ProductImageVM
    {
        public ProductImageVM()
        {
            Product = new Product();
            Image = new Image();
        }
        
        public Product Product { get; set; }

        public Image Image { get; set; }

    }
}
