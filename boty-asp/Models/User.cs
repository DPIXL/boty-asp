using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace boty_asp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(2000)]
        public string Password { get; set; }
        
        [Required]
        public int PermissionId { get; set; }
        
        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
