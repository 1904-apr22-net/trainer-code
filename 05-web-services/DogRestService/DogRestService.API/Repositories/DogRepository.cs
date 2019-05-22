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

        public IEnumerable<Dog> GetAll(string breed = null)
        {
            IEnumerable<Dog> result = _list;
            if (breed != null)
            {
                result = result.Where(x => x.Breed == breed);
            }
            return result;
        }

        public Dog Get(int id)
        {
            return _list.FirstOrDefault(x => x.Id == id);
        }

        public bool Update(Dog dog)
        {
            if (!dog.Validate() && dog.Id != 0)
            {
                throw new ArgumentException("invalid dog", nameof(dog));
            }

            var deleted = Delete(dog.Id);

            if (!deleted)
            {
                return false;
            }

            Create(dog, ignoreId: false);

            return true;
        }

        public int Create(Dog dog, bool ignoreId = true)
        {
            if (dog is null)
            {
                throw new ArgumentNullException(nameof(dog));
            }
            if (!dog.Validate())
            {
                throw new ArgumentException("invalid dog", nameof(dog));
            }
            if (ignoreId)
            {
                dog.Id = (_list.Count == 0) ? 1 : (_list.Max(x => x.Id) + 1);
            }
            _list.Add(dog);
            return dog.Id;
        }

        public bool Delete(int id)
        {
            // pattern matching syntax
            if (Get(id) is Dog dog)
            {
                _list.Remove(dog);
                return true;
            }
            return false;
        }
    }
}
