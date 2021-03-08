using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{

    public class Product
    {
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Designer { get; set; }

        [Required]
        public Type Type { get; set; }

        public DateTime CreationDate { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Photo { get; set; }

    }
}
