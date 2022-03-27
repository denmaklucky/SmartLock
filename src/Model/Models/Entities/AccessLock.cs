using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace Model.Models.Entities;

public class AccessLock : IEntityBase
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid AccessId { get; set; }
    
    [Required]
    public Guid LockId { get; set; }
    
    [Required]
    public AccessTypeEnum Type { get; set; }
}