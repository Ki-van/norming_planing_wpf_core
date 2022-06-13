using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace norming_planing_wpf_core
{
    public class Mark : QuantitativeEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int DraftId { get; set; }
        public Draft Draft { get; set; }
        public ICollection<Detail> Details { get; set; } = new ObservableCollection<Detail>();
        public ICollection<TO>? TechProcesses { get; set; }
    }
}
