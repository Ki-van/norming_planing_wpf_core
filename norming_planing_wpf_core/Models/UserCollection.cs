using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace norming_planing_wpf_core
{
    public class UserCollection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<UserCollectionItem> Items { get; set; }
    }

    public class UserCollectionItem
    {
        public string Value { get; set; }
        public int UserCollectionId { get; set; }
        public UserCollection UserCollection {get; set;}
    }
    class UserCollectionConfiguration : IEntityTypeConfiguration<UserCollection>
    {
        public void Configure(EntityTypeBuilder<UserCollection> builder)
        {
            
            builder.HasAlternateKey(uc => uc.Name);
        }
    }

    class UserCollectionItemConfiguration : IEntityTypeConfiguration<UserCollectionItem>
    {
        public void Configure(EntityTypeBuilder<UserCollectionItem> builder)
        {
            builder.HasKey(uci => new { uci.Value, uci.UserCollectionId });
        }
    }
}
