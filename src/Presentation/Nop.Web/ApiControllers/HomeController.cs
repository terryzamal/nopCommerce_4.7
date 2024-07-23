using Microsoft.AspNetCore.Mvc;

namespace Nop.Web.ApiControllers;

[Route("api/[controller]")]
[ApiController]
public partial class HomeController : ControllerBase
{
    [HttpGet("Index")]
    public virtual IActionResult Index()
    {
        return Ok();
    }
}