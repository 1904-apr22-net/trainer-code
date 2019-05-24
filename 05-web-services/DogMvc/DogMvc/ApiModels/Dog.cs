namespace DogMvc.ApiModels
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public Account Owner { get; set; }
    }
}
