using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class NormingMap
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public NormsArgument? FirstArg { get; set; }
        public NormsArgument? SecondArg { get; set; }
        public List<List<double>>? Norms { get; set; }
        public NormingMap? ParentMap { get; set; }
        public double? Coefficient { get; set; }
        public double OneHourPrice { get; set; }
        [NotMapped]
        public string TOTypeName { 
            get
            {
                if (this.TOType != null)
                    return TOType.Name;
                else
                    return "";
            } 
        }
        public int TOTypeId { get; set; }
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
    public class NormsArgument
    {
        public string Path { get; set; }
        public string Name { get; set; }
        public ICollection<string> Range { get; set; }
    }
    class NormingMapConfiguration : IEntityTypeConfiguration<NormingMap>
    {
        public void Configure(EntityTypeBuilder<NormingMap> builder)
        {
            builder.Property(nm => nm.FirstArg).HasJsonConversion();
            builder.Property(nm => nm.SecondArg).HasJsonConversion();
            builder.Property(nm => nm.Norms).HasJsonConversion();
            builder.Property(nm => nm.MembersCvalification).HasJsonConversion();
        }
    }
}
