using BuildingMarket.Properties.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Properties.Infrasructure.Persistence
{
    public class PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.SellerId)
                    .HasColumnName("seller_id");

                entity.Property(e => e.BrokerId)
                    .HasColumnName("broker_id")
                    .IsRequired(false);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });
        }
    }
}
