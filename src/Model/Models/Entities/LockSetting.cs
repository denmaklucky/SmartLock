﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model.Models.Entities;

public class LockSetting : EntityModifiedBase
{
    [Required]
    public Guid LockId { get; set; }

    [ForeignKey(nameof(LockId))]
    public virtual Lock Lock { get; set; }
}