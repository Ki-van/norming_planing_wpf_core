using System.Collections.Generic;

namespace norming_planing_wpf_core
{
    public class Assemblie : QuantitativeEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TO> TOs { get; set; }
        public ICollection<AssemblieEntry> AssemblieEntries { get; set; }
    }
    public class AssemblieEntry
    {
        public int AssemblieId { get; set; }
        public Assemblie Assemblie { get; set; }

        public int TOId { get; set; }
        public TO TO { get; set; }

        public EntryType EntryType { get; set; }
    }

    public enum EntryType
    {
        Argument,
        Result
    }
}
