using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunglassesMarket.Data;
using SunglassesMarket.Models;
using SunglassesMarket.ViewModels;

namespace SunglassesMarket.Controllers
{
    public class PurchaseController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        public PurchaseController(AppDbContext context,
                                  UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConfirmPurchase()
        {
            var order = new OrderVM();
            return View(order);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmPurchase(OrderVM model)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);

            if (ModelState.IsValid)
            {
                Order order = new Order()
                {
                    User = user,
                    Address = model.Address,
                    PhoneNumber = model.PhoneNumber,
                    Items = new List<Item>(),
                };

                _context.Orders.Add(order);


                var cart = _context.Carts.Include(x => x.Items)
                    .ThenInclude(y => y.Product)
                    .SingleOrDefault(x => x.UserId == userId);

                foreach (var item in cart.Items)
                {
                    Item newItem = new Item
                    {
                        Product = item.Product,
                        Quantity = item.Quantity,
                        Order = order,
                    };
                    _context.Items.Add(newItem);
                    order.Items.Add(newItem);
                }
                order.Cost = order.Items.Sum(x => x.Product.Price * x.Quantity);

                _context.Items.RemoveRange(cart.Items);
                user.Cart.Items.Clear();
                
                user.Orders.Add(order);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "Orders");
            }

            return View(model);
        }
    }
}
