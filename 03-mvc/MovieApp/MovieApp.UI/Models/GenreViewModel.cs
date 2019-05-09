using System.ComponentModel.DataAnnotations;

namespace MovieApp.UI.Models
{
    public class GenreViewModel
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}