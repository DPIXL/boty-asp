using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;

namespace boty_asp.Controllers;

public class ProductController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}