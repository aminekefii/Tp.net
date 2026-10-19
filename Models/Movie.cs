namespace MovieApplication.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DateAdded { get; set; }

        public string? PhotoPath { get; set; }

        // Clé étrangère facultative vers Genre (0..1)
        public Guid? GenreId { get; set; }

        // Un Movie appartient à zéro ou un Genre
        public Genre? Genre { get; set; }

        // Relation N-N avec Customer
        public ICollection<Customer> Customers { get; set; }
            = new List<Customer>();
    }
}
