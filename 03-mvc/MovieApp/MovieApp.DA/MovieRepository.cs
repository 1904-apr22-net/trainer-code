using MovieApp.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieApp.DA
{
    public class MovieRepository : IMovieRepository
    {
        private readonly List<Movie> _data;

        public MovieRepository(List<Movie> data)
        {
            _data = data ?? throw new ArgumentNullException(nameof(data));
        }

        public IEnumerable<Movie> GetAll()
        {
            return _data;
        }

        public IEnumerable<Genre> GetAllGenres()
        {
            return _data.Select(m => m.Genre).OrderBy(g => g.Id);
        }

        public Genre GetGenreById(int id)
        {
            return GetAllGenres().FirstOrDefault(g => g.Id == id);
        }

        public void Create(Movie movie)
        {
            // assign an ID...
            var id = _data.Max(x => x.Id) + 1;
            movie.Id = id;
            _data.Add(movie);
        }
    }
}
