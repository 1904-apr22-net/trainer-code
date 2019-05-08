using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieApp.BL;
using MovieApp.UI.Models;

namespace MovieApp.UI.Controllers
{
    public class MovieController : Controller
    {
        // ASP.NET heavily supports/encourages dependency injection.
        public IMovieRepository MovieRepo { get; set; }

        public MovieController(IMovieRepository movieRepo)
        {
            MovieRepo = movieRepo ?? throw new ArgumentNullException(nameof(movieRepo));
        }

        public IActionResult Index()
        {
            var movies = MovieRepo.GetAll();

            var model = movies.Select(m => new MovieViewModel
            {
                Id = m.Id,
                Title = m.Title,
                DateReleased = m.ReleaseDate,
                Genre = new GenreViewModel
                {
                    Id = m.Genre.Id,
                    Name = m.Genre.Name
                }
            });

            return View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}