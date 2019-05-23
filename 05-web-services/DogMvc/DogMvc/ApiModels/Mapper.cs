using DogMvc.Models;

namespace DogMvc.ApiModels
{
    public static class Mapper
    {
        public static Dog Map(DogViewModel viewModel) => new Dog
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            Breed = viewModel.Breed
        };

        public static DogViewModel Map(Dog dog) => new DogViewModel
        {
            Id = dog.Id,
            Name = dog.Name,
            Breed = dog.Breed
        };
    }
}
