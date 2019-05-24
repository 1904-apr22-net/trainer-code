using DogMvc.Models;

namespace DogMvc.ApiModels
{
    public static class Mapper
    {
        public static Dog Map(DogViewModel viewModel) => new Dog
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Breed = viewModel.Breed,
            Owner = new Account
            {
                Email = viewModel.OwnerEmail,
                Name = viewModel.OwnerName
            }
        };

        public static DogViewModel Map(Dog dog) => new DogViewModel
        {
            Id = dog.Id,
            Name = dog.Name,
            Breed = dog.Breed,
            OwnerEmail = dog.Owner?.Email,
            OwnerName = dog.Owner?.Name
        };
    }
}
