using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace norming_planing_wpf_core
{
    public static class PropertyBuilderExtension
    {
        public static PropertyBuilder<T> HasJsonConversion<T>(this PropertyBuilder<T> propertyBuilder) where T : class, new()
        {
            ValueConverter<T, string> converter = new            (
                v => JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                v => JsonSerializer.Deserialize<T>(v, null as JsonSerializerOptions) ?? new T()
            );

            ValueComparer<T> comparer = new            (
                (l, r) => JsonSerializer.Serialize(l, null as JsonSerializerOptions) == JsonSerializer.Serialize(r, null as JsonSerializerOptions),
                v => v == null ? 0 : JsonSerializer.Serialize(v, null as JsonSerializerOptions).GetHashCode(),
                snapshotExpression: v => JsonSerializer.Deserialize<T>(
                    JsonSerializer.Serialize(v, null as JsonSerializerOptions),
                    null as JsonSerializerOptions)
            );

            propertyBuilder.HasConversion(converter);
            propertyBuilder.Metadata.SetValueConverter(converter);
            propertyBuilder.Metadata.SetValueComparer(comparer);
            propertyBuilder.HasColumnType("jsonb");

            return propertyBuilder;
        }
    }
}
