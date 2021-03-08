using SunglassesMarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }

        public Image Image { get; set; }

        public List<Product> Products { get; set; }
    }
}
