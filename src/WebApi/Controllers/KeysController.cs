using Domain.Commands.Keys;
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
        var result = await _mediator.Send(new CreateKeyCommand(User.GetUserId(), request.LockId, request.UserId, request.Type, request.ExpiredAt), token);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(new ErrorResponse(result.ErrorCode, result.Messages));
    }

    [HttpPatch, Route("{keyId}/change-user")]
    public async  Task<IActionResult> ChangeUser([FromBody]ChangeUserForKeyRequest request, CancellationToken token)
    {
        
    }
    
    [HttpPost, Route("{keyId}/change-lock")]
    public async  Task<IActionResult> ChangeLock([FromBody]ChangeUserForKeyRequest request, CancellationToken token)
    {
        
    }

    [HttpDelete, Route("{keyId}")]
    public void Delete()
    {
        
    }
}