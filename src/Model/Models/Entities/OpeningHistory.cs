using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class OpeningHistory : EntityBase
{
    [Required]
    public Guid LockId { get; set; }
    
    [Required]
    public Guid AccessId { get; set; }
    
    [Required]
    public string UserName { get; set; }
}