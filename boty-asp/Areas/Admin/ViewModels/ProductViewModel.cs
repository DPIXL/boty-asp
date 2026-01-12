using System.ComponentModel.DataAnnotations;
using boty_asp.Models;

namespace boty_asp.Areas.Admin.ViewModels;

public class ProductViewModel {
    public Product Product { get; set; } = new Product();
    
    public IFormFile? ImageUpload { get; set; }
}