namespace MovieApp.DA.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // could add ICollection<Movie> for the reverse navigation property
        // not required
    }
}