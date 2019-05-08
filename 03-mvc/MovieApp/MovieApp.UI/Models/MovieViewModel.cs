using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieApp.UI.Models
{
    // the purpose of a view model
    // is to organize data in the way that one or more particular views
    // needs. (probably different from either (1) how C# thinks about the data
    // in your business logic... or (2) how Entity Framework thinks about the data.)
    public class MovieViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime DateReleased { get; set; }
        public GenreViewModel Genre { get; set; }
    }
}
