namespace DogRestService.API.Models
{
    public static class Mapper
    {
        public static Dog Map(DAL.Entities.Dog entity) =>
            new Dog
            {
                Id = entity.Id,
                Name = entity.Name,
                Breed = entity.Breed,
                Owner = entity.Owner is null ? null : Map(entity.Owner)
            };

        public static DAL.Entities.Dog Map(Dog dog) =>
            new DAL.Entities.Dog
            {
                Id = dog.Id,
                Name = dog.Name,
                Breed = dog.Breed,
                Owner = (dog.Owner?.Name is null || dog.Owner.Email is null) ? null : Map(dog.Owner),
                OwnerId = dog.Owner is null ? 0 : dog.Owner.Id
            };

        public static Account Map(DAL.Entities.Account entity) =>
            new Account
            {
                Id = entity.Id,
                Email = entity.Email,
                Name = entity.Name
            };

        public static DAL.Entities.Account Map(Account account) =>
            new DAL.Entities.Account
            {
                Id = account.Id,
                Email = account.Email,
                Name = account.Name
            };
    }
}
