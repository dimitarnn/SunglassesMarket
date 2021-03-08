using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{
    public class Item
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

#nullable enable

        public int? CartId { get; set; }

        public Cart? Cart { get; set; }

        public int? OrderId { get; set; }

        public Order? Order { get; set; }
#nullable disable

    }
}
