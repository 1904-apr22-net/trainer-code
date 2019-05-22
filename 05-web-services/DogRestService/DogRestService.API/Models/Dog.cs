using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestService.API.Models
{
    public class Dog
    {
        public int Id { get; set; }
        public string Breed { get; set; }
        public string Name { get; set; }
    }
}
