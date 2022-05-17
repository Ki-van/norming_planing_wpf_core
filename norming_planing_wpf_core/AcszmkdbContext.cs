using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Npgsql;
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
            modelBuilder.Entity<Draft>().HasData(
                new { Id = 1, Name = "Свинокомлекс", Deadline = new DateTime(1000000000, DateTimeKind.Utc), CustomerId = 1},
                new { Id = 2, Name = "РГС", Deadline = new DateTime(1000000000, DateTimeKind.Utc), CustomerId = 2},
                new { Id = 3, Name = "Проект 3", Deadline = new DateTime(1000000000, DateTimeKind.Utc), CustomerId = 3, Status = DraftStatus.Planning}
                );
        }
    }
}
