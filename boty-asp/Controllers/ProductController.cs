using boty_asp.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;
using boty_asp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Controllers;

public class ProductController : Controller
{
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index(int? id, int? colorId) {
        
        ViewBag.AddToOrderState = false;
        
        var prod = _context.Products
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Size)
            .Include(p => p.Category).ThenInclude(c => c.Parent)
            .FirstOrDefault(p => p.Id == id);
        
        if (prod == null) return NotFound();

        if (prod.ProductVariants.Any()) {
            var colorList = prod.ProductVariants.Select(pv => pv.Color).DistinctBy(c => c.Id).ToList();
            ViewBag.DistinctColors = colorList;
            int selectedColorId = colorId ?? colorList.FirstOrDefault().Id;
            ViewBag.SelectedColorId = selectedColorId;
        
            var allSizeList = prod.ProductVariants.Select(pv => pv.Size).DistinctBy(c => c.Id).ToList();
        
            var sizeList = prod.ProductVariants.Where(pv => pv.ColorId == selectedColorId).Select(pv => pv.Size).DistinctBy(c => c.Id).ToList();
        
            ViewBag.SizeList = allSizeList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SizeValue.ToString(),
            
                Disabled = !sizeList.Contains(s)
            }).ToList();
        }
        
        var path = new List<Category>();
        
        var currentCat = prod.Category;
        
        while (currentCat != null)  {
            path.Add(currentCat);
            
            if (currentCat.ParentId != null && currentCat.ParentId != id) {
                _context.Entry(currentCat).Reference(c => c.Parent).Load();
            }
            
            currentCat = currentCat.Parent;
        }
        path.Reverse();
        
        ViewBag.Categories = path;

        List<Product> recommended = new List<Product>();

        for (int i = 0; i < 4; i++) {
            int count = _context.Products.Count();

            int index = new Random().Next(count);
            
            var randomProduct = _context.Products
                .Skip(index)
                .FirstOrDefault();

            if (randomProduct != null) {
                recommended.Add(randomProduct);
            }
        }

        ViewBag.Recommended = recommended;
        
        ProductViewModel pwm = new ProductViewModel() {
            ProductId = prod.Id,
            Product = prod,
        };
        
        return View(pwm);
    }
    
    [HttpPost]
    public IActionResult AddToOrder(ProductViewModel pwm) {
        if (!pwm.ColorId.HasValue) {
            ViewBag.Message = "Prosím vyberte barvu.";
        }
        if (!pwm.SizeId.HasValue) {
            ViewBag.Message = "Prosím vyberte velikost.";
        }
        
        pwm.Product = _context.Products
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Size)
            .Include(p => p.Category).ThenInclude(c => c.Parent)
            .FirstOrDefault(p => p.Id == pwm.ProductId);
        
        if (pwm.Product.ProductVariants.Any()) {
            var colorList = pwm.Product.ProductVariants.Select(pv => pv.Color).DistinctBy(c => c.Id).ToList();
            ViewBag.DistinctColors = colorList;
            int selectedColorId = pwm.ColorId ?? colorList.FirstOrDefault().Id;
            ViewBag.SelectedColorId = selectedColorId;
        
            var allSizeList = pwm.Product.ProductVariants.Select(pv => pv.Size).DistinctBy(c => c.Id).ToList();
        
            var sizeList = pwm.Product.ProductVariants.Where(pv => pv.ColorId == selectedColorId).Select(pv => pv.Size).DistinctBy(c => c.Id).ToList();
        
            ViewBag.SizeList = allSizeList.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.SizeValue.ToString(),
            
                Disabled = !sizeList.Contains(s)
            }).ToList();
        }
        
        var path = new List<Category>();
        
        var currentCat = pwm.Product.Category;
        
        while (currentCat != null)  {
            path.Add(currentCat);
            
            if (currentCat.ParentId != null && currentCat.ParentId != pwm.Product.Id) {
                _context.Entry(currentCat).Reference(c => c.Parent).Load();
            }
            
            currentCat = currentCat.Parent;
        }
        path.Reverse();
        
        ViewBag.Categories = path;

        List<Product> recommended = new List<Product>();

        for (int i = 0; i < 4; i++) {
            int count = _context.Products.Count();

            int index = new Random().Next(count);
            
            var randomProduct = _context.Products
                .Skip(index)
                .FirstOrDefault();

            if (randomProduct != null) {
                recommended.Add(randomProduct);
            }
        }

        ViewBag.Recommended = recommended;

        if (!pwm.ColorId.HasValue || !pwm.SizeId.HasValue) {
            ViewBag.AddToOrderState = false;
            return View("Index", pwm);
        }
        
        
        var pv = _context.ProductVariants.FirstOrDefault(pv => pv.ColorId == pwm.ColorId && pv.SizeId == pwm.SizeId);
        
        List<int> pvIds = HttpContext.Session.GetObject<List<int>>("Cart") ?? new List<int>();
        
        HttpContext.Session.SetObject("Cart", pvIds);
        
        pvIds.Add(pv.Id);
        
        HttpContext.Session.SetObject("Cart", pvIds);
        
        ViewBag.AddToOrderState = true;
        ViewBag.Message = "Přidáno do košíku!";
        return View("Index", pwm);
    }
}