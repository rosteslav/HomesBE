using BuildingMarket.Properties.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Properties.Infrastructure.Persistence
{
    public class PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Property> Properties { get; set; }

        public virtual DbSet<BuildingType> BuildingTypes { get; set; }

        public virtual DbSet<Finish> Finishes { get; set; }

        public virtual DbSet<Furnishment> Furnishments { get; set; }

        public virtual DbSet<Garage> Garages { get; set; }

        public virtual DbSet<Heating> Heatings { get; set; }

        public virtual DbSet<Neighborhood> Neighborhoods { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

                entity.Property(e => e.BuildingTypeId)
                    .HasColumnName("building_type_id");

                entity.Property(e => e.FinishId)
                    .HasColumnName("finish_id");

                entity.Property(e => e.FurnishmentId)
                    .HasColumnName("furnishment_id");

                entity.Property(e => e.GarageId)
                    .HasColumnName("garage_id");

                entity.Property(e => e.HeatingId)
                    .HasColumnName("heating_id");

                entity.Property(e => e.NeighboourhoodId)
                    .HasColumnName("neighboourhood_id");

                entity.Property(e => e.SellerId)
                    .HasColumnName("seller_id");

                entity.Property(e => e.BrokerId)
                    .HasColumnName("broker_id")
                    .IsRequired(false);
            });

            modelBuilder.Entity<Neighborhood>(entity =>
            {
                entity.ToTable("Neighbourhoods", "neighbourhoods");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("region")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Heating>(entity =>
            {
                entity.ToTable("Heating", "heating");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("heating_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.ToTable("Garage", "garage");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("garage_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Furnishment>(entity =>
            {
                entity.ToTable("Furnishment", "furnishment");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("furnishment_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Finish>(entity =>
            {
                entity.ToTable("Finish", "finish");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("finish_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<BuildingType>(entity =>
            {
                entity.ToTable("BuildingType", "building_type");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Description)
                .HasColumnName("building_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Neighborhood>()
             .HasData(
                new { Description = "Банишора", Region = "Север" },
                new { Description = "Белите брези", Region = "Юг" },
                new { Description = "Бенковски", Region = "Север" },
                new { Description = "Борово", Region = "Юг" },
                new { Description = "Бояна", Region = "Юг" },
                new { Description = "Бъкстон", Region = "Юг" },
                new { Description = "Витоша", Region = "Юг" },
                new { Description = "Гевгелийски квартал", Region = "Запад" },
                new { Description = "Гео Милев", Region = "Изток" },
                new { Description = "Гоце Делчев", Region = "Юг" },
                new { Description = "Дианабад", Region = "Юг" },
                new { Description = "Драгалевци", Region = "Юг" },
                new { Description = "Дружба", Region = "Изток" },
                new { Description = "Дървеница", Region = "Юг" },
                new { Description = "Западен парк", Region = "Запад" },
                new { Description = "Захарна фабрика", Region = "Север" },
                new { Description = "Иван Вазов", Region = "Юг" },
                new { Description = "Изгрев", Region = "Юг" },
                new { Description = "Изток", Region = "Изток" },
                new { Description = "Илинден", Region = "Север" },
                new { Description = "Илиянци", Region = "Север" },
                new { Description = "Княжево", Region = "Юг" },
                new { Description = "Красна поляна", Region = "Запад" },
                new { Description = "Красно село", Region = "Юг" },
                new { Description = "Крива река", Region = "Юг" },
                new { Description = "Кръстова вада", Region = "Юг" },
                new { Description = "Лагерът", Region = "Запад" },
                new { Description = "Левски", Region = "Изток" },
                new { Description = "Лозенец", Region = "Юг" },
                new { Description = "Люлин", Region = "Запад" },
                new { Description = "Малашевци", Region = "Изток" },
                new { Description = "Малинова долина", Region = "Юг" },
                new { Description = "Манастирски ливади", Region = "Юг" },
                new { Description = "Младост", Region = "Юг" },
                new { Description = "Модерно предградие", Region = "Север" },
                new { Description = "Мусагеница", Region = "Юг" },
                new { Description = "Надежда", Region = "Север" },
                new { Description = "Обеля", Region = "Север" },
                new { Description = "Оборище", Region = "Изток" },
                new { Description = "Овча купел", Region = "Запад" },
                new { Description = "Орландовци", Region = "Север" },
                new { Description = "Павлово", Region = "Юг" },
                new { Description = "Подуяне", Region = "Изток" },
                new { Description = "Разсадника-Коньовица", Region = "Запад" },
                new { Description = "Редута", Region = "Изток" },
                new { Description = "Света Троица", Region = "Запад" },
                new { Description = "Симеоново", Region = "Юг" },
                new { Description = "Славия", Region = "Запад" },
                new { Description = "Слатина", Region = "Изток" },
                new { Description = "Стрелбище", Region = "Юг" },
                new { Description = "Студентски град", Region = "Юг" },
                new { Description = "Сухата река", Region = "Изток" },
                new { Description = "Факултета", Region = "Запад" },
                new { Description = "Хаджи Димитър", Region = "Изток" },
                new { Description = "Хиподрумът", Region = "Запад" },
                new { Description = "Хладилникът", Region = "Юг" },
                new { Description = "Христо Ботев", Region = "Изток" },
                new { Description = "Център", Region = "Централен" },
                new { Description = "Яворов", Region = "Изток" });

            // Heating Type
            modelBuilder.Entity<Heating>()
            .HasData(
            new { Description = "Без" },
            new { Description = "ТЕЦ" },
            new { Description = "Електричество" });

            // Garage Types
            modelBuilder.Entity<Garage>()
               .HasData(
            new { Description = "Без" },
            new { Description = "Включен в цената" },
            new { Description = "С възможност" });

            //Furnishment Types
            modelBuilder.Entity<Furnishment>()
               .HasData(
            new { Description = "еобзаведен" },
            new { Description = "Обзаведен" },
            new { Description = "До ключ" });

            //Finishing Types
            modelBuilder.Entity<Finish>()
              .HasData(
            new { Description = "Акт 16" },
            new { Description = "Акт 15" },
            new { Description = "Акт 14" },
            new { Description = "В Строеж" },
            new { Description = "На Зелено" });

            //Building Types
            modelBuilder.Entity<BuildingType>()
              .HasData(
            new { Description = "Тухла" },
            new { Description = "ЕПК" },
            new { Description = "Панел" });
        }
    }
}
