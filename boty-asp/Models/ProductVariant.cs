using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.Models
{
    public class ProductVariant
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int ColorId { get; set; }
        public virtual Color Color { get; set; }

        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal Price { get; set; }

        public int StockQuantity { get; set; }
    }
}
