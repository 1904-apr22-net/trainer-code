namespace DogRestService.DAL.Entities
{
    public class Dog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Breed { get; set; }
        public int OwnerId { get; set; }

        public virtual Account Owner { get; set; }
    }
}