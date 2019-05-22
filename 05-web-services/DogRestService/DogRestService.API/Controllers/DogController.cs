using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DogRestService.API.Models;
using DogRestService.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogRestService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly DogRepository _repo;

        public DogController(DogRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        // GET: api/Dog
        [HttpGet]
        public IEnumerable<Dog> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Dog/5
        [HttpGet("{id}", Name = "Get")]
        public Dog Get(int id)
        {
            return _repo.Get(id);
        }

        // POST: api/Dog
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Dog/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
