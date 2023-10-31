using Demo.Domain.Enities;
using Microsoft.EntityFrameworkCore;

namespace Demo.Infrastucture.Persistence
{
    public partial class ItemContext : DbContext
    {
        public ItemContext()
        {
        }

        public ItemContext(DbContextOptions<ItemContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>(entity =>
            {
                entity.ToTable("Items", "public");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}
