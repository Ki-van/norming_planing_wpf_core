using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace norming_planing_wpf_core
{
    public class Mark
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public uint StraightCount { get; set; } = 1;
        public uint OppositeCount { get; set; } = 0;
        public uint TotalCount { get; set; }
        public int DraftId { get; set; }
        public Draft Draft { get; set; }
        public ICollection<Detail> Details { get; set; } = new ObservableCollection<Detail>();
        public ICollection<TP>? TechProcesses { get; set; }

        public string GetDetailsPrefix()
        {
            string prefix = string.Empty;
            if (Details != null && Details.Count > 0)
            {
                string lastCode = ((List<Detail>)Details)[Details.Count - 1].Code;
                
            }
            return prefix;
        }
    }
   
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
        public ICollection<EmployeePosition> EmployeePositions { get; set; }
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
