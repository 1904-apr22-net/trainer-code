using DogRestService.API.Models;
using DogRestService.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogRestService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogRepository _repo;

        public DogsController(DogRepository repo) => _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        // GET: api/Dog?breed=doberman
        [HttpGet]
        //[Produces("application/xml")]
        //[FormatFilter]
        //[Authorize]
        public IEnumerable<Dog> Get([FromQuery]string breed = null) =>
            _repo.GetAll(breed);

        // GET: api/Dog/5
        [HttpGet("{id}", Name = "Get")] // this route name is used below in CreatedAtRoute
        public async Task<ActionResult<Dog>> Get(int id)
        {
            if (await _repo.GetAsync(id) is Dog dog)
            {
                return dog; // 200 OK
            }
            return NotFound(); // 404 Not Found
        }

        // POST: api/Dog
        [HttpPost]
        //[Authorize]
        public async Task<IActionResult> Post([FromBody] Dog dog)
        {
            // there is automatic validation of Data Annotations,
            // wrapping ModelState errors into a 400 Bad Request.
            try
            {
                dog.Owner.Id = await _repo.GetAccountIdByEmailAsync(dog.Owner.Email);

                //if (!User.IsInRole("Administrator"))
                //{
                //    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                //    var dogEmail = dog.Owner.Email;
                //    if (userEmail != dogEmail)
                //    {
                //        return StatusCode(StatusCodes.Status403Forbidden);
                //    }
                //}

                var id = await _repo.CreateAsync(dog);

                Dog model = await _repo.GetAsync(id);

                // there's also CreatedAtAction, same purpose
                return CreatedAtRoute("Get", new { Id = id }, model); // 201 Created
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT: api/Dog/5
        [HttpPut("{id}")]
        //[Authorize]
        public async Task<IActionResult> Put(int id, [FromBody] Dog dog)
        {
            try
            {
                Dog existing = await _repo.GetAsync(id);
                if (existing is null)
                {
                    return NotFound(); // 404 Not Found
                }
                dog.Id = id;

                //if (!User.IsInRole("Administrator"))
                //{
                //    var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                //    var dogEmail = existing.Owner.Email;
                //    var newEmail = dog.Owner.Email;
                //    if (userEmail != dogEmail || dogEmail != newEmail)
                //    {
                //        return StatusCode(StatusCodes.Status403Forbidden);
                //    }
                //}

                dog.Owner.Id = await _repo.GetAccountIdByEmailAsync(dog.Owner.Email);

                var success = await _repo.UpdateAsync(dog);
                if (!success)
                {
                    return BadRequest("invalid request"); // 400 Bad Request
                }
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message); // 400 Bad Request
            }
            return NoContent(); // 204 No Content
        }

        // DELETE: api/Dog/5
        [HttpDelete("{id}")]
        //[Authorize(Roles = "Administrator, Moderator")]
        public async Task<IActionResult> Delete(int id)
        {
            var success = await _repo.DeleteAsync(id);
            if (!success)
            {
                return NotFound(); // 404 Not Found
            }
            return NoContent(); // 204 No Content
        }
    }
}
