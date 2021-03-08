using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunglassesMarket.Data;
using SunglassesMarket.Models;

namespace SunglassesMarket.Controllers
{
    public class CartController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private Cart _cart;

        public CartController(AppDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
            //var userId = _userManager.GetUserId(User);
            //_cart = _context.Carts.SingleOrDefault(x => x.UserId == userId);
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var userCart = await _context.Carts.Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .SingleOrDefaultAsync(x => x.UserId == userId);

            ViewBag.Total = 0;
            if (userCart != null && userCart.Items.Any())
                ViewBag.Total = user.Cart.Items.Sum(x => x.Product.Price * x.Quantity);
            return View(userCart);
        }

        private async Task<int> isInCart(int productId)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var userCart = await _context.Carts.Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .SingleOrDefaultAsync(x => x.UserId == userId);

            if (userCart == null)
                return -1;

            if (!userCart.Items.Any())
                return -1;
            
            for (int i = 0; i < userCart.Items.Count; ++i)
            {
                if (userCart.Items.ElementAt(i).Product.Id == productId)
                {
                    return i;
                }
            }

            return -1;
        }

        public async Task<IActionResult> Buy(int productId)
        {
            var product = _context.Products.SingleOrDefault(x => x.Id == productId);

            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var userCart = await _context.Carts.Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .SingleOrDefaultAsync(x => x.UserId == userId);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"Product with Id = {productId} cannot be found";
                return View("NotFound");
            }

            int idx = await isInCart(productId);

            if (userCart == null)
            {
                Cart newCart = new Cart();
                user.Cart = newCart;
                userCart = newCart;
                _context.Carts.Add(newCart);
            }

            if (!userCart.Items.Any() || idx == -1)
            {
                var item = new Item
                {
                    Product = product,
                    Quantity = 1,
                };

                //_cart.Items.Add(item);
                userCart.Items.Add(item);
            }
            else
            {
                //_cart.Items.ElementAt(idx).Quantity++;
                userCart.Items.ElementAt(idx).Quantity++;
            }

            //user.Cart = _cart;
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int productId)
        {
            var userId = _userManager.GetUserId(User);
            var user = await _userManager.FindByIdAsync(userId);
            var userCart = await _context.Carts.Include(x => x.Items)
                .ThenInclude(y => y.Product)
                .SingleOrDefaultAsync(x => x.UserId == userId);

            var product = await _context.Products.SingleOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                ViewBag.ErrorMessage = $"Product with Id = {productId} cannot be found";
                return View("NotFound");
            }

            int idx = await isInCart(productId);

            if (idx == -1)
            {
                ViewBag.ErrorMessage = $"Product with Id = {productId} cannot be found";
                return View("NotFound");
            }

            var item = userCart.Items.ElementAt(idx);

            if (item.Quantity <= 1)
            {
                userCart.Items.Remove(item);
                _context.Items.Remove(item);
            }
            else
            {
                item.Quantity--;
            }

            //user.Cart = userCart;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
