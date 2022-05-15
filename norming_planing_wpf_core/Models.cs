using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using norming_planing_wpf_core.Attributes;

namespace norming_planing_wpf_core
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Draft
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Mark> Marks { get; set; }
    }

    public class Mark
    {
        public string Id { get; set; }
        public int DraftId { get; set; }
        public Draft Draft { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<TP>? TechProcesses { get; set; }
    }

    public class Detail
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string MarkId { get; set; }

        [MarkComponentParam("Количество отверстий")]
        public int HolesCount { get; set; }
        [MarkComponentParam("Диаметр отверстий")]
        public int HolesDiamtr { get; set; }
        
        public Mark Mark { get; set; }
       /* public Material Material { get; set; }
        public SteelGrade SteelGrade { get; set; }*/
    }
/*
    public class Material
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class SteelGrade
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }*/

    public class TP
    {
        public int Id { get; set; }
        public int Order { get; set; }

        public ICollection<TO> Operations { get; set; }
    }

    public class TO
    {
        public int Id { get; set; }
        public double NormTime { get; set; }
        public double NormPrice { get; set; }

        public TOType? Type { get; set; }
    }

    public class TOType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class NormingMap: IDisposable
    {
        public int Id { get; set; }
        public JsonDocument? Parametrs { get; set; }
        public Instrument? Instrument { get; set; }
        public NormingMap? ParentMap { get; set; }
        public double Coefficient { get; set; }
        public TOType TOType { get; set; }

        public void Dispose()
        {
            Parametrs?.Dispose();
        }
    }

    public class Instrument
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
