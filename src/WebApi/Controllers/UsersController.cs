using Domain.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Requests;
using WebApi.Responses;

namespace WebApi.Controllers;

[AllowAnonymous]
[ApiController, Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;

    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost, ValidateRequest, Route("signin")]
    public async  Task<IActionResult> SignIn([FromBody] SignInRequest request, CancellationToken token)
    {
        var result = await _mediator.Send(new SignInCommand(request.Login, request.Password), token);
        return result.IsSuccess ? Ok(new SignInResponse(result.AcessToken)) : BadRequest(new ErrorResponse(result.ErrorType, result.ErrorType));
    }
}