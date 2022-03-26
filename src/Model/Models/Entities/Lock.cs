using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Model.Enums;

namespace Model.Models.Entities;

public class Lock : EntityModifiedBase
{
    [Required]
    public LockStateEnum State { get; set; }
    
    [Required]
    public LockTypeEnum Type { get; set; }
    
    [Required]
    public string ActivationKey { get; set; }
    
    public string Title { get; set; }
    
    [InverseProperty(nameof(Lock))]
    public LockSettings SettingsId { get; set; }
    
    [InverseProperty(nameof(Lock))]
    public virtual ICollection<OpeningHistory> OpeningHistories { get; set; } = new List<OpeningHistory>();
}