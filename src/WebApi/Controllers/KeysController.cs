using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Requests;

namespace WebApi.Controllers;

[Authorize(Roles = "admin")]
[ApiController, Route("api/[controller]")]
public class KeysController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateKey([FromBody]CreateKeyRequest request, CancellationToken token)
    {
        return Ok("");
    }
}