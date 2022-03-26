using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize(Roles = "admin")]
[ApiController, Route("api/[controller]")]
public class KeysController : ControllerBase
{
    
}