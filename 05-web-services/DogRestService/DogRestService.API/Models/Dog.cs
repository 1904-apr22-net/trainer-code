using System.ComponentModel.DataAnnotations;

namespace DogRestService.API.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string Name { get; set; }

        public Account Owner { get; set; }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(Breed))
            {
                return false;
            }
            return true;
        }
    }
}
