using System.ComponentModel.DataAnnotations;

namespace Model.Models;

public abstract class EntityBase : IEntityBase
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public DateTime CreatedOn { get; set; }

    [Required]
    public Guid CreatedBy { get; set; }
}