using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace DogMvc.Models
{
    public class DogViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Breed { get; set; }
    }
}
