using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.BL
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
