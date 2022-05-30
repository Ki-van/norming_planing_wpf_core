using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class Material: IDisposable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public JsonDocument Scalars { get; set; }
        public MaterialType Type { get; set; }
        public void Dispose() => Scalars?.Dispose();
    }

    public class MaterialType: IDisposable
    {
        public int Id { get; set; }
        [Required, StringLength(32)]
        public string Name { get; set; }
        public JsonDocument Structure { get; set; }

        public ICollection<Material> Materials { get; set; }
        public void Dispose() => Structure?.Dispose();
    }
}
