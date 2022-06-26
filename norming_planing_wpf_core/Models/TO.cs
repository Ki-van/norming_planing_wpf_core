using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class TO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TO? Previous { get; set; }
        public double NormTime { get; set; } = 0;
        public double NormPrice { get; set; } = 0;
        
        public JsonDocument? TypeParams { get; set; }
        public uint OperationCount { get; set; } = 1;
        public string MarkCode { get; set; }
        public int MarkDraftId { get; set; }
        public Mark Mark { get; set; }
        public TOType Type { get; set; }
        public NormingMap? NormingMap { get; set; }
        public List<MembersOneCvalification>? MembersCvalification { get; set; }
        public ICollection<Assemblie> Assemblies { get; set; }
        [NotMapped]
        public Assemblie? generatedAssemblie;
        [NotMapped]
        public Assemblie? GeneratedAssemblie { 
            get {
                if (Assemblies != null && Assemblies.Count > 0)
                    try
                    {
                        return Assemblies.First(a => a.AssemblieEntries.Any(ae =>
                        {
                            return ae.AssemblieId == a.Id && ae.EntryType.Equals(EntryType.Argument);
                        }));
                    } catch
                    {
                        return null;
                    }
                    else
                    return null;

            } set { generatedAssemblie = value; } }
        public ICollection<AssemblieEntry> AssemblieEntries { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<Instrument> Instruments { get; set; }
        
    }
    public class TOType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ArgumentCount ArgumentCount { get; set; } = ArgumentCount.OneArgument;
        public ObservableCollection<string>? IncludedWorks { get; set; } = new();
        public ObservableCollection<EntityParamType>? ParamsTypes { get; set; } = new();
        public ICollection<Instrument> Instruments { get; set; }
        public ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
    public class EntityParamType
    {
        public EntityParamType():this("", "") { }
        public EntityParamType(string name, string type): this(name, type, null) { }
        public EntityParamType(string name, string type, object? identifyer)
        {
            Name = name;
            Type = type;
            Identifyer = identifyer;
        }

        public string Name { get; set; }
        public string Type { get; set; } 
        public object? Identifyer { get; set; }

    }
    public enum ArgumentCount
    {
        OneArgument,
        MoreThanOne
    }

    class TOConfiguration : IEntityTypeConfiguration<TO>
    {
        public void Configure(EntityTypeBuilder<TO> builder)
        {
            builder.Property(to => to.OperationCount).HasDefaultValue((uint)1);
            builder.Property(to => to.MembersCvalification).HasJsonConversion();
            builder.HasMany(to => to.Details).WithMany(d => d.TOs);
            builder.HasMany(to => to.Assemblies)
                .WithMany(a => a.TOs)
                .UsingEntity<AssemblieEntry>(
                    j => j
                    .HasOne(ae => ae.Assemblie)
                    .WithMany(a => a.AssemblieEntries)
                    .HasForeignKey(ae => ae.AssemblieId),
                    j => j
                    .HasOne(ae => ae.TO)
                    .WithMany(to => to.AssemblieEntries)
                    .HasForeignKey(ae => ae.TOId),
                    j =>
                    {
                        j.HasKey(j => new { j.TOId, j.AssemblieId });
                        j.ToTable("AssemblieEntry");
                    }
                );
        }
    }
    class TOTypeConfiguration : IEntityTypeConfiguration<TOType>
    {
        public void Configure(EntityTypeBuilder<TOType> builder)
        {
            builder.HasMany(tot => tot.Instruments).WithMany(i => i.TOTypes);
            builder.Property(tot => tot.IncludedWorks).HasJsonConversion();
            builder.Property(tot => tot.ParamsTypes).HasJsonConversion();

        }
    }
}
