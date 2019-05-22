using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestService.API.Models
{
    public class Dog
    {
        public int Id { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string Name { get; set; }

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
