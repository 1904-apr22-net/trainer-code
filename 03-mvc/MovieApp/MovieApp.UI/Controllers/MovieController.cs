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
                Genre = m.Genre
            });

            return View(model);
        }

        // this action method handles the GET request
        public IActionResult Create()
        {
            // provide all genres for the sake of the dropdown in the form.
            var viewModel = new MovieViewModel
            {
                Genres = MovieRepo.GetAllGenres().ToList()
            };

            return View(viewModel);
        }

        // ASP.NET supports "model binding" -
        // data that comes from the client's request
        // can be automatically packaged into an object of our choosing.

        // whatever name each value has in the form, that will be matched up to
        // properties in the object by name, without uppercase/lowercase mattering.
        [HttpPost] // this action method is only for POST requests
        public IActionResult Create(MovieViewModel viewModel)
        {
            // in all code that receives data from somewhere else,
            // especially the public/clients, i need to do validation.
            // this is called server-side validation.
            if (viewModel.Title == "Star Wars: The Last Jedi")
            {
                // adding an error for the title.
                ModelState.AddModelError("Title", "The movie was bad");

                // example of adding a error not for any specific field.
                ModelState.AddModelError("", "There were some errors");
            }

            Genre genre = null;
            if (viewModel.Genre != null)
            {
                genre = MovieRepo.GetGenreById(viewModel.Genre.Id);
                if (genre == null)
                {
                    ModelState.AddModelError("Genre", "Invalid genre ID");
                }
            }

            // we have Data Annotations library to help us with some of that.
            // (back we can also add our own model errors

            // checking the server-side validation, the received data
            // is compared against the Data Annotations attributes
            if (!ModelState.IsValid)
            {
                // send the user back the form, with what he sent pre=filled into it.
                // (better user experience)
                // (also, the ModelState errors are rendered onto the page as well.

                viewModel.Genres = MovieRepo.GetAllGenres().ToList();

                return View(viewModel);
            }

            var movie = new Movie
            {
                Title = viewModel.Title,
                ReleaseDate = viewModel.DateReleased.Value,
                Genre = genre
            };

            MovieRepo.Create(movie);

            // we should go back to the index page now...
            // but here are two ways to do that.

            // this one presents the index view, on the URL of Create.
            //return View("Index");
            // this one tells the client to make a new request (redirect)
            // to a different page and load that one.
            return RedirectToAction("Index");
        }
    }
}