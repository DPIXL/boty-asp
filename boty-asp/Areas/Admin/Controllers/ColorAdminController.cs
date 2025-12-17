using boty_asp.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Mvc;

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
        
        var color = _context.Colors.FirstOrDefault(c => c.Id == id);

        if (color == null)
        {
            return NotFound();
        }

        return View(color);
    }
    
    [Area("Admin")]
    [HttpPost, ActionName("Delete")]
    public IActionResult Delete(int id)
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