using Microsoft.EntityFrameworkCore;
using MovieApp.DA;
using MovieApp.DA.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace MovieApp.Tests
{
    public class MovieDbRepositoryTests
    {
        [Fact]
        public void GetAllShouldGetOneMovie()
        {
            // arrange
            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseInMemoryDatabase("MovieDbRepositoryTests.GetAllShouldGetOneMovie")
                .Options;
            using (var dbContext = new MovieDbContext(options))
            {
                dbContext.Movie.Add(new Movie
                {
                    Id = 1,
                    Title = "Star Wars",
                    ReleaseDate = new DateTime(1977, 1, 1),
                    Genre = new Genre { Id = 1, Name = "Action" }
                });
                dbContext.SaveChanges();
            }

            // act
            List<BL.Movie> result = null;
            using (var dbContext = new MovieDbContext(options))
            {
                var repo = new MovieDbRepository(dbContext);

                result = repo.GetAll().ToList();
            }

            // assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("Star Wars", result[0].Title);
        }
    }
}
