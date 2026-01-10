using Microsoft.AspNetCore.Mvc;

namespace boty_asp.Controllers;

public class LoginController : Controller {
    // GET
    public IActionResult Index() {
        return View();
    }
}