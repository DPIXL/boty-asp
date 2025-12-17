using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Areas.Admin.Controllers;

public class ProductAdminController : Controller {
    // GET
    
    MyContext _context = new MyContext();
    
    [Area("Admin")]
    public IActionResult Index() {
        
        var products = _context.Products.ToList();
        
        return View(products);
    }
}