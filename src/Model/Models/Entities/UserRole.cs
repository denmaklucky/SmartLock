using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class UserRole : IEntityBase
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid RoleId { get; set; }

    [Key]
    public Guid Id { get; set; }
}