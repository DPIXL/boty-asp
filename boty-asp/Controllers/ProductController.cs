using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Controllers;

public class ProductController : Controller
{
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index(int? id, int? colorId) {
        
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
            
                Disabled = colorId.HasValue && !sizeList.Contains(s)
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
        
        return View(prod);
    }
    
    [HttpPost]
    public IActionResult AddToOrder(int colId, int sizeId) {
        Console.WriteLine(colId + " " + sizeId);
        var pv = _context.ProductVariants.FirstOrDefault(p => p.ColorId == colId && p.SizeId == sizeId);
        
        return RedirectToAction("Index", "Order", new { id = pv.Id });
    }
}