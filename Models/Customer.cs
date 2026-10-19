namespace MovieApplication.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // Clé étrangère facultative vers Membershiptype (0..1)
        public Guid? MembershiptypeID { get; set; }

        public Membershiptype? Membershiptype { get; set; }

        // Relation N-N avec Movie
        public ICollection<Movie> Movies { get; set; }
            = new List<Movie>();
    }
}
