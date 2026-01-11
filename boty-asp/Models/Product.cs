using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace boty_asp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "decimal(15, 2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? DiscountPercent { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal? DPH { get; set; } 

        public string ImagesPath { get; set; }

        public bool IsActive { get; set; }
        
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
