using Microsoft.EntityFrameworkCore;
using MovieApp.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieApp.DA
{
    public class MovieDbRepository : IMovieRepository
    {
        private readonly MovieDbContext _dbContext;

        public MovieDbRepository(MovieDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public IEnumerable<Movie> GetAll()
        {
            return _dbContext.Movie.Include(m => m.Genre).AsNoTracking()
                .Select(m => new Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = new Genre { Id = m.Genre.Id, Name = m.Genre.Name },
                    ReleaseDate = m.ReleaseDate
                });
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _dbContext.Genre.AsNoTracking().Select(g => new Genre
            {
                Id = g.Id,
                Name = g.Name
            });
        }

        public Genre GetGenreById(int id)
        {
            Entities.Genre entity = _dbContext.Genre.Find(id);
            return new Genre
            {
                Id = entity.Id,
                Name = entity.Name
            };
        }

        public void Create(Movie movie)
        {
            var entity = new Entities.Movie
            {
                // ignore movie ID if set
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = _dbContext.Genre.Find(movie.Genre.Id)
            };

            _dbContext.Movie.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
