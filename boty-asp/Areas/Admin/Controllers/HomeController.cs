using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Areas.Admin.Controllers;

public class HomeController : Controller {
    // GET
    [Area("Admin")]
    public IActionResult Index() {
        return RedirectToAction("Index", "ProductAdmin");
    }
}