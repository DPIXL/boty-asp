using System.Diagnostics;
using boty_asp.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;

namespace boty_asp.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    
    MyContext _context = new MyContext();

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index() {
        var products = _context.Products.Where(i => i.Id <= 8).ToList();
        
        return View(products);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}