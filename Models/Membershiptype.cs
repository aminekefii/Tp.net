namespace MovieApplication.Models
{
    public class Membershiptype
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DurationInMonth { get; set; }

        public int DiscountRate { get; set; }

        public ICollection<Customer> Customers { get; set; }
            = new List<Customer>();
    }
}
