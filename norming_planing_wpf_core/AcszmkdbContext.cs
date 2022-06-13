using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Collections.ObjectModel;

namespace norming_planing_wpf_core
{
    public class AcszmkdbContext: DbContext
    {
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Mark> Marks  { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<TO> TOs { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<TOType> TOTypes { get; set; }
        public DbSet<Instrument> Instruments { get; set; }
        public DbSet<NormingMap> NormingMaps { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeePosition> EmployeePositions { get; set; }
        public DbSet<ShiftTask> ShiftTasks { get; set; }
        public DbSet<Material> Materials { get; set; }
        public DbSet<MaterialType> MaterialTypes { get; set; }
        public DbSet<SteelGrade> SteelGrades { get; set; }
        public DbSet<UserCollection> UserCollections { get; set; }


        static AcszmkdbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<DraftStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ShiftTaskStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<EntryType>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=acszmkdb;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<DraftStatus>();
            modelBuilder.HasPostgresEnum<ShiftTaskStatus>();
            modelBuilder.HasPostgresEnum<EntryType>();
            #region Model Configuretions
            modelBuilder.ApplyConfiguration(new DraftConfiguration());
            modelBuilder.ApplyConfiguration(new MarkConfiguration());
            modelBuilder.ApplyConfiguration(new DetailConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePositionConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftTaskConfiguration());
            modelBuilder.ApplyConfiguration(new NormingMapConfiguration());
            modelBuilder.ApplyConfiguration(new TOConfiguration());
            modelBuilder.ApplyConfiguration(new TOTypeConfiguration());
            modelBuilder.ApplyConfiguration(new TOTypeConfiguration());
            modelBuilder.ApplyConfiguration(new UserCollectionConfiguration());
            #endregion

            modelBuilder.Entity<TOType>().HasAlternateKey(tot => tot.Name);
            #region Seeds
            #region Customer
            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, Name = "Заказчик1"},
                new { Id = 2, Name = "Заказчик2"},
                new { Id = 3, Name = "Заказчик3"}
               );
            #endregion
            #region MaterialType
            modelBuilder.Entity<MaterialType>().HasData(
                new {Id = 1, Name = "Лист", Structure = JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Сторона А", "a"),
                        new StructureItem("Сторона Б", "b"),
                        new StructureItem("Толщина", "с"),
                        new StructureItem("Площадь", "S", "a*b"),
                    }) },
                new {Id = 2, Name = "Круг", Structure =
                JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Диаметр наружный", "d"),
                        new StructureItem("Площадь сечения", "S","pi*(d/2)^2" ),
                    })},
                new {Id = 3, Name = "Балка", Structure = JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Высота", "l"), 
                        new StructureItem("Ширина", "w"), 
                        new StructureItem("Толщина", "t"), 
                    } )},
                new {Id = 4, Name = "Уголок", Structure = JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Высота", "l"),
                        new StructureItem("Ширина", "w"),
                        new StructureItem("Толщина", "t"),
                    })
                }
                );
            #endregion
            #region Material
            modelBuilder.Entity<Material>().HasData(
                new { Id = 1, Name = "Балка 35Ш1", TypeId= 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 2, Name = "У 140х90х10", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 3, Name = "-12х240", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 4, Name = "-10х249", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 5, Name = "-30х330", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") }
                );
            #endregion
            #region SteelGrade
            modelBuilder.Entity<SteelGrade>().HasData(
                new { Id = 1, Name = "С345" },
                new { Id = 2, Name = "C35E " }
                );
            #endregion
            #region Draft
            modelBuilder.Entity<Draft>().HasData(
                new { Id = 1, Name = "Свинокомлекс", Deadline = DateTime.UtcNow, CustomerId = 1},
                new { Id = 2, Name = "РГС", Deadline = DateTime.UtcNow, CustomerId = 2},
                new { Id = 3, Name = "Проект 3", Deadline = DateTime.UtcNow, CustomerId = 3, Status = DraftStatus.Planning}
                );
            #endregion
            #region Mark
            modelBuilder.Entity<Mark>().HasData(
                new {Code = "М1", DraftId = 1, Name = "Балка1", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М2", DraftId = 1, Name = "Балка2", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М3", DraftId = 1, Name = "Балка3", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М1", DraftId = 2, Name = "Балка4", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М2", DraftId = 2, Name = "Балка5", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М3", DraftId = 2, Name = "Балка6", StraightCount = (uint)2, OppositeCount = (uint)0 }
                );
            #endregion
            #region TO
            modelBuilder.Entity<TO>().HasData(
              new { Id = 1, MarkCode = "М1", Name = "Сегмент кольца жёсткости", MarkDraftId = 1, TypeId = 1, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 2, MarkCode = "М1", Name = "Пластина кольца жёсткости", MarkDraftId = 1, TypeId = 1, PreviousId = 1, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 3, MarkCode = "М1", Name = "Элемент днища", MarkDraftId = 1, TypeId = 1, PreviousId = 2, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 4, MarkCode = "М1", Name = "Элемент днища", MarkDraftId = 1, TypeId = 1, PreviousId = 3, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 5, MarkCode = "М1", Name = "Элемент горловины (рыбка)", MarkDraftId = 1, TypeId = 1, PreviousId = 4, NormTime = 0.2, NormPrice = 20.1 }
              );
            #endregion
            #region DetailTO

            #endregion
            #region TOTypes
            modelBuilder.Entity<TOType>().HasData(
              new { Id = 1, Name = "Резка", ArgumentCount = ArgumentCount.OneArgument},
              new { Id = 2, Name = "Сверление", ArgumentCount = ArgumentCount.OneArgument },
              new { Id = 3, Name = "Сборка", ArgumentCount = ArgumentCount.MoreThanOne },
              new { Id = 4, Name = "Сварка", ArgumentCount = ArgumentCount.MoreThanOne },
              new { Id = 5, Name = "Зачистка", ArgumentCount = ArgumentCount.OneArgument },
              new { Id = 6, Name = "Окраска", ArgumentCount = ArgumentCount.OneArgument }
              );
            #endregion
            #region Instruments
            modelBuilder.Entity<Instrument>().HasData(
              new { Id = 1, Name = "Плазма" },
              new { Id = 2, Name = "Сверлильный станок" },
              new { Id = 3, Name = "Автоматическая сварка в аргонной стреде" },
              new { Id = 5, Name = "Болгарка" },
              new { Id = 6, Name = "Летночная пила" },
              new { Id = 7, Name = "Плазменный станок"}
              );
            #endregion
            #region Detail
            modelBuilder.Entity<Detail>().HasData(
                new { Code = "Деталь 1", StraightCount = (uint)2,  Weight = (double)20, MarkCode = "М1", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 1 },
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М1", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 1},
                new { Code = "Деталь 1", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 2},
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 2}
                );
            #endregion
            #region NormingMaps
            modelBuilder.Entity<NormingMap>().HasData(
                new { Id = 1, Name = "Резка на ленточной пиле", OneHourPrice = 200.0, TOTypeId = 1, MembersCvalification = new List<MembersOneCvalification>() }
                );
            #endregion
            #region UserCollectoin
            modelBuilder.Entity<UserCollection>().HasData(
                new { Name = "Типы сварочного шва", Values = new List<string> {"C2","C7","C17", "C21", "Т1" } }
                );
            #endregion
            #endregion

        }
    }
}
