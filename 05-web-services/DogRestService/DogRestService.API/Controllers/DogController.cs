using DogRestService.API.Models;
using DogRestService.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace DogRestService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DogRepository _repo;

        public DogController(DogRepository repo) => _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        // GET: api/Dog?breed=doberman
        [HttpGet]
        public IEnumerable<Dog> Get([FromQuery]string breed = null) => _repo.GetAll(breed);

        // GET: api/Dog/5
        [HttpGet("{id}", Name = "Get")] // this route name is used below in CreatedAtRoute
        public ActionResult<Dog> Get(int id)
        {
            if (_repo.Get(id) is Dog dog)
            {
                return dog; // 200 OK
            }
            return NotFound(); // 404 Not Found
        }

        // POST: api/Dog
        [HttpPost]
        public IActionResult Post([FromBody] Dog dog)
        {
            // there is automatic validation of Data Annotations,
            // wrapping ModelState errors into a 400 Bad Request.

            int id;
            try
            {
                id = _repo.Create(dog);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            // there's also CreatedAtAction, same purpose
            return CreatedAtRoute("Get", new { Id = id }, Get(id)); // 201 Created
        }

        // PUT: api/Dog/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Dog dog)
        {
            if (Get(id) is null)
            {
                return NotFound(); // 404 Not Found
            }
            dog.Id = id;
            try
            {
                var success = _repo.Update(dog);
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
        public IActionResult Delete(int id)
        {
            var success = _repo.Delete(id);
            if (!success)
            {
                return NotFound(); // 404 Not Found
            }
            return NoContent(); // 204 No Content
        }
    }
}
