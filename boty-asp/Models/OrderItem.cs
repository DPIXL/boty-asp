using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.Models
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }
        
        public int ProductVariantId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal UnitPrice { get; set; }
        
        
        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; }
        
        [ForeignKey(nameof(ProductVariantId))]
        public virtual ProductVariant ProductVariant { get; set; }
        
    }
}
