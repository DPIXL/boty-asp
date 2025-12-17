using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.Models
{
    public class ProductCategory
    {
        public int ProductId { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int CategoryId { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
    }
}
