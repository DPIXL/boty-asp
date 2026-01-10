using System.Security.Claims;
using boty_asp.HelperClasses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Controllers;

public class LoginController : Controller {
    
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index() {

        if (User.Identity.IsAuthenticated) return RedirectToAction("Index", "Home");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignIn(string username, string password) {
        
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Name == username);

        if (user == null)
        {
            ViewBag.Error = "Invalid Username";
            return View("Index");
        }

        if (!HashHelper.Verify(password, user.Password)) {
            ViewBag.Error = "Invalid Password";
            ViewBag.Username = user.Name;
            return View("Index");
        }
        
        var claims = new List<Claim> {
            new Claim(ClaimTypes.Name, user.Name),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.PermissionId.ToString())
        };
        
        var claimsIdentity = new ClaimsIdentity(
            claims, 
            CookieAuthenticationDefaults.AuthenticationScheme, 
            ClaimTypes.Name,
            ClaimTypes.Role
        );
        
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(claimsIdentity),
            new AuthenticationProperties { IsPersistent = true }
        );

        if (user.PermissionId == 2 || user.PermissionId == 3) {
            return RedirectToAction("Index", "Home",  new { area = "Admin" });
        }
        else {
            return RedirectToAction("Index", "Home");
        }
    }
    
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Home");
    }
}