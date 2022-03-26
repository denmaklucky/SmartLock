using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models.Entities;

public class UserRole : IEntityBase
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid RoleId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    
    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; }

    [Key]
    public Guid Id { get; set; }
}