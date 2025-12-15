using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boty_asp.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        
        public int SizeValue { get; set; } 

        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
