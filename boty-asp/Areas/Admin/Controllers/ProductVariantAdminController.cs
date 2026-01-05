using boty_asp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.ProjectModel;

namespace boty_asp.Areas.Admin.Controllers;

public class ProductVariantAdminController : Controller {
    
    MyContext _context =  new MyContext();
    
    // GET
    [Area("Admin")]
    public IActionResult Index(int? id) {

        var productvariants = _context.ProductVariants.Where(pv => pv.ProductId == id)
            .Include(pv => pv.Color)
            .Include(pv => pv.Size);

        ViewBag.Product = _context.Products.FirstOrDefault(p => p.Id == id);
        
        return View(productvariants);
    }

    [Area("Admin")]
    public IActionResult Delete(int? id) {
        var productvariant = _context.ProductVariants
            .Include(pv => pv.Color)
            .Include(pv => pv.Size)
            .FirstOrDefault(p => p.Id == id);
        
        return View(productvariant);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteFromDb(int id) {
        var pv = _context.ProductVariants.Find(id);
        
        if (pv != null) {
            _context.ProductVariants.Remove(pv);
            _context.SaveChanges();
        }
        
        return(RedirectToAction("Index", new { id = pv.ProductId}));
    }
    
    [Area("Admin")]
    public IActionResult Create(int productid) {
        
        var model = new ProductVariant { ProductId = productid };
        
        ViewBag.Colors = new SelectList(_context.Colors, "Id", "Name");
        ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "SizeValue");
        
        ViewBag.ProductId = productid;
        return View(model);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Create")]
    public async Task<IActionResult> CreateToDb(ProductVariant productvariant) {

        productvariant.Id = 0;
        
        _context.ProductVariants.Add(productvariant);
        await _context.SaveChangesAsync();
        
        return(RedirectToAction("Index", new { id = productvariant.ProductId }));
    }

    [Area("Admin")]
    public IActionResult Edit(int? id) {
        
        var productvariant = _context.ProductVariants.Find(id);
        
        ViewBag.Colors = new SelectList(_context.Colors, "Id", "Name");
        ViewBag.Sizes = new SelectList(_context.Sizes, "Id", "SizeValue");
        
        ViewBag.ProductId = productvariant.ProductId;
        
        return View(productvariant);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> EditToDatabase(ProductVariant productvariant) {
        
        _context.ProductVariants.Update(productvariant);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index", new { id = productvariant.ProductId });
    }
}