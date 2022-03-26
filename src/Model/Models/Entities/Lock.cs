using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models.Entities;

public class Lock : EntityModifiedBase
{
    public string State { get; set; }
    
    [InverseProperty(nameof(Lock))]
    public virtual ICollection<OpeningHistory> OpeningHistories { get; set; } = new List<OpeningHistory>();
}