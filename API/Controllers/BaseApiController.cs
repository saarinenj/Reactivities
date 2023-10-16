using Application.Core;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // localhost:5000/weatherforecast (class name minus Controller)
    public class BaseApiController : ControllerBase
    {
        private IMediator _mediator;

        // protected property Mediator can be used in any derived class
        // if _mediator has not been initialized, the property will initialize it
        // calling HttpContext.RequestServices.GetService<IMediator>();
        protected IMediator Mediator => _mediator ??= 
            HttpContext.RequestServices.GetService<IMediator>();


        protected ActionResult HandleResult<T>(Result<T> result)
        {
            if(result == null) return NotFound();
            if(result.IsSuccess && result.Value != null)
                return Ok(result.Value);
            if(result.IsSuccess && result.Value == null)
                return NotFound();
            return BadRequest(result.Error);
        }
    }
}