using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace boty_asp.Models;

public class Permission {
    [Key]
    public int Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}