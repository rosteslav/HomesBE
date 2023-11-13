using Demo.Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastucture.Persistence
{
    public class PropertyContext : DbContext
    {
        public PropertyContext()
        {
        }

        public PropertyContext(DbContextOptions<PropertyContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "public");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                // Used value type smallint as it would fit the purpose.
                entity.Property(e => e.NumberOfRooms)
                    .HasColumnName("number of rooms")
                    .HasColumnType("smallint");

                entity.Property(e => e.District)
                    .HasColumnName("district")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                // Intechangable values for area of the property.
                // The property is defined as a float in C# context
                // and as real in PostgreSQL context.
                entity.Property(e => e.Space)
                    .HasColumnName("space")
                    .HasColumnType("real");

                entity.Property(e => e.Floor)
                    .HasColumnName("floor")
                    .HasColumnType("smallint");

                entity.Property(e => e.TotalFloorsInBuilding)
                    .HasColumnName("total floors in building")
                    .HasColumnType("smallint");

                entity.Property(e => e.SellerId)
                    .HasColumnName("seller id")
                    .HasColumnType("int");

                // Setting the property BrokerId to nullable using IsRequired(false) setting.
                entity.Property(e => e.BrokerId)
                    .HasColumnName("broker id")
                    .HasColumnType("int")
                    .IsRequired(false);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
