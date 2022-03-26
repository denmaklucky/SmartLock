using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class Role : IEntityBase
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}