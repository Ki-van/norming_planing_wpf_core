using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace norming_planing_wpf_core
{
    class DraftConfiguration: IEntityTypeConfiguration<Draft>
    {
        public void Configure(EntityTypeBuilder<Draft> builder)
        {
            builder.Property(u => u.Status).HasDefaultValue(DraftStatus.Defining);
            builder.Property(u => u.Priority).HasDefaultValue((uint)0);
            builder.Property(u => u.CreatedAt).HasDefaultValueSql("now()");
            
        }
    }
    class MarkConfiguration : IEntityTypeConfiguration<Mark>
    {
        public void Configure(EntityTypeBuilder<Mark> builder)
        {
            builder.HasKey(u => new {u.Code, u.DraftId});
        }
    }
    
    class DetailConfiguration : IEntityTypeConfiguration<Detail>
    {
        public void Configure(EntityTypeBuilder<Detail> builder)
        {
            builder.HasKey(u => new {u.Code, u.MarkCode, u.MarkDraftId});
            builder.HasOne(d => d.Mark)
                .WithMany(m => m.Details)
                .HasForeignKey(d => new { d.MarkCode, d.MarkDraftId });
            builder.Property(d => d.OppositeCount).HasDefaultValue(0);
        }
    }

    class EmployeePositionConfiguration : IEntityTypeConfiguration<EmployeePosition>
    {
        public void Configure(EntityTypeBuilder<EmployeePosition> builder)
        {
            builder.HasAlternateKey(ep => ep.Position);
        }
    }
    class TOConfiguration : IEntityTypeConfiguration<TO>
    {
        public void Configure(EntityTypeBuilder<TO> builder)
        {
            builder.HasMany(to => to.Details).WithMany(d => d.TOs);
            builder.Property(to => to.OperationCount).HasDefaultValue((uint)1);
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
            builder.Property(tot => tot.Params).HasJsonConversion();
        }
    }

    class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasMany(e => e.ShiftTasks)
                .WithMany(st => st.Employees)
                .UsingEntity<TaskParticipation>(
                    j => j
                    .HasOne(tp => tp.ShiftTask)
                    .WithMany(st => st.Participations)
                    .HasForeignKey(tp => tp.ShiftTaskId),
                    j => j
                    .HasOne(tp => tp.Employee)
                    .WithMany(e => e.Participations)
                    .HasForeignKey(tp => tp.EmployeeId),
                    j =>
                    {
                        j.HasKey(j => new { j.EmployeeId, j.ShiftTaskId });
                        j.ToTable("TaskParticipation");
                    });
        }
    }

  
}
