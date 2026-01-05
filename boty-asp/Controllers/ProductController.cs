using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Controllers;

public class ProductController : Controller
{
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index(int? id) {
        Product prod = _context.Products
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Size)
            .Include(p => p.Category).ThenInclude(c => c.Parent)
            .FirstOrDefault(p => p.Id == id);
        
        var path = new List<Category>();
        
        var currentCat = prod.Category;
        
        while (currentCat != null)
        {
            path.Add(currentCat);
            
            if (currentCat.ParentId != null && currentCat.ParentId != id)
            {
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
}