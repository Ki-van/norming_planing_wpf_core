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
        public DbSet<TP> TPs { get; set; }
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


        static AcszmkdbContext()
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<DraftStatus>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ShiftTaskStatus>();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=acszmkdb;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresEnum<DraftStatus>();
            modelBuilder.HasPostgresEnum<ShiftTaskStatus>();

            modelBuilder.ApplyConfiguration(new DraftConfiguration());
            modelBuilder.ApplyConfiguration(new MarkConfiguration());
            modelBuilder.ApplyConfiguration(new DetailConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeePositionConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ShiftTaskConfiguration());
            

            modelBuilder.Entity<TOType>().HasAlternateKey(tot => tot.Name);

            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, Name = "Заказчик1"},
                new { Id = 2, Name = "Заказчик2"},
                new { Id = 3, Name = "Заказчик3"}
               );
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
            modelBuilder.Entity<Material>().HasData(
                new { Id = 1, Name = "Балка 35Ш1", TypeId= 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 2, Name = "У 140х90х10", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 3, Name = "-12х240", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 4, Name = "-10х249", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 5, Name = "-30х330", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") }
                );
            modelBuilder.Entity<SteelGrade>().HasData(
                new { Id = 1, Name = "С345" },
                new { Id = 2, Name = "C35E " }
                );
            modelBuilder.Entity<Draft>().HasData(
                new { Id = 1, Name = "Свинокомлекс", Deadline = DateTime.UtcNow, CustomerId = 1},
                new { Id = 2, Name = "РГС", Deadline = DateTime.UtcNow, CustomerId = 2},
                new { Id = 3, Name = "Проект 3", Deadline = DateTime.UtcNow, CustomerId = 3, Status = DraftStatus.Planning}
                );
            modelBuilder.Entity<Mark>().HasData(
                new {Code = "М1", DraftId = 1, Name = "Балка1", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М2", DraftId = 1, Name = "Балка2", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М3", DraftId = 1, Name = "Балка3", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М1", DraftId = 2, Name = "Балка4", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М2", DraftId = 2, Name = "Балка5", StraightCount = (uint)2, OppositeCount = (uint)0},
                new {Code = "М3", DraftId = 2, Name = "Балка6", StraightCount = (uint)2, OppositeCount = (uint)0 }
                );
            modelBuilder.Entity<Detail>().HasData(
                new { Code = "Деталь 1", StraightCount = (uint)2,  Weight = (double)20, MarkCode = "М1", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 1 },
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М1", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 1},
                new { Code = "Деталь 1", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 2},
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1 , SteelGradeId = 1, MaterialId = 2}
                );
        }
    }
}
