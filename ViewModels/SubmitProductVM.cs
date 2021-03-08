using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.ViewModels
{
    public class SubmitProductVM
    {
        public string Designer { get; set; }

        public string Brand { get; set; }

        public string Title { get; set; }

        public Models.Type Type { get; set; }

        public string Color { get; set; }

        public decimal Price { get; set; }

        public string ImageTitle { get; set; }

        public IFormFile ImageFile { get; set; }
    }
}
