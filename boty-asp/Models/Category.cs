using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace boty_asp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        public int? ParentId { get; set; }
        
        [ForeignKey(nameof(ParentId))]
        public virtual Category Parent { get; set; }
        
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    }
}
