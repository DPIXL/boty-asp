using boty_asp.Areas.Admin.ViewModels;
using boty_asp.Models;
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
        var size = _context.Sizes.Find(id);
    
        if (size != null)
        {
            _context.Sizes.Remove(size); 
            _context.SaveChanges();
        }
    
        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public IActionResult CreateColor() {
        return View();
    }

    [Area("Admin")]
    [HttpPost, ActionName("Create1")]
    public async Task<IActionResult> CreateColorToDb(Color color) {
        _context.Colors.Add(color);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [Area("Admin")]
    public IActionResult CreateSize() {
        return View();
    }

    [Area("Admin")]
    [HttpPost, ActionName("Create2")]
    public async Task<IActionResult> CreateSizeToDb(Size size) {
        _context.Sizes.Add(size);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public IActionResult EditColor(int? id) {
        var col = _context.Colors.Find(id);
        return View(col);
    }

    [Area("Admin")]
    [HttpPost,  ActionName("Edit1")]
    public async Task<IActionResult> EditColorToDb(Color color) {
        _context.Colors.Update(color);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    
    [Area("Admin")]
    public IActionResult EditSize(int? id) {
        var size = _context.Sizes.Find(id);
        return View(size);
    }

    [Area("Admin")]
    [HttpPost,  ActionName("Edit2")]
    public async Task<IActionResult> EditSizeToDb(Size size) {
        _context.Sizes.Update(size);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}