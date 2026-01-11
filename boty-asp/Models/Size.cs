using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace boty_asp.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        
        public int SizeValue { get; set; } 
        
        [JsonIgnore]
        public virtual ICollection<ProductVariant> ProductVariants { get; set; } = new List<ProductVariant>();
    }
}
