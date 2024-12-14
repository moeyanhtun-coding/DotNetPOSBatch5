using DotNetPOS.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetPOSBatch5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Execute<T>(Result<T> model)
        {
            if (model.IsValidationError)
            {
                return BadRequest(model);
            }

            return Ok(model);
        }
    }
}
