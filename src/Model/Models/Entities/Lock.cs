using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models.Entities;

public class Lock : EntityModifiedBase
{
    
    [Required]
    public string ActivationKey { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public string Title { get; set; }
    
    [InverseProperty(nameof(Lock))]
    public LockSetting Setting { get; set; }
    
    [InverseProperty(nameof(Lock))]
    public virtual ICollection<OpeningHistory> OpeningHistories { get; set; } = new List<OpeningHistory>();
}