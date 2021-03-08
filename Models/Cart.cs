using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{
    public class Cart
    {
        public Cart()
        {
            Items = new List<Item>();
        }
        
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<Item> Items { get; set; }
        
    }
}
