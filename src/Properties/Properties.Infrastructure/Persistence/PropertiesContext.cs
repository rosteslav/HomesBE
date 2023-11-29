using BuildingMarket.Properties.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Properties.Infrastructure.Persistence
{
    public class PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Property> Properties { get; set; }

        public virtual DbSet<BuildingType> BuildingTypes { get; set; }

        public virtual DbSet<PublishedOn> PublishedOn { get; set; }

        public virtual DbSet<Exposure> Exposures { get; set; }

        public virtual DbSet<Finish> Finishes { get; set; }

        public virtual DbSet<Furnishment> Furnishments { get; set; }

        public virtual DbSet<Garage> Garages { get; set; }

        public virtual DbSet<Heating> Heating { get; set; }

        public virtual DbSet<Neighborhood> Neighborhoods { get; set; }

        public virtual DbSet<NumberOfRooms> NumberOfRooms { get; set; }

        public virtual DbSet<IdentityUser> Users { get; set; }

        public virtual DbSet<AdditionalUserData> AdditionalUserData { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.PublishedOn)
                    .HasColumnName("published_on")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

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

            modelBuilder.Entity<PublishedOn>(entity =>
            {
                entity.ToTable("PublishedOn", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("description")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.HasData(
                    new { Id = 1, Description = "Днес" },
                    new { Id = 2, Description = "Преди 3 дни" },
                    new { Id = 3, Description = "Преди седмица" },
                    new { Id = 4, Description = "Преди месец" },
                    new { Id = 5, Description = "Всички" });
            });

            modelBuilder.Entity<Neighborhood>(entity =>
            {
                entity.ToTable("Neighbourhoods", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("region")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Heating>(entity =>
            {
                entity.ToTable("Heating", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("heating_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.ToTable("Garage", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("garage_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Furnishment>(entity =>
            {
                entity.ToTable("Furnishment", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("furnishment_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Exposure>(entity =>
            {
                entity.ToTable("Exposures", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("exposure_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.HasData(
                    new { Id = 1, Description = "Юг" },
                    new { Id = 2, Description = "Изток" },
                    new { Id = 3, Description = "Запад" },
                    new { Id = 4, Description = "Север" });
            });

            modelBuilder.Entity<Finish>(entity =>
            {
                entity.ToTable("Finish", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("finish_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<BuildingType>(entity =>
            {
                entity.ToTable("BuildingType", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("building_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<NumberOfRooms>(entity =>
            {
                entity.ToTable("NumberOfRoomsType", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("number_of_rooms_type")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<IdentityUser>().ToTable("Users", "security", u => u.ExcludeFromMigrations());

            modelBuilder.Entity<AdditionalUserData>(entity =>
            {
                entity.ToTable("AdditionalData", "security", a => a.ExcludeFromMigrations());
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

            modelBuilder.Entity<Neighborhood>()
                .HasData(
                    new { Id = 1, Description = "Банишора", Region = "Север" },
                    new { Id = 2, Description = "Белите брези", Region = "Юг" },
                    new { Id = 3, Description = "Бенковски", Region = "Север" },
                    new { Id = 4, Description = "Борово", Region = "Юг" },
                    new { Id = 5, Description = "Бояна", Region = "Юг" },
                    new { Id = 6, Description = "Бъкстон", Region = "Юг" },
                    new { Id = 7, Description = "Витоша", Region = "Юг" },
                    new { Id = 8, Description = "Гевгелийски квартал", Region = "Запад" },
                    new { Id = 9, Description = "Гео Милев", Region = "Изток" },
                    new { Id = 10, Description = "Гоце Делчев", Region = "Юг" },
                    new { Id = 12, Description = "Дианабад", Region = "Юг" },
                    new { Id = 13, Description = "Драгалевци", Region = "Юг" },
                    new { Id = 14, Description = "Дружба", Region = "Изток" },
                    new { Id = 15, Description = "Дървеница", Region = "Юг" },
                    new { Id = 16, Description = "Западен парк", Region = "Запад" },
                    new { Id = 17, Description = "Захарна фабрика", Region = "Север" },
                    new { Id = 18, Description = "Иван Вазов", Region = "Юг" },
                    new { Id = 19, Description = "Изгрев", Region = "Юг" },
                    new { Id = 20, Description = "Изток", Region = "Изток" },
                    new { Id = 21, Description = "Илинден", Region = "Север" },
                    new { Id = 22, Description = "Илиянци", Region = "Север" },
                    new { Id = 23, Description = "Княжево", Region = "Юг" },
                    new { Id = 24, Description = "Красна поляна", Region = "Запад" },
                    new { Id = 25, Description = "Красно село", Region = "Юг" },
                    new { Id = 26, Description = "Крива река", Region = "Юг" },
                    new { Id = 27, Description = "Кръстова вада", Region = "Юг" },
                    new { Id = 28, Description = "Лагерът", Region = "Запад" },
                    new { Id = 29, Description = "Левски", Region = "Изток" },
                    new { Id = 30, Description = "Лозенец", Region = "Юг" },
                    new { Id = 31, Description = "Люлин", Region = "Запад" },
                    new { Id = 32, Description = "Малашевци", Region = "Изток" },
                    new { Id = 33, Description = "Малинова долина", Region = "Юг" },
                    new { Id = 34, Description = "Манастирски ливади", Region = "Юг" },
                    new { Id = 35, Description = "Младост", Region = "Юг" },
                    new { Id = 36, Description = "Модерно предградие", Region = "Север" },
                    new { Id = 37, Description = "Мусагеница", Region = "Юг" },
                    new { Id = 38, Description = "Надежда", Region = "Север" },
                    new { Id = 39, Description = "Обеля", Region = "Север" },
                    new { Id = 40, Description = "Оборище", Region = "Изток" },
                    new { Id = 41, Description = "Овча купел", Region = "Запад" },
                    new { Id = 42, Description = "Орландовци", Region = "Север" },
                    new { Id = 43, Description = "Павлово", Region = "Юг" },
                    new { Id = 44, Description = "Подуяне", Region = "Изток" },
                    new { Id = 45, Description = "Разсадника-Коньовица", Region = "Запад" },
                    new { Id = 46, Description = "Редута", Region = "Изток" },
                    new { Id = 47, Description = "Света Троица", Region = "Запад" },
                    new { Id = 48, Description = "Симеоново", Region = "Юг" },
                    new { Id = 49, Description = "Славия", Region = "Запад" },
                    new { Id = 50, Description = "Слатина", Region = "Изток" },
                    new { Id = 51, Description = "Стрелбище", Region = "Юг" },
                    new { Id = 52, Description = "Студентски град", Region = "Юг" },
                    new { Id = 53, Description = "Сухата река", Region = "Изток" },
                    new { Id = 54, Description = "Факултета", Region = "Запад" },
                    new { Id = 55, Description = "Хаджи Димитър", Region = "Изток" },
                    new { Id = 56, Description = "Хиподрумът", Region = "Запад" },
                    new { Id = 57, Description = "Хладилникът", Region = "Юг" },
                    new { Id = 58, Description = "Христо Ботев", Region = "Изток" },
                    new { Id = 59, Description = "Център", Region = "Централен" },
                    new { Id = 60, Description = "Яворов", Region = "Изток" });

            // Heating Type
            modelBuilder.Entity<Heating>()
                .HasData(
                    new { Id = 1, Description = "Без" },
                    new { Id = 2, Description = "ТЕЦ" },
                    new { Id = 3, Description = "Електричество" });

            // Garage Types
            modelBuilder.Entity<Garage>()
                .HasData(
                    new { Id = 1, Description = "Без" },
                    new { Id = 2, Description = "Включен в цената" },
                    new { Id = 3, Description = "С възможност" });

            //Furnishment Types
            modelBuilder.Entity<Furnishment>()
                .HasData(
                    new { Id = 1, Description = "Необзаведен" },
                    new { Id = 2, Description = "Обзаведен" },
                    new { Id = 3, Description = "До ключ" });

            //Finishing Types
            modelBuilder.Entity<Finish>()
                .HasData(
                    new { Id = 1, Description = "Акт 16" },
                    new { Id = 2, Description = "Акт 15" },
                    new { Id = 3, Description = "Акт 14" },
                    new { Id = 4, Description = "В Строеж" },
                    new { Id = 5, Description = "На Зелено" });

            //Building Types
            modelBuilder.Entity<BuildingType>()
                .HasData(
                    new { Id = 1, Description = "Тухла" },
                    new { Id = 2, Description = "ЕПК" },
                    new { Id = 3, Description = "Панел" });

            modelBuilder.Entity<NumberOfRooms>()
                .HasData(
                    new { Id = 1, Description = "Едностаен" },
                    new { Id = 2, Description = "Двустаен" },
                    new { Id = 3, Description = "Тристаен" },
                    new { Id = 4, Description = "Четиристаен" },
                    new { Id = 5, Description = "Многостаен" },
                    new { Id = 6, Description = "Мезонет" },
                    new { Id = 7, Description = "Гараж" },
                    new { Id = 8, Description = "Склад" },
                    new { Id = 9, Description = "Таванско помещение" });
        }
    }
}
