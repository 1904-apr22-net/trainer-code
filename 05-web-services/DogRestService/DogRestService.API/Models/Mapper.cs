namespace DogRestService.API.Models
{
    public static class Mapper
    {
        public static Dog Map(DAL.Entities.Dog entity) =>
            new Dog
            {
                Id = entity.Id,
                Name = entity.Name,
                Breed = entity.Breed
            };

        public static DAL.Entities.Dog Map(Dog dog) =>
            new DAL.Entities.Dog
            {
                Id = dog.Id,
                Name = dog.Name,
                Breed = dog.Breed
            };
    }
}
