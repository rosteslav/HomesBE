using BuildingMarket.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Auth.Infrastructure.Persistence
{
    public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
        public virtual DbSet<AdditionalUserData> AdditionalUserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users", "security");
            modelBuilder.Entity<IdentityRole>()
                .ToTable("Roles", "security");
            modelBuilder.Entity<IdentityUserClaim<string>>()
                .ToTable("UserClaims", "security");
            modelBuilder.Entity<IdentityUserRole<string>>()
                .ToTable("UserRoles", "security");
            modelBuilder.Entity<IdentityUserLogin<string>>()
                .ToTable("UserLogins", "security");
            modelBuilder.Entity<IdentityRoleClaim<string>>()
                .ToTable("RoleClaims", "security");
            modelBuilder.Entity<IdentityUserToken<string>>()
                .ToTable("UserTokens", "security");
            modelBuilder.Entity<AdditionalUserData>(entity =>
            {
                entity.ToTable("AdditionalData", "security");
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
            });

            modelBuilder.Entity<Preferences>(entity =>
            {
                entity.ToTable("Preferences", "security");
                entity.HasKey(pref => pref.Id);

                entity.Property(pref => pref.Id)
                .HasColumnName("id");

                entity.Property(pref => pref.UserId)
                .HasColumnName("user_id")
                .HasColumnType("character varying")
                .HasMaxLength(255);

                entity.Property(pref => pref.Purpose)
                .HasColumnName("purpose")
                .HasColumnType("character varying")
                .HasMaxLength(255);

                entity.Property(pref => pref.Region)
                .HasColumnName("region")
                .HasColumnType("character varying")
                .HasMaxLength(255);

                entity.Property(pref => pref.BuildingType)
                .HasColumnName("building_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);

                entity.Property(pref => pref.PriceHigherEnd)
                .HasColumnName("price_higher_end")
                .HasColumnType("real");
            });
        }
    }
}
