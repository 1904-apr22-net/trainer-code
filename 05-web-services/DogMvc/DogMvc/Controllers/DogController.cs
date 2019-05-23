using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using DogMvc.ApiModels;
using DogMvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogMvc.Controllers
{
    [Authorize]
    public class DogController : Controller
    {
        private readonly string _url = "https://localhost:5001/api/dog";
        private readonly HttpClient _httpClient;

        public DogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Dog
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_url);

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            // deserialize from JSON
            IEnumerable<Dog> dogs = await response.Content.ReadAsAsync<IEnumerable<Dog>>();

            IEnumerable<DogViewModel> model = dogs.Select(Mapper.Map);

            return View(model);
        }

        // GET: Dog/Details/5
        public async Task<ActionResult> Details(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            // deserialize from JSON
            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            return View(model);
        }

        // GET: Dog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Dog/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DogViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(viewModel);
                }

                var dog = new Dog
                {
                    Name = viewModel.Name,
                    Breed = viewModel.Breed
                };

                HttpResponseMessage response = await _httpClient.PostAsync(
                    _url, dog, new JsonMediaTypeFormatter());

                if (!response.IsSuccessStatusCode)
                {
                    // could check specifically for 400 and look inside
                    // body for the model errors, to then add to the ModelState here.
                    ModelState.AddModelError("", "Invalid data");
                    return View(viewModel);
                }

                // i don't actually need the response body

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(viewModel);
            }
        }

        // GET: Dog/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            return View(model);
        }

        // POST: Dog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<ActionResult> Edit(int id, DogViewModel viewModel)
        {
            try
            {
                var dog = new Dog
                {
                    Name = viewModel.Name,
                    Breed = viewModel.Breed
                };

                HttpResponseMessage response = await _httpClient.PutAsync(
                    $"{_url}/{id}", dog, new JsonMediaTypeFormatter());

                if (!response.IsSuccessStatusCode)
                {
                    // could check specifically for 400 and look inside
                    // body for the model errors, to then add to the ModelState here.
                    ModelState.AddModelError("", "Invalid data");
                    return View(viewModel);
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred");
                return View(viewModel);
            }
        }

        // GET: Dog/Delete/5
        [Authorize(Roles = "Administrator, Moderator")] // must be one of these roles
        public async Task<ActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync($"{_url}/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel());
            }

            Dog dog = await response.Content.ReadAsAsync<Dog>();

            DogViewModel model = Mapper.Map(dog);

            return View(model);
        }

        // POST: Dog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator, Moderator")]
        public async Task<ActionResult> Delete(int id, IFormCollection collection)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{_url}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel());
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View("Error", new ErrorViewModel());
            }
        }
    }
}