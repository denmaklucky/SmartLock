using System.ComponentModel.DataAnnotations;
using Model.Enums;

namespace Model.Models.Entities;

public class Key : EntityModifiedBase
{
    [Required]
    public KeyTypeEnum Type { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    public bool IsDeleted { get; set; }
}