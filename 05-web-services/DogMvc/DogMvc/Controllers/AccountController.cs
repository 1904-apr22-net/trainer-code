﻿using Microsoft.AspNetCore.Mvc;

namespace DogMvc.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
