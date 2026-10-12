namespace MovieApplication.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        // Clé étrangère vers Genre
        public Guid GenreId { get; set; }

        // Un Movie appartient à un seul Genre
        public Genre? Genre { get; set; }
    }
}
