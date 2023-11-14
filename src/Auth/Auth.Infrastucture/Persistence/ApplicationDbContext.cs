using BuildingMarket.Auth.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Auth.Infrastructure.Persistence
{
    public partial class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<IdentityUser>(options)
    {
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
            modelBuilder.Entity<AdditionalUserData>()
                .ToTable("AdditionalData", "security")
                .HasKey(au => au.Id);
        }
    }
}
