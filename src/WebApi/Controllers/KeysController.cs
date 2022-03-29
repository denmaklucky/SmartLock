using Domain.Commands.Keys;
using Domain.Queries.Keys;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers;

[Authorize(Roles = "admin")]
[ApiController, Route("api/[controller]")]
public class KeysController : ControllerBase
{
    private readonly IMediator _mediator;

    public KeysController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost, Route("create")]
    public async Task<IActionResult> CreateKey([FromBody] CreateKeyRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new CreateKeyCommand(User.GetUserId(), request.LockId, request.UserId, request.Type), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [HttpPatch, Route("{keyId}/change-user")]
    public async Task<IActionResult> ChangeUser(string keyId, [FromBody] ChangeUserForKeyRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new ChangeUserForKeyCommand(User.GetUserId(), keyId, request.NewUserId), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [HttpPost, Route("{keyId}/change-lock")]
    public async Task<IActionResult> ChangeLock(string keyid, [FromBody] ChangeLockForKeyRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new ChangeLockForKeyCommand(User.GetUserId(), keyid, request.NewLockId, request.OldNewLockId), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [HttpDelete, Route("{keyId}")]
    public async Task<IActionResult> Delete(string keyId, CancellationToken token)
    {
        var result = await _mediator.Send(new DeleteKeyCommand(User.GetUserId(), keyId), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [HttpGet]
    public async Task<IActionResult> GetKeys(CancellationToken token)
    {
        var result = await _mediator.Send(new GetKeysQuery(User.GetUserId()), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }
}