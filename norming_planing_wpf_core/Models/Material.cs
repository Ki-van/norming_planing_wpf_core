using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;

namespace norming_planing_wpf_core
{
    public class Material : IDisposable 
    {
        public Material()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public JsonDocument Scalars { get; set; } 
        public MaterialType Type { get; set; }

        [NotMapped]
        private ObservableCollection<ScalarItem> _scalarItems;
        [NotMapped]
        public ObservableCollection<ScalarItem> ScalarItems
        {
            get
            {
                return _scalarItems
                    ??= Scalars.Deserialize<ObservableCollection<ScalarItem>>()
                    ?? new ObservableCollection<ScalarItem>();
            }
            set { _scalarItems = value; }
        }
        public void SerializeModel()
        {
            Scalars = JsonSerializer.SerializeToDocument(ScalarItems);
        }
        public void Dispose() => Scalars?.Dispose();
    }
    public class ScalarItem
    {
        public ScalarItem()
        {
        }

        public ScalarItem(string var)
        {
            Var = var;
        }
        [JsonConstructor]
        public ScalarItem(string var, double val)
        {
            Var = var;
            Val = val;
        }
        public string Var { get; set; }
        public double Val { get; set; } = .0;
    }
    public class MaterialType: IDisposable
    {
        [NotMapped]
        public static readonly int DefaultId = -1;
        public int Id { get; set; } = DefaultId;
        [Required, StringLength(32)]
        public string Name { get; set; }
        public JsonDocument Structure { get; set; }
        [NotMapped]
        private ObservableCollection<StructureItem> _structureItems;
        [NotMapped]
        public ObservableCollection<StructureItem> StructureItems
        {
            get
            {
                return _structureItems 
                    ??= Structure.Deserialize<ObservableCollection<StructureItem>>() 
                    ?? new ObservableCollection<StructureItem>();
            }
            set { _structureItems = value; }
        }
        public void SerializeModel()
        {
            Structure = JsonSerializer.SerializeToDocument(StructureItems);
        }

        public ICollection<Material> Materials { get; set; } = new ObservableCollection<Material>();
        public void Dispose() => Structure?.Dispose();
    }

    public class StructureItem: IEquatable<StructureItem>
    {
        public StructureItem(string name, string var,  string? func = null)
        {
            Var = var;
            Name = name;
            Func = func;
        }

        public string Var { get; set; }
        public string Name { get; set; }
        public string? Func { get; set; }

        public bool Equals(StructureItem? other)
        {
            if(other == null) return false;

            if (Var == other.Var && Name == other.Name && Func == other.Func)
                return true;
            else
                return false;
        }
    }
}
