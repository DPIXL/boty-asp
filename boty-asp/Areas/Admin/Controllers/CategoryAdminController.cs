using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Areas.Admin.Controllers;

public class CategoryAdminController : Controller {
    
    MyContext _context = new MyContext();
    
    // GET
    [Area("Admin")]
    public IActionResult Index() {
        
        var categories = _context.Categories.ToList();
        
        return View(categories);
    }
}