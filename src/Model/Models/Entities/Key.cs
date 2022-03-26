﻿using System.ComponentModel.DataAnnotations;

namespace Model.Models.Entities;

public class Key : EntityModifiedBase
{
    [Required]
    public string Type { get; set; }
    
    public bool IsDeleted { get; set; }
    
    public DateTime? ExpiredAt { get; set; }
}