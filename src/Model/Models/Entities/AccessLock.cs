using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace Model.Models.Entities;

public class AccessLock : EntityModifiedBase
{
    [Required]
    public Guid AccessId { get; set; }
    
    [Required]
    public Guid LockId { get; set; }
    
    [Required]
    public AccessTypeEnum Type { get; set; }
    
    public bool IsDeleted { get; set; }
}