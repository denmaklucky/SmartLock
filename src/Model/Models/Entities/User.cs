using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class User : IEntityBase
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
}