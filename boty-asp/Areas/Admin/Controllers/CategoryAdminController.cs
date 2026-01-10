using boty_asp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Areas.Admin.Controllers;
[Authorize(Policy = "AdminAccess")]
public class CategoryAdminController : Controller {
    
    MyContext _context = new MyContext();
    
    // GET
    [Area("Admin")]
    public IActionResult Index() {
        
        var categories = _context.Categories.ToList();
        
        return View(categories);
    }
    
    [Area("Admin")]
    public IActionResult Delete(int? id) {
        if (id == null)
        {
            return NotFound();
        }
        
        var category = _context.Categories
            .Include(c => c.Products)
            .Include(c => c.Categories)
            .FirstOrDefault(c => c.Id == id);

        if (category == null)
        {
            return NotFound();
        }

        return View(category);
    }
    
    [Area("Admin")]
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteFromDb(int id) {
        
        var category = _context.Categories.Find(id);
    
        if (category != null)
        {
            _context.Categories.Remove(category); 
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
    public async Task<IActionResult> CreateToDb(Category category) {
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [Area("Admin")]
    public IActionResult Edit(int? id) {
        ViewBag.Categories = new SelectList(_context.Categories, "Id", "Name");
        
        var cat = _context.Categories.Find(id);
        return View(cat);
    }

    [Area("Admin")]
    [HttpPost, ActionName("Edit")]
    public async Task<IActionResult> EditToDb(Category category) {
        _context.Categories.Update(category);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}