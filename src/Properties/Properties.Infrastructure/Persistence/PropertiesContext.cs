using BuildingMarket.Properties.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BuildingMarket.Properties.Infrastructure.Persistence
{
    public class PropertiesDbContext(DbContextOptions<PropertiesDbContext> options) : DbContext(options)
    {
        public virtual DbSet<Property> Properties { get; set; }

        public virtual DbSet<BuildingType> BuildingTypes { get; set; }

        public virtual DbSet<Exposure> Exposures { get; set; }

        public virtual DbSet<PublishedOn> PublishedOn { get; set; }

        public virtual DbSet<Finish> Finishes { get; set; }

        public virtual DbSet<Furnishment> Furnishments { get; set; }

        public virtual DbSet<Garage> Garages { get; set; }

        public virtual DbSet<Heating> Heating { get; set; }

        public virtual DbSet<Neighborhood> Neighborhoods { get; set; }

        public virtual DbSet<NeighbourhoodsRating> NeighbourhoodsRating { get; set; }

        public virtual DbSet<NumberOfRooms> NumberOfRooms { get; set; }

        public virtual DbSet<OrderBy> OrderBy { get; set; }

        public virtual DbSet<IdentityUser> Users { get; set; }

        public virtual DbSet<AdditionalUserData> AdditionalUserData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Properties", "properties");

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

                entity.Property(e => e.NumberOfDays)
                    .HasColumnName("number_of_days");

                entity.HasData(
                    new { Id = 1, Description = "Днес", NumberOfDays = 1 },
                    new { Id = 2, Description = "Преди 3 дни", NumberOfDays = 3 },
                    new { Id = 3, Description = "Преди седмица", NumberOfDays = 7 },
                    new { Id = 4, Description = "Преди месец", NumberOfDays = 30 },
                    new { Id = 5, Description = "Всички", NumberOfDays = 0 });
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

            modelBuilder.Entity<NeighbourhoodsRating>(entity =>
            {
                entity.ToTable("NeighbourhoodsRating", "properties", e => e.ExcludeFromMigrations());

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

            modelBuilder.Entity<OrderBy>(entity =>
            {
                entity.ToTable("OrderBy", "properties");

                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasColumnName("order_by")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);

                entity.Property(e => e.RelatedPropName)
                    .HasColumnName("related_prop_name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255)
                    .IsRequired(true);
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

                entity.Property(addData => addData.ImageURL)
                    .HasColumnName("image_url")
                    .HasColumnType("text");
            });

            modelBuilder.Entity<Neighborhood>()
                .HasData(
                    new { Id = 1, Description = "Център", Region = "Център" },
                    new { Id = 2, Description = "Зона Б-18", Region = "Север" },
                    new { Id = 3, Description = "Света Троица", Region = "Север" },
                    new { Id = 4, Description = "Захарна фабрика", Region = "Север" },
                    new { Id = 5, Description = "Модерно предградие", Region = "Север" },
                    new { Id = 6, Description = "Банишора", Region = "Север" },
                    new { Id = 7, Description = "Фондови жилища", Region = "Север" },
                    new { Id = 8, Description = "Триъгълника", Region = "Север" },
                    new { Id = 9, Description = "Надежда 1", Region = "Север" },
                    new { Id = 10, Description = "Надежда 3", Region = "Север" },
                    new { Id = 11, Description = "Връбница 1", Region = "Север" },
                    new { Id = 12, Description = "Обеля 1", Region = "Север" },
                    new { Id = 13, Description = "Обеля", Region = "Север" },
                    new { Id = 14, Description = "Обеля 2", Region = "Север" },
                    new { Id = 15, Description = "Връбница 2", Region = "Север" },
                    new { Id = 16, Description = "Илиянци", Region = "Север" },
                    new { Id = 17, Description = "Свобода", Region = "Север" },
                    new { Id = 18, Description = "Надежда 4", Region = "Север" },
                    new { Id = 19, Description = "Надежда 2", Region = "Север" },
                    new { Id = 20, Description = "Толстой", Region = "Север" },
                    new { Id = 21, Description = "Военна рампа", Region = "Север" },
                    new { Id = 22, Description = "Орландовци", Region = "Север" },
                    new { Id = 23, Description = "Хаджи Димитър", Region = "Север" },
                    new { Id = 24, Description = "Малашевци", Region = "Север" },
                    new { Id = 25, Description = "Левски В", Region = "Изток" },
                    new { Id = 26, Description = "Левски Г", Region = "Север" },
                    new { Id = 27, Description = "Левски", Region = "Изток" },
                    new { Id = 28, Description = "Сухата река", Region = "Изток" },
                    new { Id = 29, Description = "Подуяне", Region = "Изток" },
                    new { Id = 30, Description = "Яворов", Region = "Изток" },
                    new { Id = 31, Description = "Редута", Region = "Изток" },
                    new { Id = 32, Description = "Христо Ботев", Region = "Изток" },
                    new { Id = 33, Description = "Слатина", Region = "Изток" },
                    new { Id = 34, Description = "Гео Милев", Region = "Изток" },
                    new { Id = 35, Description = "Полигона", Region = "Изток" },
                    new { Id = 36, Description = "7-ми 11-ти километър", Region = "Изток" },
                    new { Id = 37, Description = "Дружба 1", Region = "Изток" },
                    new { Id = 38, Description = "Дружба 2", Region = "Изток" },
                    new { Id = 39, Description = "Оборище", Region = "Изток" },
                    new { Id = 40, Description = "Докторски паметник", Region = "Изток" },
                    new { Id = 41, Description = "Медицинска академия", Region = "Юг" },
                    new { Id = 42, Description = "Хиподрума", Region = "Юг" },
                    new { Id = 43, Description = "Красно село", Region = "Юг" },
                    new { Id = 44, Description = "Бъкстон", Region = "Юг" },
                    new { Id = 45, Description = "Павлово", Region = "Юг" },
                    new { Id = 46, Description = "м-т Гърдова глава", Region = "Юг" },
                    new { Id = 47, Description = "Княжево", Region = "Юг" },
                    new { Id = 48, Description = "в.з.Килиите", Region = "Юг" },
                    new { Id = 49, Description = "Иван Вазов", Region = "Юг" },
                    new { Id = 50, Description = "Стрелбище", Region = "Юг" },
                    new { Id = 51, Description = "Белите брези", Region = "Юг" },
                    new { Id = 52, Description = "Борово", Region = "Юг" },
                    new { Id = 53, Description = "Гоце Делчев", Region = "Юг" },
                    new { Id = 54, Description = "Манастирски ливади", Region = "Юг" },
                    new { Id = 55, Description = "в.з.Беловодски път", Region = "Юг" },
                    new { Id = 56, Description = "Бояна", Region = "Юг" },
                    new { Id = 57, Description = "в.з.Бояна", Region = "Юг" },
                    new { Id = 58, Description = "в.з.Киноцентъра 3 част", Region = "Юг" },
                    new { Id = 59, Description = "в.з.Киноцентъра", Region = "Юг" },
                    new { Id = 60, Description = "Драгалевци", Region = "Юг" },
                    new { Id = 61, Description = "Кръстова вада", Region = "Юг" },
                    new { Id = 62, Description = "Витоша", Region = "Юг" },
                    new { Id = 63, Description = "в.з.Симеоново - Драгалевци", Region = "Юг" },
                    new { Id = 64, Description = "Симеоново", Region = "Юг" },
                    new { Id = 65, Description = "в.з.Малинова долина", Region = "Юг" },
                    new { Id = 66, Description = "Лозенец", Region = "Юг" },
                    new { Id = 67, Description = "Хладилника", Region = "Юг" },
                    new { Id = 68, Description = "Изток", Region = "Юг" },
                    new { Id = 69, Description = "Изгрев", Region = "Юг" },
                    new { Id = 70, Description = "Дианабад", Region = "Юг" },
                    new { Id = 71, Description = "Мусагеница", Region = "Юг" },
                    new { Id = 72, Description = "Дървеница", Region = "Юг" },
                    new { Id = 73, Description = "Студентски град", Region = "Юг" },
                    new { Id = 74, Description = "Малинова долина", Region = "Юг" },
                    new { Id = 75, Description = "в.з.Американски колеж", Region = "Юг" },
                    new { Id = 76, Description = "Младост 4", Region = "Юг" },
                    new { Id = 77, Description = "Младост 3", Region = "Юг" },
                    new { Id = 78, Description = "Младост 2", Region = "Юг" },
                    new { Id = 79, Description = "Младост 1", Region = "Юг" },
                    new { Id = 80, Description = "Младост 1А", Region = "Юг" },
                    new { Id = 81, Description = "Експериментален", Region = "Юг" },
                    new { Id = 82, Description = "Горубляне", Region = "Юг" },
                    new { Id = 83, Description = "Карпузица", Region = "Запад" },
                    new { Id = 84, Description = "Овча купел", Region = "Запад" },
                    new { Id = 85, Description = "Славия", Region = "Запад" },
                    new { Id = 86, Description = "Лагера", Region = "Запад" },
                    new { Id = 87, Description = "Люлин 1", Region = "Запад" },
                    new { Id = 88, Description = "Люлин 8", Region = "Запад" },
                    new { Id = 89, Description = "Люлин 9", Region = "Запад" },
                    new { Id = 90, Description = "Люлин 10", Region = "Запад" },
                    new { Id = 91, Description = "Гевгелийски", Region = "Запад" },
                    new { Id = 92, Description = "Илинден", Region = "Запад" },
                    new { Id = 93, Description = "Зона Б-19", Region = "Запад" },
                    new { Id = 94, Description = "Зона Б-5", Region = "Запад" },
                    new { Id = 95, Description = "Зона Б-5-3", Region = "Запад" },
                    new { Id = 96, Description = "Сердика", Region = "Запад" },
                    new { Id = 97, Description = "Горна баня", Region = "Запад" },
                    new { Id = 98, Description = "Овча купел 2", Region = "Запад" },
                    new { Id = 99, Description = "Овча купел 1", Region = "Запад" },
                    new { Id = 100, Description = "Факултета", Region = "Запад" },
                    new { Id = 101, Description = "Красна поляна 2", Region = "Запад" },
                    new { Id = 102, Description = "Красна поляна 3", Region = "Запад" },
                    new { Id = 103, Description = "Разсадника", Region = "Запад" },
                    new { Id = 104, Description = "Красна поляна 1", Region = "Запад" },
                    new { Id = 105, Description = "Западен парк", Region = "Запад" },
                    new { Id = 106, Description = "Люлин 7", Region = "Запад" },
                    new { Id = 107, Description = "Люлин 6", Region = "Запад" },
                    new { Id = 108, Description = "Люлин 5", Region = "Запад" },
                    new { Id = 109, Description = "Люлин 4", Region = "Запад" },
                    new { Id = 110, Description = "Люлин 3", Region = "Запад" },
                    new { Id = 111, Description = "Люлин 2", Region = "Запад" },
                    new { Id = 112, Description = "Люлин - център", Region = "Запад" }
                  );

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

            // Number of Rooms
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

            // Order By
            modelBuilder.Entity<OrderBy>()
                .HasData(
                    new { Id = 1, Description = "Цена", RelatedPropName = "Price" },
                    new { Id = 2, Description = "Най-нови", RelatedPropName = "CreatedOnLocalTime" });
        }
    }
}
