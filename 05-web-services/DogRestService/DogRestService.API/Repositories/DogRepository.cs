using DogRestService.API.Models;
using DogRestService.DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DogRestService.API.Repositories
{
    public class DogRepository
    {
        private readonly DogDbContext _dbContext;

        public DogRepository(DogDbContext dbContext) =>
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        public IEnumerable<Dog> GetAll(string breed = null)
        {
            IQueryable<DAL.Entities.Dog> data = _dbContext.Dog;
            if (breed != null)
            {
                data = data.Where(x => x.Breed == breed);
            }
            IEnumerable<Dog> dogs = data.Select(Mapper.Map);
            return dogs;
        }

        public async Task<Dog> GetAsync(int id)
        {
            DAL.Entities.Dog entity = await _dbContext.Dog.FirstOrDefaultAsync(x => x.Id == id);
            return entity is null ? null : Mapper.Map(entity);
        }

        public async Task<bool> UpdateAsync(Dog dog)
        {
            if (!dog.Validate() && dog.Id != 0)
            {
                throw new ArgumentException("invalid dog", nameof(dog));
            }

            DAL.Entities.Dog existing = await _dbContext.Dog.FindAsync(dog.Id);

            if (existing is null)
            {
                return false;
            }

            DAL.Entities.Dog entity = Mapper.Map(dog);
            _dbContext.Entry(existing).CurrentValues.SetValues(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<int> CreateAsync(Dog dog)
        {
            if (dog is null)
            {
                throw new ArgumentNullException(nameof(dog));
            }
            if (!dog.Validate())
            {
                throw new ArgumentException("invalid dog", nameof(dog));
            }
            DAL.Entities.Dog entity = Mapper.Map(dog);
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
            return dog.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            DAL.Entities.Dog existing = await _dbContext.Dog.FindAsync(id);

            if (existing is null)
            {
                return false;
            }

            _dbContext.Remove(existing);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
