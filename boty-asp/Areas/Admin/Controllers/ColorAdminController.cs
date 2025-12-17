using boty_asp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Areas.Admin.Controllers;

public class ColorAdminController : Controller {
    // GET
    
    MyContext _context = new MyContext();
    
    [Area("Admin")]
    public IActionResult Index() {
        
        ListsColorSizeViewModel vm = new ListsColorSizeViewModel();
        
        vm.Colors = _context.Colors.ToList();
        vm.Sizes = _context.Sizes.ToList();
        
        return View(vm);
    }
    
    [Area("Admin")]
    public IActionResult DeleteColor(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var color = _context.Colors.Include(c => c.ProductVariants).FirstOrDefault(c => c.Id == id);

        if (color == null)
        {
            return NotFound();
        }

        return View(color);
    }
    
    [Area("Admin")]
    public IActionResult DeleteSize(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var size = _context.Sizes.Include(c => c.ProductVariants).FirstOrDefault(c => c.Id == id);

        if (size == null)
        {
            return NotFound();
        }

        return View(size);
    }
    
    [Area("Admin")]
    [HttpPost, ActionName("Delete1")]
    public IActionResult Delete1(int id)
    {
        var color = _context.Colors.Find(id);
    
        if (color != null)
        {
            _context.Colors.Remove(color); 
            _context.SaveChanges();
        }
    
        return RedirectToAction(nameof(Index));
    }
    
    [Area("Admin")]
    [HttpPost, ActionName("Delete2")]
    public IActionResult Delete2(int id)
    {
        var color = _context.Colors.Find(id);
    
        if (color != null)
        {
            _context.Colors.Remove(color); 
            _context.SaveChanges();
        }
    
        return RedirectToAction(nameof(Index));
    }
}