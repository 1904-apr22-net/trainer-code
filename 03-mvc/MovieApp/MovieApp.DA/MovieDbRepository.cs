using Microsoft.EntityFrameworkCore;
using MovieApp.BL;
using MovieApp.DA.Entities;
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

        public IEnumerable<BL.Movie> GetAll()
        {
            return _dbContext.Movie.Include(m => m.Genre).AsNoTracking()
                .Select(m => new BL.Movie
                {
                    Id = m.Id,
                    Title = m.Title,
                    Genre = new BL.Genre { Id = m.Genre.Id, Name = m.Genre.Name },
                    ReleaseDate = m.ReleaseDate
                });
        }

        public IEnumerable<BL.Genre> GetAllGenres()
        {
            return _dbContext.Genre.AsNoTracking().Select(g => new BL.Genre
            {
                Id = g.Id,
                Name = g.Name
            });
        }

        public BL.Genre GetGenreById(int id)
        {
            return GetAllGenres().FirstOrDefault(g => g.Id == id);
        }

        public void Create(BL.Movie movie)
        {
            // ignore movie ID if set

            var entity = new Entities.Movie
            {
                Title = movie.Title,
                ReleaseDate = movie.ReleaseDate,
                Genre = _dbContext.Genre.Find(movie.Genre.Id)
            };

            _dbContext.Movie.Add(entity);
            _dbContext.SaveChanges();
        }
    }
}
