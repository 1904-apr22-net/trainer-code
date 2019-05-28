using DogRestService.API.Models;
using DogRestService.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

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