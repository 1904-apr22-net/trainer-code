using System;
using System.Collections.Generic;
using System.Text;

namespace MovieApp.DA.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime ReleaseDate { get; set; }

        // foreign key column is not necessary, will be added anyway
        // as a "shadow property"

        // navigation properties
        public virtual Genre Genre { get; set; }
    }
}
