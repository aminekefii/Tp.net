using Microsoft.EntityFrameworkCore;
using MovieApplication.Models;

namespace MovieApplication.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Membershiptype> MemberShipTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Schéma par défaut
            modelBuilder.HasDefaultSchema("dbo");

            // Configuration de Movie
            modelBuilder.Entity<Movie>(entity =>
            {
                entity.ToTable("T_Movie");
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("MovieName");

                // Un Genre a plusieurs Movies, un Movie a zéro ou un Genre
                entity.HasOne(m => m.Genre)
                    .WithMany(g => g.Movies)
                    .HasForeignKey(m => m.GenreId)
                    .IsRequired(false);

                // Relation N-N Movie <-> Customer
                entity.HasMany(m => m.Customers)
                    .WithMany(c => c.Movies);
            });

            // Configuration de Genre
            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("T_Genre");
                entity.HasKey(g => g.Id);
            });

            // Configuration de Customer
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("T_Customer");
                entity.HasKey(c => c.Id);

                // Une colonne indexée ne peut pas être nvarchar(max)
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasIndex(c => c.Name)
                    .IsUnique();

                // Un Membershiptype a plusieurs Customers, un Customer a zéro ou un Membershiptype
                entity.HasOne(c => c.Membershiptype)
                    .WithMany(m => m.Customers)
                    .HasForeignKey(c => c.MembershiptypeID)
                    .IsRequired(false);
            });

            // Configuration de Membershiptype
            modelBuilder.Entity<Membershiptype>(entity =>
            {
                entity.HasKey(m => m.Id);

                entity.Property(m => m.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                // Données initiales pour alimenter la liste déroulante du formulaire Customer
                entity.HasData(
                    new Membershiptype { Id = new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e01"), Name = "Pay as You Go", DurationInMonth = 0, DiscountRate = 0 },
                    new Membershiptype { Id = new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e02"), Name = "Monthly", DurationInMonth = 1, DiscountRate = 10 },
                    new Membershiptype { Id = new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e03"), Name = "Quarterly", DurationInMonth = 3, DiscountRate = 15 },
                    new Membershiptype { Id = new Guid("8f1c2a51-3d6e-4b1a-9c7d-1a2b3c4d5e04"), Name = "Annual", DurationInMonth = 12, DiscountRate = 20 });
            });
        }
    }
}
