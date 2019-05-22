using DogRestService.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestService.API.Repositories
{
    public class DogRepository
    {
        private readonly List<Dog> _list;

        public DogRepository() : this(new List<Dog>
        {
            new Dog { Id = 1, Breed = "Golden retriever", Name = "Bill" }
        })
        { }

        public DogRepository(List<Dog> list)
        {
            _list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public IEnumerable<Dog> GetAll()
        {
            return _list;
        }

        public Dog Get(int id)
        {
            return _list.First(x => x.Id == id);
        }
    }
}
