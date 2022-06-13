using System.Collections.Generic;
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
        public ICollection<Assemblie> Assemblies { get; set; }
        public ICollection<AssemblieEntry> AssemblieEntries { get; set; }
        public ICollection<Detail> Details { get; set; }
        public ICollection<Instrument> Instruments { get; set; }
        
    }
    public class TOType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<TOTypeParam>? Params { get; set; }
        public ArgumentCount ArgumentCount { get; set; } = ArgumentCount.OneArgument;
        public TOType? ParentType { get; set; }
        public ICollection<TOType> ChildTypes { get; set; }
        public ICollection<Instrument> Instruments { get; set; }
        public ICollection<EmployeePosition> EmployeePositions { get; set; }
    }
    public class TOTypeParam
    {
        public string Name { get; set; }

    }
    public enum ArgumentCount
    {
        OneArgument,
        MoreThanOne
    }
}
