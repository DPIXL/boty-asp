using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Controllers;

using Microsoft.EntityFrameworkCore;
using Models;

public class WholeOfferController : Controller {
    
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index(int? id) { //Current Category Id, null = all
        
        var products = _context.Products.ToList();
        
        ViewBag.CurrentCategory = _context.Categories.Find(id);
        
        var path = new List<Category>();
        
        Category currentCat = _context.Categories.Include(c => c.Products).FirstOrDefault(c => c.Id == id);
        
        while (currentCat != null) {
            
            path.Add(currentCat);
            
            if (currentCat.ParentId != null && currentCat.ParentId != id) {
                _context.Entry(currentCat).Reference(c => c.Parent).Load();
            }
            
            currentCat = currentCat.Parent;
        }
        path.Reverse();
        
        ViewBag.Categories = path;
        
        return View();
    }
}
