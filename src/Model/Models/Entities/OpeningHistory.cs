using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Enums;

namespace Model.Models.Entities;

public class OpeningHistory : EntityBase
{
    [Required]
    public Guid LockId { get; set; }
    
    [ForeignKey(nameof(LockId))]
    public virtual Lock Lock { get; set; }
    
    [Required]
    public Guid AccessId { get; set; }
    
    [Required]
    public AccessTypeEnum AccessType { get; set; }
    
    [Required]
    public string UserName { get; set; }
}