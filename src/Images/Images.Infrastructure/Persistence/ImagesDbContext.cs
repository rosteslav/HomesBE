using BuildingMarket.Images.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Images.Infrastructure.Persistence
{
    public class ImagesDbContext(DbContextOptions<ImagesDbContext> options)
        : DbContext(options)
    {
        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Images", "properties");

                entity.HasKey(img => img.Id);

                entity.Property(img => img.ImageName)
                    .HasColumnName("image_name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(img => img.ImageURL)
                    .HasColumnName("image_url")
                    .HasColumnType("text");

                entity.Property(img => img.DeleteURL)
                    .HasColumnName("delete_url")
                    .HasColumnType("text");

                entity.Property(img => img.PropertyId)
                    .HasColumnName("property_id");
            });
        }
    }
}
