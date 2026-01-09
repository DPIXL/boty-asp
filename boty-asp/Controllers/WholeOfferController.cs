using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace boty_asp.Controllers;

using Microsoft.EntityFrameworkCore;
using Models;

public class WholeOfferController : Controller {
    
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index(int? catId, int? colId, int? sizeId) {

        var products = _context.Products
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Color)
            .Include(p => p.ProductVariants).ThenInclude(pv => pv.Size)
            .AsQueryable();
        
        if (catId.HasValue)
        {
            products = products.Where(p => p.CategoryId == catId);
        }

        if (colId.HasValue && sizeId.HasValue)
        {
            products = products.Where(p => p.ProductVariants.Any(pv => 
                pv.ColorId == colId && pv.SizeId == sizeId));
        }
        else
        {
            if (colId.HasValue)
            {
                products = products.Where(p => p.ProductVariants.Any(pv => pv.ColorId == colId));
            }

            if (sizeId.HasValue)
            {
                products = products.Where(p => p.ProductVariants.Any(pv => pv.SizeId == sizeId));
            }
        }
        
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name", catId);
        ViewBag.Colors = new SelectList(_context.Colors, "Id", "Name", colId);
        ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "SizeValue", sizeId);
        
        return View(products.ToList());
    }
}
