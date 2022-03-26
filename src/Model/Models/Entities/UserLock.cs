using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models.Entities;

public class UserLock
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid LockId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    [ForeignKey(nameof(LockId))]
    public Lock Lock { get; set; }
}