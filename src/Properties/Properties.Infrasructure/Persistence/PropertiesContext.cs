using BuildingMarket.Properties.Domain.Entities;
using BuildingMarket.Properties.Domain.Entities.Enums;
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

                entity.Property(e => e.TotalFloorsInBuilding)
                    .HasColumnName("total_floors_in_building");

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

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("character varying")
                    .HasMaxLength(255);

                entity.Property(e => e.Region)
                .HasColumnName("region")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Heating>(entity =>
            {
                entity.ToTable("Heating", "heating");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Type)
                .HasColumnName("heating_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Garage>(entity =>
            {
                entity.ToTable("Garage", "garage");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Type)
                .HasColumnName("garage_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Furnishment>(entity =>
            {
                entity.ToTable("Furnishment", "furnishment");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Type)
                .HasColumnName("furnishment_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Finish>(entity =>
            {
                entity.ToTable("Finish", "finish");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Type)
                .HasColumnName("finish_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<BuildingType>(entity =>
            {
                entity.ToTable("BuildingType", "building_type");

                entity.HasKey(e => e.Id)
                .HasName("id");

                entity.Property(e => e.Type)
                .HasColumnName("building_type")
                .HasColumnType("character varying")
                .HasMaxLength(255);
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Банишора",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Белите брези",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Бенковски",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Борово",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Бояна",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Бъкстон",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Витоша",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Гевгелийски квартал",
                Region = Region.West

            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Гео Милев",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Гоце Делчев",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Дианабад",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Драгалевци",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Дружба",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Дървеница",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Западен парк",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Захарна фабрика",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Иван Вазов",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Изгрев",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Изток",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Илинден",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Илиянци",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Княжево",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Красна поляна",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Красно село",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Крива река",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Кръстова вада",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Лагерът",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Левски",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Лозенец",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Люлин",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Малашевци",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Малинова долина",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Манастирски ливади",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Младост",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Модерно предградие",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Мусагеница",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Надежда",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Обеля",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Оборище",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Овча купел",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Орландовци",
                Region = Region.North
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Павлово",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Подуяне",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Разсадника-Коньовица",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Редута",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Света Троица",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Симеоново",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Славия",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Слатина",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Стрелбище",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Студентски град",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Сухата река",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Факултета",
                Region = Region.West
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Хаджи Димитър",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Хиподрумът",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Хладилникът",
                Region = Region.South
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Христо Ботев",
                Region = Region.East
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Център",
                Region = Region.Central
            });

            modelBuilder.Entity<Neighborhood>()
            .HasData(new Neighborhood()
            {
                Name = "Яворов",
                Region = Region.East
            });

            // Heating Type
            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Type = HeatingType.None
            });

            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Type = HeatingType.TPP
            });

            modelBuilder.Entity<Heating>()
            .HasData(new Heating()
            {
                Type = HeatingType.Electrical
            });

            //Garage Types
            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Type = GarageType.None
               });

            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Type = GarageType.IncludedInPrice
               });

            modelBuilder.Entity<Garage>()
               .HasData(new Garage()
               {
                   Type = GarageType.WithPossibility
               });

            //Furnishment Types
            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Type = FurnishmentType.Unfurnished
               });

            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Type = FurnishmentType.Furnished
               });

            modelBuilder.Entity<Furnishment>()
               .HasData(new Furnishment()
               {
                   Type = FurnishmentType.Turnkey
               });

            //Finishing Types
            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Type = FinishType.Act16
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Type = FinishType.Act15
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Type = FinishType.Act14
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Type = FinishType.BeingBuilt
              });

            modelBuilder.Entity<Finish>()
              .HasData(new Finish()
              {
                  Type = FinishType.InPlanning
              });

            //Building Types
            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Type = BuildingMaterial.Brick
              });

            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Type = BuildingMaterial.ReinforcedConcrete
              });

            modelBuilder.Entity<BuildingType>()
              .HasData(new BuildingType()
              {
                  Type = BuildingMaterial.Panel
              });
        }
    }
}
