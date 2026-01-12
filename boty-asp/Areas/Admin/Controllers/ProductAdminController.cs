using boty_asp.Areas.Admin.ViewModels;
using boty_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Areas.Admin.Controllers;

[Authorize(Policy = "AdminAccess")]

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
            string oldPathRelative = product.ImagesPath;

            if (!string.IsNullOrEmpty(oldPathRelative) && !oldPathRelative.Contains("0.png"))
            {

                string fileName = Path.GetFileName(oldPathRelative);
                
                string oldPathPhysical = Path.Combine(_webHostEnvironment.WebRootPath, "dbimg", fileName);
                
                if (System.IO.File.Exists(oldPathPhysical))
                {
                    System.IO.File.Delete(oldPathPhysical);
                }
            }
            
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
    public async Task<IActionResult> CreateToDb(ProductViewModel pwm) {
        
        if (pwm.ImageUpload != null)
        {
            
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "dbimg");
            
            string uniqueName = Guid.NewGuid().ToString() + ".png";
            
            string fullPhysicalPath = Path.Combine(folderPath, uniqueName);
            
            using (var fileStream = new FileStream(fullPhysicalPath, FileMode.Create))
            {
                await pwm.ImageUpload.CopyToAsync(fileStream);
            }
            
            pwm.Product.ImagesPath = "~/dbimg/" + uniqueName;
        }
        else {
            pwm.Product.ImagesPath = "~/dbimg/0.png";
        }
        
        _context.Products.Add(pwm.Product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public IActionResult Edit(int? id) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        var product = _context.Products.Find(id);
        ProductViewModel productViewModel = new ProductViewModel();
        productViewModel.Product = product;
        return View(productViewModel);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> EditToDb(ProductViewModel pwm) {
        
        if (pwm.ImageUpload != null)
        {
            string oldPathRelative = pwm.Product.ImagesPath;

            if (!string.IsNullOrEmpty(oldPathRelative) && !oldPathRelative.Contains("0.png"))
            {

                string fileName = Path.GetFileName(oldPathRelative);
                
                string oldPathPhysical = Path.Combine(_webHostEnvironment.WebRootPath, "dbimg", fileName);
                
                if (System.IO.File.Exists(oldPathPhysical))
                {
                    System.IO.File.Delete(oldPathPhysical);
                }
            }
            
            
            string folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "dbimg");
            
            string uniqueName = Guid.NewGuid().ToString() + ".png";
            
            string fullPhysicalPath = Path.Combine(folderPath, uniqueName);
            
            using (var fileStream = new FileStream(fullPhysicalPath, FileMode.Create)) {
                await pwm.ImageUpload.CopyToAsync(fileStream);
            }
            
            pwm.Product.ImagesPath = "~/dbimg/" + uniqueName;
        }
        
        _context.Products.Update(pwm.Product);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}