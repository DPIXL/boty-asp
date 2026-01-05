using boty_asp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Areas.Admin.Controllers;

public class ProductAdminController : Controller {
    // GET
    
    MyContext _context = new MyContext();
    private readonly IWebHostEnvironment _webHostEnvironment;
    
    public ProductAdminController(IWebHostEnvironment env)
    {
        _webHostEnvironment = env;
    }
    
    [Area("Admin")]
    public IActionResult Index() {
        
        var products = _context.Products.ToList();
        
        return View(products);
    }

    [Area("Admin")]
    public IActionResult Delete(int? id) {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = _context.Products
            .Include(c => c.ProductVariants)
            .FirstOrDefault(c => c.Id == id);

        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }
    
    [Area("Admin")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteFromDb(int id) {
        
        var product = _context.Products.Find(id);
    
        if (product != null)
        {
            _context.Products.Remove(product); 
            _context.SaveChanges();
        }
        
        return(RedirectToAction(nameof(Index)));
    }

    [Area("Admin")]
    public IActionResult Create() {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        return View();
    }

    [Area("Admin")]
    [HttpPost, ActionName("Create")]
    public async Task<IActionResult> CreateToDb(Product product) {
        product.ImagesPath = $"~/dbimg/0"; //REMOVE LATER - TEMPORARY
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public IActionResult Edit(int? id) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        var product = _context.Products.Find(id);
        return View(product);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> EditToDb(Product product) {
        product.ImagesPath =  $"~/dbimg/0"; //REMOVE THIS WHEN U IMPLEMENT IMAGE UPLOAD
        _context.Products.Update(product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}