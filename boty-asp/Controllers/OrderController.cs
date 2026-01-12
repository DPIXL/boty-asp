using System.Security.Claims;
using boty_asp.HelperClasses;
using Microsoft.AspNetCore.Mvc;
using boty_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace boty_asp.Controllers;

public class OrderController : Controller
{
    
    MyContext _context = new MyContext();
    
    // GET
    public IActionResult Index() {

        List<int> pvIds = HttpContext.Session.GetObject<List<int>>("Cart") ?? new List<int>();
        
        Order order = new Order();

        if (pvIds.Any())
        {
            var distinctIds = pvIds.Distinct().ToList();

            var variants = _context.ProductVariants
                .Include(x => x.Product)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .Where(x => distinctIds.Contains(x.Id)) // Load all matching variants at once
                .ToList();
            
            foreach (var pv in variants) {
                int qty = pvIds.Count(id => id == pv.Id);
                
                decimal finalPrice = (Math.Round((pv.Product.BasePrice + (pv.Product.BasePrice * (pv.Product.DPH??0m))) * (1 - (pv.Product.DiscountPercent??0m))));
                
                var orderItem = new OrderItem() {
                    ProductVariantId = pv.Id,
                    ProductVariant = pv,
                    UnitPrice = Math.Round(finalPrice), 
                    Quantity = qty
                };

                order.OrderItems.Add(orderItem);
            }
        }
        
        return View(order);
    }

    public IActionResult RemoveItem(int pvId) {

        List<int> pvIds = HttpContext.Session.GetObject<List<int>>("Cart");
        
        pvIds.RemoveAll(x => x == pvId);
        
        HttpContext.Session.SetObject("Cart", pvIds);
        
        return RedirectToAction("Index");
    }

    public IActionResult SendOrder(Order order) {
        
        foreach(var item in order.OrderItems)
        {
            // Load the missing details based on the ID sent from the hidden input
            item.ProductVariant = _context.ProductVariants
                .Include(pv => pv.Product)
                .Include(pv => pv.Color)
                .Include(pv => pv.Size)
                .FirstOrDefault(pv => pv.Id == item.ProductVariantId);
        }
        
        order.OrderDate = DateTime.Now;
        
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (int.TryParse(userIdString, out int userId)) {
            order.UserId = userId;
        }

        order.Status = 1;
        
        order.TotalPrice = order.OrderItems.Sum(x => x.UnitPrice*x.Quantity);


        _context.Orders.Add(order);
        _context.SaveChanges();
        
        return RedirectToAction("Index", "Home");
    }
}