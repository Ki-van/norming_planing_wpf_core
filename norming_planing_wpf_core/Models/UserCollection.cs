using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace norming_planing_wpf_core
{
    public class UserCollection
    {
        [Key]
        public string Name { get; set; }
        public List<string> Values { get; set; }
    }
    class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
    {
        public void Configure(EntityTypeBuilder<UserCollection> builder)
        {
            builder.Property(uc => uc.Values).HasJsonConversion();
        }
    }
}
