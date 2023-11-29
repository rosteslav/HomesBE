﻿using BuildingMarket.Auth.Domain.Entities;
using BuildingMarket.Images.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BuildingMarket.Images.Infrastructure.Persistence
{
    public class ImagesDbContext(DbContextOptions<ImagesDbContext> options)
        : DbContext(options)
    {
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<AdditionalUserData> AdditionalUserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("Images", "properties", i => i.ExcludeFromMigrations());

                entity.HasKey(img => img.Id);

                entity.Property(img => img.ImageURL)
                    .HasColumnName("image_url")
                    .HasColumnType("text");

                entity.Property(img => img.PropertyId)
                    .HasColumnName("property_id");                
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties", p => p.ExcludeFromMigrations());

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.NumberOfRooms)
                    .HasColumnName("number_of_rooms")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Space)
                    .HasColumnName("space")
                    .HasColumnType("real");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("real");

                entity.Property(e => e.Floor)
                    .HasColumnName("floor");

                entity.Property(e => e.TotalFloorsInBuilding)
                    .HasColumnName("total_floors_in_building");

                entity.Property(e => e.BuildingType)
                    .HasColumnName("building_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Finish)
                    .HasColumnName("finish")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Furnishment)
                    .HasColumnName("furnishment")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Garage)
                    .HasColumnName("garage")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Heating)
                    .HasColumnName("heating")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Neighbourhood)
                    .HasColumnName("neighbourhood")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.SellerId)
                    .HasColumnName("seller_id");

                entity.Property(e => e.BrokerId)
                    .HasColumnName("broker_id")
                    .IsRequired(false);

                entity.Property(e => e.CreatedOnUtcTime)
                    .HasColumnName("created_on_utc_time")
                    .HasColumnType("timestamptz")
                    .HasDefaultValue(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc));
            });

            modelBuilder.Entity<AdditionalUserData>(entity =>
            {
                entity.ToTable("AdditionalData", 
                    table => table.ExcludeFromMigrations());

                entity.HasKey(addData => addData.Id);

                entity.Property(addData => addData.Id)
                    .HasColumnName("id");

                entity.Property(addData => addData.FirstName)
                    .HasColumnName("first_name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(addData => addData.LastName)
                    .HasColumnName("last_name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(addData => addData.PhoneNumber)
                    .HasColumnName("phone_number")
                    .HasColumnType("character varying")
                    .HasMaxLength(15);

                entity.Property(addData => addData.UserId)
                    .HasColumnName("user_id")
                    .IsRequired(true);

                entity.Property(addData => addData.ImageUrl)
                   .HasColumnName("image_url")
                   .HasColumnType("character varying")
                   .HasMaxLength(255)
                   .IsRequired(true);
            });
        }
    }
}
