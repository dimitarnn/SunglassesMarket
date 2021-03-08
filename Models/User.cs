using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            ProfilePic = "user_default.png";
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ProfilePic { get; set; }

        public Cart Cart { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
