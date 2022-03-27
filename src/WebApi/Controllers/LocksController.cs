﻿using Domain.Commands;
using Domain.Commands.Locks;
using Domain.Queries;
using Domain.Queries.Locks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Filters;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class LocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin,user")]
    public async Task<IActionResult> GetLocks()
    {
        var result = await _mediator.Send(new GetLocksQuery(User.GetUserId()));
        return result.IsSuccess ? Ok(result) : BadRequest(new ErrorResponse(result.ErrorCode));
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost, ValidateRequest, Route("{lockId}/open")]
    public async Task<IActionResult> Open(string lockId, [FromBody]OpenLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new OpenLockCommand(lockId, request.KeyId, User.GetUserId()), token);
        return result.IsSuccess ? Ok(result) : BadRequest(new ErrorResponse(result.ErrorCode));
    }

    [Authorize(Roles = "admin")]
    [HttpPost, ValidateRequest, Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new CreateLockCommand(request.Title, request.ActivationKey, User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.ValidatorErrors));
    }

    [Authorize(Roles = "admin")]
    [HttpPatch, ValidateRequest, Route("{lockId}/update")]
    public async Task<IActionResult> PartialUpdate([FromBody] CreateLockRequest request, CancellationToken token)
    {
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpPut, ValidateRequest, Route("{lockId}/update")]
    public async Task<IActionResult> Update([FromBody] CreateLockRequest request, CancellationToken token)
    {
        return Ok();
    }

    [Authorize(Roles = "admin")]
    [HttpDelete, ValidateRequest, Route("{lockId}/delete")]
    public async Task<IActionResult> Delete(Guid lockId, CancellationToken token)
    {
        var result = await _mediator.Send(new DeleteLockCommand(lockId, User.GetUserId()));
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.ValidatorErrors));
    }
}