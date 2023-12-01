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

            modelBuilder.Entity<PreferencesOptions>(entity =>
            {
                entity.ToTable("PreferencesOptions", "security");
                entity.HasKey(prefOpt => prefOpt.Id);

                entity.Property(prefOpt => prefOpt.Id)
                .HasColumnName("id");

                entity.Property(prefOpt => prefOpt.Preference)
                .HasColumnName("preference")
                .IsRequired();

                entity.HasData(
                    new PreferencesOptions { Id = 1, Preference = "За живеене" },
                    new PreferencesOptions { Id = 2, Preference = "За инвестиция" },
                    new PreferencesOptions { Id = 3, Preference = "Бюджетен" },
                    new PreferencesOptions { Id = 4, Preference = "Луксозен" });
            });
        }
    }
}
