using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunglassesMarket.Data;
using SunglassesMarket.Models;

namespace SunglassesMarket.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class OrdersController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;

        public OrdersController(AppDbContext context,
                                UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            List<Order> orders = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Display(int orderId)
        {
            List<Item> items = await _context.Items.Include(x => x.Product).Where(y => y.OrderId == orderId).ToListAsync();
            List<Item> allItems = await _context.Items.Include(x => x.Product).ToListAsync();
            ViewBag.OrderId = orderId;
            ViewBag.Total = items.Sum(x => x.Product.Price * x.Quantity);

            return View(items);
        }
    }
}
