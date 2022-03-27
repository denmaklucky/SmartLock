﻿using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class KeyLock : IEntityBase
{
    [Required]
    public Guid UserId { get; set; }
    [Required]
    public Guid LockId { get; set; }

    [Key]
    public Guid Id { get; set; }
}