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
             .HasData(new Neighborhood()
             {
                 Description = "Банишора",
                 Region = "Север"
             });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Белите брези",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Бенковски",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Борово",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Бояна",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Бъкстон",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Витоша",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Гевгелийски квартал",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Гео Милев",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Гоце Делчев",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Дианабад",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Драгалевци",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Дружба",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Дървеница",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Западен парк",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Захарна фабрика",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Иван Вазов",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Изгрев",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Изток",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Илинден",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Илиянци",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Княжево",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Красна поляна",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Красно село",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Крива река",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Кръстова вада",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Лагерът",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Левски",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Лозенец",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Люлин",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Малашевци",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Малинова долина",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Манастирски ливади",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Младост",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Модерно предградие",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Мусагеница",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Надежда",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Обеля",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Оборище",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Овча купел",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Орландовци",
                Region = "Север"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Павлово",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Подуяне",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Разсадника-Коньовица",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Редута",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Света Троица",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Симеоново",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Славия",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Слатина",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Стрелбище",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Студентски град",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Сухата река",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Факултета",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Хаджи Димитър",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Хиподрумът",
                Region = "Запад"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Хладилникът",
                Region = "Юг"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Христо Ботев",
                Region = "Изток"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Център",
                Region = "Централен"
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Description = "Яворов",
                Region = "Изток"

            });

            // Heating Type
            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Description = "Без"
            });

            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Description = "ТЕЦ"
            });

            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Description = "Електричество"
            });

            //Garage Types
            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Description = "Без"
               });

            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Description = "Включен в цената"
               });

            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Description = "С възможност"
               });

            //Furnishment Types
            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Description = "еобзаведен"
               });

            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Description = "Обзаведен"
               });

            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Description = "До ключ"
               });

            //Finishing Types
            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Description = "Акт 16"
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Description = "Акт 15"
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Description = "Акт 14"
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Description = "В Строеж"
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Description = "На Зелено"
              });

            //Building Types
            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Description = "Тухла"
              });

            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Description = "ЕПК"
              });

            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Description = "Панел"
              });
        }
    }
}
