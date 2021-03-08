using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public decimal Cost { get; set; }

        public ICollection<Item> Items;
    }
}
