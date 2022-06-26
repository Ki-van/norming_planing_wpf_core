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
    public class AcszmkdbContext : DbContext
    {
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<Mark> Marks { get; set; }
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
           
            modelBuilder.ApplyConfiguration(new UserCollectionItemConfiguration());
            modelBuilder.ApplyConfiguration(new UserCollectionConfiguration());
            #endregion

            modelBuilder.Entity<TOType>().HasAlternateKey(tot => tot.Name);
            #region Seeds
            #region Customer
            modelBuilder.Entity<Customer>().HasData(
                new { Id = 1, Name = "Заказчик1" },
                new { Id = 2, Name = "Заказчик2" },
                new { Id = 3, Name = "Заказчик3" }
               );
            #endregion
            #region UserCollectoin&Items
            modelBuilder.Entity<UserCollection>().HasData(
                new { Id = 1, Name = "Типы сварочного шва" }
                );
            modelBuilder.Entity<UserCollectionItem>().HasData(
                new { Value = "C2", UserCollectionId = 1 },
                new { Value = "C7", UserCollectionId = 1 },
                new { Value = "C17", UserCollectionId = 1 },
                new { Value = "T1", UserCollectionId = 1 }
                );
            #endregion
            #region MaterialType
            modelBuilder.Entity<MaterialType>().HasData(
                new
                {
                    Id = 1,
                    Name = "Лист",
                    Structure = JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Сторона А", "a"),
                        new StructureItem("Сторона Б", "b"),
                        new StructureItem("Толщина", "с"),
                        new StructureItem("Площадь", "S", "a*b"),
                    })
                },
                new
                {
                    Id = 2,
                    Name = "Круг",
                    Structure =
                JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Диаметр наружный", "d"),
                        new StructureItem("Площадь сечения", "S","pi*(d/2)^2" ),
                    })
                },
                new
                {
                    Id = 3,
                    Name = "Балка",
                    Structure = JsonSerializer.SerializeToDocument(
                    new ObservableCollection<StructureItem> {
                        new StructureItem("Высота", "l"),
                        new StructureItem("Ширина", "w"),
                        new StructureItem("Толщина", "t"),
                    })
                },
                new
                {
                    Id = 4,
                    Name = "Уголок",
                    Structure = JsonSerializer.SerializeToDocument(
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
                new { Id = 1, Name = "Балка 10", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 3, Name = "У 50", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 4, Name = "У 56", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 5, Name = "У 63", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 6, Name = "У 70", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 7, Name = "У 75", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 8, Name = "У 80", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 9, Name = "У 90", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 10, Name = "У 100", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 11, Name = "У 110", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 12, Name = "-12х240", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 13, Name = "-10х249", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 14, Name = "-30х330", TypeId = 1, Scalars = JsonDocument.Parse(@"[{""Var"":""a"",""Val"":3},{""Var"":""b"",""Val"":2},{""Var"":""c"",""Val"":0.001},{""Var"":""S"",""Val"":6}]") },
                new { Id = 15, Name = "Балка 12", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 16, Name = "Балка 14", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 17, Name = "Балка 16", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 18, Name = "Балка 18", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 19, Name = "Балка 20", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 20, Name = "Балка 25", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 21, Name = "Балка 30", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 22, Name = "Балка 35", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 23, Name = "Балка 40", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 24, Name = "Балка 45", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 25, Name = "Балка 50", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 26, Name = "Балка 55", TypeId = 3, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") },
                new { Id = 2, Name = "У40", TypeId = 4, Scalars = JsonDocument.Parse(@"[{""Var"":""l"",""Val"":3},{""Var"":""w"",""Val"":2},{""Var"":""t"",""Val"":0.001}]") }

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
                new { Id = 1, Name = "Свинокомлекс", Deadline = DateTime.UtcNow, CustomerId = 1 },
                new { Id = 2, Name = "РГС", Deadline = DateTime.UtcNow, CustomerId = 2 },
                new { Id = 3, Name = "Проект 3", Deadline = DateTime.UtcNow, CustomerId = 3, Status = DraftStatus.Planning }
                );
            #endregion
            #region Mark
            modelBuilder.Entity<Mark>().HasData(
                new { Code = "М1", DraftId = 1, Name = "Балка1", StraightCount = (uint)2, OppositeCount = (uint)0 },
                new { Code = "М2", DraftId = 1, Name = "Балка2", StraightCount = (uint)2, OppositeCount = (uint)0 },
                new { Code = "М3", DraftId = 1, Name = "Балка3", StraightCount = (uint)2, OppositeCount = (uint)0 },
                new { Code = "М1", DraftId = 2, Name = "Балка4", StraightCount = (uint)2, OppositeCount = (uint)0 },
                new { Code = "М2", DraftId = 2, Name = "Балка5", StraightCount = (uint)2, OppositeCount = (uint)0 },
                new { Code = "М3", DraftId = 2, Name = "Балка6", StraightCount = (uint)2, OppositeCount = (uint)0 }
                );
            #endregion
            #region TO
            modelBuilder.Entity<TO>().HasData(
              new { Id = 1, MarkCode = "М1", Name = "Сегмент кольца жёсткости", MarkDraftId = 1, TypeId = 1, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 2, MarkCode = "М1", Name = "Пластина кольца жёсткости", MarkDraftId = 1, TypeId = 1, PreviousId = 1, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 3, MarkCode = "М1", Name = "Элемент днища", MarkDraftId = 1, TypeId = 1, PreviousId = 2, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 4, MarkCode = "М1", Name = "Элемент днища", MarkDraftId = 1, TypeId = 1, PreviousId = 3, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 5, MarkCode = "М1", Name = "Элемент горловины (рыбка)", MarkDraftId = 1, TypeId = 1, PreviousId = 4, NormTime = 0.2, NormPrice = 20.1 },
              
              new { Id = 6, MarkCode = "М1", Name = "Установка краевых рёбер жёсткости на карту", MarkDraftId = 1, TypeId = 3, PreviousId = 5, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 7, MarkCode = "М1", Name = "Установка днищ и рёбер жёсткости на карту обечайки", MarkDraftId = 1, TypeId = 3, PreviousId = 6, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 8, MarkCode = "М1", Name = "Установка монтажного прогона", MarkDraftId = 1, TypeId = 3, PreviousId = 7, NormTime = 0.2, NormPrice = 20.1 },
              new { Id = 9, MarkCode = "М1", Name = "Установка каната лебёдки с роликовым  блоком", MarkDraftId = 1, TypeId = 3, PreviousId = 8, NormTime = 0.2, NormPrice = 20.1 }
                );
            #endregion
            #region TOTypes
            modelBuilder.Entity<TOType>().HasData(
              new { Id = 1, Name = "Резка", ArgumentCount = ArgumentCount.OneArgument },
              new { Id = 2, Name = "Сверление", ArgumentCount = ArgumentCount.OneArgument, ParamsTypes = new ObservableCollection<EntityParamType>() { 
                new EntityParamType("Диаметр отверстий", typeof(Double).Name)
              } },
              new { Id = 3, Name = "Сборка", ArgumentCount = ArgumentCount.MoreThanOne },
              new { Id = 4, Name = "Сварка", ArgumentCount = ArgumentCount.MoreThanOne,
                  ParamsTypes = new ObservableCollection<EntityParamType>() {
                new EntityParamType("Тип шва", (new UserCollection()).GetType().Name, (object)1)
              }
              },
              new { Id = 5, Name = "Зачистка", ArgumentCount = ArgumentCount.OneArgument },
              new { Id = 6, Name = "Окраска", ArgumentCount = ArgumentCount.OneArgument,
                  IncludedWorks = new ObservableCollection<string>() {"Очистка", "Обезжиривание", "Грунтование пистолетом на 1 слой", "Окрашивание пистолетом на 1 слой" } }
              );
            #endregion
            #region Instruments
            modelBuilder.Entity<Instrument>().HasData(
              new { Id = 1, Name = "Плазма" },
              new { Id = 2, Name = "Сверлильный станок" },
              new { Id = 3, Name = "Автоматическая сварка в аргонной стреде" },
              new { Id = 5, Name = "Болгарка" },
              new { Id = 6, Name = "Летночная пила" },
              new { Id = 7, Name = "Плазменный станок" }
              );
            #endregion
            #region Detail
            modelBuilder.Entity<Detail>().HasData(
                new { Code = "Деталь 1", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М1", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 1 },
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М1", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 1 },
                new { Code = "Деталь 1", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 2 },
                new { Code = "Деталь 2", StraightCount = (uint)2, Weight = (double)20, MarkCode = "М2", MarkDraftId = 1, SteelGradeId = 1, MaterialId = 2 }
                );
            #endregion
            #region NormingMaps
            modelBuilder.Entity<NormingMap>().HasData(
                new
                {
                    Id = 1,
                    Name = "Резка на ленточной пиле",
                    OneHourPrice = 200.0,
                    TOTypeId = 1,
                    MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 1, Count = 1 }
                    },
                    FirstArg = new NormsArgument()
                    {
                        Range = new List<string>()
                            {
                                "Балка 12",
                                "Балка 14",
                                "Балка 16",
                                "Балка 18",
                                "Балка 20",
                                "Балка 25",
                                "Балка 30",
                                "Балка 35",
                                "Балка 40",
                                "Балка 45",
                                "Балка 50",
                                "Балка 55",
                            },
                        Name = "Тип материала, Балка",
                        Path = "Material"
                    },
                    Norms = new List<List<double>>() { new List<double>{ 0.045, 0.059, 0.07, 0.092, 0.1, 0.12, 0.14, 0.17, 0.18, 0.2, 0.23, 0.25 } }

                },
                new { Id = 2, Name = "Плазменнная резка", OneHourPrice = 150.0, TOTypeId = 1,
                    MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 3, Count = 1 }
                    }
                },
                new { Id = 3, Name = "Механизированная дуговая сварка в углекислом газе", OneHourPrice = 300.0, TOTypeId = 4,
                    MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 4, Count = 1 }
                    }
                },
                new { Id = 4, Name = "Зачистка кромок", OneHourPrice = 100.0, TOTypeId = 5, MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 1, Count = 1 }
                    }
                },
                new { Id = 5, Name = "Окрашивание лакокрасочными составами", OneHourPrice = 350.0, TOTypeId = 6, MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 1, Count = 1 },
                        new MembersOneCvalification { Cvalification = 3, Count = 1 },
                    }
                },
                new { Id = 6, Name = "Сверление отверстий на станке", OneHourPrice = 350.0, TOTypeId = 2, MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 1, Count = 1 }
                    }
                },
                new { Id = 7, Name = "Сборка балок, ригелей и прогонов", OneHourPrice = 350.0, TOTypeId = 3,
                    MembersCvalification = new List<MembersOneCvalification>(){
                        new MembersOneCvalification { Cvalification = 1, Count = 4 },
                        new MembersOneCvalification { Cvalification = 2, Count = 3 },
                        new MembersOneCvalification { Cvalification = 1, Count = 2 },
                    },
                    SecondArg = new NormsArgument()
                    {
                        Range = new List<string>()
                            {
                                "5",
                                "7",
                                "10",
                                "15",
                                "20",
                                "30",
                                "40",
                            },
                        Name = "Количество деталей в конструкции",
                        Path = "AssemblieEntries.Count"
                    },
                    FirstArg = new NormsArgument()
                    {
                        Range = new List<string>()
                            {
                                "150",
                                "200",
                                "300",
                                "400",
                                "500",
                                "800",
                                "1000",
                            },
                        Name = "Масса конструкции, кг",
                        Path = "TotalWeight"
                    },
                    Norms = new List<List<double>>() { 
                        new List<double>(){ 3.1, 2.7, 2.2, 1.8, 1.6, 1, 0.83}, 
                        new List<double>(){ 3.7, 3.1, 2.6, 2, 1.8, 1.3, 0.93}, 
                        new List<double>(){ 5.1, 4.5, 3.4, 2.5, 2.3, 1.7, 1.2}, 
                        new List<double>(){ 7.2, 6.2, 4.7, 3.3, 3, 2.1, 1.5}, 
                        new List<double>(){ 9.5, 8.4, 6.2, 4.4, 3.8, 2.8, 2}, 
                        new List<double>(){ 12.5, 10.5, 7.7, 5.5, 4.7, 3.3, 2.4}, 
                        new List<double>(){ 15, 13, 9.2, 6.6, 5.5, 4, 2.9}, 
                    }
                }
                );
            #endregion
           
            #endregion

        }
    }
}
