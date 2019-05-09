using MovieApp.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required] // null or empty string not allowed.
        [MinLength(3)]
        //[RegularExpression()]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [Required]
        public DateTime? DateReleased { get; set; }

        [Required]
        public Genre Genre { get; set; }

        // the view can only see what is on the model that you pass to it
        // so if the view needs to know about it, you need it on the model
        public List<Genre> Genres { get; set; }
    }
}
