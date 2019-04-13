using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microservices.Services.Core.Entities;
using Microservices.Services.Core.Interface.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace workout.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    [ApiController]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;

        public WorkoutsController(IWorkoutService workoutService)
        {
            _workoutService = workoutService;
        }

        /// <summary>
        /// List all the workouts in the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Workout>> Get()
        {
            return await _workoutService.GetAll();
        }

        /// <summary>
        /// Get the workouts of a user
        /// We check that the user id of token is the same than parameters
        /// if not the user can't see the workouts of someone else
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        [Authorize]
        public async Task<IActionResult> Get(int userId)
        {
            string accessToken = User.FindFirst("access_token")?.Value;
            var currentUser = HttpContext.User;

            if (currentUser.HasClaim(c => c.Type == "UserId"))
            {
                int tokenUserId = int.Parse(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                Console.Out.WriteLine("Token user id " + tokenUserId);
                Console.Out.WriteLine("User id : " + userId);
                if (tokenUserId != userId)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unauthorized access");

                }
            }

            return Ok(await _workoutService.Get(userId));
        }
    }
}
