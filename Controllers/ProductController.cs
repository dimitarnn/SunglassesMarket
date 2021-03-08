using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SunglassesMarket.Data;
using SunglassesMarket.Models;
using SunglassesMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SunglassesMarket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            ProductVM model = new ProductVM
            {
                Products = await _context.Products.ToListAsync(),
                Product = new Product(),
                Image = new Image(),
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(SubmitProductVM model)
        {
            Product product = new Product
            {
                Designer = model.Designer,
                Brand = model.Brand,
                Title = model.Title,
                Type = model.Type,
                Color = model.Color,
                CreationDate = DateTime.Now,
                Price = model.Price,
            };

            Image image = new Image();

            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.ImageFile.FileName);
                string extension = Path.GetExtension(model.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                image.ImageName = fileName;
                image.ImageFile = model.ImageFile;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);

                product.Photo = fileName;
                image.Title = model.ImageTitle;

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }

                _context.Products.Add(product);
                _context.Images.Add(image);
                await _context.SaveChangesAsync();
                return Ok(product);
            }

            return View(model);
            
        }

        [AllowAnonymous]
        public async Task<IActionResult> Details(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            return View(product);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Display()
        {
            List<Product> products = await _context.Products.ToListAsync();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductImageVM product = new ProductImageVM();
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> SaveProduct(ProductImageVM productVM)
        {
            if (ModelState.IsValid)
            {
                productVM.Product.CreationDate = DateTime.Now;

                Product product = productVM.Product;
                Image image = productVM.Image;

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                image.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }

                product.Photo = fileName;

                _context.Products.Add(product);
                _context.Images.Add(image);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            ProductImageVM productVM = new ProductImageVM();
            productVM.Product = product;

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmEdit(ProductImageVM productVM)
        {
            if (ModelState.IsValid)
            {
                Product product = productVM.Product;
                Image image = productVM.Image;

                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(image.ImageFile.FileName);
                string extension = Path.GetExtension(image.ImageFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssff") + extension;
                image.ImageName = fileName;
                string path = Path.Combine(wwwRootPath + "/images/", fileName);

                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await image.ImageFile.CopyToAsync(fileStream);
                }

                product.Photo = fileName;

                _context.Images.Add(image);
                _context.Attach(product).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(productVM);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                return RedirectToAction("Index");
            }    

            return View(product);
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(int productId)
        {
            Product product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            // delete image also ?
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
