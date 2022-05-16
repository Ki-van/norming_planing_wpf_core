using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace norming_planing_wpf_core
{
    class DraftConfiguration: IEntityTypeConfiguration<Draft>
    {
        public void Configure(EntityTypeBuilder<Draft> builder)
        {
            builder.Property(u => u.Status).HasDefaultValue(DraftStatus.Defining);
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
            builder.Property(d => d.TotalCount)
                .HasComputedColumnSql(@"""StraightCount"" + ""OppositeCount""", stored: true);
            builder.Property(d => d.TotalWeight)
                .HasComputedColumnSql(@"(""StraightCount"" + ""OppositeCount"") * ""Weight""", stored:true);
        }
    }
}
