using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filters;
using WebApi.Requests;

namespace WebApi.Controllers;

[Authorize(Roles = "admin")]
[ApiController, Route("api/[controller]")]
public class LocksController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetLocks()
    {
        var result = await _mediator.Send(new GetLocksQuery());
        return Ok(result);
    }

    [Authorize(Roles = "admin,user")]
    [HttpPost, ValidateRequest, Route("{lockId}/open")]
    public async Task<IActionResult> Open(CancellationToken token)
    {
        return Ok();
    }

    [HttpPost, ValidateRequest, Route("create")]
    public async Task<IActionResult> Create([FromBody] CreateLockRequest request, CancellationToken token)
    {
        return Ok();
    }

    [HttpPatch, ValidateRequest, Route("{lockId}/update")]
    public async Task<IActionResult> PartialUpdate([FromBody] CreateLockRequest request, CancellationToken token)
    {
        return Ok();
    }

    [HttpPut, ValidateRequest, Route("{lockId}/update")]
    public async Task<IActionResult> Update([FromBody] CreateLockRequest request, CancellationToken token)
    {
        return Ok();
    }

    [HttpDelete, ValidateRequest, Route("{lockId}/delete")]
    public async Task<IActionResult> Delete(string lockId, CancellationToken token)
    {
        return Ok();
    }
}