using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using DogMvc.ApiModels;
using DogMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogMvc.Controllers
{
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
                // should return some error view
                return View(new DogViewModel[0]);
            }

            // deserialize from JSON
            IEnumerable<Dog> dogs = await response.Content.ReadAsAsync<IEnumerable<Dog>>();

            IEnumerable<DogViewModel> model = dogs.Select(Mapper.Map);

            return View(model);
        }

        // GET: Dog/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Dog/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Dog/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Dog/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}