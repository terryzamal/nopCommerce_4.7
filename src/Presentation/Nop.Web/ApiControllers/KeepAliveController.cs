using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public partial class KeepAliveController : ControllerBase
{
    [HttpGet("Check")]
    public virtual IActionResult Check()
    {
        return Ok("I am alive!");
    }
}