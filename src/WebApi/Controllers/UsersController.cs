using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController, Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost, Route("signin")]
    public IActionResult SignIn([FromBody]object response)
    {
        return Ok("Hello, world!");
    }
}