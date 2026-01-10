using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Areas.Admin.Controllers;
[Authorize(Policy = "AdminAccess")]
public class HomeController : Controller {
    // GET
    [Area("Admin")]
    public IActionResult Index() {
        return RedirectToAction("Index", "ProductAdmin");
    }
}