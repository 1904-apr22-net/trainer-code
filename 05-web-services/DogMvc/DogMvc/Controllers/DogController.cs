using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DogMvc.ApiModels;
using DogMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DogMvc.Controllers
{
    public class DogController : Controller
    {
        private readonly HttpClient _httpClient;

        public DogController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET: Dog
        public async Task<ActionResult> Index()
        {
            var url = "https://localhost:5001/api/dog";

            HttpResponseMessage response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // should return some error view
                return View(new DogViewModel[0]);
            }

            // deserialize from JSON
            var dogs = await response.Content.ReadAsAsync<IEnumerable<Dog>>();

            var model = dogs.Select(x => new DogViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Breed = x.Breed
            });

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
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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