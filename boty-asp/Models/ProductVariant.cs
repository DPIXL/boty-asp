using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        
        [ForeignKey(nameof(ProductId))]
        public virtual Product Product { get; set; }

        public int ColorId { get; set; }
        
        [ForeignKey(nameof(ColorId))]
        public virtual Color Color { get; set; }

        public int SizeId { get; set; }
        [ForeignKey(nameof(SizeId))]
        public virtual Size Size { get; set; }

        public int StockQuantity { get; set; }
    }
}
