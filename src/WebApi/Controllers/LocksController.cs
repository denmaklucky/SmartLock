using Domain.Commands.Locks;
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
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode));
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost, ValidateRequest, Route("{lockId}/open")]
    public async Task<IActionResult> Open(string lockId, [FromBody] OpenLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new OpenLockCommand(lockId, request.KeyId, User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpPost, ValidateRequest, Route("activate")]
    public async Task<IActionResult> Create([FromBody] ActivateLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new ActivateLockCommand(request.Title, request.ActivationKey, User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpPatch, ValidateRequest, Route("{lockId}")]
    public async Task<IActionResult> PartialUpdate(string lockId, [FromBody] UpdateLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new UpdateLockCommand(User.GetUserId(), lockId, request.Title, request.Mode, request.StartOpenTime, request.EndOpenTime), token);
        return result.IsSuccess ? Ok(result) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpDelete, ValidateRequest, Route("{lockId}")]
    public async Task<IActionResult> Delete(string lockId, CancellationToken token)
    {
        var result = await _mediator.Send(new DeleteLockCommand(lockId, User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpPost, Route("{lockId}/admit")]
    public async Task<IActionResult> Admit(string lockId, [FromBody] AdmitLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new AdmitLockCommand(User.GetUserId(), lockId, request.AccessId, request.Type), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpPost, Route("{lockId}/forbid")]
    public async Task<IActionResult> Forbid(string lockId, [FromBody] ForbidLockRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new ForbidLockCommand(User.GetUserId(), lockId, request.AccessId), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [Authorize(Roles = "admin")]
    [HttpGet, Route("history")]
    public async Task<IActionResult> GetOpeningHistories(CancellationToken token)
    {
        var result = await _mediator.Send(new GetOpeningHistoryQuery(User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }
}