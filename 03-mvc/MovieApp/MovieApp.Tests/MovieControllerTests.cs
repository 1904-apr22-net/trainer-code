using Microsoft.AspNetCore.Mvc;
using MovieApp.BL;
using MovieApp.UI.Controllers;
using MovieApp.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MovieApp.Tests
{
    public class MovieControllerTests
    {
        [Fact]
        public void IndexShouldShowMovies()
        {
            // arrange
            var fakeRepo = new FakeMovieRepository();
            // we need to give the controller some imovierepository...
            // one approach is called "faking"
            // (make a class that implements the interface, just for testing.)
            var controller = new MovieController(fakeRepo);
            // a better, more scalable approach is called "mocking"
            // we use the "Moq" package to do it in C#.

            // act
            IActionResult result = controller.Index();

            // assert
            // (casts and asserts that the cast worked)
            ViewResult viewResult = Assert.IsAssignableFrom<ViewResult>(result);
            IEnumerable<MovieViewModel> model = Assert.IsAssignableFrom<IEnumerable<MovieViewModel>>(viewResult.Model);
            var movies = model.ToList();
            Assert.Single(movies);
            Assert.Equal("Star Wars", movies[0].Title);
        }
    }

    internal class FakeMovieRepository : IMovieRepository
    {
        public IEnumerable<Movie> GetAll()
        {
            return new List<Movie>
            {
                new Movie
                {
                    Id = 1,
                    Title = "Star Wars",
                    ReleaseDate = new DateTime(1977, 1, 1),
                    Genre = new Genre { Id = 1, Name = "Action" }
                }
            };
        }

        public void Create(Movie movie) => throw new NotImplementedException();
        public IEnumerable<Genre> GetAllGenres() => throw new NotImplementedException();
        public Genre GetGenreById(int id) => throw new NotImplementedException();
    }
}
