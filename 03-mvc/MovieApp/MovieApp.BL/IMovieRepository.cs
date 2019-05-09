using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.BL
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();
        void Create(Movie movie);

        IEnumerable<Genre> GetAllGenres();

        Genre GetGenreById(int id);
    }
}
