using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{
    public class Image
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Image Title")]
        public string Title { get; set; }

        public string ImageName { get; set; }

        [Required]
        [NotMapped]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }

    }
}
