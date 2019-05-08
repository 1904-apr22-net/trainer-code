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

        public void Create(Movie movie)
        {
            // assign an ID...
            var id = _data.Max(x => x.Id) + 1;
            movie.Id = id;
            _data.Add(movie);
        }
    }
}
