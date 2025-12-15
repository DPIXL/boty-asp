using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boty_asp.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [StringLength(7)]
        public string HexCode { get; set; }

        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
