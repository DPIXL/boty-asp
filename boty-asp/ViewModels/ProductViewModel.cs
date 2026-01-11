using boty_asp.Models;

namespace boty_asp.ViewModels;

public class ProductViewModel {
    
    public int ProductId { get; set; }
    public Product? Product { get; set; }
    
    public int? ColorId { get; set; }
    public int? SizeId { get; set; }
}