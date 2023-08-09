using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // localhost:5000/weatherforecast (class name minus Controller)
    public class BaseApiController : ControllerBase
    {

    }
}