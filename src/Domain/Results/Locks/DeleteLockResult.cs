﻿using Domain.Dto;

namespace Domain.Results.Locks;

public class DeleteLockResult : BaseResult
{
    public DeleteLockDto Data { get; set; }
}