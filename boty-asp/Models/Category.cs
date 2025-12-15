using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace boty_asp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        // Navigation property
        public virtual ICollection<ProductCategory> ProductCategories { get; set; } = new List<ProductCategory>();
    }
}
