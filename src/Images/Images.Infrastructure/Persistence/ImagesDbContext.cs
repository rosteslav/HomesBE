using BuildingMarket.Images.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Images.Infrastructure.Persistence
{
    public class ImagesDbContext(DbContextOptions<ImagesDbContext> options)
        : DbContext(options)
    {
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Property> Properties { get; set; }

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

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.NumberOfRooms)
                    .HasColumnName("number_of_rooms");

                entity.Property(e => e.District)
                    .HasColumnName("district")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Space)
                    .HasColumnName("space")
                    .HasColumnType("real");

                entity.Property(e => e.Floor)
                    .HasColumnName("floor");

                entity.Property(e => e.TotalFloorsInBuilding)
                    .HasColumnName("total_floors_in_building");

                entity.Property(e => e.SellerId)
                    .HasColumnName("seller_id");

                entity.Property(e => e.BrokerId)
                    .HasColumnName("broker_id")
                    .IsRequired(false);
            });
        }
    }
}
