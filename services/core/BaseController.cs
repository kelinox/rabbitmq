using System;
using System.Threading.Tasks;
using Microservices.Services.Core.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Microservices.Services.Core.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected async Task<IActionResult> HandleComputationFailure<T>(Task<T> f)
        {
            try
            {
                var result = await f.ConfigureAwait(false);
                return Ok(Response<T>.Succeeded(result));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Response<T>.Failure(ex.Message));
            }
        }

        protected async Task<IActionResult> HandleComputationFailureWithConditions<T>(Task<T> f, Func<T, bool> succeeded)
        {
            try
            {
                var result = await f.ConfigureAwait(false);

                if (succeeded(result))
                    return Ok(Response<T>.Succeeded(result));
                else
                    return BadRequest(Response<T>.Failure("Internal error occured"));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Response<T>.Failure(ex.Message));
            }
        }
    }
}