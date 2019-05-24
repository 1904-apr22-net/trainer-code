using System;
using System.Collections;
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
    public class AccountsController : ControllerBase
    {
        private readonly DogRepository _repo;

        public AccountsController(DogRepository repo) =>
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));

        [HttpGet]
        public IEnumerable<Account> Get() => _repo.GetAllAccounts();
    }
}