using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Enums;

namespace Model.Models.Entities;

public class LockSetting : EntityModifiedBase
{
    [Required]
    public Guid LockId { get; set; }
    
    [Required]
    public LockModeEnum Mode { get; set; }
    
    public TimeOnly? StartOpenTime { get; set; }
    
    public TimeOnly? EndOpenTime { get; set; }

    [ForeignKey(nameof(LockId))]
    public virtual Lock Lock { get; set; }
}