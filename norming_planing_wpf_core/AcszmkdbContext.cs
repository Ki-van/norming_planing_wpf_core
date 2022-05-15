using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

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
     
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=acszmkdb;Username=postgres;Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
