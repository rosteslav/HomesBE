using BuildingMarket.Admins.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Admins.Infrastructure.Persistence
{
    public class AdminsDbContext(DbContextOptions<AdminsDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public DbSet<Property> Properties { get; set; }

        public DbSet<Neighborhood> Neighborhoods { get; set; }

        public DbSet<NeighbourhoodsRating> NeighbourhoodsRating { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "security", u => u.ExcludeFromMigrations());
            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "security", u => u.ExcludeFromMigrations());

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties", e => e.ExcludeFromMigrations());

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

                entity.Property(e => e.Exposure)
                    .HasColumnName("exposure")
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

            modelBuilder.Entity<Neighborhood>(entity =>
            {
                entity.ToTable("Neighbourhoods", "properties", e => e.ExcludeFromMigrations());

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("region")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NeighbourhoodsRating>(entity =>
            {
                entity.ToTable("NeighbourhoodsRating", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ForLiving)
                    .HasColumnName("for_living")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(e => e.ForInvestment)
                    .HasColumnName("for_investment")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(e => e.Budget)
                    .HasColumnName("budget")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(e => e.Luxury)
                    .HasColumnName("luxury")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.HasData(new NeighbourhoodsRating
                {
                    Id = 1,
                    Budget = "[]",
                    ForInvestment = "[]",
                    ForLiving = "[]",
                    Luxury = "[]",
                },
                new NeighbourhoodsRating
                {
                    Id = 2,
                    Budget = "[]",
                    ForInvestment = "[]",
                    ForLiving = "[]",
                    Luxury = "[]",
                });
            });
        }
    }
}
