﻿using BuildingMarket.Admins.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Admins.Infrastructure.Persistence
{
    public class AdminsDbContext(DbContextOptions<AdminsDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Property> Properties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>().ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");

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
