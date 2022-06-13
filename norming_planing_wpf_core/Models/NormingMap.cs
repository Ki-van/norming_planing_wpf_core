using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class NormingMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Norms? Norms { get; set; }
        public NormingMap? ParentMap { get; set; }
        public double? Coefficient { get; set; }
        public double OneHourPrice { get; set; }
        public TOType TOType { get; set; }
        public List<MembersOneCvalification> MembersCvalification { get; set; } 
            = new List<MembersOneCvalification>() { 
                new MembersOneCvalification { Cvalification = 1, Count = 1 } 
            };
    }
    public class MembersOneCvalification
    {
        public uint Cvalification { get; set; }
        public uint Count { get; set; }
    }
    public class Norms
    {
        public string FirstArgType { get; set; }
        public ICollection<INormsArg> FirstArgRange { get; set; }
        public ICollection<INormsArg> SecondArgRange { get; set; }
        public double[,] Values { get; set; }
    }
    public interface INormsArg
    {
        public string ArgName { get; set; }
    }
    class NormingMapConfiguration : IEntityTypeConfiguration<NormingMap>
    {
        public void Configure(EntityTypeBuilder<NormingMap> builder)
        {
            builder.Property(nm => nm.Norms).HasJsonConversion();
            builder.Property(nm => nm.MembersCvalification).HasJsonConversion();
        }
    }
}
